using System.Data;
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class PatientRepository
{
    public async Task<int> AddAsync(Patient p)
    {
        const string sql = """
            INSERT INTO dbo.Patients
                (FullName, DateOfBirth, Gender, Phone, Email, Address)
            VALUES
                (@Name, @Dob, @Gender, @Phone, @Email, @Address);

            SELECT CAST(SCOPE_IDENTITY() AS INT);
            """;

        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);

        AddFields(cmd, p);

        await c.OpenAsync();

        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task<List<Patient>> GetAllAsync()
    {
        const string sql = "SELECT PatientId, FullName, DateOfBirth, Gender, Phone, Email, Address, CreatedAt FROM dbo.Patients ORDER BY PatientId;";
        var list = new List<Patient>();
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        await c.OpenAsync();
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync()) list.Add(Map(r));
        return list;
    }

    public async Task<Patient?> GetByIdAsync(int id)
    {
        const string sql = "SELECT PatientId, FullName, DateOfBirth, Gender, Phone, Email, Address, CreatedAt FROM dbo.Patients WHERE PatientId=@Id;";
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await c.OpenAsync();
        await using var r = await cmd.ExecuteReaderAsync();
        return await r.ReadAsync() ? Map(r) : null;
    }



    private static void AddFields(SqlCommand cmd, Patient p)
    {
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 120).Value = p.FullName;
        cmd.Parameters.Add("@Dob", SqlDbType.Date).Value = p.DateOfBirth.Date;
        cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 20).Value = p.Gender;
        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = p.Phone;
        cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = (object?)p.Email ?? DBNull.Value;
        cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 300).Value = (object?)p.Address ?? DBNull.Value;
    }

    private static Patient Map(SqlDataReader r) => new()
    {
        PatientId = r.GetInt32(0),
        FullName = r.GetString(1),
        DateOfBirth = r.GetDateTime(2),
        Gender = r.GetString(3),
        Phone = r.GetString(4),
        Email = r.IsDBNull(5) ? null : r.GetString(5),
        Address = r.IsDBNull(6) ? null : r.GetString(6),
        CreatedAt = r.GetDateTime(7)
    };
}

}