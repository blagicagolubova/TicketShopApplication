using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.Identity;
using TicketShop.Repository.Interface;

namespace TicketShop.Repository.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Ticket> entities;
        string errorMessage = string.Empty;

        public TicketRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Ticket>();
        }

        public void Delete(Ticket entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public Ticket Get(Guid? id)
        {
            return entities
              .Include(z => z.Movie)
              .SingleOrDefault(s => s.Id == id);
        }



       
        public IEnumerable<Ticket> GetAll()
        {
            return entities.Include(z => z.Movie).AsEnumerable();
        }

        public void Insert(Ticket entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Ticket entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
