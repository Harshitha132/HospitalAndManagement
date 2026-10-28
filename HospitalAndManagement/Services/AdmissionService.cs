using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;
namespace HospitalManagementSystem.Services;

public class AdmissionService
{
    private readonly AdmissionRepository _repository;
    public AdmissionService(AdmissionRepository repository) => _repository = repository;
    public Task<int> AddAsync(Admission a) { Validate(a); return _repository.AddAsync(a); }
    public Task<List<Admission>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Admission?> GetByIdAsync(int id) { if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id)); return _repository.GetByIdAsync(id); }
    public Task<bool> UpdateAsync(Admission a) { if (a.AdmissionId <= 0) throw new ArgumentOutOfRangeException(nameof(a.AdmissionId)); Validate(a); return _repository.UpdateAsync(a); }
    private static void Validate(Admission a)
    {
        if (a.PatientId <= 0 || a.DoctorId <= 0) throw new ArgumentException("Patient and doctor IDs must be positive.");
        if (string.IsNullOrWhiteSpace(a.RoomNumber) || string.IsNullOrWhiteSpace(a.Diagnosis)) throw new ArgumentException("Room number and diagnosis are required.");
        if (!new[] { "Admitted", "Discharged", "Cancelled" }.Contains(a.Status)) throw new ArgumentException("Invalid admission status.");
        if (a.DischargeDate.HasValue && a.DischargeDate < a.AdmissionDate) throw new ArgumentException("Discharge date cannot be before admission date.");
    }
}

