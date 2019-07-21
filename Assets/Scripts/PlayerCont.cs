using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCont : MonoBehaviour
{
    //Mouse look
    private Transform cam;
    private Transform gui;
    private float yRotation, xRotation, currentXRotation, currentYRotation, yRotationV, xRotationV;
    public float lookSensitivity = 5;
    public float lookSmoothnes = 0.1f;
    public float bottom = 60F;
    public float top = -60f;

    //Movement
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    //Timers
    public float fireTime = 1.0f;
    private float fireTimeTemp = 0.0f;
    public GameObject bullet;
    public float bulletSpeed = 10;

    //UI
    private string menuOpen = "game";

    

    // Use this for initialization
    void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        //hitscan infront to pop up item names etc

        //Control handlers
        if (menuOpen == "game")
        {
            Mouselook();
        }
        Movement();
        Timers();

        if (Input.GetKeyDown(KeyCode.E) && menuOpen == "game")
        {
            Interact();
        }


        if (Input.GetMouseButtonDown(0) && menuOpen == "game")
        {
            Attack();
        }

    }

   
    void CursorSwap()
    {
        Debug.Log("Cursor lock mode swapped.");
        if (Cursor.lockState == CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    void Movement()
    {
        //Movement
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Mouselook()
    {
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 100);
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothnes);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothnes);
        if (currentXRotation > bottom)
        {
            currentXRotation = bottom;
            xRotation = bottom;
        }

        if (currentXRotation < top)
        {
            currentXRotation = top;
            xRotation = top;
        }

        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
        cam.transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
    }

    void Timers()
    {
        fireTimeTemp -= Time.deltaTime;
    }

    void Interact()
    {
        RaycastHit useRange = new RaycastHit();
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out useRange, 5.0f))
        {
            Debug.DrawLine(cam.transform.position, useRange.transform.position, Color.cyan, 10f);
            useRange.transform.SendMessage(("Interact"), gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    void Attack()
    {
        if (fireTimeTemp <= 0)
        {
            //if(player??) 
            GameObject firedBullet = Instantiate(bullet, cam.transform.position + cam.transform.forward * 1, cam.transform.rotation);
            Rigidbody bulletRigidbody = firedBullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = transform.forward * bulletSpeed;
            fireTimeTemp = fireTime;
            Destroy(firedBullet.gameObject,2.0f);
        }
    }
}
