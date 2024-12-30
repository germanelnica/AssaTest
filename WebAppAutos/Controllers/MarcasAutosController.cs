using Application.MarcasAutos.Commands.Create;
using Application.MarcasAutos.Commands.Delete;
using Application.MarcasAutos.Commands.GetAll;
using Application.MarcasAutos.Commands.GetById;
using Application.MarcasAutos.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAppAutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarcasAutosController : ControllerBase
    {
        private readonly ILogger<MarcasAutosController> _logger;
        private readonly IMediator _mediator;

        public MarcasAutosController(ILogger<MarcasAutosController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try 
            { 
                _logger.LogInformation("Get all request");
                var marcas = await _mediator.Send(new GetAllMarcasAutosQuery());
                return Ok(marcas);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { message = ex.Message});
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Get by ID request: {Id}", id);
                var marca = await _mediator.Send(new GetMarcasAutosByIdQuery(default) { Id = id });
                return marca is not null ? Ok(marca) : NotFound(new { message = "Marca not found" });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error fetching marca by ID");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMarcaAutosCommand command)
        {
            try
            {
                _logger.LogInformation("Create request");
                var result = await _mediator.Send(command);
                return Ok(new { data = result });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error creating marca");
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMarcasAutosCommand command)
        {
            try
            {
                if (id != command.Id)
                    return BadRequest(new { message = "ID mismatch" });

                _logger.LogInformation("Update request: {Id}", id);
                var result = await _mediator.Send(command);
                return result ? NoContent() : NotFound(new { message = "Marca not found" });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error updating marca");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Delete request: {Id}", id);
                var result = await _mediator.Send(new DeleteMarcasAutosCommand(default) { Id = id });
                return result ? NoContent() : NotFound(new { message = "Marca not found" });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error deleting marca");
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
