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

    public float volume;
    public bool music;
    public bool soundEffects;

    public bool postProcessing;
    public bool effects;
    public bool motionBlur;
    public float fov;

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

        volume = controls.volume;
        music = controls.music;
        soundEffects = controls.soundEffects;

        postProcessing = controls.postProcessing;
        effects = controls.effects;
        motionBlur = controls.motionBlur;
        fov = controls.fov;
}
}
