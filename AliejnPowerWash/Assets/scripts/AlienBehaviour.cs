using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class AlienBehaviour : MonoBehaviour
{
    //1 - frog | 2 - Tall boi | 3 - busty babe
    public enum alienChoice
    {
        TutorialFrog,
        Frog,
        Lizard,
        boobs
    }
    public alienChoice alien;
    
    public float rageTotal, rageRemaining, rageLossPerSecond;
    public Image rageBar, dead;
    public Animator anim;
    bool isActive;
    private float timePassed, timeGoal= 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        dead.color = new Vector4(0, 0, 0, 0);
        rageRemaining = rageTotal;
        Renderer rend = transform.GetComponent<Renderer>();
        Texture2D tempText = Instantiate(rend.material.mainTexture as Texture2D);
        rend.material.mainTexture = tempText;
    }

    // Update is called once per frame
    void Update()
    {
        int rand = Random.Range(0, 1000);
        switch (alien)
        {
            case alienChoice.TutorialFrog:

                break;
            case alienChoice.Frog:
                break;
            case alienChoice.Lizard:
                if (rand == 5)
                {
                    if (!isActive)
                    {
                        StartCoroutine(timerLizard());
                        isActive = true;
                    }

                }
                break;
            case alienChoice.boobs:
                break;
        }
        
        //Debug.Log(rand);

        

        if (timePassed >= timeGoal)
        {
            rageRemaining -= rageLossPerSecond;
            timePassed = 0.0f;
        }
        else timePassed += Time.deltaTime;

        if(rageRemaining <= 0)
        {
            dead.color = new Vector4(1, 1, 1, 1);
            
        }
        rageBar.rectTransform.sizeDelta = new Vector2(1000 * (rageRemaining / rageTotal), 100);
    }

    private IEnumerator timerLizard()
    {
        anim.SetBool("idle", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("idle", false);
        isActive = false;
    }
}
