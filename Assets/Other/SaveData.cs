using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    public static void SavePlayer (Save PlayerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(PlayerData);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
