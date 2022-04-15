using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public GameSettings settings;
    
    #region CachedDependencies
    
    public HUDBonusManager hudBonusManager;
    public GameObject shieldVisual;
    public ParticleSystem ps;
    public GameManager gameManager;

    #endregion
    
    private AbstractRocketController[] _controllers;
    private readonly List<IUpdatable> _updatables = new();
    private readonly List<IFixedUpdateable> _fixedUpdateables = new();
    private readonly List<IColliderController> _colliderControllers = new();
    private readonly List<IBonusApplicable> _bonusApplicables = new();
    
    void Start()
    {
        _controllers = new AbstractRocketController[]
        {
          new MovementRocketController(this, GetComponent<Rigidbody2D>(), settings.movement),
          new VisualRocketController(this, ps, shieldVisual, settings.particles),
          new CollisionRocketController(this, gameManager),
        };
        foreach (var controller in _controllers)
        {
            if (controller is IUpdatable updatable)
                _updatables.Add(updatable);
            if (controller is IFixedUpdateable fixedUpdateable)
                _fixedUpdateables.Add(fixedUpdateable);
            if (controller is IColliderController colliderController)
                _colliderControllers.Add(colliderController);
            if (controller is IBonusApplicable bonusApplicable)
                _bonusApplicables.Add(bonusApplicable);
        }
    }
    
    void Update()
    {
        foreach (var updatable in _updatables)
        {
            updatable.Update();
        }
    }

    void FixedUpdate()
    {
        ProcessBonuses();
        foreach (var fixedUpdateable in _fixedUpdateables)
        {
            fixedUpdateable.FixedUpdate();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var colliderController in _colliderControllers)
        {
            colliderController.OnCollisionEnter2D(collision);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        foreach (var colliderController in _colliderControllers)
        {
            colliderController.OnTriggerEnter2D(collider2d);
        }
    }

    #region Bonuses

    public List<BonusScript.Bonus> appliedBonuses = new();

    private void ProcessBonuses()
    {
        for (var index = appliedBonuses.Count - 1; index >= 0; index--)
        {
            var bonus = appliedBonuses[index];
            bonus.duration -= Time.fixedDeltaTime;
            if (bonus.duration < 0)
            {
                foreach (var bonusApplicable in _bonusApplicables)
                {
                    bonusApplicable.OnBonusDeleted(bonus);
                }
                appliedBonuses.RemoveAt(index);
            }
        }
    }

    public void ApplyBonus(GameObject go)
    {
        BonusScript.Bonus bonus = go.GetComponent<BonusScript>().bonus;

        // special case - bonus is point - goes straight to GameManager
        if (bonus.type == BonusScript.Bonus.BonusType.Point)
        {
            gameManager.AddPoint();
            return;
        }
        
        appliedBonuses.Add(bonus);
        
        // it should not be there - rather in gamemanager
        hudBonusManager.AddBonus(bonus);

        
        foreach (var bonusApplicable in _bonusApplicables)
        {
            bonusApplicable.OnBonusAdded(bonus);
        }
    }
    
    public void ResetBonuses()
    {
        foreach (var bonus in appliedBonuses)
        {
            foreach (var bonusApplicable in _bonusApplicables)
            {
                bonusApplicable.OnBonusDeleted(bonus);
            }   
        }
        hudBonusManager.ResetBonuses();
        appliedBonuses.Clear();
    }
    
    #endregion
}
