using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Business.Services
{
	public interface IMovieService
	{
		IQueryable<MovieModel> Query();
		bool Add(MovieModel model);
		bool Update(MovieModel model);
		bool Delete(int id);
	}

	public class MovieService : IMovieService
	{
		private readonly Db _db;

		public MovieService(Db db)
		{
			_db = db;
		}

		//Burasıda okey 
		public bool Add(MovieModel model)
		{
			if (_db.Movies.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
				return false;
			Movie entity = new Movie()
			{
				DirectorId = model.DirectorId,
				Name = model.Name.Trim(),
				ReleaseYear = model.ReleaseYear,
				ImdbRank = model.ImdbRank,
				MoviesGenres = model.GenreIds?.Select(gid => new MovieGenre() { GenreId = gid }).ToList()
			};
			_db.Movies.Add(entity);
			_db.SaveChanges();
			return true;
		}

		//Burası okey
		public bool Delete(int id)
		{
			Movie entity = _db.Movies.Include(m => m.MoviesGenres).SingleOrDefault(s => s.Id == id);
			if (entity is null)
				return false;
			_db.MovieGenres.RemoveRange(entity.MoviesGenres); // 
			_db.Movies.Remove(entity);
			_db.SaveChanges();
			return true;
		}

		//burasıda tamam
		public IQueryable<MovieModel> Query()
		{
			return _db.Movies.Include(s => s.Director)
				.OrderByDescending(s => s.ImdbRank).ThenBy(s => s.Name)
				.Select(s => new MovieModel()
				{
					DirectorId = s.DirectorId,
					DirectorOutput = s.Director.Name + ' ' + s.Director.Surname,
					Id = s.Id,
					Name = s.Name,
					ReleaseYear = s.ReleaseYear,
					ImdbRank = s.ImdbRank,
					GenreIds = s.MoviesGenres.Select(mg => mg.GenreId).ToList()
				});
		}


		//public bool Update(MovieModel model)
		//{
		//    if (_db.Movies.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
		//        return false;
		//    Movie existingEntity = _db.Movies.SingleOrDefault(s => s.Id == model.Id);
		//    if (existingEntity is null)
		//        return false;
		//    existingEntity.DirectorId = model.DirectorId;
		//    existingEntity.Name = model.Name.Trim();
		//    existingEntity.ReleaseYear = model.ReleaseYear;
		//    existingEntity.ImdbRank = model.ImdbRank;
		//    existingEntity.DirectorId = model.DirectorId;
		//    _db.Movies.Update(existingEntity);
		//    _db.SaveChanges();
		//    return true;
		//}


		public bool Update(MovieModel model)
		{
			if (_db.Movies.Any(m => m.Name.ToUpper() == model.Name.ToUpper().Trim() && m.Id != model.Id))
			{
				return false;
			}


			Movie existingEntity = _db.Movies.Include(m => m.MoviesGenres).SingleOrDefault(s => s.Id == model.Id);

			if (existingEntity is null)
			{
				return false;
			}
			_db.MovieGenres.RemoveRange(existingEntity.MoviesGenres); // ilişkiliyi sil


			// Tüm alanları kontrol et, herhangi bir değişiklik varsa güncelle

			existingEntity.Name = model.Name.Trim();
			existingEntity.ReleaseYear = model.ReleaseYear;
			existingEntity.ImdbRank = model.ImdbRank;
			existingEntity.DirectorId = model.DirectorId;

			existingEntity.MoviesGenres = model.GenreIds?.Select(gid => new MovieGenre() { GenreId = gid }).ToList();

			_db.Movies.Update(existingEntity);
			_db.SaveChanges();
			return true;
		}


	}
}
