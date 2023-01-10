/*
 * Programmer: Jack
 * Purpose: Create and Use a Database for saving and loading data
 * Input: Player saves variables to database
 * Output: Loading saved variables
 */

using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    string dbName = "URI=file:Database.db";

    public Sprite CheckFilled;

    GameObject lvl1;
    GameObject lvl2;
    GameObject lvl3;
    GameObject check1;
    GameObject check2;
    GameObject check3;

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
        string isItTrue = "0";

        if (SceneManager.GetActiveScene().name == "HubWorld")
        {
            lvl1 = GameObject.Find("Level_1");
            lvl2 = GameObject.Find("Level_2");
            lvl3 = GameObject.Find("Level_3");
            check1 = GameObject.Find("CheckBox1");
            check2 = GameObject.Find("CheckBox2");
            check3 = GameObject.Find("CheckBox3");

            // Deciding to allow entry into levels

            Read("Save", "unlocked", 1, isItTrue);
            if (int.Parse(isItTrue) == 1)
            {
                lvl1.GetComponent<BoxCollider2D>().enabled = false;
            }
            isItTrue = "0";

            Read("Save", "unlocked", 2, isItTrue);
            if (int.Parse(isItTrue) == 1)
            {
                lvl2.GetComponent<BoxCollider2D>().enabled = false;
            }
            isItTrue = "0";

            Read("Save", "unlocked", 3, isItTrue);
            if (int.Parse(isItTrue) == 1)
            {
                lvl3.GetComponent<BoxCollider2D>().enabled = false;
            }
            isItTrue = "0";

            // Deciding if levels have been completed

            Read("Save", "cleared", 1, isItTrue);
            //Debug.Log(isItTrue);
            if (int.Parse(isItTrue) == 1)
            {
                check1.GetComponent<SpriteRenderer>().sprite = CheckFilled;
            }
            isItTrue = "0";

            Read("Save", "cleared", 2, isItTrue);
            if (int.Parse(isItTrue) == 1)
            {
                check2.GetComponent<SpriteRenderer>().sprite = CheckFilled;
            }
            isItTrue = "0";

            Read("Save", "cleared", 3, isItTrue);
            if (int.Parse(isItTrue) == 1)
            {
                check3.GetComponent<SpriteRenderer>().sprite = CheckFilled;
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
            Command.CommandText = "CREATE TABLE IF NOT EXISTS Save (id INTEGER, level INTEGER, unlocked INTEGER, cleared INTEGER);";
            Command.ExecuteReader();

            Command = Connection.CreateCommand();

            // Creating the Player Table if it doesn't already exist
            Command.CommandText = "CREATE TABLE IF NOT EXISTS Player (id INTEGER, character TEXT, health INTEGER, mana INTEGER);";

            Command.ExecuteNonQuery();

            Command = Connection.CreateCommand();

            #region Initial Writes to Tables

            // Initial Write for Save Table

            Command.CommandText = "INSERT OR REPLACE INTO Save ('id', 'level', 'unlocked', 'cleared') VALUES (1, '1', 0, 1);";
            Command.ExecuteNonQuery();

            Command = Connection.CreateCommand();
            Command.CommandText = "INSERT OR REPLACE INTO Save ('id', 'level', 'unlocked', 'cleared') VALUES (2, '2', 0, 1);";
            Command.ExecuteNonQuery();

            Command = Connection.CreateCommand();
            Command.CommandText = "INSERT OR REPLACE INTO Save ('id', 'level', 'unlocked', 'cleared') VALUES (3, '3', 0, 1);";
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

    public string Read(string table, string column, int row, string variableToChange) // FIX!!
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //Setting up an object command to allow db caontrol
            using (var command = connection.CreateCommand())
            {
                /*command.CommandText = "SELECT DISTINCT " + column + " FROM " + table + " WHERE id = " + row + " ORDER BY id;";
                command.CommandText = "SELECT " + column + ", id FROM " + table + " WHERE id = " + row + ";";
                command.ExecuteScalar();

                variableToChange = new SqliteCommand(command.CommandText, connection).ToString();




                variableToChange = command.ExecuteScalar().ToString();*/




                
                command.CommandText = "SELECT " + column + ", id FROM " + table + " WHERE id = " + row.ToString() + ";";
                variableToChange = command.ExecuteReader().Read().ToString();
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
                command.CommandText = "SELECT DISTINCT " + column + " FROM " + table + " WHERE id = " + row.ToString() + " ORDER BY id;";
                command.ExecuteNonQuery();

                //IDataReader dataReader = command.ExecuteReader();
                command.CommandText = "UPDATE " + table + " SET " + column + " = " + variableToUse + " WHERE id = " + row.ToString();
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