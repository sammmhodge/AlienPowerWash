using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public bool Clicked, rightClicked, leftCloser;
    public GameObject jet, testBall, jet2;
    // Start is called before the first frame update
    void Start()
    {
        jet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Clicked = false;
        rightClicked = false;
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                if (hit.rigidbody)
                {
                    Vector3 distanceRight = hit.point - jet.transform.position;
                    Debug.Log("Right " + distanceRight);
                    Vector3 distanceLeft = hit.point - jet2.transform.position;
                    Debug.Log("Left " + distanceLeft);
                    Vector3 hitSpot = hit.point;
                    testBall.transform.position = hit.point;
                    if (distanceLeft.x >= distanceRight.x)
                    {
                        jet.transform.LookAt(hitSpot);
                        jet.transform.rotation *= Quaternion.Euler(0, 180, 0);


                        jet.transform.localScale = new Vector3(distanceRight.z / 2, distanceRight.z / 2, distanceRight.z);

                    }
                    else
                    {
                        jet2.transform.LookAt(hitSpot);
                        jet2.transform.rotation *= Quaternion.Euler(0, 180, 0);


                        jet2.transform.localScale = new Vector3(distanceLeft.z / 2, distanceLeft.z / 2, distanceLeft.z);

                    }

                }
                    
                    //Debug.Log(hit.point);
                    
                    
                    //Debug.Log(hit.rigidbody.tag);
            }
            Debug.Log("Left Mouse  pressed 0");
            Clicked = true;
        }

        if (Input.GetMouseButton(1))
        {
            Debug.Log("Right Mouse Pressed 1");
            rightClicked = true;
        }

        if (Clicked == true)
        {
            jet.SetActive(true);
        }
        else
        {
            jet.SetActive(false);
        }
        

    }
}
