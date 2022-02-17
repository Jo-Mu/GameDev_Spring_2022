using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorSelector : MonoBehaviour
{
    public static Color selectedColor;
    private Color[] colors = new Color[] { Color.white, Color.red, Color.blue, Color.yellow, Color.green};
    private int arrayPos = 0;
    private GameObject tile;
    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        selectedColor = Color.white;
        tile = (GameObject)Instantiate(Resources.Load("GridSquare"), transform);
        tile.transform.localPosition = new Vector3(0, 0, 0);
        rend = tile.GetComponent<Renderer>();
    }

    private void colorSelect()
    {
        switch(arrayPos)
        {
            case 0:
                selectedColor = colors[0];
                rend.material.color = selectedColor;
                break;
            case 1:
                selectedColor = colors[1];
                rend.material.color = selectedColor;
                break;
            case 2:
                selectedColor = colors[2];
                rend.material.color = selectedColor;
                break;
            case 3:
                selectedColor = colors[3];
                rend.material.color = selectedColor;
                break;
            case 4:
                selectedColor = colors[4];
                rend.material.color = selectedColor;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //scroll sometimes doesnt work
        if(Input.mouseScrollDelta.y  < 0)
        {
            if(arrayPos < colors.Length)
            {
                arrayPos += 1;
                colorSelect();
            }
            else
            {
                arrayPos = 0;
                colorSelect();
            }
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            if (arrayPos != 0)
            {
                arrayPos -= 1;
                colorSelect();
            }
            else
            {
                arrayPos = 3;
                colorSelect();
            }
        }
    }
}
