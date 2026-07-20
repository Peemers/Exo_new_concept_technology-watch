using BusinessRoomBooking.Controllers;
using BusinessRoomBooking.Core.Dtos.Room.Response;
using BusinessRoomBooking.Core.Dtos.Worker.Request;
using BusinessRoomBooking.Core.Dtos.Worker.Response;
using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Domain;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BusinessRoomBooking.Tests.Controllers;

public class WorkerControllerTests
{
  [Fact]
  public async Task CreateWorker_ShouldReturnCreatedWorker()
  {
    //Arrange
    IBaseRepository<Worker> workerRepository = Substitute.For<IBaseRepository<Worker>>();
    
    CreateWorkerRequestDto dto = new CreateWorkerRequestDto
    {
      FirstName = "Test",
      LastName = "Test",
      Email = "test@test.be",
    };

    WorkerController controller = new WorkerController(workerRepository);
    
    //Act
    
    ActionResult<WorkerResponseDto> result = await controller.CreateWorker(dto);
    
    //Assert
    
    CreatedAtActionResult createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
    WorkerResponseDto workerResponseDto = Assert.IsType<WorkerResponseDto>(createdResult.Value);
    
    Assert.Equal(dto.Email, workerResponseDto.Email);
    Assert.Equal(dto.FirstName, workerResponseDto.FirstName);
    Assert.Equal(dto.LastName, workerResponseDto.LastName);
    
    await workerRepository.Received(1).AddAsync(Arg.Any<Worker>());
    await workerRepository.Received(1).SaveChangesAsync();
  }

  [Fact]
  public async Task GetById_ShouldReturnWorker()
  {
    IBaseRepository<Worker> workerRepository = Substitute.For<IBaseRepository<Worker>>();

    Worker worker = new Worker
    {
      FirstName = "Test",
      LastName = "Test",
      Email = "test@test.be",
      Id = Guid.NewGuid()
    };
    
    workerRepository.GetByIdAsync(worker.Id).Returns(worker);
    
    WorkerController controller = new WorkerController(workerRepository);
    
    ActionResult<WorkerResponseDto> result = await controller.GetById(worker.Id);
    
    OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
    WorkerResponseDto workerResponseDto = Assert.IsType<WorkerResponseDto>(okResult.Value);
    
    Assert.Equal(worker.Email, workerResponseDto.Email);
    Assert.Equal(worker.Id, workerResponseDto.Id);
    Assert.Equal(worker.FirstName, workerResponseDto.FirstName);
    Assert.Equal(worker.LastName, workerResponseDto.LastName);
  }
}