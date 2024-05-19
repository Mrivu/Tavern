using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    public TextMeshProUGUI EventText;

    public Slider SuccessFill;
    public Image GrayFill;

    public TextMeshProUGUI RequieredPercentage;
    public TextMeshProUGUI CurrentPercentage;

    private float timer = 0.0f;
    private float timerStart = 2.0f;
    private bool startTime = false;
    private float RandomPercentage;

    // Slider only goes from 0.1 to 0.9

    void Update()
    {
        if (timer >= 0f && startTime)
        {
            timer -= Time.deltaTime;

            SuccessFill.value = Mathf.Lerp(SuccessFill.value, 0.1f + RandomPercentage*0.8f, 0.01f / timerStart);
            CurrentPercentage.text = SuccessFill.value < 0.1f ? "0%" : Mathf.Round(Mathf.Round((Mathf.Lerp(SuccessFill.value, 0.1f + RandomPercentage * 0.8f, 0.01f / timerStart) - 0.1f) * 100f) / 0.8f).ToString() + "%";
        }
        else if (startTime)
        {
            startTime = false;
            Debug.Log(SuccessFill.value);

            // Timer end
            Debug.Log("Resolve end");
            if ((Mathf.Round(SuccessFill.value * 100f) / 100f) > 1-GrayFill.fillAmount)
            {
                Debug.Log("Fail");
            }
            else
            {
                Debug.Log("Success");
            }

        }
    }

    public void ResolvePress()
    {
        SuccessFill.value = 0.0f;
        RandomPercentage = Mathf.Round(Random.Range(0.0f, 1f) * 100f) / 100f;
        Debug.Log(RandomPercentage);
        timer = timerStart;
        startTime = true;
    }

    public void SetGrayArea(float percentage)
    {
        // Slider only goes from 0.1 to 0.9
        GrayFill.fillAmount = Mathf.Round((0.1f + percentage*0.8f) * 100f) / 100f;
        RequieredPercentage.text = (1f - percentage) < 0.01f ? "Failed" : Mathf.Round(100f - percentage*100f).ToString() + "%";
    }
}
