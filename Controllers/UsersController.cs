using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Final.Controllers
{
    public class UsersController : Controller
    {
        private readonly FinalContext _context;

        public UsersController(FinalContext context)
        {
            _context = context;
        }

        public IActionResult Termos()
        {
            return View();
        }

        public IActionResult DoctorsTable()
        {
            return Redirect("/Doctors/Tabela");
        }

        public IActionResult UserCadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar([Bind("Id,Name,Email,Password,Captcha")] Users users, string name, string email, string password, bool captcha)
        {
            if (ModelState.IsValid)
            {
                Users userName = _context.Users.Where(u => u.Name == name).FirstOrDefault();
                Users mail = _context.Users.Where(u => u.Email == email).FirstOrDefault();

                if (userName != null)
                {
                    TempData["RegisterName"] = "Este nome já esta cadastrado";
                }
                else if (mail != null)
                {
                    TempData["EmailRegister"] = "Este Email já esta sendo usado";
                }
                else
                {
                    _context.Add(users);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(UserLogin));
                }
              
            }

            if (captcha != true)
            {
                TempData["NullTerms"] = "Aceite nossos termos";
            }
            else if (name == null)
            {
                TempData["NullName"] = "Insira o seu nome";
            }
            else if (email == null)
            {
                TempData["NullEmail"] = "Insira o seu Email";
            }
            else if (password == null)
            {
                TempData["NullPassword"] = "Crie uma senha";
            }
            else if (password.Length < 5)
            {
                TempData["Minimum"] = "A senha deve ter no mínimo 5 caracteres";
            }
            return View("UserCadastro");
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        public IActionResult Login(UserLogin userLogin, string email, string password)
        {
            if (ModelState.IsValid)
            {
                Users mail = _context.Users.Where(o => o.Email == email).FirstOrDefault();
                Users pass = _context.Users.Where(o => o.Password == password).FirstOrDefault();

                
               
                
                    if (mail == null)
                    {
                        TempData["EmailRegister"] = "Email não cadastrado";
                    }
                    else if (pass == null)
                    {
                        TempData["PasswordRegister"] = "Senha Incorreta";
                    }
                    else
                    {

                        return Redirect("/Games/Game");
                    }
                
                
            }
            else
            {
                if (email == null)
                {
                    TempData["NullEmail"] = "Insira o seu Email";
                }
                else if (password == null)
                {
                    TempData["NullPassword"] = "Insira sua senha";
                }
            }
            return View("UserLogin");
        }
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'FinalContext.Users'  is null.");
        }
        
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Email")] Users users, int id, string name, string email)
        {
            if (id != users.Id)
            {
                return NotFound();
            }
            else if (name == null)
            {
                TempData["NullName"] = "Insira um nome";
            }
            else if (email == null)
            {
                TempData["NullEmail"] = "Insira um Email";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        public async Task<IActionResult> Deletar(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'FinalContext.Users'  is null.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
