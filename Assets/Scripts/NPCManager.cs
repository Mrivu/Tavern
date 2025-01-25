using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    // Patron Stats
    private int Population = 10;

    public List<NPC> NPCs = new List<NPC>();

    // NPC'S
    private class NPC1 : NPC
    {
        public NPC1()
        {
            ID = 0;
            Name = "Harry";
            Alliance = 0;
            Rascal = false;
            Patron = true;
            Active = true;
            SickTime = 0;
            Wealth = 1.4f;
            Drunkness = 0;
            Hunger = 0;
            BarMood = 2;
            DrunkMoodSwing = 0.67f;
            Satisfaction = 2f;
            Position = new Vector3(5, 0, 0);
            NPCObject = null;
        }
    }

    private class NPC2 : NPC
    {
        public NPC2()
        {
            ID = 1;
            Name = "Sally";
            Alliance = 2;
            Rascal = false;
            Patron = true;
            Active = true;
            SickTime = 0;
            Wealth = 0.8f;
            Drunkness = 0;
            Hunger = 0;
            BarMood = 2;
            DrunkMoodSwing = 0.37f;
            Satisfaction = 2f;
            Position = new Vector3(10, 0, 8);
            NPCObject = null;
        }
    }


    void Start()
    {
        NPCs.Add(new NPC1());
        NPCs.Add(new NPC2());
        LoadNPCs();
    }

    void LoadNPCs()
    {
        for (int i = 0; i < NPCs.Count; i++)
        {
            NPC npc = NPCs[i];
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = npc.Position;
            npc.NPCObject = cube;
            NPC npcBehavior = cube.AddComponent<NPC>();
            npcBehavior.Initialize(npc);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
