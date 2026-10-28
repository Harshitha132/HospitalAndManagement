namespace HospitalManagementSystem.Models;

public class Doctor
{
    public int DoctorId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public decimal ConsultationFee { get; set; }
    public bool IsActive { get; set; } = true;
}
