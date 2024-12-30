using Application.Interfaces;
using Application.MarcasAutos.Commands.Update;
using MediatR;

namespace Application.MarcasAutos.Handlers.Update
{
    public class UpdateMarcaAutosCommandHandler : IRequestHandler<UpdateMarcasAutosCommand, bool>
    {
        private readonly IMarcasAutosRepository _marcasAutosRepository;

        public UpdateMarcaAutosCommandHandler(IMarcasAutosRepository marcasAutosRepository)
        {
            _marcasAutosRepository = marcasAutosRepository;
        }

        public Task<bool> Handle(UpdateMarcasAutosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingMarca = _marcasAutosRepository.GetById(request.Id);
                if (existingMarca == null)
                {
                    return Task.FromResult(false);
                }

                existingMarca.Nombre = request.Nombre;
                existingMarca.Pais = request.Pais;
                existingMarca.Descripcion = request.Descripcion;
                _marcasAutosRepository.Update(existingMarca);
                return Task.FromResult(true);
            }
            catch
            {
                throw new ApplicationException($"Ocurrió un error al actualizar la marca con ID {request.Id}. Por favor intente nuevamente.");
            }
        }
    }
}
