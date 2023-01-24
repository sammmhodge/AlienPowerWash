using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public bool Clicked, rightClicked, leftCloser;
    public GameObject jet, testBall, jet2;
    private Color[] _colours;
    private Vector2 lastTouchPos;
    private bool touchedLastFrame;
    public float totalSoap, currentSoap, reloadAmount; 
    // Start is called before the first frame update
    void Start()
    {
        
        jet.SetActive(false);
        jet2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //variable declaration
        Vector3 mousePos = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Clicked = false;
        rightClicked = false;

        //checks if lmb down
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
        else touchedLastFrame = false;

        if (Input.GetMouseButtonDown(1))
        {
            altFire();
            Debug.Log("rmb pressed");
            rightClicked = true;
        }

        if (Clicked != true)
        {
            jet.SetActive(false);
            jet2.SetActive(false);
        }
        

    }



    private void Fire()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        //fires raycast
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            //checks for hitting an object with a rigidbody
            if (hit.rigidbody)
            {
                //Saves location that was hit in a variable
                Vector3 hitSpot = hit.point;

                //if at or halfway across the screen it will use the right jet otherwise it uses the left one
                if (Input.mousePosition.x >= 960)
                {
                    //Tells the cone to look at where was hit. This then rotates 180 to get it to face the right way
                    jet.transform.LookAt(hitSpot);
                    jet.transform.rotation *= Quaternion.Euler(0, 180, 0);

                    //selects the correct jet
                    jet.SetActive(true);
                    jet2.SetActive(false);

                    //calculates the distance between the 2 points, this is then used to apply as a scale for the size of the jet to hit the target and maintain a good and consistent size
                    Vector3 distanceRight = hit.point - jet.transform.position;
                    jet.transform.localScale = new Vector3(distanceRight.z / 2, distanceRight.z / 2, distanceRight.z);

                }
                else
                {
                    jet2.transform.LookAt(hitSpot);
                    jet2.transform.rotation *= Quaternion.Euler(0, 180, 0);

                    jet2.SetActive(true);
                    jet.SetActive(false);
                    Vector3 distanceLeft = hit.point - jet2.transform.position;
                    jet2.transform.localScale = new Vector3(distanceLeft.z / 2, distanceLeft.z / 2, distanceLeft.z);

                }

                Draw(hit);

            }

        }
        Debug.Log("Left Mouse  pressed 0");
        Clicked = true;
        return;
    }



    private void Draw(RaycastHit hit)
    {
        //obtaining variables needed for painting
        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCol = hit.collider as MeshCollider;
        //checks they exist
        if (rend == null || meshCol == null)
        {
            Debug.Log("retrningin");
            return;
        }

        //get the location to be painted

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        Debug.Log(tex);
        pixelUV.x *= tex.width;
        //Debug.Log(pixelUV.x);
        pixelUV.y *= tex.height;
        Color _colour = new Vector4(0, 0, 0, 0);
        _colours = Enumerable.Repeat(Color.clear, 5 * 5).ToArray();
        var x = (int)(pixelUV.x - (5 / 2));
        var y = (int)(pixelUV.y - (5 / 2));
        if (touchedLastFrame)
        {
            for (float f = 0.01f; f < 1.00f; f += 0.01f)
            {
                //Lerps the painting so that it makes a smooth paint 
                float distx = lastTouchPos.x - x;
                //Debug.Log(distx);
                float disty = lastTouchPos.y - y;
                Debug.Log(disty);
                if(distx >= -100f && distx <= 100f && disty >= -100f && disty <= 100f)
                {
                    var lerpx = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                    var lerpy = (int)Mathf.Lerp(lastTouchPos.y, y, f);

                    tex.SetPixels(lerpx, lerpy, 5, 5, _colours);
                }
                
                
                
                //tex.SetPixels((int)pixelUV.x, (int)pixelUV.y, 5, 5, _colours);
            }
            //paints
            tex.Apply();
        }
        lastTouchPos = new Vector2(x, y);
        touchedLastFrame = true;
        return;
    }


    private void altFire()
    {
        if(currentSoap < totalSoap)
        {
            currentSoap += reloadAmount;
        }
    }
}



