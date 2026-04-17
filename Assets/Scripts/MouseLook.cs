using UnityEngine;
public class MouseLook : MonoBehaviour
{
    public float sensitivity = 200f;
    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        // Only run mouse look during actual gameplay
        if (Time.timeScale == 0f) return;  // 

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}