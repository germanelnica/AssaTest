using Application.Interfaces;
using Application.MarcasAutos.Commands.Create;
using Domain.Entities;
using MediatR;

namespace Application.MarcasAutos.Handlers.Create
{
    public class CreateMarcaAutosCommandHandler : IRequestHandler<CreateMarcaAutosCommand, MarcasAutosEntity>
    {
        private readonly IMarcasAutosRepository _marcasAutosRepository;

        public CreateMarcaAutosCommandHandler(IMarcasAutosRepository marcasAutosRepository)
        {
            _marcasAutosRepository = marcasAutosRepository;
        }

        public Task<MarcasAutosEntity> Handle(CreateMarcaAutosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var nuevaMarca = new MarcasAutosEntity 
                { 
                    Nombre = request.Nombre,
                    Descripcion= request.Descripcion, 
                    Pais = request.Pais 
                };
                _marcasAutosRepository.Add(nuevaMarca);
                return Task.FromResult(nuevaMarca);
            }
            catch
            {
                throw new ApplicationException("Ocurrió un error al crear la nueva marca. Por favor intente nuevamente.");
            }
        }
    }
}
