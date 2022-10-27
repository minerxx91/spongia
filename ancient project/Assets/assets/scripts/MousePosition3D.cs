    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MousePosition3D : MonoBehaviour
    {

        public Vector3 MousePosition;
        [SerializeField] LayerMask layerMask;

        manager managerVariables;


        private void Start()
        {
            managerVariables = GameObject.Find("Manager").GetComponent<manager>();
        }
        private void Update()
        {

            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                MousePosition = raycastHit.point;

                managerVariables.Player.target = raycastHit.collider.gameObject;
                if(raycastHit.collider.gameObject.name == "zem")
                {
                    managerVariables.Player.target = null;
                }
                }
                


            }
        
        }
    }
