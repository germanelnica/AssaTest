using Domain.Entities;
using MediatR;

namespace Application.MarcasAutos.Commands.Create
{
    public record CreateMarcaAutosCommand(string Nombre, string? Descripcion, string? Pais) : IRequest<MarcasAutosEntity>;
}
