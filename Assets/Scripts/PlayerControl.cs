using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Define Constants //
    public float speed = 8.0f;
    public float minSpeed = 2.0f;
    public float accel = 0.75f;
    public float jumpSpeed = 9.05f;
    public float gravity = 24.0f;

    // Initialize Variables //
    public GameObject player;
    public Camera firstPerson;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float maxSpeed;
    private float curSpeed;
    private float airSpeed;
    private bool moveInUse = false;

    void Start()
    {
        // Assign Components to Variables //
        controller = this.GetComponent<CharacterController>();

        // Set Cursor Hidden //
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Load Variables with Initial Values //
        maxSpeed = speed;
    }

    void Update()
    {
        // Show and Hide Cursor //
        if (Input.GetButton("Cancel") && Cursor.visible == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetMouseButtonDown(0) && Cursor.visible == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        MoveCharacter();
        transform.rotation = Quaternion.Euler(0.0f, firstPerson.transform.rotation.eulerAngles.y, 0);
    }

    private void MoveCharacter()
    {
        // Determine Speed Based Off Buttons Pressed //
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = speed / 2.2f;
        }
        else if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            maxSpeed = (Mathf.Sqrt(2) / 2) * speed;
        }
        else {
            maxSpeed = speed;
        }

        // Determine Speed, Factor In Acceleration //

        // Character Movement //
        if (!controller.isGrounded)
        {
            airSpeed = curSpeed;
            moveDirection.x = 0;
            moveDirection.z = airSpeed * Input.GetAxis("Vertical");

            if (Input.GetAxis("Vertical") == -1)
            {
                airSpeed -= accel;
            }
        }
        // If grounded, zero out y component and allow jumping
        else {
            if (curSpeed > maxSpeed)
            {
                curSpeed = maxSpeed;
            }

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {

                if (moveInUse == false)
                {
                    if (curSpeed < minSpeed)
                    {
                        curSpeed = minSpeed;
                    }
                    moveInUse = true;
                }
                else {
                    curSpeed += accel;
                }
            }
            else {
                curSpeed -= accel;
                moveInUse = false;
            }

            moveDirection = new Vector3(Input.GetAxis("Horizontal") * curSpeed, 0, Input.GetAxis("Vertical") * curSpeed);
            if (Input.GetButtonDown("Jump")) moveDirection.y = jumpSpeed;
        }
        moveDirection = transform.TransformDirection(moveDirection);

        // Apply Gravity //
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the Player //
        controller.Move(moveDirection * Time.deltaTime);
    }
}