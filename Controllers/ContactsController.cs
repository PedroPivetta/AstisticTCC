using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Models;

namespace Final.Controllers
{
    public class ContactsController : Controller
    {
        private readonly FinalContext _context;

        public ContactsController(FinalContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
              return _context.Doctors != null ? 
                          View(await _context.Doctors.ToListAsync()) :
                          Problem("Entity set 'FinalContext.Doctors'  is null.");
        }

        private bool DoctorsExists(int id)
        {
          return (_context.Doctors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
