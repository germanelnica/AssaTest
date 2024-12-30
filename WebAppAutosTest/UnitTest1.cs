using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Postgres;
using WebAppAutos.Controllers;
using Moq;
using MediatR;
using Application.MarcasAutos.Commands.Create;
using Application.MarcasAutos.Commands.Delete;
using Application.Interfaces;
using Application.MarcasAutos.Commands.GetAll;
using Microsoft.AspNetCore.Http;

namespace WebAppAutosTest
{
    public class UnitTest1
    {
        private readonly DatabaseContext _dbContext;
        private readonly MarcasAutosController _controller;

        public UnitTest1()
        {
            var loggerMock = new Mock<ILogger<MarcasAutosController>>();
            var mediatorMock = new Mock<IMediator>();

            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(Guid.NewGuid().ToString())
             .Options;

            _dbContext = new DatabaseContext(options);

            _controller = new MarcasAutosController(loggerMock.Object, mediatorMock.Object);

        }
        [Fact]
        public async void GetAll_CheckReturns()
        {

            // Arrange
            // Arrange: Configurar datos de prueba
            var marcasAutos = new List<MarcasAutosEntity>
            {
                new MarcasAutosEntity { Id = 1, Descripcion = "Marca A", Pais = "USA", Nombre = "Ford" },
                new MarcasAutosEntity { Id = 2, Descripcion = "Marca B", Pais = "Japón", Nombre = "Toyota" },
                new MarcasAutosEntity { Id = 3, Descripcion = "Marca C", Pais = "Alemania", Nombre = "BMW" }
            };

            // Crear un mock de IMediator
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllMarcasAutosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(marcasAutos); // Simular la respuesta del query

            // Crear un mock de ILogger
            var loggerMock = new Mock<ILogger<MarcasAutosController>>();

            // Crear la instancia del controlador con los mocks
            var controller = new MarcasAutosController(loggerMock.Object, mediatorMock.Object);

            // Act: Llamar al método Get del controlador
            var result = await controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var marcas = Assert.IsType<List<MarcasAutosEntity>>(okResult.Value);

            Assert.Equal(marcasAutos.Count, marcas.Count);

        }


        [Fact]
        public async Task GetNotFound()
        {
            // Act
            var result = await _controller.GetById(1222222);  // ID that doesn't exist

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);  // Assert that it's a NotFoundResult
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var command = new CreateMarcaAutosCommand("Marca Test", "Marca Test", "Belgium");
         
            var marcaAutosEntity = new MarcasAutosEntity
            {
                Id = 121,  // Example ID that will be returned
                Nombre = command.Nombre,
                Pais = command.Pais,
                Descripcion = command.Descripcion,
            };

            // Mock the mediator to return the created entity
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<CreateMarcaAutosCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(marcaAutosEntity);

            var loggerMock = new Mock<ILogger<MarcasAutosController>>();

            var controller = new MarcasAutosController(loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await controller.Create(command);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(MarcasAutosController.GetById), createdAtActionResult.ActionName);  // Action name should match GetById
            Assert.Equal(121, createdAtActionResult.RouteValues["id"]);  // ID should match the created entity's ID
            Assert.Equal(marcaAutosEntity, createdAtActionResult.Value);  // The returned value should match the created entity
        }

        [Fact]
        public async Task DeleteReturnsNoContent()
        {
            // Arrange
            var id = 1;  // Example ID of the object to delete
            var deleteCommand = new DeleteMarcasAutosCommand(default) { Id = id };

            // Mock the mediator to return true (indicating that the deletion was successful)
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteMarcasAutosCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

            var loggerMock = new Mock<ILogger<MarcasAutosController>>();

            var controller = new MarcasAutosController(loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);  // Assert that the result is NoContent
            Assert.Equal(204, noContentResult.StatusCode);  // Check if the status code is 204 (No Content)
        }
    }
}