using UnityEngine;
using System.Collections.Generic;

public class GemsController : MonoBehaviour
{
    [SerializeField] GameObject gemPrefab;
    [SerializeField] Transform[] gemSpawnPoints;
    [SerializeField] private int maxGems = 5;
    private List<GameObject> activeGems = new List<GameObject>();

    void Start()
    {
        SpawnGems();
    }

    // Para spawnear la gemas selecciono un punto de spawn aleatorio de los disponibles 
    void SpawnGems()
    {
        // Solo genero 5 gemas al mismo tiempo
        while (activeGems.Count < maxGems)
        {
            Transform spawnPoint = GetRandomSpawnPoint();
            GameObject newGem = Instantiate(gemPrefab, spawnPoint.position, Quaternion.identity);
            newGem.GetComponent<GemController>().SetSpawn(this); 
            activeGems.Add(newGem);
        }
    }

    public Transform GetRandomSpawnPoint()
    {
        return gemSpawnPoints[Random.Range(0, gemSpawnPoints.Length)];
    }

    // Al recoger la gema la destruyo y spawneo otra, el delay es para que de tiempo al audio a reproducirse
    public void GemCollected(GameObject gem, float delay)
    {
        activeGems.Remove(gem);
        Destroy(gem, delay); 
        SpawnGems();
    }
}
