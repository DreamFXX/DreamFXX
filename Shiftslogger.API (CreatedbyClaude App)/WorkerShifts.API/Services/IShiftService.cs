using WorkerShifts.API.Models.DTOs;

namespace WorkerShifts.API.Services;

public interface IShiftService
{
    Task<IEnumerable<ShiftDto>> GetAllShiftsAsync();
    Task<ShiftDto?> GetShiftByIdAsync(int id);
    Task<IEnumerable<ShiftDto>> GetShiftsByWorkerIdAsync(int workerId);
    Task<IEnumerable<ShiftDto>> GetShiftsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<ShiftDto> CreateShiftAsync(CreateShiftDto createShiftDto);
    Task<bool> UpdateShiftAsync(int id, UpdateShiftDto updateShiftDto);
    Task<bool> DeleteShiftAsync(int id);
    Task<bool> MarkShiftAsPaidAsync(int id);
}
