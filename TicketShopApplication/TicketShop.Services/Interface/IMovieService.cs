using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Services.Interface
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
        Movie GetDetailsForMovie(Guid? id);
        void CreateNewMovie(Movie p);
        void UpdeteExistingMovie(Movie p);
    
        void DeleteMovie(Guid id);
        
    }
}
