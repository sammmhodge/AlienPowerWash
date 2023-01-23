using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = transform.GetComponent<Renderer>();
        Texture2D tempText = Instantiate(rend.material.mainTexture as Texture2D);
        rend.material.mainTexture = tempText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
