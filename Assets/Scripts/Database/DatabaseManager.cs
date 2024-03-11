using MySql.Data.MySqlClient;
using Unity.Netcode;

public class DatabaseManager : NetworkBehaviour
{
    private MySqlConnection connection;

    private void Start()
    {
        if(IsServer)
        {
            connection = new MySqlConnection(
                "server=127.0.0.1;" +
                "port=3306;" +
                "user=root;" +
                "password=123456;" +
                "database=Platform");
            connection.Open();
        }
    }
}
