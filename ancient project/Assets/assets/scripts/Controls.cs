using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Controls : MonoBehaviour
{
    //player = controls
    public KeyCode MoveUp = KeyCode.W;
    public KeyCode MoveDown = KeyCode.S;
    public KeyCode MoveRight = KeyCode.D;
    public KeyCode MoveLeft = KeyCode.A;

    public KeyCode Block = KeyCode.LeftShift;
    public KeyCode Attack = KeyCode.Mouse0;
    public KeyCode LockTarget = KeyCode.Mouse1;

    public KeyCode Jump = KeyCode.Space;

    public KeyCode Interact = KeyCode.E;

    public KeyCode ability1 = KeyCode.Alpha1;
    public KeyCode ability2 = KeyCode.Alpha2;
    public KeyCode ability3 = KeyCode.Alpha3;
    public KeyCode ability4 = KeyCode.Alpha4;

    public void saveData()
    {
        Save.saveSystem(this);
    }

    public void loadData()
    {
        SettingsData data = Save.loadSystem();
    }
}
