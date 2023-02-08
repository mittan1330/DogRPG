using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timerText;

    public void OnSetTimerText(float time)
    {
        timerText.SetText("TIME " + time.ToString("f2"));
    }
}
