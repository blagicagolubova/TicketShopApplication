using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketShop.Domain.Identity;

namespace TicketShop.Domain.DomainModels
{
    public class Order:BaseEntity
    {
      
        public string UserId { get; set; }
        public TicketShopApplicationUser User { get; set; }
        public virtual ICollection<TicketInOrder> Tickets { get; set; }
    }
}
