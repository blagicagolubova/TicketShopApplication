﻿using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;
using TicketShop.Services.Interface;

namespace TicketShop.web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            List<Order> orders = this._orderService.getAllOrders();

            return View(orders);
        }

        public IActionResult Details(Guid id)
        {
            Order order = this._orderService.getOrderDetails(id);
            return View(order);
        }

        public FileContentResult CreateInvoice(Guid id)
        {

            Order order = this._orderService.getOrderDetails(id);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);


            document.Content.Replace("{{OrderNumber}}", order.Id.ToString());
            document.Content.Replace("{{UserName}}", order.User.UserName);

            StringBuilder sb = new StringBuilder();

            var totalPrice = 0.0;

            foreach (var item in order.Tickets)
            {
                totalPrice += item.Quantity * item.OrderedTicket.TicketPrice;
                sb.AppendLine("Tickets for " + item.OrderedTicket.Movie.MovieName + " with quantity of: " + item.Quantity + " and price of: " + item.OrderedTicket.TicketPrice + "$");
            }


            document.Content.Replace("{{TicketList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", totalPrice.ToString() + "$");


            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }
    }
}
