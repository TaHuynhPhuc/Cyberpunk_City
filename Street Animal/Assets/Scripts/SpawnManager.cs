using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float waitForSeconds = 1.5f;
    private Vector3 platformPosition = new Vector3 (11, -3.95f, 0);
    private Vector3 hammerPosition;
    [SerializeField] private GameObject bird;
    [SerializeField] private GameObject dog;
    private int point;
    private int pointNextLevel = 5;
    private bool gameModeRun = true;
    private bool isPlayerLive = true;
    private bool SpawnAnimal = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        gameModeRun = PlayerController.instance.gameModeRun;
        isPlayerLive = PlayerController.instance.isPlayerLive;
        point = PlayerController.instance.point;
        UpdateLevel();
    }

   IEnumerator Spawn()
    {
        yield return new WaitForSeconds(RandomSpawn());
        GameObject platform = ObjectPool.instance.GetObject(ObjectPool.instance.pooledObjectsHammer);
        GameObject hammer = ObjectPool.instance.GetObject(ObjectPool.instance.pooledObjectsPlatform);

        if (platform != null && isPlayerLive && gameModeRun && PlayerController.instance.isStartGame)
        {
            platform.transform.position = platformPosition;
            platform.SetActive(true);
            if (SpawnAnimal)
            {
                Instantiate(bird, new Vector3(11, -3, 0), platform.transform.rotation);
                SpawnAnimal = false;
            }
        }
        else if (hammer != null && isPlayerLive && !gameModeRun && PlayerController.instance.isStartGame)
        {
            hammer.transform.position = new Vector3(10, Random.Range(-2, 3), 0);
            hammer.SetActive(true);
            if (SpawnAnimal)
            {
                Instantiate(dog, new Vector3(hammer.transform.position.x + 5, -3.25f, 0), hammer.transform.rotation);
                SpawnAnimal = false;
            }
        }
        StartCoroutine(Spawn());
    }

    private float RandomSpawn()
    {
        return Random.Range(waitForSeconds, 2.5f);
    }

    private void UpdateLevel()
    {
        if (point == pointNextLevel)
        {
            pointNextLevel += 5;
            SpawnAnimal = true;
        }
    }
}
