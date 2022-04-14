using System.Globalization;
using TMPro;
using UnityEngine;

public class CurrentScoreScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void UpdateScore(int value)
    {
        text.text = value.ToString(CultureInfo.InvariantCulture);
    }
}
