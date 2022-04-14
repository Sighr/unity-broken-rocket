using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameSettings settings;

    public EntitiesManager bonuses;
    public EntitiesManager enemies;

    public Rigidbody2D rocket;
    public RocketScript rocketScript;
    public Transform spawnTransform;
    
    public ScoreManager scoreManager;
    public GameObject loseMenu;
    

    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(GenerateBonusesCoroutine());
        StartCoroutine(GenerateEnemiesCoroutine());
    }
    
    private IEnumerator GenerateBonusesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(settings.bonusSpawn.spawnTimeInterval);
            if (Time.timeScale == 0)
                continue;
            bonuses.CreateInRing(rocket.position, settings.bonusSpawn.spawnMinDistance, settings.bonusSpawn.spawnMaxDistance);
        }
    }
    
    private IEnumerator GenerateEnemiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(settings.enemy.spawnTimeInterval);
            if (Time.timeScale == 0)
                continue;
            var enemy = enemies.CreateInRing(rocket.position, settings.enemy.spawnMinDistance, settings.enemy.spawnMaxDistance);
            var enemyScript = enemy.GetComponent<EnemyScript>();
            enemyScript.target = rocket.transform;
            enemyScript.lifeTime = settings.enemy.minLifeTime + Random.value * (settings.enemy.maxLifeTime - settings.enemy.minLifeTime);
        }
    }

    public void Lose()
    {
        scoreManager.Lose();
        loseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void RestartGame()
    {
        enemies.DestroyAll();
        bonuses.DestroyAll();
        rocketScript.ResetBonuses();
        scoreManager.Points = 0;
        
        rocket.gameObject.transform.position = spawnTransform.position;
        rocket.rotation = 0;
        rocket.velocity = Vector2.zero;
        rocket.angularVelocity = 0;
        loseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void AddPoint()
    {
        scoreManager.Points += 1;
    }
}
