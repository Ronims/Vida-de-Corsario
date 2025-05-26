using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobSpawner : MonoBehaviour
{
    [Header("Configuração dos Mobs")]
    public GameObject[] mobPrefabs;  // Lista de mobs a spawnar.
    public int quantidadeParaSpawnar = 10;

    [Header("Área de Spawn")]
    public Vector3 areaCentro = Vector3.zero;
    public Vector3 areaTamanho = new Vector3(50, 0, 50);

    [Header("Tentativas de posicionamento")]
    public int maxTentativas = 30;

    void Start()
    {
        SpawnMobs();
    }

    void SpawnMobs()
    {
        for (int i = 0; i < quantidadeParaSpawnar; i++)
        {
            GameObject mobPrefab = mobPrefabs[Random.Range(0, mobPrefabs.Length)];
            Vector3 spawnPos;

            if (EncontrarPosicaoNoNavMesh(out spawnPos))
            {
                Instantiate(mobPrefab, spawnPos, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Não foi possível encontrar uma posição válida no NavMesh.");
            }
        }
    }

    bool EncontrarPosicaoNoNavMesh(out Vector3 result)
    {
        for (int i = 0; i < maxTentativas; i++)
        {
            Vector3 randomPoint = areaCentro + new Vector3(
                Random.Range(-areaTamanho.x / 2, areaTamanho.x / 2),
                0,
                Random.Range(-areaTamanho.z / 2, areaTamanho.z / 2)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(areaCentro, areaTamanho);
    }
}
