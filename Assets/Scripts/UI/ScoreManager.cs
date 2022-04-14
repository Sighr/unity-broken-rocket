using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _points;
    public int Points
    {
        get => _points;
        set
        {
            _points = value;
            currentScore.UpdateScore(value);
        }
    }
    public CurrentScoreScript currentScore;
    public FinalScoreScript finalScore;
    
    public void Lose()
    {
        finalScore.SetScore(_points);
    }
}