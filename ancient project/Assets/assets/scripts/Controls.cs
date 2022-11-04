using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

        MoveUp = data.MoveUp;
        MoveDown = data.MoveDown;
        MoveRight = data.MoveRight;
        MoveLeft = data.MoveLeft;

        Block = data.Block;
        Attack = data.Attack;
        LockTarget = data.LockTarget;

        Jump = data.Jump;

        Interact = data.Interact;

        ability1 = data.ability1;
        ability2 = data.ability2;
        ability3 = data.ability3;
        ability4 = data.ability4;
    }

    private void waitForInput()
    {
        StartCoroutine(bindMoveUp());
    }
    IEnumerator bindMoveUp()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    this.MoveUp = kcode;
                    yield break;
                }
            }
            yield return null;
        }
    }
}
