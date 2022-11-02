using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trident : MonoBehaviour
{
    float time =0;
    float velocity = 36;
    private void Start()
    {
        transform.LookAt(GameObject.Find("Player").transform.position + new Vector3(0,.5f,0));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * velocity;


        time += Time.deltaTime;
        if(time > 5)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
    }
}
