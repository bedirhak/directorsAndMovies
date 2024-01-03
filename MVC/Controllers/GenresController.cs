#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class GenresController : Controller
    {
        // TODO: Add service injections here
        private readonly IGenreService _genreService;
        private readonly IMovieService _movieService;

        public GenresController(IGenreService genreService, IMovieService movieService)
        {
            _genreService = genreService;
            _movieService = movieService;
        }

        // GET: Genres
        public IActionResult Index()
        {
            List<GenreModel> genreList = _genreService.GetList();

            return View(genreList);
        }

        // GET: Genres/Details/5
        public IActionResult Details(int id)
        {
            GenreModel genre = _genreService.Query().SingleOrDefault(s => s.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["Name"] = new SelectList(_genreService.Query().ToList(), "Id", "Name");
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                bool result = _genreService.Add(genre);
                // TODO: Add insert service logic here
                if (result)
                {
                    TempData["Message"] = "Genre added successfully.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Genre couldn't be added!");
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["Name"] = new SelectList(_genreService.Query().ToList(), "Id", "Name");
            return View(genre);
        }

        // GET: Genres/Edit/5
        // GET: Movies/Edit/5
        public IActionResult Edit(int id)
        {
            GenreModel genre = _genreService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
            if (genre == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(genre);
        }

        // POST: Movies/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                bool result = _genreService.Update(genre);
                if (result)
                {
                    TempData["Message"] = "Genre updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Genre couldn't be updated!");
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(genre);
        }

        // GET: Genre/Delete/5
        public IActionResult Delete(int id)
        {

            GenreModel genre = _genreService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Movies/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            _genreService.Delete(id);
            TempData["Message"] = "Genre deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
