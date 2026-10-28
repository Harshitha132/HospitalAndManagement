using System.Data;
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class HospitalFeeRepository
{
    public async Task<int> AddAsync(HospitalFee f)
    {
        const string sql = """
            INSERT INTO dbo.HospitalFees
                (AdmissionId, ConsultationFee, RoomCharges, MedicineCharges, OtherCharges, PaymentStatus, PaymentDate)
            VALUES (@AdmissionId, @Consultation, @Room, @Medicine, @Other, @Status, @PaymentDate);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            """;
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        AddFields(cmd, f);
        await c.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task<List<HospitalFee>> GetAllAsync()
    {
        const string sql = "SELECT HospitalFeeId, AdmissionId, ConsultationFee, RoomCharges, MedicineCharges, OtherCharges, TotalAmount, PaymentStatus, PaymentDate FROM dbo.HospitalFees ORDER BY HospitalFeeId;";
        var list = new List<HospitalFee>();
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        await c.OpenAsync();
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync()) list.Add(Map(r));
        return list;
    }

    public async Task<HospitalFee?> GetByIdAsync(int id)
    {
        const string sql = "SELECT HospitalFeeId, AdmissionId, ConsultationFee, RoomCharges, MedicineCharges, OtherCharges, TotalAmount, PaymentStatus, PaymentDate FROM dbo.HospitalFees WHERE HospitalFeeId=@Id;";
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await c.OpenAsync();
        await using var r = await cmd.ExecuteReaderAsync();
        return await r.ReadAsync() ? Map(r) : null;
    }
    public async Task<bool> UpdateAsync(HospitalFee f)
    {
        const string sql = """
            UPDATE dbo.HospitalFees SET AdmissionId=@AdmissionId, ConsultationFee=@Consultation,
                RoomCharges=@Room, MedicineCharges=@Medicine, OtherCharges=@Other,
                PaymentStatus=@Status, PaymentDate=@PaymentDate WHERE HospitalFeeId=@Id;
            """;
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand(sql, c);
        AddFields(cmd, f);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = f.HospitalFeeId;
        await c.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        await using var c = DatabaseHelper.CreateConnection();
        await using var cmd = new SqlCommand("DELETE FROM dbo.HospitalFees WHERE HospitalFeeId=@Id;", c);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await c.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    private static void AddFields(SqlCommand cmd, HospitalFee f)
    {
        cmd.Parameters.Add("@AdmissionId", SqlDbType.Int).Value = f.AdmissionId;
        AddMoney(cmd, "@Consultation", f.ConsultationFee);
        AddMoney(cmd, "@Room", f.RoomCharges);
        AddMoney(cmd, "@Medicine", f.MedicineCharges);
        AddMoney(cmd, "@Other", f.OtherCharges);
        cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = f.PaymentStatus;
        cmd.Parameters.Add("@PaymentDate", SqlDbType.DateTime2).Value = (object?)f.PaymentDate ?? DBNull.Value;
    }

    private static void AddMoney(SqlCommand cmd, string name, decimal value)
    {
        var p = cmd.Parameters.Add(name, SqlDbType.Decimal);
        p.Precision = 10; p.Scale = 2; p.Value = value;
    }

    private static HospitalFee Map(SqlDataReader r) => new()
    {
        HospitalFeeId = r.GetInt32(0),
        AdmissionId = r.GetInt32(1),
        ConsultationFee = r.GetDecimal(2),
        RoomCharges = r.GetDecimal(3),
        MedicineCharges = r.GetDecimal(4),
        OtherCharges = r.GetDecimal(5),
        TotalAmount = r.GetDecimal(6),
        PaymentStatus = r.GetString(7),
        PaymentDate = r.IsDBNull(8) ? null : r.GetDateTime(8)
    };
}
