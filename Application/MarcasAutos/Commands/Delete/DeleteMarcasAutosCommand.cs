using MediatR;

namespace Application.MarcasAutos.Commands.Delete
{
    public record DeleteMarcasAutosCommand(int Id) : IRequest<bool>;
}
