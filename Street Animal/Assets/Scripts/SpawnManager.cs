using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float waitForSeconds = 1.5f;
    private Vector3 platformPosition = new Vector3 (11, -3.95f, 0);
    [SerializeField] private GameObject bird;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   IEnumerator Spawn()
    {
        yield return new WaitForSeconds(RandomSpawn());
        GameObject platform = ObjectPool.instance.GetPlatformObject();
        if (platform != null && PlayerController.instance.isPlayerLive)
        {
            platform.transform.position = platformPosition;
            platform.SetActive(true);
            Instantiate(bird, new Vector3(platform.transform.position.x, -3, 0), bird.transform.rotation);
        }
        StartCoroutine(Spawn());
    }

    private float RandomSpawn()
    {
        return Random.Range(waitForSeconds, 2.5f);
    }
}
