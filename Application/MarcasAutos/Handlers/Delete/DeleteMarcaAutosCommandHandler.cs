using Application.Interfaces;
using Application.MarcasAutos.Commands.Delete;
using MediatR;

namespace Application.MarcasAutos.Handlers.Delete
{
    public class DeleteMarcaAutosCommandHandler : IRequestHandler<DeleteMarcasAutosCommand, bool>
    {
        private readonly IMarcasAutosRepository _marcasAutosRepository;

        public DeleteMarcaAutosCommandHandler(IMarcasAutosRepository marcasAutosRepository)
        {
            _marcasAutosRepository = marcasAutosRepository;
        }

        public Task<bool> Handle(DeleteMarcasAutosCommand request, CancellationToken cancellationToken)
        {
            try 
            { 
                var existingMarca = _marcasAutosRepository.GetById(request.Id);
                if (existingMarca == null)
                {
                    throw new ApplicationException($"La Marca de auto con id:{request.Id} no existe.");

                }

                _marcasAutosRepository.Remove(existingMarca);
                return Task.FromResult(true);
            }
            catch
            {
                throw new ApplicationException($"Ocurrió un error al eliminar la marca con ID {request.Id}. Por favor intente nuevamente.");
            }
        }
    }
}
