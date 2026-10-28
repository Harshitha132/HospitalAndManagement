using System.Data;
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class DoctorRepository
{
    public async Task<int> AddAsync(Doctor d)
    {
        const string sql = """
            INSERT INTO dbo.Doctors (FullName, Specialization, Phone, Email, ConsultationFee, IsActive)
            VALUES (@Name, @Specialization, @Phone, @Email, @Fee, @Active);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            """;
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        AddFields(cmd, d);
        await c.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    private static void AddFields(SqlCommand cmd, Doctor d)
    {
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 120).Value = d.FullName;
        cmd.Parameters.Add("@Specialization", SqlDbType.NVarChar, 100).Value = d.Specialization;
        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = d.Phone;
        cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = (object?)d.Email ?? DBNull.Value;
        var fee = cmd.Parameters.Add("@Fee", SqlDbType.Decimal); fee.Precision = 10; fee.Scale = 2; fee.Value = d.ConsultationFee;
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = d.IsActive;
    }

    private static Doctor Map(SqlDataReader r) => new()
    {
        DoctorId = r.GetInt32(0),
        FullName = r.GetString(1),
        Specialization = r.GetString(2),
        Phone = r.GetString(3),
        Email = r.IsDBNull(4) ? null : r.GetString(4),
        ConsultationFee = r.GetDecimal(5),
        IsActive = r.GetBoolean(6)
    };
}

