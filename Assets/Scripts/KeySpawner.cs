using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform[] spawnPoints;

    private void Start()
    {
        SpawnKey();
    }

    void SpawnKey()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(keyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
