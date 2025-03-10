using WorkerShifts.API.Models;
using WorkerShifts.API.Models.DTOs;

namespace WorkerShifts.API.Services;

public interface IWorkerService
{
    Task<IEnumerable<WorkerDto>> GetAllWorkersAsync();
    Task<WorkerDto?> GetWorkerByIdAsync(int id);
    Task<WorkerDto> CreateWorkerAsync(CreateWorkerDto createWorkerDto);
    Task<bool> UpdateWorkerAsync(int id, UpdateWorkerDto updateWorkerDto);
    Task<bool> DeleteWorkerAsync(int id);
}
