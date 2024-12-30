using Application.Interfaces;
using Application.MarcasAutos.Commands.GetAll;
using Domain.Entities;
using MediatR;

namespace Application.MarcasAutos.Handlers.GetAll
{
    public class GetAllMarcasAutosQueryHandler : IRequestHandler<GetAllMarcasAutosQuery, IEnumerable<MarcasAutosEntity>>
    {
        private readonly IMarcasAutosRepository _marcasAutosRepository;
        public GetAllMarcasAutosQueryHandler(IMarcasAutosRepository marcasAutosRepository)
        {
            _marcasAutosRepository = marcasAutosRepository;
        }

        public Task<IEnumerable<MarcasAutosEntity>> Handle(GetAllMarcasAutosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _marcasAutosRepository.GetAll();
                return Task.FromResult(result);
            }
            catch
            {
                throw new ApplicationException("Ocurrió un error al obtener las marcas de autos. Por favor intente nuevamente.");
            }
        }
    }
}
