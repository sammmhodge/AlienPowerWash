using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panel, levelSelectPanel;
    public void play(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void LevelSelect()
    {
        panel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }
}
