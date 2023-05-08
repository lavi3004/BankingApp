using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Identity;
using BankingApp.Areas.Identity.Data;
using System.Collections;

namespace BankingApp.Controllers
{
    public class CardsController : Controller
    {
        private readonly BankingAppContext _context;

        private readonly UserManager<User> _userManager;

        private User _user;

        public CardsController(BankingAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public CardsController(BankingAppContext context)
        {
            _context = context;
        }

        private User getUser()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            User user = _userManager.FindByIdAsync(userId).Result;

            return user;
        }

        // GET: Cards
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
                var cards = await _context.Cards
                    .Where(c => c.CardHolder == _user)
                    .ToListAsync();

                if (cards != null)
                {
                    return View(cards);
                }
                else
                {
                    return Problem("Entity set 'BankingAppContext.Cards' is null.");
                }
            }
        }

        public async Task<IEnumerable<Card>> GetCards()
        {
            var cards = await _context.Cards
                               .ToListAsync();

            return cards;
        }

        public Card FindCardById(int cardId)
        {
            // Find the card with the specified ID
            var card =  _context.Cards.Find(cardId);

            // Check if the card was found
            if (card == null)
            {
                return null;
            }
            return card;
        }



            // GET: Cards/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name, CardNumber, CVV,ExpirationDate,IsLocked")] Card card)
        {
            card.CardHolder = getUser();
            if (ModelState.IsValid)
            {  
               createCard(card);
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }


        public async void createCard(Card card)
        {
            _context.Add(card);
            await _context.SaveChangesAsync();
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CardNumber,ExpirationDate,IsLocked")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
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
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cards == null)
            {
                return Problem("Entity set 'BankingAppContext.Cards'  is null.");
            }
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
          return (_context.Cards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
