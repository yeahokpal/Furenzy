using Mono.Data.Sqlite;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Windows.Input;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    string dbName = "URI=file:Database.db";
    GameObject Fox;
    GameObject Bunny;
    GameObject Bird;
    GameObject Ferret;

    private void Awake()
    {
        if (!Directory.Exists(Application.streamingAssetsPath + "/Saves/"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Saves/");
        }
    }

    private void Start()
    {
        int healthTest = 1;
        string test = "";
        CreateDB();
        Write("player", "health", 1, healthTest.ToString()); //testing
        Read("player", "health", 1, test);
        Debug.Log(test);
    }

    private void Update()
    {
        Fox = GameObject.Find("Fox(Clone)");
        Bunny = GameObject.Find("Bunny(Clone)");
        Bird = GameObject.Find("Bird(Clone)");
        Ferret = GameObject.Find("Ferret(Clone)");
    }

    #region Methods

    public void CreateDB() // DONE!!
    {
        // Create DB connection
        using (var Connection = new SqliteConnection(dbName))
        {
            Connection.Open();

            // set up an object command to control db
            IDbCommand Command = Connection.CreateCommand();

            // Creating the Save Table if it doesn't already exist
            Command.CommandText = "CREATE TABLE IF NOT EXISTS Save (id INTEGER, level INTEGER, unlocked INTEGER, cleared INTEGER, playerCount INTEGER);";
            Command.ExecuteReader();

            Command = Connection.CreateCommand();


            // Creating the Player Table if it doesn't already exist
            Command.CommandText = "CREATE TABLE IF NOT EXISTS Player (id INTEGER, character TEXT, health INTEGER, mana INTEGER);";

            Command.ExecuteNonQuery();
            Connection.Close();
        }
    }

    public void Read(string table, string column, int id, string variableToChange) // DONE ----- TEST!!
    {
        Debug.Log("Start Read");
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //Setting up an object command to allow db caontrol
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM " + table + " ORDER BY id";
                command.ExecuteNonQuery();

                
                IDataReader dataReader = command.ExecuteReader();
                variableToChange = ($"{dataReader.GetInt32(id)} {dataReader[column]}");
                Debug.Log("End Read");
            }
            connection.Close();
        }

        /*// Connecting to the database
        IDbConnection Connection = new SqliteConnection(dbName);
        IDbCommand Command = Connection.CreateCommand();

        // Accessing the desired table
        Connection.Open();
        Command.CommandText = "SELECT * FROM " + table + " ORDER BY id";
        
        IDataReader dataReader = Command.ExecuteReader();

        // Reading and changing variableToChange
        while (dataReader.Read())
        {
            Debug.Log("reading data");
            //variableToChange = table --> id/row --> column
            variableToChange = ($"{dataReader.GetInt32(id)} {dataReader[column]}");
        }

        Connection.Close();*/
    }

    public void Write(string table, string column, int row, string variableToUse)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            Debug.Log("Start Write");
            connection.Open();

            //Setting up an object command to allow db caontrol
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM " + table + " WHERE id =" + row;
                command.ExecuteNonQuery();


                IDataReader dataReader = command.ExecuteReader();
                command.CommandText = "INSERT " + variableToUse + " INTO " + table + " (" + column + ") VALUES (" + row + ");"; // TEST THIS LINE
                //($"{dataReader.GetInt32(row)} {dataReader[column]}") = variableToUse;
                command.ExecuteNonQuery();
                Debug.Log("End Write");
            }
            connection.Close();
        }

        /*Debug.Log("Start Write");
        IDbConnection Connection = new SqliteConnection(dbName);
        IDbCommand Command = Connection.CreateCommand();

        Connection.Open();

        Command.CommandText = "SELECT * FROM " + table + " ORDER BY id";

        IDataReader dataReader = Command.ExecuteReader();

        while (dataReader.Read())
        {
            Command = Connection.CreateCommand();

            Command.CommandText = "INSERT INTO " + table + " (" + column + ") VALUES(" + variableToUse + ");";
            Command.ExecuteNonQuery();
            Debug.Log("End Write");
        }

        Connection.Close();*/
    }

    // Creates and Opens the Database
    /*public IDbConnection CreateAndOpenDatabase(string saveName)
    {
        // Finding the database that is of slot saveName
        string dataBase = "URI=file:" + Application.streamingAssetsPath + "/Saves/" + saveName + ".sqlite";
        // Initializing dbConnection to the database of saveName
        IDbConnection dbConnection = new SqliteConnection(dataBase);
        // Opening the saveName database
        dbConnection.Open();

        // Initializes dbCommand to create a command whenever
        IDbCommand dbCommand = dbConnection.CreateCommand();

        // Creating the Save Table
        dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS Save (level INTEGER, unlocked BOOLEAN, cleared BOOLEAN);";
        dbCommand.ExecuteReader();

        dbCommand = dbConnection.CreateCommand();

        // Creating the Player Table
        dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS Player (character STRING, health INTEGER, mana INTEGER );";
        dbCommand.ExecuteReader();

        return dbConnection;
    }*/
    #endregion
}