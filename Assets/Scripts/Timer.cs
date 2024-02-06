using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [SerializeField] public static float currentTime;
    [SerializeField] private bool countDown;

    [Header("Format Settings")]
    [SerializeField] private bool hasFormat;
    [SerializeField] private TimerFormats format;

    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    [Header("Text Before Timer")]
    [SerializeField] private string text;

    private void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthsDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
        timeFormats.Add(TimerFormats.ThousandthsDecimal, "0.000");
    }

    private void Update()
    {
        if (countDown)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        if (hasFormat)
        {
            timerText.text = text + "\n" + currentTime.ToString(timeFormats[format]);
        }
        else
        {
            timerText.text = text + "\n" + currentTime.ToString();
        }
    }

    private enum TimerFormats
    {
        Whole,
        TenthsDecimal,
        HundrethsDecimal,
        ThousandthsDecimal
    }

    public static void ResetTime()
    {
        currentTime = 0;
    }
}
