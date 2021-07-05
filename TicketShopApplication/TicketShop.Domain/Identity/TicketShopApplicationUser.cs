using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Domain.Identity
{
    public class TicketShopApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ShoppingCart UserCart { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
