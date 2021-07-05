using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.DTO;
using TicketShop.Domain.Identity;
using TicketShop.Repository;
using TicketShop.Services.Interface;

namespace TicketShop.web.Controllers
{
    public class ShoppingCartController : Controller
    {
       
        private readonly UserManager<TicketShopApplicationUser> _userManager;
        private readonly IShoppingCartService _shoppingCartService; 

        public ShoppingCartController(ApplicationDbContext context, UserManager<TicketShopApplicationUser> userManager, IShoppingCartService shoppingCartService)
        {
          
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            return View(this._shoppingCartService.getShoppingCartInfo(userId));

        }

        public IActionResult DeleteTicketFromShoppingCart(Guid? id)
        {
            

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.deleteProductFromShoppingCart(userId, id);

            if (result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        /*public IActionResult OrderNow()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.orderNow(userId);

            return RedirectToAction("Index", "ShoppingCart");
           
        }*/

        private Boolean Order()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.orderNow(userId);

            return result;
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "EShop Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                var result = this.Order();

                if (result)
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
                else
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

    }
}
