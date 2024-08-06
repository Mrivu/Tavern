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
        public List<int> choiceID;
        public List<float> choiceChance;
    }

    public class Event1 : Event
    {
        public Event1()
        {
            eventID = 1;
            eventText = "Stop Brawl";
            eventDescription = "Two customers started a Brawl!";
            baseSuccess = 0.8f;

            choices = new List<string>() { "Bribe", "Side with attacker", "Side with victim", "Kick from tavern" };
            choiceResultText = new List<string>() { "+10 success chance, costs gold", "+25 success chance, the victim won't be happy", "+25 success chance, the attacker won't be happy", "+45 success chance, potentially lose customers" };
            choiceID = new List<int>() { 1, 2, 3, 4 };
            choiceChance = new List<float>() { 0.1f, 0.25f, 0.25f, 0.45f };
        }
    }

    private static Dictionary<int, Event> eventDictionary;

    static Events()
    {
        eventDictionary = new Dictionary<int, Event>();
        AddEvent(new Event1());
    }

    public static void AddEvent(Event newEvent)
    {
        if (eventDictionary.ContainsKey(newEvent.eventID))
        {
            Debug.LogWarning("Event with ID " + newEvent.eventID + " already exists.");
        }
        else
        {
            eventDictionary[newEvent.eventID] = newEvent;
        }
    }

    public static Event GetEvent(int eventID)
    {
        Event foundEvent;
        if (eventDictionary.TryGetValue(eventID, out foundEvent))
        {
            return foundEvent;
        }
        else
        {
            Debug.LogWarning("Event with ID " + eventID + " not found.");
            return null;
        }
    }
}
