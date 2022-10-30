using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static GameObject Audiomanager_d;

    [SerializeField] AudioClip PlayerAttack1;
    [SerializeField] AudioClip PlayerAttack2;
    [SerializeField] AudioClip PlayerAttack3;
    [SerializeField] AudioClip PlayerShield;

    [SerializeField] AudioClip PlayerRoll;

    [SerializeField] AudioClip PortalEnter;

    [SerializeField] AudioClip EnemyDamageIncome1;
    [SerializeField] AudioClip EnemyDamageIncome2;
    [SerializeField] AudioClip EnemyDamageIncome3;

    [SerializeField] AudioClip PoseidonMelee;

    AudioSource run;


    AudioSource[] AS;
    private void Start()
    {
        AS = GetComponents<AudioSource>();

        run = AS[1];

        if (Audiomanager_d != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Audiomanager_d = this.gameObject;
        }

        DontDestroyOnLoad(gameObject);
    }   
    public void PlayRun()
    {
        if(!run.isPlaying)
        {
            run.Play();
        }
        
    }
    public void StopRun()
    {
        run.Stop();
    }
    public void PlayPlayerAttack()
    {
        int random = Random.Range(1, 2);
        if(random == 1)
        {
            AS[0].PlayOneShot(PlayerAttack2);
        }
        if(random == 2)
        {
            AS[0].PlayOneShot(PlayerAttack3);
        }
     

    }
    public void PlayPlayerAttackS()
    {
        
            AS[0].PlayOneShot(PlayerAttack1);
        

    }
    public void PlayPlayerRoll()
    {

        AS[0].PlayOneShot(PlayerRoll);


    }
    public void PlayPortalEnter()
    {

        AS[0].PlayOneShot(PortalEnter);


    }
    public void PlayEnemyDamageIncome()
    {
        int random = Random.Range(1, 3);
        if (random == 1)
        {
            AS[0].PlayOneShot(EnemyDamageIncome1);
        }
        if (random == 2)
        {
            AS[0].PlayOneShot(EnemyDamageIncome2);
        }
        if (random == 3)
        {
            AS[0].PlayOneShot(EnemyDamageIncome3);
        }
        


    }
    public void PlayPoseidonMelee()
    {

        AS[0].PlayOneShot(PoseidonMelee);


    }
    public void PlayPlayerShield()
    {
        AS[0].PlayOneShot(PlayerShield);
    }
}
