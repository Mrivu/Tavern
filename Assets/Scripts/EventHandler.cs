using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EventHandler : MonoBehaviour
{
    public TextMeshProUGUI EventText;
    public TextMeshProUGUI EventDescription;

    public Slider SuccessFill;
    public Image GrayFill;

    public TextMeshProUGUI RequieredPercentage;
    public TextMeshProUGUI CurrentPercentage;

    public Button ResolveButton;
    public Button ContinueButton;

    // Choice Buttons
    public Button C1;
    public Button C2;
    public Button C3;
    public Button C4;

    private float timer = 0.0f;
    private float timerStart = 2.0f;
    private bool startTime = false;
    private float RandomPercentage;

    Events.Event currentEvent;

    // Slider only goes from 0.1 to 0.9

    public void Activate(float grayarea, Events.Event returnEvent)
    {
        gameObject.SetActive(true);
        SetGrayArea(grayarea);
        EventText.text = returnEvent.eventText;
        EventDescription.text = returnEvent.eventDescription;
        currentEvent = returnEvent;

        C1.GetComponentInChildren<TextMeshProUGUI>().text = returnEvent.choices[0];
        C2.GetComponentInChildren<TextMeshProUGUI>().text = returnEvent.choices[1];
        C3.GetComponentInChildren<TextMeshProUGUI>().text = returnEvent.choices[2];
        C4.GetComponentInChildren<TextMeshProUGUI>().text = returnEvent.choices[3];

        CurrentPercentage.text = "0%";
        SuccessFill.value = 0f;

        EnableChoices();
    }

    private void Awake()
    {
        ContinueButton.onClick.AddListener(ContinuePress);
        ContinueButton.gameObject.SetActive(false);
        ResolveButton.onClick.AddListener(ResolvePress);
        C1.onClick.AddListener(Input1);
        C2.onClick.AddListener(Input2);
        C3.onClick.AddListener(Input3);
        C4.onClick.AddListener(Input4);
    }

    private void Input1()
    {
        Debug.Log("Input1");
        DisableChoices();
        AddSuccessRate(currentEvent.choiceChance[0]);
    }
    private void Input2() 
    { 
        DisableChoices();
        AddSuccessRate(currentEvent.choiceChance[1]);
    }
    private void Input3()
    {
        DisableChoices();
        AddSuccessRate(currentEvent.choiceChance[2]);
    }
    private void Input4() 
    {
        DisableChoices();
        AddSuccessRate(currentEvent.choiceChance[3]);
    }

    private void DisableChoices()
    {
        C1.interactable = false;
        C2.interactable = false;
        C3.interactable = false;
        C4.interactable = false;

        C1.gameObject.GetComponent<Image>().color = Color.gray;
        C2.gameObject.GetComponent<Image>().color = Color.gray;
        C3.gameObject.GetComponent<Image>().color = Color.gray;
        C4.gameObject.GetComponent<Image>().color = Color.gray;
    }

    private void EnableChoices()
    {
        C1.interactable = true;
        C2.interactable = true;
        C3.interactable = true;
        C4.interactable = true;

        C1.gameObject.GetComponent<Image>().color = Color.white;
        C2.gameObject.GetComponent<Image>().color = Color.white;
        C3.gameObject.GetComponent<Image>().color = Color.white;
        C4.gameObject.GetComponent<Image>().color = Color.white;
    }

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
                HandleResult("Failed");
            }
            else
            {
                HandleResult("Success");
            }

        }
    }

    private void AddSuccessRate(float increase)
    {
        GrayFill.fillAmount -= increase*0.8f;
        int textInt = int.Parse(RequieredPercentage.text.Replace("%", ""));
        RequieredPercentage.text = (textInt + increase*100).ToString() + "%";
    }

    private void ResolvePress()
    {
        ResolveButton.interactable = false;
        ResolveButton.gameObject.GetComponent<Image>().color = Color.gray;
        SuccessFill.value = 0.0f;
        RandomPercentage = Mathf.Round(Random.Range(0.0f, 1f) * 100f) / 100f;
        Debug.Log(RandomPercentage);
        timer = timerStart;
        startTime = true;
    }

    private void ContinuePress()
    {
        // Reset and close
        ContinueButton.interactable = false;
        ContinueButton.gameObject.SetActive(false);

        ResolveButton.gameObject.SetActive(true);
        ResolveButton.interactable = true;
        ResolveButton.gameObject.GetComponent<Image>().color = Color.white;

        gameObject.SetActive(false);
    }

    private void HandleResult(string result)
    {
        ResolveButton.gameObject.SetActive(false);

        ContinueButton.gameObject.SetActive(true);
        ContinueButton.interactable = true;

        CurrentPercentage.text = result;
    }

    public void SetGrayArea(float percentage)
    {
        // Slider only goes from 0.1 to 0.9
        GrayFill.fillAmount = Mathf.Round((0.1f + percentage*0.8f) * 100f) / 100f;
        RequieredPercentage.text = (1f - percentage) < 0.01f ? "Failed" : Mathf.Round(100f - percentage*100f).ToString() + "%";
    }
}
