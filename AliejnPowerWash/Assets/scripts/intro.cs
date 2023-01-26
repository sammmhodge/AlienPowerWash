using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadSceneAsync(1);
    }
}
