namespace HospitalManagementSystem.Models;

public class Admission
{
    public int AdmissionId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string Status { get; set; } = "Admitted";
}
