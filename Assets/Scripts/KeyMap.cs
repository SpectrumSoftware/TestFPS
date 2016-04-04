using UnityEngine;

public class KeyMap : MonoBehaviour
{
    // Initialize Player Keys //
    public float mouseX;
    public float mouseY;
    public bool mouseLeft;
    public float scroll;

    public float horizontal;
    public float vertical;
    public bool jump;
    public bool escape;
    public bool shift;

    void Update()
    {
        // Mouse //
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        mouseLeft = Input.GetMouseButtonDown(0);
        scroll = Input.GetAxis("Mouse ScrollWheel");

        // Keyboard //
        horizontal = Input.GetAxis("Horizontal");    // Left & Right Arrow Keys
        vertical = Input.GetAxis("Vertical");        // Up & Down Arrow Keys
        jump = Input.GetButtonDown("Jump");
        escape = Input.GetButton("Cancel");
        shift = Input.GetKey(KeyCode.LeftShift);
    }
}