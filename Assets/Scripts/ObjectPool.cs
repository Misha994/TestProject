using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Dictionary<SizeShape, List<GameObject>> objectPools = new Dictionary<SizeShape, List<GameObject>>();

    public void InitializeObjectPool(SizeShape objectName, GameObject objectPrefab, int size = 10)
    {
        objectPools[objectName] = new List<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            objectPools[objectName].Add(obj);
        }
    }

    public GameObject GetObject(SizeShape objectName)
    {
        List<GameObject> objectPool = objectPools[objectName];

        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                objectPool[i].SetActive(true);
                return objectPool[i];
            }
        }

        GameObject newObject = Instantiate(objectPools[objectName][0], Vector3.zero, Quaternion.identity);
        newObject.SetActive(true);
        objectPool.Add(newObject);
        return newObject;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ClearAllObject()
    {
        foreach (KeyValuePair<SizeShape, List<GameObject>> entry in objectPools)
        {
            for (int i = 0; i < entry.Value.Count; i++)
            {
                ReturnObject(entry.Value[i]);
            }
        }
    }
}