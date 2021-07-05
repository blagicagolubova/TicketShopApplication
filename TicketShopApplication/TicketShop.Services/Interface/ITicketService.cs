using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.DTO;

namespace TicketShop.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket p);
        void UpdeteExistingTicket(Ticket p);
        AddToShoppingCardDto GetShoppingCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToShoppingCart(AddToShoppingCardDto item, string userID);
    }
}
