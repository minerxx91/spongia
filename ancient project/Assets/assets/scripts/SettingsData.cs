using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//playerdata = settings
public class SettingsData
{
    public KeyCode MoveUp;
    public KeyCode MoveDown;
    public KeyCode MoveRight;
    public KeyCode MoveLeft;

    public KeyCode Block;
    public KeyCode Attack;
    public KeyCode LockTarget;

    public KeyCode Jump;

    public KeyCode Interact;

    public KeyCode ability1;
    public KeyCode ability2;
    public KeyCode ability3;
    public KeyCode ability4;

    public SettingsData(Controls controls)
    {
        MoveUp = controls.MoveUp;
        MoveDown = controls.MoveDown;
        MoveRight = controls.MoveRight;
        MoveLeft = controls.MoveLeft;

        Block = controls.Block;
        Attack = controls.Attack;
        LockTarget = controls.LockTarget;

        Jump = controls.Jump;

        Interact = controls.Interact;

        ability1 = controls.ability1;
        ability2 = controls.ability2;
        ability3 = controls.ability3;
        ability4 = controls.ability4;
    }
}
