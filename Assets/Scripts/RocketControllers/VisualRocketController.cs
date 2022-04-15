using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualRocketController : AbstractRocketController, IUpdatable, IBonusApplicable
{
    private readonly ParticleSystem _ps;
    private readonly GameObject _shield;
    private readonly GameSettings.Particles _settings;

    private readonly float _psParticleStartSpeed;
    
    private Coroutine _disableFumeCoroutine;

    public VisualRocketController(RocketScript outerScript, ParticleSystem ps, GameObject shield, GameSettings.Particles settings)
        : base(outerScript)
    {
        _ps = ps;
        _shield = shield;
        _psParticleStartSpeed = _ps.main.startSpeed.constant;
        _settings = settings;
    }

    public void Update()
    {
        if (Time.timeScale == 0)
        {
            if (_ps.isPlaying)
            {
                _ps.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }
            return;
        }
        ProcessKeyboardInput();
        
        ProcessTouchInput();
        
        _shield.transform.rotation = Quaternion.identity;
    }

    private void ProcessTouchInput()
    {
        if (Input.touchCount == 0)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                StartParticles();
                break;
            case TouchPhase.Ended:
                StopParticles();
                break;
        }
    }

    private void ProcessKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartParticles();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopParticles();
        }
    }

    private void StartParticles()
    {
        var main = _ps.main;
        main.startSpeed = _psParticleStartSpeed;
        _ps.Play();
        if (_disableFumeCoroutine != null)
        {
            OuterScript.StopCoroutine(_disableFumeCoroutine);
            _disableFumeCoroutine = null;
        }
    }

    private void StopParticles()
    {
        var main = _ps.main;
        main.startSpeed = _settings.afterburnParticleStartSpeed;
        _disableFumeCoroutine = OuterScript.StartCoroutine(DisableFumeCoroutine());
    }

    private IEnumerator DisableFumeCoroutine()
    {
        yield return new WaitForSeconds(_settings.afterburnSeconds);
        _ps.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }

    #region Bonuses

    private readonly List<BonusScript.Bonus> _appliedBonuses = new();
    
    public void OnBonusAdded(BonusScript.Bonus bonus)
    {
        if (bonus.type != BonusScript.Bonus.BonusType.Shield)
        {
            return;
        }
        _appliedBonuses.Add(bonus);
        _shield.SetActive(true);
    }

    public void OnBonusDeleted(BonusScript.Bonus bonus)
    {
        if (bonus.type != BonusScript.Bonus.BonusType.Shield)
        {
            return;
        }
        _appliedBonuses.Remove(bonus);
        if (_appliedBonuses.Count == 0)
        {
            _shield.SetActive(false);
        }
    }
    
    #endregion
}