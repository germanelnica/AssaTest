using Domain.Entities;
using MediatR;

namespace Application.MarcasAutos.Commands.GetById
{
    public record GetMarcasAutosByIdQuery(int Id) : IRequest<MarcasAutosEntity?>;
}
