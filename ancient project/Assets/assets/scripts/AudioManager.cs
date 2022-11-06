using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float GlobalVolume = 1;
    public float MusicVolume = 1;
    public static GameObject Audiomanager_d;

    [SerializeField] AudioClip PlayerAttack1;
    [SerializeField] AudioClip PlayerAttack2;
    [SerializeField] AudioClip PlayerAttack3;
    [SerializeField] AudioClip PlayerShield;

    [SerializeField] AudioClip PlayerRoll;
    [SerializeField] AudioClip Ability1;



    [SerializeField] AudioClip PortalEnter;

    [SerializeField] List<AudioClip> Music = new List<AudioClip>();

    [SerializeField] List<AudioClip> HadesDialogs = new List<AudioClip>();
    [SerializeField] AudioClip HadesLaugh;
    [SerializeField] List<AudioClip> KingDialogs = new List<AudioClip>();
    [SerializeField] AudioClip KingLaugh;
    [SerializeField] List<AudioClip> PlayerDialogs = new List<AudioClip>();


    

    [SerializeField] List<AudioClip> MinotaurAttacks = new List<AudioClip>();
    [SerializeField] List<AudioClip> MinotaurRageAttacks = new List<AudioClip>();
    [SerializeField] List<AudioClip> MinotaurRandom = new List<AudioClip>();
    [SerializeField] AudioClip MinotaurChrcanie;
    public AudioClip Dychanie;
    public AudioClip RageDychanie;
    [SerializeField] AudioClip grow1;
    [SerializeField] AudioClip grow2;
    [SerializeField] AudioClip MinotaurHit;

    [SerializeField] List<AudioClip> PoseidonRandom = new List<AudioClip>();






    [SerializeField] AudioClip PoseidonMelee;

    AudioSource run;
    AudioSource MusicList;

    AudioSource[] AS;

    
    private void Start()
    {


        if (Audiomanager_d != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Audiomanager_d = this.gameObject;
        }

        DontDestroyOnLoad(gameObject);


        AS = GetComponents<AudioSource>();

        


        run = AS[1];
        MusicList = AS[3];
        print(AS[3]);
        
    }
    private void Update()
    {
        GameObject.Find("Manager").GetComponent<Controls>().music = MusicVolume;

        if(this.gameObject.scene.name == "Menu")
        {
            if (!MusicList.isPlaying)
            {
                PlayMusic(0);
            }

        }
        for (int i = 0; i < AS.Length; i++)
        {
            AS[i].volume = GlobalVolume;
        }
        MusicList.volume *= MusicVolume;
    }
    public void PlayRun()
    {
        if(!run.isPlaying)
        {
            run.Play();
        }
        
    }
    public void PlayLow()
    {
        if (!AS[2].isPlaying)
        {
            AS[2].Play();
        }

    }
    public void StopRun()
    {
        run.Stop();
    }
    public void StopLow()
    {
        AS[2].Stop();
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

    public void PlayPoseidonMelee()
    {

        AS[0].PlayOneShot(PoseidonMelee);


    }
    public void PlayPlayerShield()
    {
        AS[0].PlayOneShot(PlayerShield);
    }
    private void PlayMinotaurAttack()
    {
        AS[0].PlayOneShot(MinotaurAttacks[Random.Range(0, MinotaurAttacks.Count)]);
    }
    public void PlayMinotaurAttackDelay()
    {

        Invoke(nameof(PlayMinotaurAttack), 0.5f);
    }
    private void PlayMinotaurRageAttack()
    {
        AS[0].PlayOneShot(MinotaurRageAttacks[Random.Range(0, MinotaurRageAttacks.Count)]);
    }
    public void PlayMinotaurAttackRageDelay()
    {

        Invoke(nameof(PlayMinotaurRageAttack), 0.5f);
    }
    public void PlayMinotaurRandom()
    {
        AS[0].PlayOneShot(MinotaurRandom[Random.Range(0, MinotaurRandom.Count)]);
    }
    public void PlayMinotaurChrcanie()
    {
        AS[0].PlayOneShot(MinotaurChrcanie);
    }
    public void PlayMinotaurHit()
    {
        AS[0].PlayOneShot(MinotaurHit);
    }
    public void PlayMinotaurGrow1()
    {
        AS[0].PlayOneShot(grow1);
    }
    public void PlayMinotaurGrow2()
    {
        AS[0].PlayOneShot(grow2);
    }
    public void PlayKingDialog()
    {
        AS[0].PlayOneShot(KingDialogs[Random.Range(0, KingDialogs.Count)]);
    }
    public void PlayPlayerDialog()
    {
        AS[0].PlayOneShot(PlayerDialogs[Random.Range(0, PlayerDialogs.Count)]);
    }
    public void PlayHadesDialog()
    {
        AS[0].PlayOneShot(HadesDialogs[Random.Range(0, HadesDialogs.Count)]);
    }
    public void PlayAbility1()
    {
        AS[0].PlayOneShot(Ability1);

    }
    public void PlayPoseidonRandom()
    {
        AS[0].PlayOneShot(PoseidonRandom[Random.Range(0, PoseidonRandom.Count)]);
    }
    public void PlayMusic(int order)
    {
        MusicList.clip = Music[order];
        MusicList.Play();
    }

}
