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
    [Column("time")]
    public string Time { get; set; }
    [Column("content")]
    public string Content { get; set; }
    [Column("solved")]
    public bool Solved { get; set; }

    public FaultData() { }
    public FaultData(int id, string time, string content, bool solved)
    {
        Id = id;
        Time = time;
        Content = content;
        Solved = solved;
    }
}

[Table("Usage")]
public class UsageData
{
    [Column("id"), PrimaryKey]
    public int Id { get; set; }
    [Column("phoneNumber")]
    public int PhoneNumber { get; set; }
    [Column("stationId")]
    public string StationId { get; set; }
    [Column("time")]
    public string Time { get; set; }
    [Column("state")]
    public EStationState State { get; set; }

    public UsageData() { }
    public UsageData(int id, int phoneNumber, string stationId, string time, EStationState state)
    {
        Id = id;
        PhoneNumber = phoneNumber;
        StationId = stationId;
        Time = time;
        State = state;
    }
}

[Table("User")]
public class UserData
{
    [Column("phoneNumber"),PrimaryKey]
    public int PhoneNumber { get; set; }
    [Column("model")]
    public string Model { get; set; }
    [Column("password")]
    public string Password { get; set; }

    public UserData() { }
    public UserData(int phoneNumber, string model, string password)
    {
        PhoneNumber = phoneNumber;
        Model = model;
        Password = password;
    }
}