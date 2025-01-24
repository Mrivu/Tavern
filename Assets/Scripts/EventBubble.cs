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
        Events.Event @event = Events.GetEvent(Random.Range(1,3));
        GameHandler.EventPopup.GetComponent<EventHandler>().Activate((2f-@event.baseSuccess - ChanceLostToTime), @event);
        Destroy(gameObject);
    }

}
