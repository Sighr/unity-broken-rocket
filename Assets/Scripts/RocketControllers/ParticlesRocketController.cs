using System.Collections;
using UnityEngine;

public class ParticlesRocketController : AbstractRocketController, IUpdatable
{
    private ParticleSystem _ps;
    private GameSettings.Particles _settings;

    private float _psParticleStartSpeed;
    
    private Coroutine _disableFumeCoroutine;

    public ParticlesRocketController(RocketScript outerScript, ParticleSystem ps, GameSettings.Particles settings)
        : base(outerScript)
    {
        _ps = ps;
        _psParticleStartSpeed = _ps.main.startSpeed.constant;
        _settings = settings;
    }

    public void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        ProcessKeyboardInput();

        ProcessTouchInput();
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
}