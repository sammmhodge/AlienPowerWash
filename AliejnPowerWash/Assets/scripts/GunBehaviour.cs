using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunBehaviour : MonoBehaviour
{
    public bool Clicked, rightClicked, leftCloser;
    public GameObject jet, testBall, jet2, platform, monster;
    private Color[] _colours;
    private Vector2 lastTouchPos;
    private bool touchedLastFrame;
    public float turnSpeed, raiseSpeed, totalSoap, currentSoap, reloadAmount, soapUsed, rageReduced;
    float totalPixels = (1024 * 1024), maxPixels, currentProgress;
    public TMP_Text percentageText;
    public Image soapBar, lived;
    private float soapPercentage;

    // Start is called before the first frame update
    void Start()
    {
        lived.color = new Vector4(0, 0, 0, 0);
        Texture2D _tex = monster.GetComponent<Renderer>().material.mainTexture as Texture2D;
        //for(int x = 0; x < _tex.width; x++)
        //{
        //    for(int y = 0; y < _tex.height; y++)
        //    {

        //    }
        //}

        Color[] _colours = _tex.GetPixels(0, 0, 1024, 1024);
        for (int i = 0; i < _colours.Length; i++)
        {
            if (_colours[i].a == 0)
            {
                totalPixels--;
            }
        }
        jet.SetActive(false);
        jet2.SetActive(false);
        //Debug.Log(totalPixels);
        maxPixels = totalPixels;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(monster.transform);
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
            //Debug.Log("rmb pressed");
            rightClicked = true;
        }

        if (Clicked != true)
        {
            jet.SetActive(false);
            jet2.SetActive(false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
            turnRight();
        else if (Input.GetKey(KeyCode.LeftArrow))
            turnLeft();

        if (Input.GetKey(KeyCode.UpArrow))
            raisePlatform();
        else if (Input.GetKey(KeyCode.DownArrow))
            lowerPlatform();

        soapPercentage = currentSoap / totalSoap;
        soapBar.rectTransform.sizeDelta = new Vector2(1000 * soapPercentage, 100);

        //Debug.Log(currentProgress * 100 + "%");
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
                //Debug.Log(hit.transform.name);
                //Saves location that was hit in a variable
                Vector3 hitSpot = hit.point;
                //Debug.Log(Input.mousePosition);
                //if at or halfway across the screen it will use the right jet otherwise it uses the left one
                if (Input.mousePosition.x >= 960)
                {
                    //Tells the cone to look at where was hit. This then rotates 180 to get it to face the right way
                    jet.transform.LookAt(hitSpot);
                    jet.transform.rotation *= Quaternion.Euler(0, 180, 180);

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
                currentSoap -= soapUsed;
                if (currentSoap > 0)
                {
                    Draw(hit);
                }
                else if (currentSoap <= 0)
                {
                    monster.GetComponent<AlienBehaviour>().rageRemaining -= rageReduced;
                    Debug.Log(monster.GetComponent<AlienBehaviour>().rageRemaining);
                }
            }

        }
        //Debug.Log("Left Mouse  pressed 0");
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
            ////Debug.Log("retrningin");
            return;
        }

        //get the location to be painted

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        
       
        
        ////Debug.Log(tex);
        //Debug.Log(tex.width * tex.height);
        pixelUV.x *= tex.width;
        //Debug.Log(pixelUV.x);
        pixelUV.y *= tex.height;
        Color _colour = new Vector4(0, 0, 0, 0);
        _colours = Enumerable.Repeat(Color.clear, 10 *10).ToArray();
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
                //Debug.Log(disty);
                if(distx >= -100f && distx <= 100f && disty >= -100f && disty <= 100f)
                {
                    var lerpx = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                    var lerpy = (int)Mathf.Lerp(lastTouchPos.y, y, f);
                    Color[] _replacedColours = tex.GetPixels(lerpx, lerpy, 10, 10);
                    //Debug.Log(tex.GetPixels(lerpx, lerpy, 10, 10)[0]);
                    for (int i = 0; i < _replacedColours.Length; i++)
                    {
                        if (_replacedColours[i].a > 0.0f)
                        {
                            //Debug.Log(_replacedColours[i].a);
                            
                            totalPixels--;
                            //Debug.Log(totalPixels);
                        }
                        
                    }
                    tex.SetPixels(lerpx, lerpy, 10, 10, _colours);
                    
                }
                
                
                
                //tex.SetPixels((int)pixelUV.x, (int)pixelUV.y, 5, 5, _colours);
            }
            //paints
            tex.Apply();
            //Debug.Log(totalPixels);
            currentProgress = Mathf.InverseLerp(maxPixels, 0, totalPixels);
            int percentageProgress = (int)(currentProgress * 100);
            //Debug.Log(percentageProgress);
            percentageText.text = percentageProgress + "%";
            if(percentageProgress >= 75)
            {
                lived.color = new Vector4(1, 1, 1, 1);
            }
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
            if (currentSoap > totalSoap) currentSoap = totalSoap;
        }
    }

    private void turnRight()
    {
        platform.transform.Rotate(0, turnSpeed, 0);
    }
    
    private void turnLeft()
    {
        platform.transform.Rotate(0, turnSpeed * -1, 0);
    }

    private void raisePlatform()
    {
        platform.transform.position = new Vector3(platform.transform.position.x, platform.transform.position.y + raiseSpeed, platform.transform.position.z);
    }

    private void lowerPlatform()
    {
        platform.transform.position = new Vector3(platform.transform.position.x, platform.transform.position.y - raiseSpeed, platform.transform.position.z);
    }
}



