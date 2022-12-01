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

    private void Start()
    {
        
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

    public IDbConnection CreateAndOpenDatabase(string saveName)
    {
        string dataBase = "URI=file:" + Application.streamingAssetsPath + "/Saves/" + saveName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dataBase);
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS Save (seed INTEGER )";
        dbCommand.ExecuteReader();

        dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS Player (health INTEGER, maxhealth INTEGER, playerx FLOAT, playery FLOAT )";
        dbCommand.ExecuteReader();

        return dbConnection;
    }
    #endregion
}