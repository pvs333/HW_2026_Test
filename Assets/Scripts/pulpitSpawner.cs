using UnityEngine;
using System.Collections;

public class pulpitSpawner : MonoBehaviour
{
    [SerializeField] private bool first;

    public Transform[] spawnPoints;
    public GameObject pulpitPrefab;
    public Transform[] pulpitPos;
    public float duration = 1f;
    float spawnTime;
    int randomIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = gameManager.GetPulpitSpawnTime();
        randomIndex = Random.Range(0, spawnPoints.Length);
        if (first) spawnTime = 4;
        StartCoroutine(SpawnPulpit(spawnTime));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnPulpit(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject pulpit = Instantiate(pulpitPrefab, spawnPoints[randomIndex].position, Quaternion.identity);

        float time = 0f;

        Vector3 startPos = pulpit.transform.position;
        Vector3 endPos = pulpitPos[randomIndex].position;

        while (time < duration)
        {
            time += Time.deltaTime;

            pulpit.transform.position = Vector3.Lerp(
                startPos,
                endPos,
                time / duration
            );

            yield return null;
        }
    }
}
