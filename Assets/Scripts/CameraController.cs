using UnityEngine;

using UnityEngine;

using UnityEngine;

public class CameraController : MonoBehaviour 
{
    private float movementSpeed = 100f;
    private const float MIN_Y = 250;
    private const float MAX_Y = 400;
    private Transform transform;

    void Start() 
    {
        transform = GameObject.Find("Main Camera").transform;
    }

    void Update() 
    {
        // Movement
        float moveRight = Input.GetKey(KeyCode.A) ? 1 : Input.GetKey(KeyCode.D) ? -1 : 0;
        float moveUp = Input.GetKey(KeyCode.Q) ? 1 : Input.GetKey(KeyCode.E) ? -1 : 0;
        float moveForward = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;

        Vector3 moveDirection = new Vector3(moveRight, 0, moveForward).normalized;
        Vector3 newPos = transform.position + movementSpeed * Time.deltaTime * moveDirection;

        // Camera local forward and backward movement
        newPos += transform.forward * movementSpeed * Time.deltaTime * moveForward;

        if (newPos.y < MIN_Y) 
        {
            transform.position = new Vector3(transform.position.x, MIN_Y, transform.position.z);
        } 
        else if (newPos.y > MAX_Y) 
        {
            transform.position = new Vector3(transform.position.x, MAX_Y, transform.position.z);
        } 
        else 
        {
            transform.position = newPos;
        }
    }
}


/*
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

        Vector3 moveDirection = new Vector3(moveRight, zoom, moveForward).normalized;
        Vector3 newPos = transform.position;

        if (moveForward != 0 || moveRight != 0 || zoom != 0) 
        {
            newPos = tranform.position + movementSpeed * Time.deltaTime * tranform.TransformDirection(moveDirection);
        }

        if (newPos.y < MIN_Y) 
        {
            tranform.position = new Vector3(tranform.position.x, MIN_Y, tranform.position.z);
        } 
        else if (newPos.y > MAX_Y) 
        {
            tranform.position = new Vector3(tranform.position.x, MAX_Y, tranform.position.z);
        } 
        else 
        {
            tranform.position = newPos;
        }
    }
}


/*
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
        float moveForward = Input.GetKey(KeyCode.S) ? 1 : Input.GetKey(KeyCode.W) ? -1 : 0;
        float moveRight = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
        float zoom = Input.GetKey(KeyCode.Q) ? 1 : Input.GetKey(KeyCode.E) ? -1 : 0;

        if (moveForward == 1) {
            var p = 2;
        }

        Vector3 moveDirection = new Vector3(moveRight, zoom, moveForward).normalized;
        Vector3 newPos = transform.position;
        if (zoom != 0) {
            newPos = tranform.position + movementSpeed * Time.deltaTime * tranform.TransformDirection(moveDirection);
        } else if (moveRight != 0) {
            newPos = tranform.position + movementSpeed * Time.deltaTime * tranform.TransformDirection(moveDirection);
        } else if (moveForward != 0) {
            newPos = tranform.position + movementSpeed * Time.deltaTime * moveDirection;
        }
        
        

        
        if (newPos.y < MIN_Y) {
            tranform.position = new Vector3(tranform.position.x, MIN_Y, tranform.position.z);
        } else if (newPos.y > MAX_Y) {
            tranform.position = new Vector3(tranform.position.x, MAX_Y, tranform.position.z);
        } else {
            tranform.position = newPos;
        }
        
    }
}
*/
