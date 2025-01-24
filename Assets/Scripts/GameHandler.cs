using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler Instance;

    // Stores various gameobjects for easy access and manipulation

    public GameObject EventBubble;
    public GameObject EventPopup;


    // Reputation values at start of game
    public int AzureReputation = -20;
    public int RefugeesReputation = -30;
    public int TownsfolkReputation = 80;
    public int RascalsReputation = -40;
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
