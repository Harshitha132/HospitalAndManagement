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

    private void AddFields(SqlCommand cmd, Patient p)
    {
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100)
            .Value = p.FullName;

        cmd.Parameters.Add("@Dob", SqlDbType.Date)
            .Value = p.DateOfBirth.Date;

        cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 20)
            .Value = p.Gender;

        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20)
            .Value = p.Phone;

        cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150)
            .Value = (object?)p.Email ?? DBNull.Value;

        cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250)
            .Value = (object?)p.Address ?? DBNull.Value;
    }
}