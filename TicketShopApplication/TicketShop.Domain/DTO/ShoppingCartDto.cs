using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> Tickets { get; set; }
        public double TotalPrice { get; set; }
    }
}
