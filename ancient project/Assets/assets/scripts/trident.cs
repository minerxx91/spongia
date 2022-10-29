using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trident : MonoBehaviour
{
    float time =0;
    private void Start()
    {
        transform.LookAt(GameObject.Find("Player").transform.position + new Vector3(0,0,0));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * 18;


        time += Time.deltaTime;
        if(time > 5)
        {
            Destroy(gameObject);
        }
    }
}
