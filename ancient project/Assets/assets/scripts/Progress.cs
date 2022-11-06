using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Progress : MonoBehaviour
{
    public int level = 1;
    public float difficulty = 1f;

    public void saveData()
    {
        SaveProgress.saveSystem(this);
        print("saved");
    }

    public void loadData()
    {
        ProgressData dataProgress = SaveProgress.loadSystem();
    }
}
