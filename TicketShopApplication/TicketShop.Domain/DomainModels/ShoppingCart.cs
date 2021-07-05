using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketShop.Domain.Identity;

namespace TicketShop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public TicketShopApplicationUser Owner { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
    }
}
