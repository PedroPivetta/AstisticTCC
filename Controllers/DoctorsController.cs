using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Models;
using System.Drawing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQLitePCL;

namespace Final.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly FinalContext _context;

        public DoctorsController(FinalContext context)
        {
            _context = context;
        }

        //Controle de tráfego

        public IActionResult Termos()
        {
            return View();
        }

        public IActionResult AreaMed(Doctors doctors)
        {
            return View(); 
        }

        public IActionResult MedEdit()
        {
            return View();
        }

        public IActionResult MedCadastro()
        {
            return View();
        }

        public IActionResult UserTable()
        {
            return Redirect("/Users/Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar([Bind("Id,Name,Email,Password,Cep,Crm,Graduated,Telephone,Captcha")] Doctors doctors, string graduated, string name, string email, string password, string crm, string cep, string telephone, bool captcha)
        {
            if (ModelState.IsValid)
            {
                Doctors userName = _context.Doctors.Where(a => a.Name == name).FirstOrDefault();
                Doctors mail = _context.Doctors.Where(a => a.Email == email).FirstOrDefault();
                Doctors certificado = _context.Doctors.Where(a => a.Crm == crm).FirstOrDefault();
                if (userName != null)
                {
                    TempData["ExistName"] = "Este nome ja esta cadastrado";
                }
                else if (mail != null)
                {
                    TempData["ExistEmail"] = "Este email já foi cadastrado";
                }
                else if (certificado != null)
                {
                    TempData["ExistCrm"] = "Este CRM ja foi cadastrado";
                }
                else
                {
                    _context.Add(doctors);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(MedLogin));
                }

            }
            else if (graduated == "Escolha sua Profissão")
            {
                TempData["GraduatedErro"] = "Escolha uma área de atuação";
            }
            else if (name == null)
            {
                TempData["NameErro"] = "Insira seu nome";
            }
            else if (email == null)
            {
                TempData["EmailErro"] = "Informe seu email";
            }
            else if (crm == null)
            {
                TempData["CrmErro"] = "Informe o seu CRM";
            }
            else if (crm.Length != 6)
            {
                TempData["CrmLength"] = "Número inválido";
            }
            else if (captcha == false)
            {
                TempData["TermsErro"] = "Aceite nossos termos";
            }
            else if (password == null)
            {
                TempData["SenhaErro"] = "Crie uma senha";
            }
            else if (password.Length < 5)
            {
                TempData["SenhaLength"] = "A senha ter ao menos 5 digitos";
            }
            else if (telephone == null)
            {
                TempData["TelErro"] = "Informe seu telefone";
            }
            else if (telephone.Length < 15)
            {
                TempData["TelLength"] = "Numero de telefone inválido";
            }

            else if (cep == null)
            {
                TempData["CepErro"] = "Informe o seu endereço";
            }
            else if (cep.Length < 9)
            {
                TempData["CepLength"] = "CEP inválido";
            }
            return View("MedCadastro");
        }

        public IActionResult MedLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Acessar(DoctorsLogin login, string email, string password)
        {
            if (ModelState.IsValid)
            {
                Doctors userName = _context.Doctors.Where(a => a.Email == email).FirstOrDefault();
                Doctors pass = _context.Doctors.Where(a => a.Password == password).FirstOrDefault();

                if (userName == null)
                {
                    TempData["NullEmail"] = "Email não cadastrado";
                }
                else if (pass == null)
                {
                    TempData["Nullpassword"] = "Senha Incorreta";
                }
                else
                {
                    return Redirect("/Home");
                }
            }
            else if (email == null)
            {
                TempData["ErroEmail"] = "Insira o seu Email";
            }
            else if (password == null)
            {
                TempData["ErroPassword"] = "Insira sua senha";
            }
           
            return View("MedLogin");
        }

        // GET: Doctors
        public async Task<IActionResult> Tabela()
        {
            return _context.Doctors != null ?
                          View(await _context.Doctors.ToListAsync()) :
                          Problem("Entity set 'FinalContext.Doctors'  is null.");
        }
  
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       

        // GET: Doctors/Edit/5
        public async Task<IActionResult> DocEdit(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctors = await _context.Doctors.FindAsync(id);
            if (doctors == null)
            {
                return NotFound();
            }
            return View(doctors);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DocEdit(int id, [Bind("Id,Name,Email,Password,Cep,Crm,Graduated,Telephone,Captcha")] Doctors doctors)
        {
            if (id != doctors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorsExists(doctors.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Tabela));
            }
            return View("DocEdit");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Doctors == null)
            {
                return Problem("Entity set 'FinalContext.Doctors'  is null.");
            }
            var doctors = await _context.Doctors.FindAsync(id);
            if (doctors != null)
            {
                _context.Doctors.Remove(doctors);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Tabela));
        }

        private bool DoctorsExists(int id)
        {
          return (_context.Doctors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
