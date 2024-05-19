using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Handles NPC behavior
    private GameHandler GameHandler;

    public float EventChance = 5.0f; // Chance of an event happening every second
    private float timer = 0.0f;

    void Start()
    {
        GameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check Event
        timer += Time.deltaTime; 

        if (timer >= 1.0f) 
        {
            timer = 0.0f;

            if (Random.Range(0.0f, 100.0f) < EventChance)
            {
                //Debug.Log("Event happened!");
                GameObject bubble = Instantiate(GameHandler.EventBubble, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z), Quaternion.identity);
                bubble.AddComponent<EventBubble>();
                bubble.SetActive(true);

            }
        }
    }
}
