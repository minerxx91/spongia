using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
   

    public static GameObject manager_d;

    [SerializeField] GameObject playerPrefab;

    

    public int levelIndex = 0;

    public float GravityForce = 1f;



    public class PlayerStats
    {
        public float Speed = 5;

        public float MaxHealth = 100;
        public float Health = 100;
        public float HealthRegen = 5;

        public float Stamina = 100;
        public float MaxStamina = 100;
        public float StaminaRegen = 10;

        public float Damage = 20;
        public float DamageIncrease = 0;

        public float JumpSpeed = 1f;
        public float JumpTime = 0.8f;
        public float JumpCooldown = 1f;
        public float JumpCost = 20;
        public bool Jumping = false;

        public float gravityIncrease = 0;




        public Vector3 LobbySpawn = new Vector3(4, 2, 2);
        public Vector3 LVL1Spawn = new Vector3(3, 2, 2);
        public Vector3 LVL2Spawn = new Vector3(6, 2, 2);
        public Vector3 LVL3Spawn = new Vector3(3,2, 2);
        public Vector3 LVL4Spawn = new Vector3(3, 2, 2);
        public Vector3 LVL5Spawn = new Vector3(3, 2, 2);

    }
    public PlayerStats Player = new PlayerStats();


    private void Start()
    {

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

        
        
        if (levelIndex < 5)
        {
            levelIndex++;
            SceneManager.LoadScene(levelIndex);

           

            
           
            
        }
        else
        {
            levelIndex = 0;
            SceneManager.LoadScene(levelIndex);
        }
        
        
    }
    


}