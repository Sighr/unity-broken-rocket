public abstract class AbstractRocketController
{
    protected RocketScript OuterScript;
    
    protected AbstractRocketController(RocketScript outerScript)
    {
        OuterScript = outerScript;
    }
}