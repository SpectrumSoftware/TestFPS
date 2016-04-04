using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Define Constants //
    public float speedV = 3.0f;

    // Initialize Variables //
    public Camera firstPerson;
    public Camera thirdPerson;
    public GameObject player;
    public PlayerControl playerControl;
    public KeyMap keyMap;
    private Vector3 offset;
    private float pitch = 0;

    void Start()
    {
        // Set Camera Defaults //
        firstPerson.enabled = true;
        thirdPerson.enabled = false;
        firstPerson.GetComponent<AudioListener>().enabled = firstPerson.GetComponent<AudioListener>().enabled;
        thirdPerson.GetComponent<AudioListener>().enabled = !thirdPerson.GetComponent<AudioListener>().enabled;
    }

    void Update()
    {
        // Assign Needed Objects & Components to Variables //
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        keyMap = player.GetComponent<KeyMap>();

        pitch -= speedV * keyMap.mouseY;
        pitch %= 360;
    }

    void LateUpdate()
    {
        if (pitch > 89) pitch = 89;
        else if (pitch < -89) pitch = -89;

        if (keyMap.scroll / Mathf.Abs(keyMap.scroll) == 1)
        {
            thirdPerson.enabled = false;
            thirdPerson.GetComponent<AudioListener>().enabled = !thirdPerson.GetComponent<AudioListener>().enabled;
            firstPerson.enabled = true;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            pitch = 0;

        }
        else if (keyMap.scroll / Mathf.Abs(keyMap.scroll) == -1)
        {
            firstPerson.enabled = false;
            firstPerson.GetComponent<AudioListener>().enabled = !firstPerson.GetComponent<AudioListener>().enabled;
            thirdPerson.enabled = true;
        }

        if (firstPerson.enabled)
        {
            transform.eulerAngles = new Vector3(pitch, playerControl.yaw, 0);
        }
        else if (thirdPerson.enabled)
        {
            transform.eulerAngles = new Vector3(17, playerControl.yaw, 0);
        }


    }
}