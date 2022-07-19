using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using S07E04.InvoiceManagementSystem.MVC.Authorization;
using S07E04.InvoiceManagementSystem.MVC.Data;
using S07E04.InvoiceManagementSystem.MVC.Models;

namespace S07E04.InvoiceManagementSystem.MVC.Controllers
{
    public class InvoicesController : DI_BaseController
    {
        public InvoicesController(
            ApplicationDbContext Context,
            IAuthorizationService AuthorizationService,
            UserManager<IdentityUser> UserManager)
            : base(Context, AuthorizationService, UserManager)
        {
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var invoices = await Context.Invoices.ToListAsync();

            var currentUserId = UserManager.GetUserId(User);

            var isManager = User.IsInRole(Constants.InvoiceManagersRole);
            var isAdmin = User.IsInRole(Constants.InvoiceAdminRole);

            if (!isManager && !isAdmin)
                invoices = invoices
                    .Where(i => i.CreatorId == currentUserId)
                    .ToList(); ;

            if (Context.Invoices == null)
                return Problem("Entity set 'ApplicationDbContext.Invoices'  is null.");

            return View(invoices);
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || Context.Invoices == null)
                return NotFound();

            var invoice = await Context.Invoices
                .FirstOrDefaultAsync(m => m.InvoiceId == id);

            if (invoice == null)
                return NotFound();

            var isCreator = await AuthorizationService.AuthorizeAsync(
                User, invoice, InvoiceOperations.Read);

            var isManager = User.IsInRole(Constants.InvoiceManagersRole);

            if (!isCreator.Succeeded && !isManager)
                return Forbid();

            return View(invoice);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id, InvoiceStatus status)
        {
            var invoice = await Context.Invoices.FindAsync(id);

            if (invoice is null)
                return NotFound();

            var invoiceOperation = status == InvoiceStatus.Approved ?
                InvoiceOperations.Approve
                : InvoiceOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, invoice, invoiceOperation);

            if (isAuthorized.Succeeded == false)
                return Forbid();


            invoice.Status = status;
            Context.Invoices.Update(invoice);
            await Context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatorId,InvoiceId,InvoiceAmount,InvoiceMonth,InvoiceOwner")] Invoice invoice)
        {
            //invoice.CreatorId = UserManager.GetUserId(User);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, invoice, InvoiceOperations.Create);

            if (isAuthorized.Succeeded == false)
                return Forbid();

            if (ModelState.IsValid)
            {
                Context.Invoices.Add(invoice);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || Context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await Context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, invoice, InvoiceOperations.Update);

            if (!isAuthorized.Succeeded)
                return Forbid();


            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreatorId,Status,InvoiceId,InvoiceAmount,InvoiceMonth,InvoiceOwner")] Invoice invoice)
        {
            if (id != invoice.InvoiceId)
                return NotFound();

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, invoice, InvoiceOperations.Update);

            if (isAuthorized.Succeeded == false)
                return Forbid();

            if (ModelState.IsValid)
            {
                try
                {
                    Context.Update(invoice);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
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

            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || Context.Invoices == null)
                return NotFound();


            var invoice = await Context.Invoices
                .FirstOrDefaultAsync(m => m.InvoiceId == id);

            if (invoice == null)
                return NotFound();


            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Context.Invoices == null)
                return Problem("Entity set 'ApplicationDbContext.Invoices'  is null.");

            var invoice = await Context.Invoices.FindAsync(id);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, invoice, InvoiceOperations.Delete);

            if (isAuthorized.Succeeded == false)
                return Forbid();

            if (invoice != null)
                Context.Invoices.Remove(invoice);

            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return (Context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
