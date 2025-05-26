using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;          // O barco
    public Vector3 offset = new Vector3(0, 3, -6); // Posição da câmera em relação ao barco
    public float sensitivity = 3f;
    public float distance = 6f;
    public float minY = 5f;
    public float maxY = 80f;

    private float currentX = 0f;
    private float currentY = 30f;

    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 dir = new Vector3(0, 0, -distance);
        transform.position = target.position + rotation * dir + Vector3.up * offset.y;
        transform.LookAt(target.position + Vector3.up * offset.y);
    }
}
