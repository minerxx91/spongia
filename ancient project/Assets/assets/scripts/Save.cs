using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Save
{
    //savesystem = save
    public static void saveSystem(Controls controls)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.riù";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(controls);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingsData loadSystem()
    {
        string path = Application.persistentDataPath + "/data.riù";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

}
