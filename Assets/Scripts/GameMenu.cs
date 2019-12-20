using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private static GameObject victoryPanel, defeaturePanel;
    private static Joystick joystick;
    private static Transform menuTransform;
    public static int nextLevel;
    public static Vector2 MoveControlCoordinates
    {
        get
        {
            //if (joystick.Direction != Vector2.zero)
            //    Logger.AddContent(UILogDataTypes.PressedButton, "Joystick diraction: " + joystick.Direction, true);
            //else
            //    Logger.AddContent(UILogDataTypes.PressedButton, "Joystick not used now", true);
            Vector2 _anchPoint = menuTransform.GetChild(3).GetChild(2).GetComponent<RectTransform>().anchoredPosition;
            return new Vector2(Screen.width - _anchPoint.x, _anchPoint.y);
            //return joystick.Direction;
        }
    }
    public static float MoveControlRadius
    {
        get
        {
            return menuTransform.GetChild(3).GetChild(2).GetComponent<RectTransform>().rect.height;
        }
    }
    public static Vector2 joystickDiraction
    {
        get
        {
            return joystick.Direction;
        }
    }
    public static bool jump
    {
        get
        {
            if (_jump)
            {
                _jump = false;
                return true;
            }
            else
                return false;
        }
        private set
        {
            _jump = value;
        }
    }
    private static bool _jump;

    // Start is called before the first frame update
    void Start()
    {
        victoryPanel = transform.GetChild(1).gameObject;
        defeaturePanel = transform.GetChild(2).gameObject;
        menuTransform = transform;
        joystick = menuTransform.GetChild(3).GetChild(4).GetComponent<FixedJoystick>();
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
    public void Jump()
    {
        jump = true;
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
