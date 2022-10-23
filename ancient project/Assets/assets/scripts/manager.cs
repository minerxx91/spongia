using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    [SerializeField] float PlayerSpeed = 5;
    [SerializeField] float PlayerHealth = 100;

    public class PlayerStats
    {
        public float Speed;

        public float MaxHealth = 100;
        public float Health = 100;
        public float HealthRegen = 5;

        public float Stamina = 100;
        public float MaxStamina = 100;
        public float StaminaRegen = 5;
    }
    public PlayerStats Player = new PlayerStats();


    private void Start()
    {
        Player.Speed = PlayerSpeed;
        Player.Health = PlayerHealth;
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


}