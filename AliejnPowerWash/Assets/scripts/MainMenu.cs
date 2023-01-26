using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject panel, levelSelectPanel, mainCam;
    public Vector3 startPos;
    public Quaternion startRot;

    private void Start()
    {
        startPos = mainCam.transform.position;
        startRot = mainCam.transform.rotation;
    }
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

    public void Credits()
    {
        mainCam.transform.position = new Vector3(352, 70, 516);
        mainCam.transform.eulerAngles = new Vector3 (0, -23, 0);
        StartCoroutine(resetCam());
    }

    IEnumerator resetCam()
    {
        yield return new WaitForSeconds(10f);
        mainCam.transform.position = startPos;
        mainCam.transform.eulerAngles = new Vector3(startRot.x,startRot.y,startRot.z);
    }
}
