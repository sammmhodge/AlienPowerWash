using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienBehaviour : MonoBehaviour
{
    public float rageTotal, rageRemaining, rageLossPerSecond;
    public Image rageBar, dead;
   
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
}
