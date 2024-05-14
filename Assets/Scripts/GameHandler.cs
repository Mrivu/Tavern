using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Stores various gameobjects for easy access and manipulation

    public GameObject EventBubble;
    public GameObject EventPopup;

    // Get Gameobjects
    private void Awake()
    {
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
