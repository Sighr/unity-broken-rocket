using UnityEditor;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public GameObject[] prefabs;
    
    public GameObject CreateAtLocation(Vector2 position, GameObject prefab = null)
    {
        if (prefab is null && prefabs.Length == 0)
        {
            throw new System.ArgumentException("Prefabs array is empty");
        }
        prefab ??= SelectRandomPrefab();
        return Create(prefab, position);
    }
    
    public GameObject CreateInRing(Vector2 center, float innerRadius, float outerRadius, GameObject prefab = null)
    {
        prefab ??= SelectRandomPrefab();
        Vector2 point = Random.insideUnitCircle;
        Vector2 position = point * (outerRadius - innerRadius) + point.normalized * innerRadius + center;
        return Create(prefab, position);
    }

    private GameObject Create(GameObject prefab, Vector2 position)
    {
#if (UNITY_EDITOR)
        if (!EditorApplication.isPlaying)
        {
            GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab, transform);
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.identity;
            return null;
        }
#endif
        return Instantiate(prefab, position, Quaternion.identity, transform);
    }


    private GameObject SelectRandomPrefab()
    {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
    public void DestroyAll()
    {
        foreach (Transform entity in transform)
        {
            Destroy(entity.gameObject);
        }
    }
}