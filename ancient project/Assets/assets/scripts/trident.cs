using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trident : MonoBehaviour
{
    float time =0;
    private void Start()
    {

    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * 10;


        time += Time.deltaTime;
        if(time > 5)
        {
            Destroy(gameObject);
        }
    }
}
