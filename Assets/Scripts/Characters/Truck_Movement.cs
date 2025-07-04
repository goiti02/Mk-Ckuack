using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_Movement : MonoBehaviour
{
    [Header("Player Settings")]
    public float movementSpeed = 5f;
    public float mouseSensibility;
    public bool moving;
    public float startspeed =5f;

    public Transform cameraTransform;

    [Header("Camera Settings")]
    public float cameraHeight;
    public float cameraDistance;
    public float cameraSmoothSpeed;

    private float cameraPitch;//Control vertical de inclinación


    // Start is called before the first frame update
    void Start()
    {
        cursorSetup();
        moving = false;
        movementSpeed = startspeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementWASD();
        CameraFollow();
        
    }
    private void LateUpdate()
    {
        CameraSetUp();
    }
    void cursorSetup()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void PlayerMovementWASD()
    {
        float moveX = 0f;
        float moveZ = 0f;
        if (!moving)
        {
            movementSpeed = startspeed;
        }


        //Asignación de teclas de movimiento MAL
        if (Input.GetKey(KeyCode.W))
        {
            moveZ = +1f;
            moving = true;
        }
        else if (Input.GetKeyUp(KeyCode.W)) moving = false;
        if (Input.GetKey(KeyCode.S))
        {
            moveZ = -1f;
            moving = true;
        }
        else if (Input.GetKeyUp(KeyCode.S)) moving = false;
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
            moving = true;
        }
        else if (Input.GetKeyUp(KeyCode.D)) moving = false;
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            moving = true;
        }
        else if (Input.GetKeyUp(KeyCode.A)) { moving = false; }


        //Crear vectores de movimiento
        Vector3 directionMovement = (transform.right * moveX + transform.forward * moveZ).normalized;

        //Crear fuerza de movimiento
        transform.position += directionMovement * movementSpeed * Time.deltaTime;
        if (moving)
        {
            movementSpeed += 1f * Time.deltaTime;
        }
    }

       public void CameraFollow()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);


            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);

            transform.Rotate(Vector3.up * mouseX);
        }
        public void CameraSetUp()
        {
            Vector3 newPos = transform.position - transform.forward * cameraDistance + Vector3.up * cameraDistance;

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, newPos, Time.deltaTime * cameraSmoothSpeed);

            cameraTransform.LookAt(transform.position + Vector3.up * cameraHeight * 0.5f);
        }

}

