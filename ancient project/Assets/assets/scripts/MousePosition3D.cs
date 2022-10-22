using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{

    public Vector3 MousePosition;

    private void Update()
    {

        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            
            MousePosition = raycastHit.point;
            
        }
        
    }
}
