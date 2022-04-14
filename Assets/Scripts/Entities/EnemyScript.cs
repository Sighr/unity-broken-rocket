using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public GameSettings settings;
    public Transform target;
    
    private float _speed;
    
    public float lifeTime;
    
    void Start()
    {
        _speed = settings.enemy.minSpeed + Random.value * (settings.enemy.maxSpeed - settings.enemy.minSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
        transform.position += (target.position - transform.position).normalized * Time.fixedDeltaTime * _speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Rocket"))
        {
            Destroy(gameObject);
        }
    }
}
