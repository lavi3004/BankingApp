using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankingApp.Data;
using BankingApp.Models;
using BankingApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace BankingApp.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly BankingAppContext _context;

        private readonly UserManager<User> _userManager;

        private User _user;

        public BankAccountsController(BankingAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private User getUser()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            User user = _userManager.FindByIdAsync(userId).Result;

            return user;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            _user = _userManager.FindByIdAsync(userId).Result;

            if (userId == null)
            {
                return Redirect("~/Identity/Account/Login");
            }
            else
            {
                var bankAccounts = await _context.BankAccounts
                    .Where(c => c.User == _user)
                    .ToListAsync();

                if (bankAccounts != null)
                {
                    return View(bankAccounts);
                }
                else
                {
                    return Problem("Entity set 'BankingAppContext.BankAccounts' is null.");
                }
            }
        }

        public async Task<List<BankAccount>> GetBankAccountsOfAUser(User user)
        {
            return await _context.BankAccounts
                .Where(c => c.User == user)
                .ToListAsync();
        }


        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BankAccounts == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public IActionResult Create()
        {
            ViewBag.Currency = Enum.GetValues(typeof(CurrencyEnum))
                                .Cast<CurrencyEnum>()
                                .Select(c => new SelectListItem
                                {
                                    Text = c.ToString(),
                                    Value = c.ToString()
                                });
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IBAN,SWIFT,Balance,Currency")] BankAccount bankAccount)
        {
            bankAccount.User = getUser();
            if (ModelState.IsValid)
            {
                _context.Add(bankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BankAccounts == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IBAN,SWIFT,Balance,Currency")] BankAccount bankAccount)
        {
            if (id != bankAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountExists(bankAccount.Id))
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
            return View(bankAccount);
        }

        public async Task EditWhileMakingAPayment(string bankAccountName ,int ammount)
        {
            var bankAccount = _context.BankAccounts
                .Where(ba => ba.Name.Contains(bankAccountName))
                .First();

            bankAccount.Balance = bankAccount.Balance - ammount;
            try
            {
                _context.Update(bankAccount);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankAccountExists(bankAccount.Id))
                {
                    throw new Exception();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BankAccounts == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BankAccounts == null)
            {
                return Problem("Entity set 'BankingAppContext.BankAccounts'  is null.");
            }
            var bankAccount = await _context.BankAccounts.FindAsync(id);
            if (bankAccount != null)
            {
                _context.BankAccounts.Remove(bankAccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountExists(int id)
        {
          return (_context.BankAccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
