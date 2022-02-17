using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManager : MonoBehaviour
{
    [SerializeField]
    private int rows = 5;
    [SerializeField]
    private int columns = 5;
    [SerializeField]
    private float tileSize = 1;

    public static Color nodeColor;
    private Color[] nodeColors = new Color[] {Color.red, Color.blue, Color.yellow, Color.green};
    private Renderer rend;
    private List<Vector2> nodeTiles = new List<Vector2> { };
  
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void Awake()
    {
        
    }

    public void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("GridSquare"), transform);
        GameObject referenceNode = (GameObject)Instantiate(Resources.Load("Node"), transform);
        int i = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                int rand = UnityEngine.Random.Range(1, 37);
                if(rand % 4 == 0)
                {
                    GameObject node = (GameObject)Instantiate(referenceNode, transform);

                    float posX = c * tileSize;
                    float posY = r * -tileSize;

                    node.transform.position = new Vector2(posX, posY);
                    rend = node.GetComponent<Renderer>();
                    //nodeColor = nodeColors[UnityEngine.Random.Range(0, 4)];
                    nodeColor = nodeColors[i];
                    rend.material.color = nodeColor;
                    node.name = "Node " + r + "," + c;
                    if (i == 3)
                        i = 0;
                    else
                        i++;
                }
                else
                {
                    GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                    float posX = c * tileSize;
                    float posY = r * -tileSize;

                    tile.transform.position = new Vector2(posX, posY);
                    tile.name = "Tile " + r + "," + c;
                }
                
            }
        }
        Destroy(referenceTile);
        Destroy(referenceNode);

        float gridW = columns * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }

    public void deleteGrid()
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
