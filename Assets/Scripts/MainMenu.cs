using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static Dropdown levelSelector;
    private const int levelCount = 2;
    private List<string> levels;
    // Start is called before the first frame update
    void Start()
    {
        levelSelector = transform.GetChild(0).GetChild(0).GetComponent<Dropdown>();
        levels = new List<string>();
        for (int i = 1; i <= levelCount; i++)
        {
            levels.Add("Level" + i.ToString());
            levelSelector.options.Add(new Dropdown.OptionData("Level - " + i.ToString()));
        }
        levelSelector.value = 1;
        levelSelector.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(levels[levelSelector.value], LoadSceneMode.Single);
    }
}
