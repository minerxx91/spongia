using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    [SerializeField] float PlayerSpeed = 5;
    [SerializeField] float PlayerHealth = 100;

    public static GameObject manager_d;

    public class PlayerStats
    {
        public float Speed;

        public float MaxHealth = 100;
        public float Health = 100;
        public float HealthRegen = 5;

        public float Stamina = 100;
        public float MaxStamina = 100;
        public float StaminaRegen = 5;

        public float JumpSpeed = 1f;
        public float JumpTime = .8f;
        public float JumpCooldown = 1f;
        public float JumpCost = 20;
    }
    public PlayerStats Player = new PlayerStats();


    private void Start()
    {
        
        Player.Speed = PlayerSpeed;
        Player.Health = PlayerHealth;

        
        if (manager_d != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            manager_d = this.gameObject;
        }

        DontDestroyOnLoad(gameObject);
    }
    public void DamagePlayer(float damage)
    {
        if (Player.Health > damage)
        {
            Player.Health -= damage;
        }
        else
        {
            Player.Health = 0;
        }
    }
    public void SwapScene()
    {
        
        
            if(GameObject.Find("Player").scene.name == "LVL1")
            {
                SceneManager.LoadScene("Lobby");
            }
            else
            {
                SceneManager.LoadScene("LVL1");
            }
        
    }


}