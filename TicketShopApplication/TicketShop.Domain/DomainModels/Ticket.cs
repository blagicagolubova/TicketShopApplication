using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketShop.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {

        public DateTime Date { get; set; }

        public int TicketPrice { get; set; }

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }

        public virtual ICollection<TicketInOrder> Orders { get; set; }
    }
}
