using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panel;
    public void play()
    {
        //SceneManager.LoadScene(1);
        panel.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }
}
