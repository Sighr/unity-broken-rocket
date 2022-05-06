using UnityEngine;

public class HelpTextScript : MonoBehaviour
{
    public float timeTillDisable;

    private void Start()
    {
        Destroy(gameObject, timeTillDisable);
    }
}