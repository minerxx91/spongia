using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{

    public Vector3 MousePosition;
    [SerializeField] LayerMask layerMask;

    private void Update()
    {

        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            
            MousePosition = raycastHit.point;
            
        }
        
    }
}
