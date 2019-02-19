using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] GameObject[] enemies = new GameObject[3];
    [SerializeField] Transform[] portals = new Transform[4];

    private void Start()
    {
        StartCoroutine(FirstTypeWaveSpawn());
    }

    void Spawn(GameObject enemy, Transform portal)
    {
        Instantiate(enemy, portal.position, Quaternion.identity);
    }

    IEnumerator FirstTypeWaveSpawn()
    {
        yield return new WaitForSecondsRealtime(20f);
        Spawn(enemies[0], portals[0]);
        yield return new WaitForSecondsRealtime(2f);
        Spawn(enemies[0], portals[0]);
        yield return new WaitForSecondsRealtime(2f);
        Spawn(enemies[0], portals[0]);
    }
}
