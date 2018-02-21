using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfingScript : MonoBehaviour {

    CreateGrid gridScript;
    public GameObject seeker; //They call me the Seeker, I've been searching low n high
    public GameObject target; //What i'm after

    void Awake()
    {
        gridScript = GetComponent<CreateGrid>();
    }

    void Start()
    {
        FindPath(seeker.transform.position, target.transform.position);
    }

    void FindPath(Vector3 start, Vector3 target)
    {
        print("1");
        Node startNode = gridScript.NodeFromWorldPoint(start);
        print("2");
        Node targetNode = gridScript.NodeFromWorldPoint(target);

        print("3");

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost )
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in gridScript.GetNeighbours(currentNode))
            {
                if (!neighbour.traversable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);

                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = getDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }

            

        }





    }

    int getDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }

        return 14 * dstX + 10 * (dstY - dstX);

    }

    void RetracePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        gridScript.path = path;
    }

}
