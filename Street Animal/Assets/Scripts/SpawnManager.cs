using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float waitForSeconds = 1.5f;
    private Vector3 platformPosition = new Vector3 (11, -3.95f, 0);
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
        GameObject prefabs = ObjectPool.instance.GetPooledObject();
        if (prefabs != null )
        {
            prefabs.transform.position = platformPosition;
            prefabs.SetActive(true);
        }
        StartCoroutine(Spawn());
    }

    private float RandomSpawn()
    {
        return Random.Range(waitForSeconds, 2.5f);
    }
}
