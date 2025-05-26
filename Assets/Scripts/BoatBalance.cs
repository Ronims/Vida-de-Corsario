using UnityEngine;

public class BoatBalance : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Estabilização")]
    public float torqueStabilizer = 50f;  // Força para estabilizar pitch e roll
    public float maxAngle = 20f;           // Ângulo máximo de inclinação antes de corrigir forte

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();

        // Ajuste do tensor de inércia para dar estabilidade natural (exemplo)
        rb.inertiaTensor = new Vector3(5f, 0.5f, 5f); // Muito mais "pesado" no pitch e roll
        rb.inertiaTensorRotation = Quaternion.identity; 
    }

    void FixedUpdate()
    {
        Vector3 localEuler = transform.localEulerAngles;
        localEuler = NormalizeAngles(localEuler);

        // Estabilizar pitch (x) e roll (z)
        Vector3 torque = Vector3.zero;

        // Torque para corrigir pitch
        if (Mathf.Abs(localEuler.x) > 0.1f)
        {
            float pitchError = Mathf.Clamp(localEuler.x, -maxAngle, maxAngle);
            torque.x = -pitchError * torqueStabilizer;
        }

        // Torque para corrigir roll
        if (Mathf.Abs(localEuler.z) > 0.1f)
        {
            float rollError = Mathf.Clamp(localEuler.z, -maxAngle, maxAngle);
            torque.z = -rollError * torqueStabilizer;
        }

        rb.AddRelativeTorque(torque);
    }

    Vector3 NormalizeAngles(Vector3 angles)
    {
        // Converte ângulos de 0-360 para -180 a 180
        angles.x = (angles.x > 180) ? angles.x - 360 : angles.x;
        angles.y = (angles.y > 180) ? angles.y - 360 : angles.y;
        angles.z = (angles.z > 180) ? angles.z - 360 : angles.z;
        return angles;
    }
}
