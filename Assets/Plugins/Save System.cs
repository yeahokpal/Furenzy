using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using UnityEngine;

public class SqliteManager : MonoBehaviour
{
    string dbName = "URI=file:Inventory.db";

    private void Awake()
    {
        if (!Directory.Exists(Application.streamingAssetsPath + "/Saves/"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Saves/");
        }
    }

    #region Methods

    public void Read(string saveName)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase(saveName);
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM Save";
        IDataReader dataReader = dbCommand.ExecuteReader();

        dataReader.Read();

        dbConnection.Close();

        dbConnection = CreateAndOpenDatabase(saveName);
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM Player";
        dataReader = dbCommand.ExecuteReader();

        dataReader.Read();

        dbConnection.Close();
    }

    public void Write(string saveName)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase(saveName);
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand = dbConnection.CreateCommand();

        dbConnection.Close();
    }

    // Creates and Opens the Database
    public IDbConnection CreateAndOpenDatabase(string saveName)
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
        dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS Save (level INTEGER, unlocked BOOLEAN, cleared BOOLEAN)";
        dbCommand.ExecuteReader();

        dbCommand = dbConnection.CreateCommand();

        // Creating the Player Table
        dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS Player (character STRING, health INTEGER, mana INTEGER )";
        dbCommand.ExecuteReader();

        return dbConnection;
    }
    #endregion
}