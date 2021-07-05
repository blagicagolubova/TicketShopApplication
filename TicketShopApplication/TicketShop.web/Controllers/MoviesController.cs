using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketShop.Domain.DomainModels;
using TicketShop.Repository;
using TicketShop.Services.Interface;

namespace TicketShop.web.Controllers
{
    public class MoviesController : Controller
    {
        
        private readonly IMovieService _movieService;

        public MoviesController(ApplicationDbContext context, IMovieService movieService)
        {
            
            _movieService = movieService;
        }

        // GET: Movies
        public IActionResult Index()
        {
            return View(_movieService.GetAllMovies());
        }

        // GET: Movies/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetDetailsForMovie(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,MovieName,MovieGenre,MovieImage")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                this._movieService.CreateNewMovie(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetDetailsForMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,MovieName,MovieGenre,MovieImage")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._movieService.UpdeteExistingMovie(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetDetailsForMovie(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._movieService.DeleteMovie(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(Guid id)
        {
            return this._movieService.GetDetailsForMovie(id) != null;
        }
    }
}
