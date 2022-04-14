using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Assets/Create/GameSettings")]
public class GameSettings : ScriptableObject
{
    [System.Serializable]
    public class Movement
    {
        public float initialTorque;
        public float initialVerticalSpeed;
        public float penaltyHeight;
        public float gravityPenaltyQuad;
    }
    public Movement movement;
    
    [System.Serializable]
    public class Particles
    {
        public float afterburnSeconds;
        public float afterburnParticleStartSpeed;
    }
    public Particles particles;
    
    [System.Serializable]
    public class Cloud
    {
        public float maxFloatDistance;
        public float timeTillRedirect;
    }
    public Cloud cloud;
    
    [System.Serializable]
    public class BonusSpawn
    {
        public float spawnTimeInterval;
        public float spawnMinDistance;
        public float spawnMaxDistance;
    }
    public BonusSpawn bonusSpawn;
    
    [System.Serializable]
    public class Enemy
    {
        public float spawnTimeInterval;
        public float spawnMinDistance;
        public float spawnMaxDistance;
        public float minSpeed;
        public float maxSpeed;
        public float minLifeTime;
        public float maxLifeTime;
    }
    public Enemy enemy;
}