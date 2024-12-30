using Application.Interfaces;
using Application.MarcasAutos.Commands.GetById;
using Domain.Entities;
using MediatR;

namespace Application.MarcasAutos.Handlers.GetById
{
    internal class GetMarcaAutosByIdQueryHandler : IRequestHandler<GetMarcasAutosByIdQuery, MarcasAutosEntity?>
    {
        private readonly IMarcasAutosRepository _marcasAutosRepository;
        public GetMarcaAutosByIdQueryHandler(IMarcasAutosRepository marcasAutosRepository)
        {
            _marcasAutosRepository = marcasAutosRepository;
        }
        public Task<MarcasAutosEntity?> Handle(GetMarcasAutosByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _marcasAutosRepository.GetById(request.Id);
                return Task.FromResult(result);
            }
            catch
            {
                throw new ApplicationException($"Ocurrió un error al obtener la marca con ID {request.Id}. Por favor intente nuevamente.");
            }
        }
    }
}
