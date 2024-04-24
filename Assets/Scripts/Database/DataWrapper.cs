using SQLite4Unity3d;

[Table("ChargingStation")]
public class ChargingStationData
{
    [Column("id"),PrimaryKey]
    public string Id { get; set; }
    [Column("type")]
    public string Type { get; set; }
    [Column("price")]
    public float Price { get; set; }
    [Column("power")]
    public float Power { get; set; }
    [Column("voltage")]
    public string Voltage { get; set; }

    public ChargingStationData() { }
    public ChargingStationData(string id, string type, float price, float power, string voltage)
    {
        Id = id;
        Type = type;
        Price = price;
        Power = power;
        Voltage = voltage;
    }
}

[Table("Fault")]
public class FaultData
{
    [Column("id"), PrimaryKey]
    public int Id { get; set; }

    [Column("stationId")]
    public string StationId { get; set; }
    [Column("time")]
    public string Time { get; set; }
    [Column("content")]
    public string Content { get; set; }
    [Column("solved")]
    public bool Solved { get; set; }

    public FaultData() { }
}

[Table("Usage")]
public class UsageData
{
    [Column("id"), PrimaryKey]
    public int Id { get; set; }
    [Column("phoneNumber")]
    public string PhoneNumber { get; set; }
    [Column("stationId")]
    public string StationId { get; set; }
    [Column("time")]
    public string Time { get; set; }
    [Column("state")]
    public int State { get; set; }

    public UsageData() { }

    public static string StateName(EStationState state)
    {
        return state switch
        {
            EStationState.Available => "空闲",
            EStationState.Booked => "被预定",
            EStationState.Ocuppied => "使用中",
            EStationState.Repairing => "维修中",
            _ => string.Empty,
        };
    }
}

[Table("User")]
public class UserData
{
    [Column("phoneNumber"),PrimaryKey]
    public string PhoneNumber { get; set; }
    [Column("model")]
    public string Model { get; set; }
    [Column("password")]
    public string Password { get; set; }

    public UserData() { }
}