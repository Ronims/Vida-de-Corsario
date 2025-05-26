using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 50f;

void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;
    }
    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");   // W/S
        float turnInput = Input.GetAxis("Horizontal"); // A/D

        // Move para frente/trás
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);

        // Rotaciona o barco
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
         if (Input.GetKeyDown(KeyCode.Escape))
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    }
}
