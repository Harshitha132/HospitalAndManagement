namespace HospitalManagementSystem.Models;

public class HospitalFee
{
    public int HospitalFeeId { get; set; }
    public int AdmissionId { get; set; }
    public decimal ConsultationFee { get; set; }
    public decimal RoomCharges { get; set; }
    public decimal MedicineCharges { get; set; }
    public decimal OtherCharges { get; set; }
    // Calculated by SQL Server; do not write to this property in INSERT/UPDATE.
    public decimal TotalAmount { get; set; }
    public string PaymentStatus { get; set; } = "Pending";
    public DateTime? PaymentDate { get; set; }
}
