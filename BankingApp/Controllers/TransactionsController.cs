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
	public class TransactionsController : Controller
	{
		private readonly BankingAppContext _context;

		private readonly UserManager<User> _userManager;

		private readonly BankAccountsController _bankAccountsController;

		private User _user;

		public List<BankAccount> _bankAccounts { get; set; }

		public int _selectedValue { get; set; }

		public TransactionsController(BankingAppContext context, UserManager<User> userManager, BankAccountsController bankAccountsController)
		{
			_context = context;
			_userManager = userManager;
			_bankAccountsController = bankAccountsController;
		}

		private User getUser()
		{
			var userId = _userManager.GetUserId(HttpContext.User);
			User user = _userManager.FindByIdAsync(userId).Result;

			return user;
		}


		// GET: Transactions
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
				var transactions = await _context.Transactions
					.Where(t => t.Sender == _user)
					.ToListAsync();

				var bankAccounts = await _bankAccountsController.GetBankAccountsOfAUser(_user);

				var selectListItems = bankAccounts.Select(ba => new SelectListItem
				{
					Value = ba.Id.ToString(),
					Text = ba.Name
				}).ToList();

				ViewBag.BankAccounts = selectListItems;

				if (transactions != null)
				{
					return View(transactions);
				}
				else
				{
					return Problem("Entity set 'BankingAppContext.Cards' is null.");
				}
			}
		}

		// GET: Transactions/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Transactions == null)
			{
				return NotFound();
			}

			var transaction = await _context.Transactions
				.FirstOrDefaultAsync(m => m.Id == id);
			if (transaction == null)
			{
				return NotFound();
			}

			return View(transaction);
		}

		// GET: Transactions/Create
		public async Task<IActionResult> CreateAsync()
		{
			var userId = _userManager.GetUserId(HttpContext.User);
			_user = _userManager.FindByIdAsync(userId).Result;


			if (userId == null)
			{
				return Redirect("~/Identity/Account/Login");
			}
			else
			{
				var bankAccounts = await _bankAccountsController.GetBankAccountsOfAUser(_user);

				var selectListItems = bankAccounts.Select(ba => new SelectListItem
				{
					Value = ba.Id.ToString(),
					Text = ba.Name
				}).ToList();

				ViewBag.BankAccounts = selectListItems;
				return View();
			}
		}

		// POST: Transactions/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,AccountName,Amount,Reciver")] Transaction transaction)
		{
			transaction.Date = DateTime.Now;
			transaction.Sender= getUser();
			await _bankAccountsController.EditWhileMakingAPayment( transaction.AccountName,transaction.Amount);
			if (ModelState.IsValid)
			{
				_context.Add(transaction);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(transaction);
		}

		// GET: Transactions/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Transactions == null)
			{
				return NotFound();
			}

			var transaction = await _context.Transactions.FindAsync(id);
			if (transaction == null)
			{
				return NotFound();
			}
			return View(transaction);
		}

		// POST: Transactions/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Date,Reciver")] Transaction transaction)
		{
			if (id != transaction.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(transaction);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!TransactionExists(transaction.Id))
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
			return View(transaction);
		}

		// GET: Transactions/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Transactions == null)
			{
				return NotFound();
			}

			var transaction = await _context.Transactions
				.FirstOrDefaultAsync(m => m.Id == id);
			if (transaction == null)
			{
				return NotFound();
			}

			return View(transaction);
		}

		// POST: Transactions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Transactions == null)
			{
				return Problem("Entity set 'BankingAppContext.Transactions'  is null.");
			}
			var transaction = await _context.Transactions.FindAsync(id);
			if (transaction != null)
			{
				_context.Transactions.Remove(transaction);
			}
			
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool TransactionExists(int id)
		{
		  return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
