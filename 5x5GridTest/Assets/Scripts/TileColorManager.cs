using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorManager : MonoBehaviour
{
    public Renderer rend;
    public colorSelector colorSelector;

    private Color currColor;
    private Color combinedColor;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.white;
    }
    void OnMouseEnter()
    {
        currColor = rend.material.GetColor("_Color");
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (currColor == Color.white)
                rend.material.color = colorSelector.selectedColor;
            else
            {
                if ((currColor == Color.yellow && colorSelector.selectedColor == Color.blue) || (currColor == Color.blue && colorSelector.selectedColor == Color.yellow))
                {
                    rend.material.color = Color.green;
                }
                else
                {
                    combinedColor = Color.Lerp(currColor, colorSelector.selectedColor, 0.5f);
                    rend.material.color = combinedColor;
                }
            }
        }
        if (Input.GetMouseButton(1))
            rend.material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
