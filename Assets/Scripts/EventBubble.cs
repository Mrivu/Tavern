using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;

public class EventBubble : MonoBehaviour
{
    private GameHandler GameHandler;

    public float EventBaseChance = 100.0f; // Event success chance with no variables (execpt time)
    private float timer = 0.0f;

    void Start()
    {
        GameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        EventBaseChance -= Time.deltaTime * 3f;
        Debug.Log(EventBaseChance);
    }

    private void OnMouseDown()
    {
        Global.CameraFreeze = true;
        Debug.Log(EventBaseChance);
        Debug.Log("Event popup");
        GameHandler.EventPopup.SetActive(true);
        Destroy(gameObject);
    }

}
