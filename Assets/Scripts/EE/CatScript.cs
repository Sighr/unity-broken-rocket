using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    public Transform rocketTransform;
    
    public float CAT_FREEZE_ROCKET_UPPER_BORDER;
    
    // Update is called once per frame
    void Update()
    {
        if (rocketTransform.position.y > CAT_FREEZE_ROCKET_UPPER_BORDER)
        {
            return;
        }

        transform.position = new Vector3(rocketTransform.position.x, transform.position.y, 0);
    }
}
