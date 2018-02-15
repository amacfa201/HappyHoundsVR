using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {

    Node[,] nodeGrid;
    public Vector2 gridSize;
    public float nodeRadius;
    public LayerMask untraversableMask;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1 ,gridSize.y));
        if (nodeGrid != null)
        {
            foreach (Node n in nodeGrid)
            {
                //Gizmos.color = (n.traversable) ? Color.white : Color.red;
                //Gizmos.color = Color.red;

                if (!n.traversable)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(n.nodePos, Vector3.one * (nodeDiameter - 0.1f));
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawCube(n.nodePos, Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }
    }

    void Start()
    {
        //print("x = " + Vector3.right * gridSize.x / 2);
        //print("y = " + Vector3.forward * gridSize.y / 2);
        //print("z = " + Vector3.up * gridSize.z / 2);
        //print("BL = " + (transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2 - Vector3.up * gridSize.z / 2));
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        //gridSizeZ = Mathf.RoundToInt(gridSize.z / nodeDiameter);
       // CreateTheGrid();
        //StartCoroutine(OneTimeUpdate());
        InvokeRepeating("CreateTheGrid", 0.8f, 2f);
    }

    public void CreateTheGrid()
    {
        nodeGrid = new Node[gridSizeX, gridSizeY];
        //Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2  -Vector3.forward * gridSize.y / 2 -Vector3.up * gridSize.z/2;
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2; 
        //print("BL = " + worldBottomLeft);
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {

                Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
                    //Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.up * (j * nodeDiameter + nodeRadius) + Vector3.right * (k * nodeDiameter + nodeRadius);
                    bool traversable = !(Physics.CheckSphere(worldPoint, nodeRadius, untraversableMask));
                    nodeGrid[i, j] = new Node(traversable, worldPoint);
               
            }
        }
    }


    IEnumerator OneTimeUpdate() // Called a few seconds after game start to regenerate grid with the dome doors shut
    {
        yield return new WaitForSeconds(0.8f);
        CreateTheGrid();
    }

    
}
