using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SlideController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Direção relativa à rotação do objeto
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move *= moveSpeed;

        // Aplicar gravidade (opcional se você quer que o objeto caia)
        velocity.y += gravity * Time.deltaTime;
        move.y = velocity.y;

        // Move com colisão e desliza naturalmente nas superfícies
        controller.Move(move * Time.deltaTime);

        // Zera a velocidade vertical ao tocar o chão
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}
