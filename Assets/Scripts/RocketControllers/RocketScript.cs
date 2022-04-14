using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public GameSettings settings;
    
    #region CachedDependencies

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
          new ParticlesRocketController(this, ps, settings.particles),
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
    
    public void ApplyBonus(GameObject go)
    {
        BonusScript.Bonus bonus = go.GetComponent<BonusScript>().bonus;
        
        // special case - bonus is point - goes straight to GameManager
        if (bonus.type == BonusScript.Bonus.BonusType.Point)
        {
            gameManager.AddPoint();
            return;
        }
        
        foreach (var bonusApplicable in _bonusApplicables)
        {
            bonusApplicable.ApplyBonus(bonus);
        }
    }
    
    public void ResetBonuses()
    {
        foreach (var bonusApplicable in _bonusApplicables)
        {
            bonusApplicable.ResetBonuses();
        }
    }
}
