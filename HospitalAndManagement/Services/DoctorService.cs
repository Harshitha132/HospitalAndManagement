using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;
namespace HospitalManagementSystem.Services;

public class DoctorService
{
    private readonly DoctorRepository _repository;
    public DoctorService(DoctorRepository repository) => _repository = repository;
    public Task<int> AddAsync(Doctor d)
    {
        if (string.IsNullOrWhiteSpace(d.FullName)) throw new ArgumentException("Doctor name is required.");
        if (string.IsNullOrWhiteSpace(d.Specialization)) throw new ArgumentException("Specialization is required.");
        if (string.IsNullOrWhiteSpace(d.Phone)) throw new ArgumentException("Doctor phone is required.");
        if (d.ConsultationFee < 0) throw new ArgumentException("Consultation fee cannot be negative.");
        return _repository.AddAsync(d);
    }
    public Task<List<Doctor>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Doctor?> GetByIdAsync(int id) { if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id)); return _repository.GetByIdAsync(id); }

}