using System.Collections.Generic;
using UnityEngine;

public class MovementRocketController : AbstractRocketController, IFixedUpdateable, IBonusApplicable
{
    private readonly Rigidbody2D _rb;
    private readonly Transform _transform;
    
    private float _torque;
    private float _verticalSpeed;
    
    private readonly GameSettings.Movement _settings;

    public MovementRocketController(RocketScript outerScript, Rigidbody2D rigidbody, GameSettings.Movement settings)
        : base(outerScript)
    {
        _rb = rigidbody;
        _transform = rigidbody.transform;
        _settings = settings;
        _torque = _settings.initialTorque;
        _verticalSpeed = _settings.initialVerticalSpeed;
    }

    public void FixedUpdate()
    {
        ProcessBonuses();

        ProcessKeyboardInputFixed();
        ProcessTouchInputFixed();
        ApplyGravityPenalty();
    }
    
    // maybe it should be extracted into the base class. somehow put it as protected member into interface 
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

    private void ProcessTouchInputFixed()
    {
        if (Input.touchCount == 0)
        {
            return;
        }
        ProcessManeuvering();
    }

    private void ProcessKeyboardInputFixed()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ProcessManeuvering();
        }
    }

    private void ApplyGravityPenalty()
    {
        float dy = _transform.position.y - _settings.penaltyHeight;
        if (dy > 0)
        {
            float additionalGravity = dy * dy * _settings.gravityPenaltyQuad;
            _rb.AddForce(Vector3.down * additionalGravity);
        }
    }

    private void ProcessManeuvering()
    {
        _rb.AddForce(_transform.up * GetVerticalSpeed());
        _rb.AddTorque(_torque);
    }
    
    private float GetVerticalSpeed()
    {
        float speed = _verticalSpeed;
        foreach (var bonus in BonusList)
        {
            speed *= bonus.power;
        }
        Debug.Log(speed);
        return speed;
    }

    #region Bonuses
    
    public List<BonusScript.Bonus> BonusList { get; } = new();
    public BonusScript.Bonus.BonusType[] ApplicableBonusTypes => new[] {BonusScript.Bonus.BonusType.Speed};

    #endregion
}