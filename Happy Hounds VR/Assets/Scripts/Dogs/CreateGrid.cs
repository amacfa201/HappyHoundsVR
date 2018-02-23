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
    public Transform player;
    public bool showGrid;

    public List<Node> path;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateTheGrid();
    }

    public int MaxSize {
        get { return gridSizeX * gridSizeY; }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1 ,gridSize.y));
        #region OG Gizmos
        if (showGrid)
        {
            if (nodeGrid != null)
            {
                Node playerNode = NodeFromWorldPoint(player.position);

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

                    if (playerNode == n)
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawCube(n.nodePos, Vector3.one * (nodeDiameter - 0.1f));
                    }
                    if (path != null)
                    {
                        if (path.Contains(n))
                        {
                            Gizmos.color = Color.black;
                            Gizmos.DrawCube(n.nodePos, Vector3.one * (nodeDiameter - 0.1f));
                        }
                    }

                }
            }
        }
        #endregion
    }

    void Update()
    {
        //print("x = " + Vector3.right * gridSize.x / 2);
        //print("y = " + Vector3.forward * gridSize.y / 2);
        //print("z = " + Vector3.up * gridSize.z / 2);
        //print("BL = " + (transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2 - Vector3.up * gridSize.z / 2));
        
        //gridSizeZ = Mathf.RoundToInt(gridSize.z / nodeDiameter);
       //CreateTheGrid();
        //StartCoroutine(OneTimeUpdate());
        //InvokeRepeating("CreateTheGrid", 0.8f, 2f);
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
    
                    bool traversable = !(Physics.CheckSphere(worldPoint, nodeRadius, untraversableMask));
                    nodeGrid[i, j] = new Node(traversable, worldPoint, i,j);
               
            }
        }
    }


    IEnumerator OneTimeUpdate() // Called a few seconds after game start to regenerate grid with the dome doors shut
    {
        yield return new WaitForSeconds(0.8f);
        CreateTheGrid();
    }


    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x - transform.position.x) / gridSize.x + 0.5f - (nodeRadius / gridSize.x);
        float percentY = (worldPosition.z - transform.position.z) / gridSize.y + 0.5f - (nodeRadius / gridSize.y);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return nodeGrid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(nodeGrid[checkX, checkY]);
                }

            }
        }
        return neighbours;
    }

    
}
