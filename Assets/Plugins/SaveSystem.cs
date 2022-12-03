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
        if (!File.Exists("Database.db"))
            CreateDB();
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

            Command = Connection.CreateCommand();

            #region Initial Writes to Tables

            // Initial Write for Save Table

            Command.CommandText = "INSERT OR REPLACE INTO Save ('id', 'level') VALUES (1, '1');";
            Command.ExecuteNonQuery();

            Command = Connection.CreateCommand();
            Command.CommandText = "INSERT OR REPLACE INTO Save ('id', 'level') VALUES (2, '2');";
            Command.ExecuteNonQuery();

            Command = Connection.CreateCommand();
            Command.CommandText = "INSERT OR REPLACE INTO Save ('id', 'level') VALUES (3, '3');";
            Command.ExecuteNonQuery();

            // Initial Writes for Player Table

            Command = Connection.CreateCommand();

            Command.CommandText = "INSERT OR REPLACE INTO Player ('id', 'character', 'health', 'mana') VALUES (1, 'Fox', 3, 1);";
            Command.ExecuteNonQuery();
            Command = Connection.CreateCommand();

            Command.CommandText = "INSERT OR REPLACE INTO Player ('id', 'character', 'health', 'mana') VALUES (2, 'Bunny', 3, 1);";
            Command.ExecuteNonQuery();

            Command = Connection.CreateCommand();
            Command.CommandText = "INSERT OR REPLACE INTO Player ('id', 'character', 'health', 'mana') VALUES (3, 'Bird', 3, 1);";
            Command.ExecuteNonQuery();

            Command = Connection.CreateCommand();
            Command.CommandText = "INSERT OR REPLACE INTO Player ('id', 'character', 'health', 'mana') VALUES (4, 'Ferret', 3, 1);";
            Command.ExecuteNonQuery();

            Command = Connection.CreateCommand();
            #endregion

            Connection.Close();
        }
    }

    public string Read(string table, string column, int row, string variableToChange) // DONE ----- TEST!!
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //Setting up an object command to allow db caontrol
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM " + table + " ORDER BY id;";
                command.ExecuteNonQuery();

                command.CommandText = "SELECT DISTINCT " + column + " FROM " + table + " WHERE id = " + row + " ORDER BY id;";
                variableToChange = command.ExecuteScalar().ToString();
            }
            connection.Close();

            return variableToChange;
        }
    }

    public void Write(string table, string column, int row, string variableToUse)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //Setting up an object command to allow db caontrol
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT DISTINCT " + column + " FROM " + table + " WHERE id = " + row + " ORDER BY id;"; // NOT GETTING AN EXISTING ROW
                command.ExecuteNonQuery();

                //IDataReader dataReader = command.ExecuteReader();
                command.CommandText = "UPDATE " + table + " SET " + column + " = " + variableToUse + " WHERE id = " + row;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    #endregion
}