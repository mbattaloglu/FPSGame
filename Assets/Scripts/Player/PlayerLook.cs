using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;

    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //Calculate rotation
        xRotation -= mouseY * ySensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //Apply rotation
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //Rotate the player
        transform.Rotate(Vector3.up * mouseX * xSensitivity * Time.deltaTime);
    }
}
