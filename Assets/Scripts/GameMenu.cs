using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private static GameObject victoryPanel, defeaturePanel;
    public static int nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        victoryPanel = transform.GetChild(1).gameObject;
        defeaturePanel = transform.GetChild(2).gameObject;
        nextLevel = int.Parse(SceneManager.GetActiveScene().name[SceneManager.GetActiveScene().name.Length - 1].ToString()) + 1;
        Logger.AddContent(UILogDataTypes.SceneData, 
            "Scene: " + SceneManager.GetActiveScene().name + 
            ", levels count : " + Settings.levelsCount +
            ", next level : " + nextLevel);
    }
    public static void Victory()
    {
        victoryPanel.SetActive(true);
    }
    public static void Defeture()
    {
        defeaturePanel.SetActive(true);
    }
    public void NextLevel()
    {
        Logger.RemoveAllContent();
        if (nextLevel > Settings.levelsCount)
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        else
            SceneManager.LoadScene("Level" + nextLevel, LoadSceneMode.Single);
    }
    public void RestartLevel()
    {
        Logger.RemoveAllContent();
        SceneManager.LoadScene("Level" + (nextLevel - 1), LoadSceneMode.Single);
    }
    public void MainMenu()
    {
        Logger.RemoveAllContent();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
