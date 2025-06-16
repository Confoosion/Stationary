using UnityEngine;
using System.Collections;

public class BoxSpawning : MonoBehaviour
{
    public static BoxSpawning Singleton;
    [SerializeField] int boxesToSpawn;
    [SerializeField] int boxesSpawned;
    [SerializeField] private float boxSpawnFrequency;
    private Coroutine spawnBoxes;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void StartSpawningBoxes(int amount, float frequency)
    {
        boxesToSpawn = amount;
        boxSpawnFrequency = frequency;
        spawnBoxes = StartCoroutine(SpawnBoxes());
    }

    private IEnumerator SpawnBoxes()
    {
        boxesSpawned = 0;
        while (boxesSpawned < boxesToSpawn)
        {
            BoxManager.Singleton.SpawnBox();
            boxesSpawned++;

            yield return new WaitForSeconds(boxSpawnFrequency);
        }

        spawnBoxes = null;
    }
}
