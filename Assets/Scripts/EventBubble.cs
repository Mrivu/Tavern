using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;

public class EventBubble : MonoBehaviour
{
    private GameHandler GameHandler;

    public float ChanceLostToTime = 1.0f; // Event time chance

    void Start()
    {
        GameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        ChanceLostToTime -= Time.deltaTime * 0.03f;
        //Debug.Log(EventBaseChance);
    }

    private void OnMouseDown()
    {
        Global.CameraFreeze = true;
        Debug.Log(ChanceLostToTime);
        GameHandler.EventPopup.SetActive(true);

        // Slider only goes from 0.1 to 0.9
        GameHandler.EventPopup.GetComponent<EventHandler>().Activate((2f-Events.GetEvent(1).baseSuccess - ChanceLostToTime), Events.GetEvent(1));
        Destroy(gameObject);
    }

}
