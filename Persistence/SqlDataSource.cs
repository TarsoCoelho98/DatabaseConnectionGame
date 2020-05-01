using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.Networking;
using System.Text;
using System;
using System.Data;
using System.Linq;
using Assets.Scripts.Persistence.DAL.Interfaces;


public class SqlDataSource : MonoBehaviour, ISqliteConnectionProvider
{
    public string DatabaseName;
    protected string DatabasePath;
    public SqliteConnection Connection => new SqliteConnection($"Data Source = {DatabasePath};");

    [SerializeField]
    protected bool isCopyDataBaseMode;

    protected void Awake()
    {
        if (isCopyDataBaseMode)
            CopyDatabaseFileIfNotExits();
        else
            CreateDatabaseFileIfNotExits();
    }

    #region Create Database

    protected void CopyDatabaseFileIfNotExits()
    {
        if (string.IsNullOrEmpty(DatabaseName))
        {
            Debug.LogError("DataBaseName não informado.");
            return;
        }

        DatabasePath = Path.Combine(Application.persistentDataPath, DatabaseName);

        var originDatabasePath = string.Empty;
        var isAndroid = false;

        if (File.Exists(DatabasePath))
            return;

#if UNITY_EDITOR || UNITY_WP8 || UNITY_WINRT
        originDatabasePath = Path.Combine(Application.streamingAssetsPath, DatabaseName);

#elif UNITY_STANDALONE_OSX
        originDatabasePath = Path.Combine(Application.dataPath, "/Resources/Data/StreamingAssets/", DatabaseName);

#elif UNITY_IOS
        originDatabasePath = Path.Combine(Application.dataPath, "Raw", DatabaseName);

#elif UNITY_ANDROID
        isAndroid = true;
        originDatabasePath = "jar:file://" + Application.dataPath + "!/assets/" + DatabaseName;
        StartCoroutine(GetInternalFileAndroid(originDatabasePath));
#endif

        if (isAndroid)
            return;

        File.Copy(originDatabasePath, DatabasePath);
    }

    private IEnumerator GetInternalFileAndroid(string path)
    {
        var request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
            Debug.Log("Erro ao ler Android File: " + request.error);
        else
        {
            File.WriteAllBytes(DatabasePath, request.downloadHandler.data);
            Debug.Log("Arquivo copiado");
        }
    }

    protected void CreateDatabaseFileIfNotExits()
    {
        if (string.IsNullOrEmpty(DatabaseName))
        {
            Debug.LogError("DataBaseName não informado.");
            return;
        }

        DatabasePath = Path.Combine(Application.persistentDataPath, DatabaseName);

        if (File.Exists(DatabasePath))
            return;

        SqliteConnection.CreateFile(DatabasePath);
        print(DatabasePath);

        CreateTables();
    }

    public void CreateTables()
    {
        CreateWeaponTable();
        CreateCharacterTable();
    }

    #endregion   

    protected void CreateCharacterTable()
    {
        using (var conn = Connection)
        {
            var str = new StringBuilder();

            str.Append("CREATE TABLE TB_CHARACTER(ID_CHARACTER INTEGER PRIMARY KEY, NM_CHARACTER TEXT NOT NULL, VL_ATTACK INTEGER NOT NULL,");
            str.Append(" VL_DEFENSE INT NOT NULL, VL_AGILITY INTEGER NOT NULL, VL_HEALTH INTEGER NOT NULL, FK_WEAPON INTEGER,");
            str.Append(" CONSTRAINT FK_WEAPON FOREIGN KEY(FK_WEAPON) REFERENCES TB_WEAPON(ID_WEAPON) ON UPDATE CASCADE ON DELETE RESTRICT)");

            conn.Open();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = str.ToString();
                command.ExecuteNonQuery();
            }
        }
    }

    protected void CreateWeaponTable()
    {
        using (var conn = Connection)
        {
            var str = new StringBuilder();
            str.AppendLine("CREATE TABLE TB_WEAPON(ID_WEAPON INTEGER PRIMARY KEY, NM_WEAPON TEXT NOT NULL,");
            str.AppendLine(" VL_ATTACK INTEGER NOT NULL, VL_WEAPON INTEGER NOT NULL)");

            conn.Open();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = str.ToString();
                command.ExecuteNonQuery();
            }
        }
    }
    
}
