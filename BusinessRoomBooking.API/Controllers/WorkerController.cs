using BusinessRoomBooking.Core.Dtos.Worker.Request;
using BusinessRoomBooking.Core.Dtos.Worker.Response;
using BusinessRoomBooking.Core.Exceptions.WorkerExceptions;
using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Core.Mappers;
using BusinessRoomBooking.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BusinessRoomBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController(
  IBaseRepository<Worker> workerRepository) : ControllerBase
{
  [HttpGet("{id:guid}")]
  public async Task<ActionResult<WorkerResponseDto>> GetById(Guid id)
  {
    Worker? worker = await workerRepository.GetByIdAsync(id);
    if (worker is null) throw new WorkerNotFoundException(id);
    return Ok(worker.ToWorkerResponseDto());
  }

  [HttpPost]
  public async Task<ActionResult<WorkerResponseDto>> Create([FromBody] CreateWorkerRequestDto dto)
  {
    Worker worker = dto.ToWorker();
    
    await workerRepository.AddAsync(worker);
    await workerRepository.SaveChangesAsync();

    WorkerResponseDto roomResponseDto = worker.ToWorkerResponseDto();
    
    return CreatedAtAction(nameof(GetById), new { id = worker.Id }, roomResponseDto);
  }
}