using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float EventChance = 20.0f; // Chance of an event happening every second
    private float timer = 0.0f;

    // NPC Stats
    // Factions: Azure, Refugees, Townsfolk, Rascals
    public int ID = 0;
    public string Name = "None";
    public int Alliance = 0;
    public bool Rascal = false; // Rascals hide as townsfolk
    public bool Patron = false;
    public bool Active = false;
    public float SickTime = 0;
    // Wealth : 0 = Poor, 1 = Moderate, 2 = Rich
    public float Wealth = 1;
    // Drunkness : 0 = Sober, 1 = Tipsy, 2 = Drunk, 3 = Passed Out
    public float Drunkness = 0;
    // Hunger : 0 = Full, 1 = Peckish, 2 = Hungry, 3 = Starving
    public float Hunger = 0;
    // BarMood : 0 = Happy, 1 = Content, 2 = Neutral, 3 = Unhappy, 4 = Angry
    public float BarMood = 2;
    // DrunkMoodSwing : 0 = Leaning towards Happy, 1 = Learning towards Angry
    public float DrunkMoodSwing = 0.67f;
    // Satisfaction (General satisfaction of Town's status) : 0 = Very Unhappy, 1 = Unhappy, 2 = Neutral, 3 = Happy, 4 = Very Happy
    public float Satisfaction = 2f;

    public Vector3 Position = new Vector3(0, 0, 0);
    public GameObject NPCObject;

    public void Initialize(NPC npc)
    {
        ID = npc.ID;
        Name = npc.Name;
        Rascal = npc.Rascal;
        Alliance = npc.Alliance;
        Patron = npc.Patron;
        Active = npc.Active;
        SickTime = npc.SickTime;
        Wealth = npc.Wealth;
        Drunkness = npc.Drunkness;
        Hunger = npc.Hunger;
        BarMood = npc.BarMood;
        DrunkMoodSwing = npc.DrunkMoodSwing;
        Satisfaction = npc.Satisfaction;
        Position = npc.Position;
        NPCObject = npc.NPCObject;

        switch (Alliance)
        {
            case 0:
                NPCObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 1:
                NPCObject.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 2:
                NPCObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case 3:
                NPCObject.GetComponent<Renderer>().material.color = Color.red;
                break;
        }

        NPCObject.name = "NPC" + ID.ToString();
    }

    void Start()
    {

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
                GameObject bubble = Instantiate(GameHandler.Instance.EventBubble, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z), Quaternion.identity);
                bubble.AddComponent<EventBubble>();
                bubble.SetActive(true);

            }
        }
    }
}
