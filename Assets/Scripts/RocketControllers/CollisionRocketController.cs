using System.Collections.Generic;
using UnityEngine;

public class CollisionRocketController : AbstractRocketController, IColliderController
{
    private GameManager _gameManager;
    
    public CollisionRocketController(RocketScript outerScript, GameManager gameManager)
        : base(outerScript)
    {
        _gameManager = gameManager;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var go = collision.gameObject;
        if (go.layer == LayerMask.NameToLayer("Obstacles")
            || go.layer == LayerMask.NameToLayer("Enemies") && OuterScript.appliedBonuses.Count == 0)
        {
            _gameManager.Lose();
        }
    }

    public void OnTriggerEnter2D(Collider2D collider2d)
    {
        var go = collider2d.gameObject;
        if (go.layer == LayerMask.NameToLayer("Bonuses"))
        {
            OuterScript.ApplyBonus(go);
        }
    }
}