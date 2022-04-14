using System.Globalization;
using TMPro;
using UnityEngine;

public class DebugSliderScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void SetText(float value)
    {
        text.text = value.ToString(CultureInfo.InvariantCulture);
    }
}
