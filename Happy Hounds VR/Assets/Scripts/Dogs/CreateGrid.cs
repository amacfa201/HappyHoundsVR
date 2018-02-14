using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {

    Node[,,] nodeGrid;
    public Vector3 gridSize;
    public float nodeRadius;
    public LayerMask untraversableMask;
    float nodeDiameter;
    int gridSizeX, gridSizeY, gridSizeZ;
    

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, gridSize.z));
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
                //else
                //{
                    //Gizmos.color = Color.white;
                   // Gizmos.DrawCube(n.nodePos, Vector3.one * (nodeDiameter - 0.1f));
                //}
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
        gridSizeZ = Mathf.RoundToInt(gridSize.z / nodeDiameter);
        CreateTheGrid();
    }

    void CreateTheGrid()
    {
        nodeGrid = new Node[gridSizeX, gridSizeY, gridSizeZ];
        //Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2  -Vector3.forward * gridSize.y / 2 -Vector3.up * gridSize.z/2;
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.up * gridSize.y / 2 - Vector3.forward * gridSize.z / 2;
        //print("BL = " + worldBottomLeft);
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                for (int k = 0; k < gridSizeZ; k++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.up * (j * nodeDiameter + nodeRadius) + Vector3.forward * (k * nodeDiameter + nodeRadius);
                    //Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.up * (j * nodeDiameter + nodeRadius) + Vector3.right * (k * nodeDiameter + nodeRadius);
                    bool traversable = !(Physics.CheckSphere(worldPoint, nodeRadius, untraversableMask));
                    nodeGrid[i, j, k] = new Node(traversable, worldPoint);
                }
            }
        }
    }

    
}
