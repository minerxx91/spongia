using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    

    public AudioSource run;


    AudioSource[] AS;
    private void Start()
    {
        AS = GetComponents<AudioSource>();

        run = AS[1];
    }   
    public void PlayRun()
    {
        AS[1].Play();
    }

}
