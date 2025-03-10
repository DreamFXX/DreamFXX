using Microsoft.EntityFrameworkCore;
using WorkerShifts.API.Data;
using WorkerShifts.API.Models;
using WorkerShifts.API.Models.DTOs;

namespace WorkerShifts.API.Services;

public class ShiftService : IShiftService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ShiftService> _logger;

    public ShiftService(ApplicationDbContext context, ILogger<ShiftService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ShiftDto>> GetAllShiftsAsync()
    {
        _logger.LogInformation("Getting all shifts");

        return await _context.Shifts
            .Include(s => s.Worker)
            .Select(s => new ShiftDto
            {
                Id = s.Id,
                WorkerId = s.WorkerId,
                WorkerName = s.Worker != null ? $"{s.Worker.FirstName} {s.Worker.LastName}" : null,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Notes = s.Description,
                HourlyRate = s.HourlyRate,
                IsPaid = s.IsPaid,
                Duration = s.EndTime - s.StartTime,
                TotalPay = s.HourlyRate * (decimal)(s.EndTime - s.StartTime).TotalHours
            })
            .ToListAsync();
    }

    public async Task<ShiftDto?> GetShiftByIdAsync(int id)
    {
        _logger.LogInformation("Getting shift with ID: {Id}", id);

        var shift = await _context.Shifts
            .Include(s => s.Worker)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (shift == null)
        {
            _logger.LogWarning("Shift with ID: {Id} not found", id);
            return null;
        }

        return new ShiftDto
        {
            Id = shift.Id,
            WorkerId = shift.WorkerId,
            WorkerName = shift.Worker != null ? $"{shift.Worker.FirstName} {shift.Worker.LastName}" : null,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Notes = shift.Description,
            HourlyRate = shift.HourlyRate,
            IsPaid = shift.IsPaid,
            Duration = shift.EndTime - shift.StartTime,
            TotalPay = shift.HourlyRate * (decimal)(shift.EndTime - shift.StartTime).TotalHours
        };
    }

    public async Task<IEnumerable<ShiftDto>> GetShiftsByWorkerIdAsync(int workerId)
    {
        _logger.LogInformation("Getting shifts for worker ID: {WorkerId}", workerId);

        return await _context.Shifts
            .Include(s => s.Worker)
            .Where(s => s.WorkerId == workerId)
            .Select(s => new ShiftDto
            {
                Id = s.Id,
                WorkerId = s.WorkerId,
                WorkerName = s.Worker != null ? $"{s.Worker.FirstName} {s.Worker.LastName}" : null,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Notes = s.Description,
                HourlyRate = s.HourlyRate,
                IsPaid = s.IsPaid,
                Duration = s.EndTime - s.StartTime,
                TotalPay = s.HourlyRate * (decimal)(s.EndTime - s.StartTime).TotalHours
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ShiftDto>> GetShiftsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        _logger.LogInformation("Getting shifts between {StartDate} and {EndDate}", startDate, endDate);

        return await _context.Shifts
            .Include(s => s.Worker)
            .Where(s => s.StartTime >= startDate && s.EndTime <= endDate)
            .Select(s => new ShiftDto
            {
                Id = s.Id,
                WorkerId = s.WorkerId,
                WorkerName = s.Worker != null ? $"{s.Worker.FirstName} {s.Worker.LastName}" : null,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Notes = s.Description,
                HourlyRate = s.HourlyRate,
                IsPaid = s.IsPaid,
                Duration = s.EndTime - s.StartTime,
                TotalPay = s.HourlyRate * (decimal)(s.EndTime - s.StartTime).TotalHours
            })
            .ToListAsync();
    }

    public async Task<ShiftDto> CreateShiftAsync(CreateShiftDto createShiftDto)
    {
        _logger.LogInformation("Creating a new shift for worker ID: {WorkerId}", createShiftDto.WorkerId);

        // Validate worker exists
        var workerExists = await _context.Workers.AnyAsync(w => w.Id == createShiftDto.WorkerId);
        if (!workerExists)
        {
            _logger.LogWarning("Cannot create shift. Worker with ID: {WorkerId} not found", createShiftDto.WorkerId);
            throw new KeyNotFoundException($"Worker with ID {createShiftDto.WorkerId} not found");
        }

        // Validate times
        if (createShiftDto.EndTime <= createShiftDto.StartTime)
        {
            _logger.LogWarning("Cannot create shift. End time must be after start time");
            throw new ArgumentException("End time must be after start time");
        }

        var shift = new Shift
        {
            WorkerId = createShiftDto.WorkerId,
            StartTime = createShiftDto.StartTime,
            EndTime = createShiftDto.EndTime,
            Description = createShiftDto.Notes,
            HourlyRate = createShiftDto.HourlyRate,
            IsPaid = false
        };

        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Shift created with ID: {Id}", shift.Id);

        var duration = shift.EndTime - shift.StartTime;

        return new ShiftDto
        {
            Id = shift.Id,
            WorkerId = shift.WorkerId,
            WorkerName = null, // We don't have worker info loaded here
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Notes = shift.Description,
            HourlyRate = shift.HourlyRate,
            IsPaid = shift.IsPaid,
            Duration = duration,
            TotalPay = shift.HourlyRate * (decimal)duration.TotalHours
        };
    }

    public async Task<bool> UpdateShiftAsync(int id, UpdateShiftDto updateShiftDto)
    {
        _logger.LogInformation("Updating shift with ID: {Id}", id);

        var shift = await _context.Shifts.FindAsync(id);

        if (shift == null)
        {
            _logger.LogWarning("Shift with ID: {Id} not found for update", id);
            return false;
        }

        // Validate times
        if (updateShiftDto.EndTime <= updateShiftDto.StartTime)
        {
            _logger.LogWarning("Cannot update shift. End time must be after start time");
            throw new ArgumentException("End time must be after start time");
        }

        shift.StartTime = updateShiftDto.StartTime;
        shift.EndTime = updateShiftDto.EndTime;
        shift.Description = updateShiftDto.Notes;
        shift.HourlyRate = updateShiftDto.HourlyRate;
        shift.IsPaid = updateShiftDto.IsPaid;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Shift with ID: {Id} updated successfully", id);
        return true;
    }

    public async Task<bool> DeleteShiftAsync(int id)
    {
        _logger.LogInformation("Deleting shift with ID: {Id}", id);

        var shift = await _context.Shifts.FindAsync(id);

        if (shift == null)
        {
            _logger.LogWarning("Shift with ID: {Id} not found for deletion", id);
            return false;
        }

        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Shift with ID: {Id} deleted successfully", id);
        return true;
    }

    public async Task<bool> MarkShiftAsPaidAsync(int id)
    {
        _logger.LogInformation("Marking shift with ID: {Id} as paid", id);

        var shift = await _context.Shifts.FindAsync(id);

        if (shift == null)
        {
            _logger.LogWarning("Shift with ID: {Id} not found for marking as paid", id);
            return false;
        }

        shift.IsPaid = true;
        await _context.SaveChangesAsync();

        _logger.LogInformation("Shift with ID: {Id} marked as paid successfully", id);
        return true;
    }
}
