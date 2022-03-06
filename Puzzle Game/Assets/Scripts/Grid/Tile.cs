using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{


   

   
    private ColorEnum colorIdentity;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject lineObjectPrefab;


    private GridManager gridManager; 

    private GameObject spawnedLineObject;

    private int xId; 
    private int yId;
    [SerializeField]
    private bool inUse = false; //flag to determine if its part of a connection already 


    //experimental linkedlist data structure technique to see if I can validate connections
    [SerializeField]
    private GameObject prevTile;


    void Update()
    {
       
    }
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    public void Init() 
    {
        AssignColorIdentity();
    }

    private void AssignColorIdentity()
    {
        int randNum = Random.Range(0, 5);

        switch (randNum)
        {
            case 0:
                colorIdentity = ColorEnum.RED;
                spriteRenderer.color = Color.red;
                break;

            case 1:
                colorIdentity = ColorEnum.BLUE;
                spriteRenderer.color = Color.blue;
                break;

            case 2:
                colorIdentity = ColorEnum.GREEN;
                spriteRenderer.color = Color.green;
                break;

            case 3:
                colorIdentity = ColorEnum.NONE;
                spriteRenderer.color = Color.black;
                break;

            case 4:
                colorIdentity = ColorEnum.NONE;
                spriteRenderer.color = Color.black;
                break;

            default:
                Debug.Log("Default case reached in Tile class");
                colorIdentity = ColorEnum.NONE;
                spriteRenderer.color = Color.black;
                break;
        }
    }
   
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        
        //if placed near a tile that is not a neighboring tile 
        if (col.gameObject.CompareTag("LineHead"))
        {
             if(!IsFromNeighborTile(col.gameObject))
             {
                Debug.Log("not from a neighbor tile");
               return;
              }   
        }

        if (colorIdentity == ColorEnum.NONE)
        {
           //Debug.Log("This is tile: " + gameObject.name);
           // Debug.Log("This is col: " + col.gameObject.transform.parent.name);
            if (col.gameObject.CompareTag("LineHead"))
            {
             
                if(prevTile == null)
                {
                    prevTile = col.gameObject.transform.parent.gameObject.transform.parent.gameObject; //somehow this works...
                }


                SnapToPosition(col.gameObject);
                gridManager.AddConnectedTiles(this.gameObject);
                if (spawnedLineObject == null)
                {
                    CreateLineObject();
                }
            }                
        }
       

       //if placed next to a color tile
       if(colorIdentity != ColorEnum.NONE)
        {
            if(col.gameObject.CompareTag("LineHead"))
            {
                //send to gridmanager to determine if its a connection
                //if it is a valid connection(bool method?), snap to position. And remove all line head in that list and reset the list ?
                var firstTile = gridManager.getFirstConnectedTile();
                if(GameObject.ReferenceEquals(firstTile, this.gameObject))
                {
                    return; //this is when the lineobject hits itself
                }


                if(firstTile.GetComponent<Tile>().GetTileColorIdentity() == GetTileColorIdentity())
                {
                    //why does the tile class have info on when a connection is made, time to refactor 


                    SnapToPosition(col.gameObject);
                    
                    gridManager.AddConnectedTiles(this.gameObject);
                    //alert gridmanger about connection 
                    gridManager.ValidateConnection();
                }
            }
        } 


    }
    private void CreateLineObject()
    {
      
        spawnedLineObject = Instantiate(lineObjectPrefab, transform.position, Quaternion.identity);
        spawnedLineObject.name = name + " lineObject";
        spawnedLineObject.transform.parent = transform;

        var Line = spawnedLineObject.gameObject.GetComponentInChildren<Line>();

        if (Line != null)
            Line.SetLineID(GetXID(), GetYID());     
    }

    public void DestroyLineObject()
    {
       
        if (spawnedLineObject != null)
        {
            Destroy(spawnedLineObject);
        }
    }

    //simple solution, a neighboring tile is a tile that has x and y value within a range of 1 
    private bool IsFromNeighborTile(GameObject lineObject)
    {
        var Line = lineObject.GetComponent<Line>();
        if (Line != null)
        {
           int lnx = Line.GetLineXID();
           int lny = Line.GetLineYID();

            //no reason to check same value because OnTriggerEnter2D will cover it
            if ((lnx > GetXID() + 1) || (lnx < GetXID() - 1))
            {
               
                return false;
            }
              

            if ((lny > GetYID() + 1) || (lny < GetYID() - 1))
            {               
                return false;
            }
        } 
        else
        {
            Debug.Log("false cuz null in Tile class ");
            return false;   
        }

       
        return true;
    }


    private void SnapToPosition(GameObject lineObject)
    {
        var lr = lineObject.gameObject.transform.parent.GetComponentInChildren<LineRenderer>();
        var Line = lineObject.gameObject.GetComponent<Line>();
        Line.DisableDrag();
        lineObject.gameObject.transform.position = transform.position;
        lr.SetPosition(2, new Vector3(lineObject.gameObject.transform.localPosition.x, lineObject.gameObject.transform.localPosition.y, 0f));
    }


    private void OnMouseDown()
    {
        if ((spawnedLineObject == null) && colorIdentity != ColorEnum.NONE)
        {
           
            gridManager.AddConnectedTiles(this.gameObject);
            CreateLineObject();
        }          
    }
  

    //GETTERS AND SETTERS 
    public ColorEnum GetTileColorIdentity()
    {
        return colorIdentity;
    }

    public int GetXID()
    {
        return xId;
    }

    public int GetYID()
    {
        return yId;
    }

    public void SetTileId(int xId, int yId)
    {
        this.xId = xId;
        this.yId = yId;
    }

    public bool IsInUse()
    {
        return inUse;
    }

    public void SetInUse(bool boolParam)
    {
        inUse = boolParam;
    }

    public GameObject GetPrevTile()
    {
        return prevTile;
    }

}
