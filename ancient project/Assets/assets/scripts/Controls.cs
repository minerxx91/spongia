using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.EventSystems;

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

    public List<KeyCode> binds = new List<KeyCode>(13);
    bool free = true;
    Menu menu = new Menu();
    [SerializeField] GameObject bind;
    [SerializeField] GameObject error;
    GameObject button;
    [SerializeField] GameObject[] buttons;
    private void Awake()
    {

        binds.Add(MoveUp);
        binds.Add(MoveDown);
        binds.Add(MoveRight);
        binds.Add(MoveLeft);
        binds.Add(Block);
        binds.Add(Attack);
        binds.Add(LockTarget);
        binds.Add(Jump);
        binds.Add(Interact);
        binds.Add(ability1);
        binds.Add(ability2);
        binds.Add(ability3);
        binds.Add(ability4);
        if (this.gameObject.scene.name == "Menu")
        {
            bind.SetActive(true);
            error.SetActive(true);
        }
        
    }

    private void Start()
    {
        if (this.gameObject.scene.name == "Menu")
        {
            bind.SetActive(false);
            error.SetActive(false);
        }
          
    }

    public void updateList()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<TMPro.TextMeshProUGUI>().text = binds[i].ToString();
        }
    }

    public void saveData()
    {
        Save.saveSystem(this);
        print("saved");
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
        print("loaded");

        binds[0] = data.MoveUp;
        binds[1] = data.MoveDown;
        binds[2] = data.MoveLeft;
        binds[3] = data.MoveRight;
        binds[5] = data.Block;
        binds[6] = data.Attack;
        binds[7] = data.LockTarget;
        binds[8] = data.Jump;
        binds[9] = data.ability1;
        binds[10] = data.ability2;
        binds[11] = data.ability3;
        binds[12] = data.ability4;
    }
    void bindscreenON()
    {
        bind.SetActive(true);
    }

    void bindscreenOFF()
    {
        bind.SetActive(false);
    }

    void bindErrorON()
    {
        error.SetActive(true);
    }

    void bindErrorOFF()
    {
        error.SetActive(false);
    }
    public void moveUpBind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindMoveUp());
    }

    IEnumerator bindMoveUp()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[0] = MoveUp;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if(Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count-1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[0] = kcode;
                        MoveUp = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void moveDownBind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindMoveDown());
    }

    IEnumerator bindMoveDown()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[1] = MoveDown;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[1] = kcode;
                        MoveDown = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }
    public void moveRightBind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindMoveRight());
    }

    IEnumerator bindMoveRight()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[2] = MoveRight;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[2] = kcode;
                        MoveRight = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void moveLeftBind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindMoveLeft());
    }

    IEnumerator bindMoveLeft()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[3] = MoveLeft;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[3] = kcode;
                        MoveLeft = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void blockBind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindBlock());
    }

    IEnumerator bindBlock()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[4] = Block;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[4] = kcode;
                        Block = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void attackBind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindAttack());
    }

    IEnumerator bindAttack()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[5] = Attack;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[5] = kcode;
                        Attack = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void lockTargetBind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindLockTarget());
    }

    IEnumerator bindLockTarget()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[6] = LockTarget;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[6] = kcode;
                        Attack = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void jumpBind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindJump());
    }

    IEnumerator bindJump()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[7] = Jump;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[7] = kcode;
                        Jump = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void interactBind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindInteract());
    }

    IEnumerator bindInteract()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[8] = Interact;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[8] = kcode;
                        Interact = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void ability1Bind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindability1());
    }

    IEnumerator bindability1()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[9] = ability1;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[9] = kcode;
                        ability1 = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void ability2Bind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindAbility2());
    }

    IEnumerator bindAbility2()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[10] = ability2;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[10] = kcode;
                        ability2 = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void ability3Bind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindAbility3());
    }

    IEnumerator bindAbility3()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[11] = ability3;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[11] = kcode;
                        ability3 = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    public void ability4Bind()
    {
        button = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(bindAbility4());
    }

    IEnumerator bindAbility4()
    {
        bindscreenON();
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            binds[12] = ability4;
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bindErrorOFF();
                    bindscreenOFF();
                    yield break;
                }
                else if (Input.GetKey(kcode))
                {
                    for (int i = 0; i < binds.Count; i++)
                    {
                        if (binds[i] == kcode)
                        {
                            print("pouzite");
                            free = false;
                            bindErrorON();
                            break;
                        }
                        else if (i == binds.Count - 1 && kcode != binds[i])
                        {
                            free = true;
                        }
                    }

                    if (free)
                    {
                        print("free");
                        binds[12] = kcode;
                        ability4 = kcode;
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = kcode.ToString();
                        bindErrorOFF();
                        bindscreenOFF();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }
}
