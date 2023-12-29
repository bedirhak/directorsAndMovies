using Business.Models;
using DataAccess.Contexts;

namespace Business.Services
{
    public interface IDirectorService
    {
        IQueryable<DirectorModel> Query();
    }

    public class DirectorService : IDirectorService
    {
        private readonly Db _db;

        public DirectorService(Db db)
        {
            _db = db;
        }

        public IQueryable<DirectorModel> Query()
        {
            return _db.Directors.OrderBy(a => a.Name).Select(a => new DirectorModel()
            {
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname
            });
        }
    }
}
