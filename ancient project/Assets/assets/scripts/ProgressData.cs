using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressData
{
    public int level;
    public float difficulty;

    public ProgressData(Progress progress)
    {
        level = progress.level;
        difficulty = progress.difficulty;
    }
}
