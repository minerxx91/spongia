
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset;
    [SerializeField] float yOffset = 2;
        
    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void LateUpdate()
    {

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime * 5);
        transform.position = smoothedPosition;

        transform.LookAt(target.position + new Vector3(0, yOffset, 0));


    }
}
