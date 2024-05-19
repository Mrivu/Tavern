using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;

public class EventBubble : MonoBehaviour
{
    private GameHandler GameHandler;

    public float EventBaseChance = 1.0f; // Event success chance with no variables (execpt time)
    private float timer = 0.0f;

    void Start()
    {
        GameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        EventBaseChance -= Time.deltaTime * 0.03f;
        //Debug.Log(EventBaseChance);
    }

    private void OnMouseDown()
    {
        Global.CameraFreeze = true;
        Debug.Log(EventBaseChance);
        GameHandler.EventPopup.SetActive(true);

        // Slider only goes from 0.1 to 0.9
        GameHandler.EventPopup.GetComponent<EventHandler>().SetGrayArea(1f-EventBaseChance);
        Destroy(gameObject);
    }

}
