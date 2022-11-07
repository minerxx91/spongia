using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{

    public int portalIndex;

    bool avaiable = false;
    AudioManager audioManager;
    manager managerVariables;
    LevelLoader lvlloader;
    [SerializeField] Material NotAvaiable;
    [SerializeField] Material Avaiable;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
        lvlloader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }
    void Update()
    {
        if (managerVariables.ButtonAvaiable == portalIndex)
        {
            avaiable = true;
            this.gameObject.GetComponent<Renderer>().material = Avaiable;
            print("pasuje");
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material = NotAvaiable;
            avaiable = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            if (avaiable)
            {
                if (Input.GetKey(GameObject.Find("Manager").GetComponent<Controls>().Interact))
                {
                    audioManager.PlayPortalEnter();
                    managerVariables.levelIndex = portalIndex + 1;
                    lvlloader.SwitchScene();
                }
            }

           
        }



    }
}
