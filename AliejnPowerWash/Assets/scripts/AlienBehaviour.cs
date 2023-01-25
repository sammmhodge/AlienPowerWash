using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class AlienBehaviour : MonoBehaviour
{
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
        //Debug.Log(rand);

        if (rand == 5)
        {
            if(!isActive)
            {
                StartCoroutine(timer());
                isActive = true;
            }

        }

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

    private IEnumerator timer()
    {
        anim.SetBool("idle", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("idle", false);
        isActive = false;
    }
}
