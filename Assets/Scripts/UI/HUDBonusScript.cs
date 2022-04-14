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

    void Update()
    {
        _duration -= Time.deltaTime;
        image.fillAmount = _duration / maxDuration;
        if (_duration < 0)
        {
            Destroy(gameObject);
        }
    }
}
