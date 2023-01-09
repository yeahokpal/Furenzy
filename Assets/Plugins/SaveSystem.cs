/*
 * Programmer: Jack
 * Purpose: Create and Use a Database for saving and loading data
 * Input: Player saves variables to database
 * Output: Loading saved variables
 */

using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    string dbName = "URI=file:Database.db";

    public Sprite CheckFilled;

    #region Default Methods
    private void Awake()
    {
        if (!Directory.Exists(Application.streamingAssetsPath + "/Saves/"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Saves/");
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (!File.Exists("Database.db"))
            CreateDB();
    }

    private void Update()
    {
        // Setting HubWorld Level Gates According to Save Data
        string isItTrue = "";

        GameObject lvl1;
        GameObject lvl2;
        GameObject lvl3;

        if (SceneManager.GetActiveScene().name == "HubWorld")
        {
            lvl1 = GameObject.Find("Level_1");
            lvl2 = GameObject.Find("Level_2");
            lvl3 = GameObject.Find("Level_3");

            // Deciding to allow entry into levels

            Read("Save", "unlocked", 1, isItTrue);
            if (isItTrue != "0")
            {
                lvl1.SetActive(false);
            }
            isItTrue = "";

            Read("Save", "unlocked", 2, isItTrue);
            if (isItTrue != "0")
            {
                lvl2.SetActive(false);
            }
            isItTrue = "";

            Read("Save", "unlocked", 3, isItTrue);
            if (isItTrue != "0")
            {
                lvl3.SetActive(false);
            }
            isItTrue = "";

            // Deciding if levels have been completed

            Read("Save", "cleared", 1, isItTrue);
            if (isItTrue != "0")
            {
                lvl1.GetComponent<SpriteRenderer>().sprite = CheckFilled;
            }
            isItTrue = "";

            Read("Save", "cleared", 2, isItTrue);
            if (isItTrue != "0")
            {
                lvl2.GetComponent<SpriteRenderer>().sprite = CheckFilled;
            }
            isItTrue = "";

            Read("Save", "cleared", 3, isItTrue);
            if (isItTrue != "0")
            {
                lvl3.GetComponent<SpriteRenderer>().sprite = CheckFilled;
            }
        }
    }
    #endregion

    #region Custom Methods

    public void CreateDB()
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

            Command.CommandText = "INSERT OR REPLACE INTO Save ('id', 'level', 'unlocked', 'cleared') VALUES (1, '1', 0, 0);";
            Command.ExecuteNonQuery();

            Command = Connection.CreateCommand();
            Command.CommandText = "INSERT OR REPLACE INTO Save ('id', 'level', 'unlocked', 'cleared') VALUES (2, '2', 0, 0);";
            Command.ExecuteNonQuery();

            Command = Connection.CreateCommand();
            Command.CommandText = "INSERT OR REPLACE INTO Save ('id', 'level', 'unlocked', 'cleared') VALUES (3, '3', 0, 0);";
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

    public string Read(string table, string column, int row, string variableToChange)
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

    public void DeleteAndRecreateDatabase()
    {
        File.Delete("Database.db");
        CreateDB();
    }
    #endregion
}