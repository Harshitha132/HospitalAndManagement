using System.Data;
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class AdmissionRepository
{
    public async Task<int> AddAsync(Admission a)
    {
        const string sql = """
            INSERT INTO dbo.Admissions (PatientId, DoctorId, AdmissionDate, DischargeDate, RoomNumber, Diagnosis, Status)
            VALUES (@PatientId, @DoctorId, @AdmissionDate, @DischargeDate, @Room, @Diagnosis, @Status);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            """;
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        AddFields(cmd, a);
        await c.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task<List<Admission>> GetAllAsync()
    {
        const string sql = "SELECT AdmissionId, PatientId, DoctorId, AdmissionDate, DischargeDate, RoomNumber, Diagnosis, Status FROM dbo.Admissions ORDER BY AdmissionId;";
        var list = new List<Admission>();
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        await c.OpenAsync();
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync()) list.Add(Map(r));
        return list;
    }

    public async Task<Admission?> GetByIdAsync(int id)
    {
        const string sql = "SELECT AdmissionId, PatientId, DoctorId, AdmissionDate, DischargeDate, RoomNumber, Diagnosis, Status FROM dbo.Admissions WHERE AdmissionId=@Id;";
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await c.OpenAsync();
        await using var r = await cmd.ExecuteReaderAsync();
        return await r.ReadAsync() ? Map(r) : null;
    }
    public async Task<bool> UpdateAsync(Admission a)
    {
        const string sql = """
            UPDATE dbo.Admissions SET PatientId=@PatientId, DoctorId=@DoctorId, AdmissionDate=@AdmissionDate,
                DischargeDate=@DischargeDate, RoomNumber=@Room, Diagnosis=@Diagnosis, Status=@Status
            WHERE AdmissionId=@Id;
            """;
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        AddFields(cmd, a);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = a.AdmissionId;
        await c.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }


    private static void AddFields(SqlCommand cmd, Admission a)
    {
        cmd.Parameters.Add("@PatientId", SqlDbType.Int).Value = a.PatientId;
        cmd.Parameters.Add("@DoctorId", SqlDbType.Int).Value = a.DoctorId;
        cmd.Parameters.Add("@AdmissionDate", SqlDbType.DateTime2).Value = a.AdmissionDate;
        cmd.Parameters.Add("@DischargeDate", SqlDbType.DateTime2).Value = (object?)a.DischargeDate ?? DBNull.Value;
        cmd.Parameters.Add("@Room", SqlDbType.NVarChar, 20).Value = a.RoomNumber;
        cmd.Parameters.Add("@Diagnosis", SqlDbType.NVarChar, 300).Value = a.Diagnosis;
        cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = a.Status;
    }

    private static Admission Map(SqlDataReader r) => new()
    {
        AdmissionId = r.GetInt32(0),
        PatientId = r.GetInt32(1),
        DoctorId = r.GetInt32(2),
        AdmissionDate = r.GetDateTime(3),
        DischargeDate = r.IsDBNull(4) ? null : r.GetDateTime(4),
        RoomNumber = r.GetString(5),
        Diagnosis = r.GetString(6),
        Status = r.GetString(7)
    };
}
