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
    public int Voltage { get; set; }

    public ChargingStationData() { }
    public ChargingStationData(string id, string type, float price, float power, int voltage)
    {
        Id = id;
        Type = type;
        Price = price;
        Power = power;
        Voltage = voltage;
    }
    public ChargingStationData(ChargingStation station) : this(station.id, station.data.type, station.data.price, station.data.power, station.data.voltage)
    {

    }
}

[Table("Fault")]
public class FaultData
{
    public int Id { get; set; }
    public string Time { get; set; }
    public string Content { get; set; }
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
    public int Id { get; set; }
    public int PhoneNumber { get; set; }
    public string StationId { get; set; }
    public string Time { get; set; }
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
    public int PhoneNumber { get; set; }
    public string Model { get; set; }
    public string Password { get; set; }

    public UserData() { }
    public UserData(int phoneNumber, string model, string password)
    {
        PhoneNumber = phoneNumber;
        Model = model;
        Password = password;
    }
}