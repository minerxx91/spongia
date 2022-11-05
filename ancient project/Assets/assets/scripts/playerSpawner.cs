using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    manager managerVariables;
    private void Awake()
    {
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();

        switch (this.gameObject.scene.buildIndex)
            {
                case 0:
                    Instantiate(player, managerVariables.Player.LobbySpawn, Quaternion.identity).transform.name = "Player";

                return;
                case 1:
                    Instantiate(player, managerVariables.Player.LVL1Spawn, Quaternion.identity).transform.name = "Player";
                return;
                case 2:
                    Instantiate(player, managerVariables.Player.LVL2Spawn, Quaternion.identity).transform.name = "Player";
                return;
                case 3:
                    Instantiate(player, managerVariables.Player.LVL3Spawn, Quaternion.identity).transform.name = "Player";
                return;
                case 4:
                    Instantiate(player, managerVariables.Player.LVL4Spawn, Quaternion.identity).transform.name = "Player";
                return;
                case 5:
                    Instantiate(player, managerVariables.Player.LVL5Spawn, Quaternion.identity).transform.name = "Player";
                return;
                case 6:
                    Instantiate(player, managerVariables.Player.LVL6Spawn, Quaternion.identity).transform.name = "Player";
                return;
        }
    }
}
