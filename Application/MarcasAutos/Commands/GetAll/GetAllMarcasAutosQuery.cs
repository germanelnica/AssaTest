using Domain.Entities;
using MediatR;

namespace Application.MarcasAutos.Commands.GetAll
{
    public class GetAllMarcasAutosQuery : IRequest<IEnumerable<MarcasAutosEntity>>
    {
    }
}
