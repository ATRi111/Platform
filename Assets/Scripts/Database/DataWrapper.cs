using System;

public class ChargingStationData
{
    public string id;
    public string type;
    public float price;
    public float power;
    public float electricity;

    public ChargingStationData(string id, string type, float price, float power, float electricity)
    {
        this.id = id ?? throw new ArgumentNullException(nameof(id));
        this.type = type;
        this.price = price;
        this.power = power;
        this.electricity = electricity;
    }
}

public class FaultData
{
    public int id;
    public DateTime time;
    public string content;
    public bool solved;

    public FaultData(int id, DateTime time, string content, bool solved)
    {
        this.id = id;
        this.time = time;
        this.content = content;
        this.solved = solved;
    }
}

public class UsageData
{
    public int id;
    public int phoneNumber;
    public string stationId;
    public DateTime time;
    public EStationState state;

    public UsageData(int id, int phoneNumber, string stationId, DateTime time, EStationState state)
    {
        this.id = id;
        this.phoneNumber = phoneNumber;
        this.stationId = stationId;
        this.time = time;
        this.state = state;
    }
}

public class UserData
{
    public int phoneNumber;
    public string model;
    public string password;

    public UserData(int phoneNumber, string model, string password)
    {
        this.phoneNumber = phoneNumber;
        this.model = model;
        this.password = password;
    }
}