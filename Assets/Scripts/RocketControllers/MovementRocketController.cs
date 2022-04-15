using System.Collections.Generic;
using UnityEngine;

public class MovementRocketController : AbstractRocketController, IFixedUpdateable
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
        ProcessKeyboardInputFixed();
        ProcessTouchInputFixed();
        ApplyGravityPenalty();
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
        foreach (var bonus in OuterScript.appliedBonuses)
        {
            if (bonus.type == BonusScript.Bonus.BonusType.Speed)
            {
                speed *= bonus.power;
            }
        }
        return speed;
    }
}