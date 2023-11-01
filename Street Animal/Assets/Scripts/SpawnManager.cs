using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float waitForSeconds = 1.5f;
    private Vector3 platformPosition = new Vector3 (11, -3.95f, 0);
    private Vector3 hammerPosition;
    [SerializeField] private GameObject bird;
    private int point;
    private int pointNextLevel = 5;
    private bool gameModeRun = true;
    private bool isPlayerLive = true;
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
    }

   IEnumerator Spawn()
    {
        yield return new WaitForSeconds(RandomSpawn());
        GameObject platform = ObjectPool.instance.GetObject(ObjectPool.instance.pooledObjectsModeRun);
        GameObject hammer = ObjectPool.instance.GetObject(ObjectPool.instance.pooledObjectsModeFly);

        if (platform != null && isPlayerLive && gameModeRun)
        {
            platform.transform.position = platformPosition;
            platform.SetActive(true);
            if (point == pointNextLevel)
            {
                pointNextLevel += 5;
                Instantiate(bird, new Vector3(platform.transform.position.x, -3, 0), bird.transform.rotation);
                Debug.Log("Bird");
            }
        }else if (hammer != null && isPlayerLive && !gameModeRun)
        {
            hammer.transform.position = new Vector3(10, Random.Range(-2, 3), 0);
            hammer.SetActive(true);
            Debug.Log("Fly");
            if (point == pointNextLevel)
            {
                pointNextLevel += 5;
                //Instantiate(bird, new Vector3(platform.transform.position.x, -3, 0), bird.transform.rotation);
            }
        }
        StartCoroutine(Spawn());
    }

    private float RandomSpawn()
    {
        return Random.Range(waitForSeconds, 2.5f);
    }
}
