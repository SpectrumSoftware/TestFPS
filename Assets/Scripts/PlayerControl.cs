using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Define Constants //
    public float speed = 4.0f;
    public float jumpSpeed = 9.05f;
    public float gravity = 24.0f;
    public float speedH = 3.0f;

    // Initialize Variables //
    public GameObject player;
    private Vector3 moveDirection = Vector3.zero;
    public float vspeed;
    public float hspeed;
    public float yaw = 0;

    void Start()
    {
        // Set Cursor Hidden //
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Assign Needed Components to Variables //
        CharacterController controller = GetComponent<CharacterController>();
        KeyMap keyMap = player.GetComponent<KeyMap>();

        // Show and Hide Cursor //
        if (keyMap.escape && Cursor.visible == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (keyMap.mouseLeft && Cursor.visible == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Determine Speed Based Off Buttons Pressed //
        if (keyMap.shift)
        {
            vspeed = speed / 2.2f;
        }
        else {
            vspeed = speed;
        }

        if (keyMap.horizontal != 0 && keyMap.vertical != 0)
        {
            hspeed = vspeed * Mathf.Sin(45);
        }
        else {
            hspeed = vspeed;
        }

        // Character Movement //
        if (!controller.isGrounded)
        {
            moveDirection.x = keyMap.horizontal * (hspeed * 0.8f);
            moveDirection.z = keyMap.vertical * (vspeed * 0.8f);
        }
        // If grounded, zero out y component and allow jumping
        else {
            moveDirection = new Vector3(keyMap.horizontal * hspeed, 0, keyMap.vertical * vspeed);
            if (keyMap.jump) moveDirection.y = jumpSpeed;
        }
        moveDirection = transform.TransformDirection(moveDirection);

        // Apply Gravity //
        moveDirection.y -= gravity * Time.deltaTime;

        // Adjust Y Axis Direction //
        yaw += speedH * keyMap.mouseX;
        yaw %= 360;
        transform.eulerAngles = new Vector3(0, yaw, 0);

        // Move the Player //
        controller.Move(moveDirection * Time.deltaTime);
    }
}