using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.DTO;
using TicketShop.Domain.Identity;
using TicketShop.Repository;
using TicketShop.Services.Interface;

namespace TicketShop.web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ApplicationDbContext _context;
        public TicketsController(ITicketService ticketService, ApplicationDbContext context)
        {
            _ticketService = ticketService;
            _context = context;
        }


        // GET: Tickets
        public IActionResult Index()
        {
            var allTickets = this._ticketService.GetAllTickets();
            return View(allTickets);
        }

        // ADD TO SHOPPING CART
        public IActionResult AddTicketToCard(Guid? id)
        {
            // var model = this._productService.GetShoppingCartInfo(id);
            // var ticket = await _context.Tickets.Include(t=>t.Movie).Where(z => z.Id.Equals(id)).FirstOrDefaultAsync();
            /*AddToShoppingCardDto model = new AddToShoppingCardDto
            {
                SelectedTicket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };
            return View(model);*/
            var model = this._ticketService.GetShoppingCartInfo(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTicketToCard([Bind("TicketId", "Quantity")] AddToShoppingCardDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._ticketService.AddToShoppingCart(item, userId);

            if (result)
            {
                return RedirectToAction("Index", "Tickets");
            }

            return View(item);
            /* var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

             var userShoppingCard = await _context.ShoppingCarts.Where(z => z.OwnerId.Equals(userId)).FirstOrDefaultAsync();

             if(item.TicketId != null && userShoppingCard != null)
             {
                 var ticket =await _context.Tickets.Where(z => z.Id.Equals(item.TicketId)).FirstOrDefaultAsync();

                 if(ticket != null)
                 {
                     TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                     {
                         Ticket = ticket,
                         TicketId = ticket.Id,
                         ShoppingCart = userShoppingCard,
                         ShoppingCartId = userShoppingCard.Id,
                         Quantity = item.Quantity
                     };
                     _context.Add(itemToAdd);
                     await _context.SaveChangesAsync();
                 }
                 return RedirectToAction("Index", "Tickets");
             }


             var result = this._productService.AddToShoppingCart(item, userId);

             if (result)
             {
                 return RedirectToAction("Index", "Products");
             }

             return View(item);*/
        }
        // GET: Tickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            ViewData["Movies"] = _context.Movies.Select(x=> new Movie(x.Id, x.MovieName));
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Date,TicketPrice,MovieId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                this._ticketService.CreateNewTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", ticket.MovieId);
            ViewData["Movies"] = _context.Movies.Select(x => new Movie(x.Id, x.MovieName));
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Date,TicketPrice,MovieId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._ticketService.UpdeteExistingTicket(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
             this._ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return this._ticketService.GetDetailsForTicket(id) != null;
        }

        [HttpGet]
        public FileContentResult ExportAllTickets()
        {
            string fileName = "Orders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All Orders");

                worksheet.Cell(1, 1).Value = "Ticket Id";
                worksheet.Cell(1, 2).Value = "Ticket Date";
                worksheet.Cell(1, 3).Value = "Ticket Movie";
                worksheet.Cell(1, 4).Value = "Ticket Movie Genre";
                worksheet.Cell(1, 5).Value = "Ticket Price";
                

                List<Ticket> tickets = this._ticketService.GetAllTickets();

                for (int i = 1; i <= tickets.Count(); i++)
                {
                    var item = tickets[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.Date;
                    worksheet.Cell(i + 1, 3).Value = item.Movie.MovieName;
                    worksheet.Cell(i + 1, 4).Value = item.Movie.MovieGenre;
                    worksheet.Cell(i + 1, 5).Value = item.TicketPrice;
                    
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }

            }
        }

    }
}
