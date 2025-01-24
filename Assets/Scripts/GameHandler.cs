using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler Instance;

    // Stores various gameobjects for easy access and manipulation

    public GameObject EventBubble;
    public GameObject EventPopup;

    // Patron Stats
    private static int Population = 0;
    private static int AvailableCustomers = 0;
    private static int ActivePatrons = 0;
    private static int Resting = 0;
    private static int Sick = 0;

    // Reputation values at start of game
    private static int AzureReputation = -20;
    private static int RefugeesReputation = -30;
    private static int TownsfolkReputation = 80;
    private static int RascalsReputation = -40;
    // Reputation values (can be edited)
    public List<int> Reputations = new List<int>();

    // Get Gameobjects
    private void Awake()
    {
        Reputations = new List<int>() { AzureReputation, RefugeesReputation, TownsfolkReputation, RascalsReputation };

        // Ensure there's only one instance of GameHandler
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keep GameHandler alive across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy any duplicates
        }
        
        EventBubble = GameObject.Find("EventBubbleInstance");
        EventPopup = GameObject.Find("EventCanvas");
    }

    // Hide Gameobjects
    void Start()
    {
        EventBubble.SetActive(false);
        EventPopup.SetActive(false);
    }
}
