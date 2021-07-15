using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{

    public static void SaveGame(string sceneName, CharacterControl control, DamageDetector damageDetector, GameObject[] enemyList)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/gamedata.data";
        
        FileStream stream = new FileStream(path, FileMode.Create);
        
        GameData data = new GameData(sceneName, control, damageDetector, enemyList);
        
        formatter.Serialize(stream, data);
        
        stream.Close();
            
    }

    public static GameData LoadGame()
    {
        string path = Application.dataPath + "/gamedata.data";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();                
            return data;
        }
        else
        {
            Debug.Log("Save file not found " + path);
            return null;
        }
    }

}
