using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveProgress
{
    public static void saveSystem(Progress progress)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.rit";
        FileStream streamProgress = new FileStream(path, FileMode.Create);

        ProgressData dataProgress = new ProgressData(progress);

        formatter.Serialize(streamProgress, dataProgress);
        streamProgress.Close();
    }

    public static ProgressData loadSystem()
    {
        string path = Application.persistentDataPath + "/progress.rit";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream streamProgress = new FileStream(path, FileMode.Open);

            ProgressData dataProgress = formatter.Deserialize(streamProgress) as ProgressData;
            streamProgress.Close();

            return dataProgress;
        }
        else
        {
            Progress progress = new Progress();
            BinaryFormatter formatter = new BinaryFormatter();
            path = Application.persistentDataPath + "/progress.rit";
            FileStream streamProgress = new FileStream(path, FileMode.Create);

            ProgressData dataProgress = new ProgressData(progress);

            formatter.Serialize(streamProgress, dataProgress);
            streamProgress.Close();
            return null;
        }
    }
}
