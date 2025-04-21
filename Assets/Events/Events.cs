using System;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
    public class Event
    {
        public int eventID;
        public string eventText;
        public string eventDescription;
        public float baseSuccess;

        public List<string> choices;
        public List<string> choiceResultText;
        public string FailureText;
        public List<float> choiceChance;
        public List<Dictionary<int, int>> choiceReputation;
        public List<int> choiceGold;
        public List<bool> choiceEnabled;
    }

    public class Brawl : Event
    {
        // Factions: Azure, Refugees, Townsfolk, Rascals
        // KEY: ID, VALUE: Reputation change

        public Brawl()
        {
            eventID = 1;
            eventText = "Stop Brawl";
            eventDescription = "A guard and a townsman started a brawl. \"Damn you! I'll hang make sure you hang!\", cries the guard. \"You asked for this you bastard!\", retorts the townsman.";
            baseSuccess = 0.8f;

            choices = new List<string>() { "Bribe them to stop", "Side with guard", "Side with townsman", "Kick both from the tavern" };
            choiceResultText = new List<string>() { "+10 success chance, costs gold. Both will be slightly unhappy", "+25 success chance, the townsman won't be happy", "+25 success chance, the guard won't be happy", "+45 success chance. Both will be slightly unhappy" };
            choiceChance = new List<float>() { 0.1f, 0.25f, 0.25f, 0.45f };
            choiceReputation = new List<Dictionary<int, int>>()
            {
               new Dictionary<int, int>()
               {
                   { 0, -3 },
                   { 2, -3 },
               },
               new Dictionary<int, int>()
               {
                   { 0, 5 },
                   { 2, -10 }
               },
               new Dictionary<int, int>()
               {
                   { 0, -10 },
                   { 2, 5 }
               },
               new Dictionary<int, int>()
               {
                   { 0, -5 },
                   { 2, -5 }
               },
               new Dictionary<int, int>() // If failed
               {
                   { 0, -15 },
                   { 2, -15 }
               }
            };

            choiceGold = new List<int>() { 0, 0, 0, 0, 0 }; // 5th is fail
            choiceEnabled = new List<bool>() { true, true, true, true };
        }
    }

    public class Thief : Event
    {
        public Thief()
        {
            eventID = 2;
            eventText = "You noticed a thief";
            int stealAmount = (int)UnityEngine.Random.Range(0.05f, 0.3f) * GameHandler.Instance.Gold;
            eventDescription = "A thief grabbed " + stealAmount.ToString() + " Gold from behind the counter";
            baseSuccess = 0.4f;

            choices = new List<string>() { "Catch", "Call patrons", "Call guards", "Let them escape" };
            choiceResultText = new List<string>() { "Catch the thief yourself", "Requires reputation above 0 with the Townsfolk, +25 success chance, Tavern patrons might get hurt.", "Requires reputation above 0 with the Azure. +25 when reputation 0, +35 when reputation 30 and +45 when reputation 60.", "-100 success chance. The thief will escape" };
            // CHeck Azure reputation
            float azureHelp = 0.0f;
            choiceEnabled = new List<bool>() { true, true, true, true };
            if (GameHandler.Instance.Reputations[0] >= 60)
            {
                azureHelp = 0.45f;
            }
            else if (GameHandler.Instance.Reputations[0] >= 30)
            {
                azureHelp = 0.35f;
            }
            else if (GameHandler.Instance.Reputations[0] > 0)
            {
                azureHelp = 0.25f;
            }
            else
            {
                azureHelp = 0.0f;
                Debug.Log("Azure reputation too low");
                choiceEnabled[2] = false;
            }
            if (GameHandler.Instance.Reputations[2] < 1)
            {
                choiceEnabled[1] = false;
            }
            choiceChance = new List<float>() { 0.0f, 0.25f, azureHelp, -1.0f };
            choiceReputation = new List<Dictionary<int, int>>()
            {
               new Dictionary<int, int>()
               {
                   { 2, 2 },
               },
               new Dictionary<int, int>()
               {
                   { 2, -5 }
               },
               new Dictionary<int, int>()
               {
                   { 0, -3 },
               },
               new Dictionary<int, int>()
               {
                   { 3, -5 }
               },
               new Dictionary<int, int>() // If failed
               {
                   { 3, -10 }
               }
            };

            choiceGold = new List<int>() { 0, 0, 0, 0, -stealAmount }; // 5th is fail
        }
    }


    private static Dictionary<int, Func<Event>> eventFactory = new Dictionary<int, Func<Event>>();

    static Events()
    {
        eventFactory.Add(1, () => new Brawl());
        eventFactory.Add(2, () => new Thief());
    }

    public static Event GetEvent(int eventID)
    {
        if (eventFactory.ContainsKey(eventID))
        {
            return eventFactory[eventID]();
        }
        else
        {
            Debug.LogWarning("Event with ID " + eventID + " not found.");
            return null;
        }
    }
}
