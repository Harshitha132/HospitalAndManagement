using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;
namespace HospitalManagementSystem.Services;

public class HospitalFeeService
{
    private readonly HospitalFeeRepository _repository;
    public HospitalFeeService(HospitalFeeRepository repository) => _repository = repository;
    public Task<int> AddAsync(HospitalFee f) { Validate(f); return _repository.AddAsync(f); }
    public Task<List<HospitalFee>> GetAllAsync() => _repository.GetAllAsync();
    public Task<HospitalFee?> GetByIdAsync(int id) { if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id)); return _repository.GetByIdAsync(id); }
    public Task<bool> UpdateAsync(HospitalFee f) { if (f.HospitalFeeId <= 0) throw new ArgumentOutOfRangeException(nameof(f.HospitalFeeId)); Validate(f); return _repository.UpdateAsync(f); }
    public Task<bool> DeleteAsync(int id) { if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id)); return _repository.DeleteAsync(id); }
    private static void Validate(HospitalFee f)
    {
        if (f.AdmissionId <= 0) throw new ArgumentException("Admission ID must be positive.");
        if (f.ConsultationFee < 0 || f.RoomCharges < 0 || f.MedicineCharges < 0 || f.OtherCharges < 0) throw new ArgumentException("Charges cannot be negative.");
        if (!new[] { "Pending", "PartiallyPaid", "Paid" }.Contains(f.PaymentStatus)) throw new ArgumentException("Invalid payment status.");
    }
}
