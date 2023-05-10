using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float movementSpeed = 100f;
    private const float MIN_Y = 250;
    private const float MAX_Y = 400;

    private Transform tranform;

    void Start()
    {
        tranform = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        // Movement
        float moveForward = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        float moveRight = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
        float zoom = Input.GetKey(KeyCode.Q) ? 1 : Input.GetKey(KeyCode.E) ? -1 : 0;

        if (zoom == 0) {
            
        } else if () {
            
        } else if () {
            
        }
        
        Vector3 moveDirection = new Vector3(moveRight, zoom, moveForward).normalized;
        Vector3 newPos = tranform.position + movementSpeed * Time.deltaTime * tranform.TransformDirection(moveDirection);

        
        if (newPos.y < MIN_Y) {
            tranform.position = new Vector3(tranform.position.x, MIN_Y, tranform.position.z);
        } else if (newPos.y > MAX_Y) {
            tranform.position = new Vector3(tranform.position.x, MAX_Y, tranform.position.z);
        } else {
            tranform.position = newPos;
        }
        
    }
}
