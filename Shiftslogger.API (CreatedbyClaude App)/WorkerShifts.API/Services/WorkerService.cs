using Microsoft.EntityFrameworkCore;
using WorkerShifts.API.Data;
using WorkerShifts.API.Models;
using WorkerShifts.API.Models.DTOs;

namespace WorkerShifts.API.Services;

public class WorkerService : IWorkerService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<WorkerService> _logger;

    public WorkerService(ApplicationDbContext context, ILogger<WorkerService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<WorkerDto>> GetAllWorkersAsync()
    {
        _logger.LogInformation("Getting all workers");
        
        return await _context.Workers
            .Select(w => new WorkerDto
            {
                Id = w.Id,
                FirstName = w.FirstName,
                LastName = w.LastName,
                EmployeeId = w.EmployeeId,
                Department = w.Department,
                PhoneNumber = w.PhoneNumber,
                Email = w.Email,
                HireDate = w.HireDate,
                IsActive = w.IsActive
            })
            .ToListAsync();
    }

    public async Task<WorkerDto?> GetWorkerByIdAsync(int id)
    {
        _logger.LogInformation("Getting worker with ID: {Id}", id);
        
        var worker = await _context.Workers.FindAsync(id);
        
        if (worker == null)
        {
            _logger.LogWarning("Worker with ID: {Id} not found", id);
            return null;
        }
        
        return new WorkerDto
        {
            Id = worker.Id,
            FirstName = worker.FirstName,
            LastName = worker.LastName,
            EmployeeId = worker.EmployeeId,
            Department = worker.Department,
            PhoneNumber = worker.PhoneNumber,
            Email = worker.Email,
            HireDate = worker.HireDate,
            IsActive = worker.IsActive
        };
    }

    public async Task<WorkerDto> CreateWorkerAsync(CreateWorkerDto createWorkerDto)
    {
        _logger.LogInformation("Creating a new worker");
        
        var worker = new Worker
        {
            FirstName = createWorkerDto.FirstName,
            LastName = createWorkerDto.LastName,
            EmployeeId = createWorkerDto.EmployeeId,
            Department = createWorkerDto.Department,
            PhoneNumber = createWorkerDto.PhoneNumber,
            Email = createWorkerDto.Email,
            HireDate = createWorkerDto.HireDate,
            IsActive = true
        };
        
        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Worker created with ID: {Id}", worker.Id);
        
        return new WorkerDto
        {
            Id = worker.Id,
            FirstName = worker.FirstName,
            LastName = worker.LastName,
            EmployeeId = worker.EmployeeId,
            Department = worker.Department,
            PhoneNumber = worker.PhoneNumber,
            Email = worker.Email,
            HireDate = worker.HireDate,
            IsActive = worker.IsActive
        };
    }

    public async Task<bool> UpdateWorkerAsync(int id, UpdateWorkerDto updateWorkerDto)
    {
        _logger.LogInformation("Updating worker with ID: {Id}", id);
        
        var worker = await _context.Workers.FindAsync(id);
        
        if (worker == null)
        {
            _logger.LogWarning("Worker with ID: {Id} not found for update", id);
            return false;
        }
        
        worker.FirstName = updateWorkerDto.FirstName;
        worker.LastName = updateWorkerDto.LastName;
        worker.Department = updateWorkerDto.Department;
        worker.PhoneNumber = updateWorkerDto.PhoneNumber;
        worker.Email = updateWorkerDto.Email;
        worker.IsActive = updateWorkerDto.IsActive;
        
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Worker with ID: {Id} updated successfully", id);
        return true;
    }

    public async Task<bool> DeleteWorkerAsync(int id)
    {
        _logger.LogInformation("Deleting worker with ID: {Id}", id);
        
        var worker = await _context.Workers.FindAsync(id);
        
        if (worker == null)
        {
            _logger.LogWarning("Worker with ID: {Id} not found for deletion", id);
            return false;
        }
        
        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Worker with ID: {Id} deleted successfully", id);
        return true;
    }
}
