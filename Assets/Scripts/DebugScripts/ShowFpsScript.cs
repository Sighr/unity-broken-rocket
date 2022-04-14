using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ShowFpsScript : MonoBehaviour
{
    
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.text = (1 / Time.smoothDeltaTime).ToString("F2");
    }
}
