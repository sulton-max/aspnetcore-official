using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;
        
        
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Movie != null)
            {
                var moviesQuery = _context.Movie.Select(x => x);
                var genresQuery = _context.Movie.Select(x => x.Genre).OrderBy(x => x);

                if (!string.IsNullOrWhiteSpace(SearchString))
                    moviesQuery = moviesQuery.Where(x => x.Title.Contains(SearchString));

                if (!string.IsNullOrWhiteSpace(MovieGenre))
                    moviesQuery = moviesQuery.Where(x => x.Genre.Equals(MovieGenre));

                Genres = new SelectList(await genresQuery.Distinct().ToListAsync());
                Movie = await moviesQuery.ToListAsync();
            }
        }
    }
}
