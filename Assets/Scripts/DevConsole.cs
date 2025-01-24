using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DevConsole : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI ConsoleText;
    private bool on = false;

    void Start()
    {
        ConsoleText.gameObject.SetActive(on);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            on = !on;
            ConsoleText.gameObject.SetActive(on);
        }
    }
}
