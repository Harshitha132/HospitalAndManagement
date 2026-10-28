using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;
namespace HospitalManagementSystem.Services;

public class PatientService
{
    private readonly PatientRepository _repository;
    public PatientService(PatientRepository repository) => _repository = repository;
    public Task<int> AddAsync(Patient p)
    {
        if (string.IsNullOrWhiteSpace(p.FullName)) throw new ArgumentException("Patient name is required.");
        if (p.DateOfBirth.Date > DateTime.Today) throw new ArgumentException("Date of birth cannot be in the future.");
        if (!new[] { "Female", "Male", "Other" }.Contains(p.Gender)) throw new ArgumentException("Gender must be Female, Male, or Other.");
        if (string.IsNullOrWhiteSpace(p.Phone)) throw new ArgumentException("Patient phone is required.");
        return _repository.AddAsync(p);
    }
    public Task<List<Patient>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Patient?> GetByIdAsync(int id) { if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id)); return _repository.GetByIdAsync(id); }
   
}