using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
   

    public static GameObject manager_d;

    [SerializeField] GameObject playerPrefab;

    LevelLoader lvlloader;

    public int levelIndex = 0;

    public float GravityForce = 1f;

    public int ScenarioOrder = 0;



    public class PlayerStats
    {
        public float Speed = 5;

        public float MaxHealth = 100;
        public float Health = 100;
        public float HealthRegen = .5f;

        public float Stamina = 100;
        public float MaxStamina = 100;
        public float StaminaRegen = 10;

        public float Damage = 20 / 2;   
        public float DamageIncrease = 0;
        public float Resistence = 0;
        public float BetweenAttackCooldown = .6f;
        public float AttackCooldown = 2f;
        public bool AttackInProcess = false;
        public bool AttackReady = true;
        public float AttackCost = 5f;

        public bool enlightened = false;

        public float JumpSpeed = 1.5f;
        public float JumpTime = 0.8f;
        public float JumpCooldown = 1.5f;
        public float JumpCost = 20;
        public bool Jumping = false;

        public float ShieldCooldown = 8;
        public float ShieldStaminaCost = 10;

        public float Ability1Cooldown = 10f;
        public float Ability1Duration = 5f;
        public float Ability1StaminaCost = 20;

        public float Ability2Cooldown = 30f;
        public float Ability2Duration = 10f;
        public float Ability2StaminaCost = 20;
        public Vector3 Ability2normalSize = new Vector3(1,1,1);
        public Vector3 Ability2growSize = new Vector3(1.5f, 1.5f, 1.5f);
        public bool Ability2Raged = false;
        public float Ability2timeToRageTick = 0;

        public float Ability3Cooldown = 10f;
        public float Ability3StaminaCost = 20;
        public float Ability3Damage = 40; //treba zmenit na  40
        public bool Ability3trident = true;


        public float gravityIncrease = 0;
        public GameObject target;

        public bool MeduzaUnlocked = true;
        public bool MinotaurUnlocked = true;
        public bool PoseidonUnlocked = false;
        public bool ZeusUnlocked = true;




        public Vector3 LobbySpawn = new Vector3(245, 83, 80);
        public Vector3 LVL1Spawn = new Vector3(13.2343512f, 0.070555687f, -125.339912f);
        public Vector3 LVL2Spawn = new Vector3(2.5999999f, 2, -20.1000004f);
        public Vector3 LVL3Spawn = new Vector3(265, 215, 140);
        public Vector3 LVL4Spawn = new  Vector3(212, 403, 180);
        public Vector3 LVL5Spawn = new Vector3(212, 403, 180);
        public Vector3 LVL6Spawn = new Vector3(0,0,0);

        public bool absorb = false;
        public bool absorb2 = false;
    }
    public PlayerStats Player = new PlayerStats();

    public class PoseidonStats
    {
        public float Health = 500;
        public float maxHealth = 500;
        public float Damage = 20;
        public float DamageIncrease = 0;
    }

    public PoseidonStats Poseidon = new PoseidonStats();
    public class MinotaurStats
    {
        public float Health = 250;
        public float maxHealth = 250;
        public float Damage = 15;
        public float DamageIncrease = 0;
    }
    public MinotaurStats Minotaur = new MinotaurStats();

    public class HadesStats
    {
        public float Health = 750;
        public float maxHealth = 750;
        public float Damage = 20;
        public float DamageIncrease = 0;
    }

    public HadesStats Hades = new HadesStats();

    private void Awake()
    {
        Save.loadSystem();
    }

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
        lvlloader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        
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
    public void toLobby()
    {
        SceneManager.LoadScene("Lobby");

    }
     public bool skapalUz = false;

    private void Update()
    {
        if (Player.Health <= 0 && ! skapalUz)
        {
            GameObject.Find("Player").GetComponent<Player>().died = true;
            print("LOL skapal si");
            Invoke(nameof(toLobby), 5);
            skapalUz = true;
        }
        else if (GameObject.FindGameObjectsWithTag("Boss").Length != 0)
        {
            
            if (GameObject.FindGameObjectsWithTag("Boss")[0].name == "Minotaur")
            {
                if (Minotaur.Health == 0)
                {
                    print("endgame");
                    Destroy(GameObject.FindGameObjectsWithTag("Boss")[0].gameObject);
                    Player.MinotaurUnlocked = true;
                    Invoke(nameof(toLobby), 5);
                    ScenarioOrder = 2;

                }
            }
            if (GameObject.FindGameObjectsWithTag("Boss")[0].name == "Poseidon")
            {
                if (Poseidon.Health == 0)
                {
                    print("endgame");
                    Destroy(GameObject.FindGameObjectsWithTag("Boss")[0].gameObject);
                    Player.PoseidonUnlocked = true;
                    Invoke(nameof(toLobby), 5);
                    ScenarioOrder =  3;
                }
            }
        }
        
    }



}