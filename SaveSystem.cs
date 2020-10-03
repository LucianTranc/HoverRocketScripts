using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem {

    //https://www.youtube.com/watch?v=XOjd_qU2Ido

    public static void SavePlayer (PlayerData data) {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static void DeleteData () {

        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path)) {

            File.Delete(path);

        }

    }

    public static PlayerData LoadPlayer() {

        string path = Application.persistentDataPath + "/player.data";
        PlayerData data;
        if (File.Exists(path)) {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            
            return data;

        }
        else {

            Debug.Log("path not found");

            data = new PlayerData();
            return data;

        }

    }
    
}
