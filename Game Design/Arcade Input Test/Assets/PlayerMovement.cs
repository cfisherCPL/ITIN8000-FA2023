using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed;

    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime);

        //jumping logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        //button testing to log
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Debug.Log("Button 0 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Debug.Log("Button 1 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Debug.Log("Button 2 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Debug.Log("Button 3 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            Debug.Log("Button 4 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Debug.Log("Button 5 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            Debug.Log("Button 6 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Debug.Log("Button 6 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button8))
        {
            Debug.Log("Button 8 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            Debug.Log("Button 9 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button10))
        {
            Debug.Log("Button 10 Pressed");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button11))
        {
            Debug.Log("Button 11 Pressed");
        }
    }
}
