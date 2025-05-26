using UnityEngine;
using UnityEngine.AI;

public class EnemyBoatAI : MonoBehaviour
{
    public Transform player;

    public float minShootDistance = 10f;
    public float idealDistance = 20f;
    public float maxShootDistance = 25f;
    public float rotationSpeed = 2f;

    public float angleForShooting = 60f;
    public GameObject cannonBallPrefab;
    public Transform[] cannonMuzzles;
    public float shootCooldown = 2.0f;
    public float cannonForce = 500f;

    private NavMeshAgent agent;
    private int currentCannonIndex = 0;
    private float nextShootTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        Vector3 directionToPlayer = toPlayer.normalized;

        // Ajuste de distância automática
        if (distance < minShootDistance)
        {
            // Muito perto, afasta
            Vector3 moveDir = -directionToPlayer;
            agent.isStopped = false;
            agent.SetDestination(transform.position + moveDir * 5f);
        }
        else if (distance > maxShootDistance)
        {
            // Muito longe, aproxima
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            // Dentro do alcance ideal: parar e girar lateralmente
            agent.isStopped = true;

            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            Quaternion sideRotation = lookRotation * Quaternion.Euler(0, 90f, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, sideRotation, Time.deltaTime * rotationSpeed);

            float angle = Vector3.Angle(transform.right, directionToPlayer);

            if (distance >= minShootDistance && distance <= maxShootDistance && angle <= angleForShooting)
            {
                if (Time.time >= nextShootTime)
                {
                    Shoot();
                    nextShootTime = Time.time + shootCooldown;
                }
            }
        }

        // Forçar disparo manual para testes
        if (Input.GetKeyDown(KeyCode.F))
        {
            TestShoot();
        }
    }

    void Shoot()
    {
        Transform muzzle = cannonMuzzles[currentCannonIndex];

        GameObject cannonBall = Instantiate(cannonBallPrefab, muzzle.position, muzzle.rotation);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(muzzle.forward * cannonForce);
        }

        Debug.Log("Enemy boat fired from: " + muzzle.name);

        currentCannonIndex = (currentCannonIndex + 1) % cannonMuzzles.Length;
    }

    void TestShoot()
    {
        Debug.Log("TESTE: Forçando disparo manual.");
        Shoot();
    }

    void OnDrawGizmosSelected()
    {
        // Distância mínima (vermelho)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minShootDistance);

        // Distância ideal (amarelo)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, idealDistance);

        // Distância máxima (verde)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxShootDistance);

        // Ângulo de disparo (azul)
        Gizmos.color = Color.blue;
        Vector3 rightDir = transform.right * maxShootDistance;
        Quaternion leftRotation = Quaternion.Euler(0, -angleForShooting, 0);
        Quaternion rightRotation = Quaternion.Euler(0, angleForShooting, 0);

        Vector3 leftBoundary = leftRotation * rightDir;
        Vector3 rightBoundary = rightRotation * rightDir;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
