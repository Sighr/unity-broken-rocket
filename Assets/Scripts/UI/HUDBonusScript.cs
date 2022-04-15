using UnityEngine;
using UnityEngine.UI;

public class HUDBonusScript : MonoBehaviour
{
    public Image image;
    public float maxDuration;
    
    private float _duration;

    void Start()
    {
        _duration = maxDuration;
    }

    void FixedUpdate()
    {
        _duration -= Time.fixedDeltaTime;
        image.fillAmount = _duration / maxDuration;
        if (_duration < 0)
        {
            Destroy(gameObject);
        }
    }
}
