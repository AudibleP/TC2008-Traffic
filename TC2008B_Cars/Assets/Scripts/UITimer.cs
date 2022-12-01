using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
   public TextMeshProUGUI timeText;


    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
        //StepManager.OnStepChanged += UpdateTime;
    }
    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
        //StepManager.OnStepChanged -= UpdateTime;
    }
    private void UpdateTime()
    {


        timeText.text = $"{TimeManager.Hour.ToString("00")}:{TimeManager.Minute:00}\n Step: {StepManager.Step}\n";
    }

   
}

