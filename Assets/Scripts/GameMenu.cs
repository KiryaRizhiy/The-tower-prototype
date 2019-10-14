using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    private static GameObject victoryPanel, defeaturePanel;

    // Start is called before the first frame update
    void Start()
    {
        victoryPanel = transform.GetChild(1).gameObject;
        defeaturePanel = transform.GetChild(2).gameObject;
    }

    public static void Victory()
    {
        victoryPanel.SetActive(true);
    }
    public static void Defeture()
    {
        defeaturePanel.SetActive(true);
    }
}
