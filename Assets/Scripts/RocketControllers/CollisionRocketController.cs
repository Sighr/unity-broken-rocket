using System.Collections.Generic;
using UnityEngine;

public class CollisionRocketController : AbstractRocketController, IFixedUpdateable, IColliderController, IBonusApplicable
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
            || go.layer == LayerMask.NameToLayer("Enemies") && BonusList.Count == 0)
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

    public void FixedUpdate()
    {
        ProcessBonuses();
    }
    
    private void ProcessBonuses()
    {
        for (var index = BonusList.Count - 1; index >= 0; index--)
        {
            var bonus = BonusList[index];
            bonus.duration -= Time.fixedDeltaTime;
            if (bonus.duration < 0)
            {
                BonusList.RemoveAt(index);
            }
        }
    }

    #region Bonuses

    public List<BonusScript.Bonus> BonusList { get; } = new();
    public BonusScript.Bonus.BonusType[] ApplicableBonusTypes => new[] {BonusScript.Bonus.BonusType.Shield};

    #endregion
}