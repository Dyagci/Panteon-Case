using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private int penSize = 5;
    private Renderer renderer;
    private Color[] colors;
    public Camera cam;
    private RaycastHit hit;
    private Wall wall;
    [SerializeField]private Vector2 touchPos,lastTouchPos;
    private bool touchedLastFrame;
    // Start is called before the first frame update
    void Start()
    {
        //renderer = GetComponent<Renderer>();
        colors = Enumerable.Repeat(Color.red, penSize * penSize).ToArray();
        //tipHeight = tip.localScale.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // var Ray = cam.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(Ray, out hit))
           // {
           //     lastTouchPos = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            //}
        }
        else if (Input.GetMouseButton(0))
        {
            Draw();
        }
    }

    private void Draw()
    {
        var Ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(Ray,out hit))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                if (wall == null)
                {
                    wall = hit.transform.GetComponent<Wall>();
                }
                touchPos = new Vector2(hit.textureCoord.x, hit.textureCoord.y);

                var x = (int) (touchPos.x * wall.textureSize.x - (penSize / 2));
                var y = (int) (touchPos.y * wall.textureSize.y - (penSize / 2));
                if (y < 0 || y > wall.textureSize.y || x < 0 || x > wall.textureSize.x)
                {
                    return;
                }
                if (touchedLastFrame)
                {
                    if (x > 2010 || y > 2010 || x < 0 || y < 0)
                    {
                        return;
                    }
                    else
                    {
                        wall.texture.SetPixels(x,y,penSize,penSize,colors);
                    }
                    for (float f = 0.01f; f < 1.00f; f+=0.03f)
                    {
                        var lerpX = (int) Mathf.Lerp(lastTouchPos.x, x, f);
                        var lerpY = (int) Mathf.Lerp(lastTouchPos.y, y, f);
                        if (lerpX > 2010 || lerpY > 2010 || lerpX < 0 || lerpY < 0)
                        {
                            return;
                        }
                        else
                        {
                            Debug.Log(lerpX);
                            Debug.Log(lerpY);
                            wall.texture.SetPixels(lerpX,lerpY,penSize,penSize,colors);
                        }
                    }
                    wall.texture.Apply();
                }

                lastTouchPos = new Vector2(x, y);
                touchedLastFrame = true;
                return;
            }
            
        }

        wall = null;
        touchedLastFrame = false;
    }
}
