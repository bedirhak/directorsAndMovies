using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{

    public interface IGenreService
    {
        IQueryable<GenreModel> Query();
        bool Add(GenreModel model);
        bool Update(GenreModel model);
        bool Delete(int id);
        List<GenreModel> GetList();
    }

    public class GenreService : IGenreService
    {
        private readonly Db _db;

        public GenreService(Db db)
        {
            _db = db;
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.Include(r => r.MovieGenres).Select(r => new GenreModel()
            {
                Name = r.Name,
                Id = r.Id,

                // querying over many to many relationship
                MovieNamesOutput = string.Join("<br />", r.MovieGenres.Select(ur => ur.Movie.Name)), // to show user names in details operation
                MovieIdsInput = r.MovieGenres.Select(ur => ur.MovieId).ToList() // to set selected UserIds in edit operation
            }).OrderByDescending(r => r.Name);
        }
        public bool Add(GenreModel model)
        {
            if (_db.Genres.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return false;
            Genre entity = new Genre()
            {
                Name = model.Name.Trim(),
            };
            _db.Genres.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Update(GenreModel model)
        {
            Genre existingEntity = _db.Genres.SingleOrDefault(s => s.Id == model.Id);
            if (existingEntity is null)
            {
                return false;
            }

            // Tüm alanları kontrol et, herhangi bir değişiklik varsa güncelle
            if (!string.Equals(existingEntity.Name.Trim(), model.Name.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                existingEntity.Name = model.Name.Trim();
                _db.Genres.Update(existingEntity);
                _db.SaveChanges();
                return true;
            }

            return false; // Herhangi bir değişiklik yoksa güncelleme yapmıyorum
        }

        public bool Delete(int id)
        {
            Genre entity = _db.Genres.SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return false;
            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return true;
        }


        public List<GenreModel> GetList()
        {
            // since we wrote the Query method above, we should call it
            // and return the result as a list by calling ToList method
            return Query().ToList();
        }

    }
}
