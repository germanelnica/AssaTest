using MediatR;

namespace Application.MarcasAutos.Commands.Update
{
    public record UpdateMarcasAutosCommand(int Id, string Nombre, string? Pais, string? Descripcion) : IRequest<bool>;
}
