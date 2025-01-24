using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EventHandler : MonoBehaviour
{
    public TextMeshProUGUI EventText;
    public TextMeshProUGUI EventDescription;
    public TextMeshProUGUI ChoiceDescription;

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
    private Button[] Choices;

    // Reputation
    public TextMeshProUGUI Azure;
    public TextMeshProUGUI Refugees;
    public TextMeshProUGUI Townsfolk;
    public TextMeshProUGUI Rascals;
    private TextMeshProUGUI[] Factions;

    private float timer = 0.0f;
    private float timerStart = 2.0f;
    private bool startTime = false;
    private float RandomPercentage;

    private List<bool> enabledChoices;

    Events.Event currentEvent;

    // Slider only goes from 0.1 to 0.9

    public void Activate(float grayarea, Events.Event returnEvent)
    {
        gameObject.SetActive(true);
        SetGrayArea(grayarea);
        EventText.text = returnEvent.eventText;
        EventDescription.text = returnEvent.eventDescription;
        currentEvent = returnEvent;
        enabledChoices = returnEvent.choiceEnabled;
        Choices = new Button[] { C1, C2, C3, C4 };
        Factions = new TextMeshProUGUI[] { Azure, Refugees, Townsfolk, Rascals };
        SetReputationText();

        for (int i = 0; i < Choices.Count(); i++)
        {
            Choices[i].GetComponentInChildren<TextMeshProUGUI>().text = returnEvent.choices[i];
        }

        CurrentPercentage.text = "0%";
        SuccessFill.value = 0f;
        
        DisableChoices();
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

    private void SetReputationText()
    {
        for (int i = 0; i < Factions.Count(); i++)
        {
            Factions[i].text = GameHandler.Instance.Reputations[i].ToString();
        }
    }

    private void Input1()
    {
        DisableChoices();
        HandleChoice(0);
    }
    private void Input2() 
    { 
        DisableChoices();
        HandleChoice(1);
    }
    private void Input3()
    {
        DisableChoices();
        HandleChoice(2);
    }
    private void Input4() 
    {
        DisableChoices();
        HandleChoice(3);
    }

    private void HandleChoice(int choice)
    {
        AddSuccessRate(currentEvent.choiceChance[choice]);

        // Update reputation
        foreach (KeyValuePair<int, int> entry in currentEvent.choiceReputation[choice])
        {
            GameHandler.Instance.Reputations[entry.Key] += entry.Value;
            if (GameHandler.Instance.Reputations[entry.Key] > 100)
            {
                GameHandler.Instance.Reputations[entry.Key] = 100;
            }
            else if (GameHandler.Instance.Reputations[entry.Key] < -100)
            {
                GameHandler.Instance.Reputations[entry.Key] = -100;
            }   
        }

        SetReputationText();
    }

    private void DisableChoices()
    {
        for (int i = 0; i < Choices.Count(); i++)
        {
            Choices[i].interactable = false;
            Choices[i].gameObject.GetComponent<Image>().color = Color.gray;
        }
    }

    private void EnableChoices()
    {
        for (int i = 0; i < Choices.Count(); i++)
        {
            if (enabledChoices[i])
            {
                Choices[i].interactable = true;
                Choices[i].gameObject.GetComponent<Image>().color = Color.white;
            }
        }
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
        if (RectTransformUtility.RectangleContainsScreenPoint(C1.gameObject.GetComponent<RectTransform>(), UnityEngine.Input.mousePosition, null))
        {
            ChoiceDescription.text = currentEvent.choiceResultText[0];
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(C2.gameObject.GetComponent<RectTransform>(), UnityEngine.Input.mousePosition, null))
        {
            ChoiceDescription.text = currentEvent.choiceResultText[1];
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(C3.gameObject.GetComponent<RectTransform>(), UnityEngine.Input.mousePosition, null))
        {
            ChoiceDescription.text = currentEvent.choiceResultText[2];
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(C4.gameObject.GetComponent<RectTransform>(), UnityEngine.Input.mousePosition, null))
        {
            ChoiceDescription.text = currentEvent.choiceResultText[3];
        }
        else
        {
            ChoiceDescription.text = "Hover above choices to see more";
        }
    }

    private void AddSuccessRate(float increase)
    {
        GrayFill.fillAmount -= increase*0.8f;
        int textInt = int.Parse(RequieredPercentage.text.Replace("%", ""));
        RequieredPercentage.text = textInt + increase * 100 > 100 ? "100%" : textInt + increase * 100 < 1 ? "0%" : (textInt + increase*100).ToString() + "%";
    }

    private void ResolvePress()
    {
        DisableChoices();
        ResolveButton.interactable = false;
        ResolveButton.gameObject.GetComponent<Image>().color = Color.gray;
        SuccessFill.value = 0.0f;
        RandomPercentage = Mathf.Round(UnityEngine.Random.Range(0.0f, 1f) * 100f) / 100f;
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
        RequieredPercentage.text = (1f - percentage) < 0.01f ? "0%" : Mathf.Round(100f - percentage*100f).ToString() + "%";
    }
}
