using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketShop.Domain.DomainModels
{
    public class Movie : BaseEntity
    {
        public string MovieName { get; set; }

        public string MovieGenre { get; set; }

        public string MovieImage { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public Movie(Guid id, string movieName)
        {
            Id = id;
            MovieName = movieName;
        }

        public Movie()
        {

        }
    }
}
