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

        public bool MeduzaUnlocked = true;
        public bool MinotaurUnlocked = true;
        public bool PoseidonUnlocked = true;
        public bool ZeusUnlocked = true;




        public Vector3 LobbySpawn = new Vector3(245, 83, 80);
        public Vector3 LVL1Spawn = new Vector3(-18.1394997f, -1.90734863e-06f, 3.03419876f);
        public Vector3 LVL2Spawn = new Vector3(2.5999999f, 2, -20.1000004f);
        public Vector3 LVL3Spawn = new Vector3(265, 215, 140);
        public Vector3 LVL4Spawn = new  Vector3(50, 5, 50);
        public Vector3 LVL5Spawn = new Vector3(212, 403, 180);
        public Vector3 LVL6Spawn = new Vector3(212, 403, 180);

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
    public class MinotaurStats
    {
        public float Health = 750;
        public float maxHealth = 750;
        public float Damage = 15;
        public float DamageIncrease = 0;
    }

    public MinotaurStats Minotaur = new MinotaurStats();
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
        DynamicGI.UpdateEnvironment();
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