using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public GameSettings settings;

    private Vector3 _startPosition;
    private float _timeTillRedirect;
    private Vector3 _direction; 

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        Redirect();
        _timeTillRedirect = Random.value * settings.cloud.timeTillRedirect;
    }

    void FixedUpdate()
    {
        _timeTillRedirect -= Time.fixedDeltaTime;
        if (_timeTillRedirect < 0)
        {
            _timeTillRedirect = settings.cloud.timeTillRedirect;
            Redirect();
        }
        transform.position += _direction * Time.fixedDeltaTime;
    }

    private void Redirect()
    {
        _direction = Random.value > 0.5 ? Vector3.left : Vector3.right;
        var distance = _startPosition - transform.position;
        if (distance.magnitude > settings.cloud.maxFloatDistance)
        {
            _direction = distance.normalized;
        }
    }
}
