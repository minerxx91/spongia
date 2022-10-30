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
        public float HealthRegen = .5f;

        public float Stamina = 100;
        public float MaxStamina = 100;
        public float StaminaRegen = 10;

        public float Damage = 20 / 2;//   / 2 lebo pri attacku sa to jebne 2 krat
        public float DamageIncrease = 0;
        public float Resistence = 0;
        public float BetweenAttackCooldown = .4f;
        public float AttackCooldown = 1f;
        public bool AttackInProcess = false;
        public bool AttackReady = true;

        public float JumpSpeed = 1.5f;
        public float JumpTime = 0.8f;
        public float JumpCooldown = 1.5f;
        public float JumpCost = 20;
        public bool Jumping = false;

        public float ShieldCooldown = 8;

        public float StunCooldown = 10f;
        public float StunDuration = 5f;

        public float gravityIncrease = 0;
        public GameObject target;




        public Vector3 LobbySpawn = new Vector3(4, 2, 2);
        public Vector3 LVL1Spawn = new Vector3(-18.1394997f, -1.90734863e-06f, 3.03419876f);
        public Vector3 LVL2Spawn = new Vector3(6, 2, 2);
        public Vector3 LVL3Spawn = new Vector3(51, 6, 65);
        public Vector3 LVL4Spawn = new  Vector3(50, 5, 50);
        public Vector3 LVL5Spawn = new Vector3(3, 2, 2);

    }
    public PlayerStats Player = new PlayerStats();

    public class PoseidonStats
    {
        public float Health = 1000;
        public float maxHealth = 1000;
        public float Damage = 20;
        public float DamageIncrease = 0;
    }

    public PoseidonStats Poseidon = new PoseidonStats();

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

        
      
            SceneManager.LoadScene(levelIndex);

           

            
           
            
        
        
        
    }
    


}