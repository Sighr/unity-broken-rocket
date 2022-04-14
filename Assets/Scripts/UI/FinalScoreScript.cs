using System.Globalization;
using TMPro;
using UnityEngine;

public class FinalScoreScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetScore(int value)
    {
        text.text = $"You collected {value.ToString(CultureInfo.InvariantCulture)} points";
    }
}
