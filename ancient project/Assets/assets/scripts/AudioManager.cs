using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip PlayerAttack;

     AudioSource run;


    AudioSource[] AS;
    private void Start()
    {
        AS = GetComponents<AudioSource>();

        run = AS[1];
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
        AS[0].PlayOneShot(PlayerAttack);
    }

}
