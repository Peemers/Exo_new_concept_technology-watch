using BusinessRoomBooking.Core.Dtos.Worker.Request;
using BusinessRoomBooking.Core.Dtos.Worker.Response;
using BusinessRoomBooking.Core.Mappers;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Tests.Mappers;

public class WorkerMapperTests
{
  [Fact]
  public void ToWorkerResponseDto_ShouldMapCorrectly()
  {
    //Arrange

    Worker worker = new Worker
    {
      Email = "test@test.be",
      FirstName = "Test",
      LastName = "Test",
      Id = Guid.NewGuid(),
    };

    //Act

    WorkerResponseDto result = worker.ToWorkerResponseDto();

    //Assert

    Assert.Equal(worker.Id, result.Id);
    Assert.Equal(worker.Email, result.Email);
    Assert.Equal(worker.FirstName, result.FirstName);
    Assert.Equal(worker.LastName, result.LastName);
  }

  [Fact]
  public void ToWorker_ShouldMapCorrectly()
  {
    //Arrange

    CreateWorkerRequestDto dto = new CreateWorkerRequestDto
    {
      FirstName = "Test",
      LastName = "Test",
      Email = "test@test.be",
    };
    //Act

    Worker worker = dto.ToWorker();

    //Assert

    Assert.Equal(dto.FirstName, worker.FirstName);
    Assert.Equal(dto.LastName, worker.LastName);
    Assert.Equal(dto.Email, worker.Email);
  }
}