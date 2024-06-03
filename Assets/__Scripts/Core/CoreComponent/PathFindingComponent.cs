using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingComponent : CoreComponent
{
    public Node CurrentNode;
    public Vector2 Direction;
    public float RefreshPathCooldown = 0.3f;

    private float RefreshPathTimer;
    private List<Node> path = new List<Node>();
    private List<Node> checkedNeighbours = new List<Node>();

    private void Start()
    {
        RefreshPathTimer = Time.time;
        CurrentNode = FindClosestNode(gameObject);
        FindPath(CurrentNode, NodeGraph.Instance.PlayerNode);
    }

    public override void PhysicsUpdate()
    {
        if (Time.time > RefreshPathTimer + RefreshPathCooldown)
        {
            FindPath(CurrentNode, NodeGraph.Instance.PlayerNode);
            RefreshPathTimer = Time.time;
        }

        CurrentNode = FindClosetNodeInNeighbours(transform.position);
        Direction = ReturnLastNodeDirection();

    }

    // TODO: need to make this search algorithm faster by using Binary search
    public Node FindClosestNode(Vector2 position)
    {
        Node closestNode = new Node(Vector2.positiveInfinity);
        float closestDistance = float.MaxValue;

        foreach (var nodeList in NodeGraph.Instance.Nodes)
        {
            foreach (var node in nodeList)
            {
                float distance = Vector2.Distance(node.WorldPosition, position);
                if (closestDistance > distance)
                {
                    closestDistance = distance;
                    closestNode = node;
                }
            }
        }

        return closestNode;
    }

    public Node FindClosestNode(GameObject targetObject)
    {
        return FindClosestNode(gameObject.transform.position);
    }

    public Node FindClosetNodeInNeighbours(Vector2 position)
    {
        float closestDistance = Vector2.Distance(CurrentNode.WorldPosition, position);
        if (closestDistance < 0.45f) return CurrentNode;
        Node closestNode = CurrentNode;

        foreach (Node node in CurrentNode.Neighbours)
        {
            float distance = Vector2.Distance(node.WorldPosition, position);
            if (closestDistance > distance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    public Node FindClosestNodeInNodeGraph(Vector2 position)
    {
        Node closestNode = new Node(Vector2.positiveInfinity);
        float closestDistance = float.MaxValue;

        List<List<Node>> nodeList = NodeGraph.Instance.Nodes;
        int yIndex = nodeList.Count / 2;
        int xIndex = nodeList[0].Count / 2;

        while (nodeList.Count > 1)
        {
            if (position.y > nodeList[yIndex][xIndex].WorldPosition.y)
            {
                nodeList.RemoveRange(0, yIndex);
            }
            else
            {
                nodeList.RemoveRange(nodeList.Count - 1, yIndex);
            }

            if (position.x > nodeList[yIndex][xIndex].WorldPosition.x)
            {
                foreach (var nodeRow in nodeList)
                {
                    nodeRow.RemoveRange(0, xIndex);
                }
            }
            else
            {
                foreach (var nodeRow in nodeList)
                {
                    nodeRow.RemoveRange(nodeList[0].Count - 1, xIndex);
                }
            }


        }


        return closestNode;
    }

    public List<Node> FindPath(Node startingNode, Node targetNode)
    {
        List<Node> empty = new List<Node>();
        empty.Add(startingNode);
        Queue<Node> nodesToCheck = new Queue<Node>();

        HashSet<Node> visitedNodes = new HashSet<Node>();

        Dictionary<Node, Node> parentChildNodes = new Dictionary<Node, Node>();

        nodesToCheck.Enqueue(startingNode);
        visitedNodes.Add(startingNode);
        while (nodesToCheck.Count > 0)
        {
            Node currentNode = nodesToCheck.Dequeue();

            if (currentNode.Equals(targetNode))
            {
                return ReconstructPath(parentChildNodes, targetNode);
            }

            foreach (Node neighbour in currentNode.Neighbours)
            {
                if (!visitedNodes.Contains(neighbour))
                {
                    visitedNodes.Add(neighbour);
                    nodesToCheck.Enqueue(neighbour);
                    parentChildNodes.Add(neighbour, currentNode);
                }
            }
        }

        return null;
    }

    public Vector2 ReturnNextNodeDirection()
    {
        try
        {
            float x = path[0].WorldPosition.x - CurrentNode.WorldPosition.x;
            float y = path[0].WorldPosition.y - CurrentNode.WorldPosition.y;

            return new Vector2(x, y);
        }
        catch (Exception)
        {
        }

        return Vector2.zero;
    }

    public bool CheckIfNextNodeIsInAir()
    {
        try
        {
            if (path[0].TileType == TileType.InAir)
                return true;
            return false;
        }
        catch (Exception)
        {
        }

        return false;
    }

    public bool CheckIfTargetNodeIsHigher(Node target)
    {
        if (CurrentNode.WorldPosition.y < target.WorldPosition.y)
        {
            return true;
        }

        return false;
    }

    public Node ReturnNodeToJumpTo()
    {
        Node targetNode = path[0];
        bool foundNode = false;
        foreach (Node node in path)
        {
            if (foundNode)
            {
                targetNode = node;
                return targetNode;

            }

            if (targetNode.TileType == TileType.InAir)
            {
                targetNode = node;
            }
            else
            {
                foundNode = true;
            }
        }

        return targetNode;
    }

    public float JumpToNode(Node targetNode,Rigidbody2D rb)
    {
        if(targetNode == null) return 0 ;
        float horizontalDistance = MathF.Abs(CurrentNode.WorldPosition.x - targetNode.WorldPosition.x) /2;
        float verticalHeight = MathF.Abs(CurrentNode.WorldPosition.y - targetNode.WorldPosition.y) + 1;
        int FacingDirection = (CurrentNode.WorldPosition.x <= targetNode.WorldPosition.x) ? 1 : -1;
        
        float gravity = Physics.gravity.magnitude * rb.gravityScale;
        float verticalVelocity = Mathf.Sqrt(2 * gravity * verticalHeight);
        float timeToPeak = verticalVelocity / gravity;
        float horizontalVelocity = horizontalDistance / timeToPeak * FacingDirection;

        Vector3 jumpVelocity = new Vector3(horizontalVelocity , verticalVelocity, 0f);
        rb.velocity = jumpVelocity;
        return timeToPeak;
    }
    
        public Vector2 ReturnLastNodeDirection()
        {
            if (path.Count == 0) return Vector2.zero;
            Node targetNode = path[0];
            
            foreach (Node node in path)
            {
                if (targetNode.TileType == node.TileType)
                {
                    targetNode = node;
                }
                else
                {
                    break;
                }
                    
            }

            float x = Mathf.Clamp(targetNode.WorldPosition.x - CurrentNode.WorldPosition.x,-1,1);
            float y = Mathf.Clamp(targetNode.WorldPosition.y - CurrentNode.WorldPosition.y,-1,1);
            Vector2 direction = new Vector2(x, y);

            return direction;
        }
        private List<Node> ReconstructPath(Dictionary<Node, Node> parents, Node goal)
        {
            Node current = goal;
            List<Node> pathToGoal = new List<Node>();
            while (parents.ContainsKey(current))
            {
                pathToGoal.Insert(0,current);
                current = parents[current];
            }

            path = pathToGoal;
            return pathToGoal;
        }
        private List<Node> CheckNeighbours(List<Node> neighbours, Node targetNode)
        {
            if (path[^1].Equals(targetNode)) return path;

            foreach (var neighbour in neighbours)
            {
                if (neighbour.WorldPosition == targetNode.WorldPosition)
                {
                    path.Add(neighbour);
                    return path;
                }
                else if ((neighbour.TileType == TileType.aboveGround || neighbour.TileType == TileType.InAir) && !checkedNeighbours.Contains(neighbour))
                {
                    path.Add(neighbour);
                    checkedNeighbours.Add(neighbour);

                    CheckNeighbours(neighbour.Neighbours, targetNode);

                    if (path[^1].Equals(targetNode)) return path;
                }
                else
                {
                    checkedNeighbours.Add(neighbour);
                }


            }

            path.Remove(path[^1]);
            return path;
        }
        
    }

    public enum NodeDirection
    {
        Right,
        Left,
        Up,
        Down,
        None
    }
