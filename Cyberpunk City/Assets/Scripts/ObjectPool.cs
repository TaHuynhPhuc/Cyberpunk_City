using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public List<GameObject> pooledObjectsHammer = new List<GameObject>();
    public List<GameObject> pooledObjectsPlatform = new List<GameObject>();
    private int amountToPool = 5;

    [SerializeField] private GameObject plaform;
    [SerializeField] private GameObject hammer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnAndInputList(plaform, pooledObjectsHammer);
        SpawnAndInputList(hammer, pooledObjectsPlatform);
    }

    public GameObject GetObject(List<GameObject> listObj)
    {
        for (int i = 0; i < listObj.Count; i++)
        {
            if (!listObj[i].activeInHierarchy)
            {
                return listObj[i];
            }
        }
        return null;
    }

    private void SpawnAndInputList(GameObject obj, List<GameObject> listObj)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject gameObject = Instantiate(obj);
            gameObject.SetActive(false);
            listObj.Add(gameObject);
        }
    }
}
