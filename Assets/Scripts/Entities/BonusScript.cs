using System;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    [Serializable]
    public class Bonus
    {
        public enum BonusType
        {
            Point,
            Speed,
            Shield
        }

        public BonusType type;
        public float power;
        public float duration;
    }
    public Bonus bonus;
    
    public float lifeTime;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
