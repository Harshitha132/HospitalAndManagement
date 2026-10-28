using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Repositories;
using HospitalManagementSystem.Services;

var patientService = new PatientService(new PatientRepository());
var doctorService = new DoctorService(new DoctorRepository());
var admissionService = new AdmissionService(new AdmissionRepository());
var feeService = new HospitalFeeService(new HospitalFeeRepository());

try
{
    await using var connection = DatabaseHelper.CreateConnection();
    await connection.OpenAsync();
    Console.WriteLine("Connected to HospitalManagementDB successfully.");
    Console.WriteLine($"Patients: {(await patientService.GetAllAsync()).Count}");
    Console.WriteLine($"Doctors: {(await doctorService.GetAllAsync()).Count}");
    Console.WriteLine($"Admissions: {(await admissionService.GetAllAsync()).Count}");
    Console.WriteLine($"Hospital fee records: {(await feeService.GetAllAsync()).Count}");
}
catch (SqlException ex)
{
    Console.WriteLine($"SQL Server error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Application error: {ex.Message}");
}
