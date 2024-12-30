using Application.Interfaces;
using Domain.Entities;

namespace Persistence.Postgres.Repositories
{
    public class MarcasAutosRepository : GenericRepository<MarcasAutosEntity>, IMarcasAutosRepository
    {
        public MarcasAutosRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
