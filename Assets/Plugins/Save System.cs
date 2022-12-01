using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using UnityEngine;

public class SqliteManager : MonoBehaviour
{
    #region Variables

    SavesManager savesManager;
    healthManager healthManager;
    GameObject player;

    #endregion

    #region Default Methods

    private void Awake()
    {
        if (!Directory.Exists(Application.streamingAssetsPath + "/Saves/"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Saves/");
        }
    }

    #endregion

    #region Custom Methods

    public void Read(string saveName)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase(saveName);
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM Save";
        IDataReader dataReader = dbCommand.ExecuteReader();

        dataReader.Read();

        savesManager.currentSave.seed = dataReader.GetInt32(0);

        dbConnection.Close();

        dbConnection = CreateAndOpenDatabase(saveName);
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM Player";
        dataReader = dbCommand.ExecuteReader();

        dataReader.Read();

        savesManager.currentSave.playerMaxHealth = dataReader.GetInt32(1);

        savesManager.currentSave.playerHealth = dataReader.GetInt32(0);

        savesManager.currentSave.playerx = dataReader.GetFloat(2);

        savesManager.currentSave.playery = dataReader.GetFloat(3);

        dbConnection.Close();
    }

    public void Write(string saveName)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase(saveName);
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "INSERT OR REPLACE INTO Save (seed) VALUES (" + savesManager.currentSave.seed + ")";
        dbCommand.ExecuteNonQuery();

        dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "INSERT OR REPLACE INTO Player (health, maxhealth, playerx, playery) VALUES (" + healthManager.health + ", " + healthManager.maxHealth + ", " + player.transform.position.x + ", " + player.transform.position.y + ")"; // 10
        dbCommand.ExecuteNonQuery();

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

        player = GameObject.Find("player");

        dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS Player (health INTEGER, maxhealth INTEGER, playerx FLOAT, playery FLOAT )";
        dbCommand.ExecuteReader();

        return dbConnection;
    }

    #endregion
}