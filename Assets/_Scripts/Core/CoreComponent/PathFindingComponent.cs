using System;
using System.Collections.Generic;
using UnityEngine;

    public class PathFindingComponent : CoreComponent
    {
        public float UpdateCurrentNodeCooldown = 0.01f;
        public Node currentNode;
        private float UpdateCurrentNodeTimer;
        private List<Node> path = new List<Node>();
        private List<Node> checkedNeighbours = new List<Node>();

        private void Start()
        {
            currentNode = FindClosestNode(gameObject);
            UpdateCurrentNodeTimer = Time.time;
        }

        public override void PhysicsUpdate()
        {
            currentNode = FindClosetNodeInNeighbours(transform.position);
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

        public Node FindClosetNodeInNeighbours(Vector2 position)
        {
            float closestDistance = Vector2.Distance(currentNode.WorldPosition, position);
            if (closestDistance < 0.45f) return currentNode;
            Node closestNode = currentNode;
            
            foreach (Node node in currentNode.Neighbours)
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
                    nodeList.RemoveRange(0,yIndex);
                }
                else
                {
                    nodeList.RemoveRange(nodeList.Count-1,yIndex);
                }

                if (position.x > nodeList[yIndex][xIndex].WorldPosition.x)
                {
                    foreach (var nodeRow in nodeList)
                    {
                        nodeRow.RemoveRange(0,xIndex);
                    }
                }
                else
                {
                    foreach (var nodeRow in nodeList)
                    {
                        nodeRow.RemoveRange(nodeList[0].Count-1,xIndex);
                    }
                }
                   
                
            }

            return closestNode;
        }
        public Node FindClosestNode(GameObject targetObject)
        {
            return FindClosestNode(gameObject.transform.position);
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
                        parentChildNodes.Add(neighbour,currentNode);
                    }
                }
            }
            return null;
        }
        

        public Vector2 ReturnNextNodeDirection()
        {
            try
            {
                float x = path[0].WorldPosition.x - currentNode.WorldPosition.x;
                float y = path[0].WorldPosition.y - currentNode.WorldPosition.y;
                
                return new Vector2(x, y);
                
                /*if (x == -1)return NodeDirection.Left;
                if (x == 1) return NodeDirection.Right;
                if (y == 1) return NodeDirection.Up;
                if (y == -1) return NodeDirection.Down;*/
            }
            catch (Exception) { }
            
            return Vector2.zero;
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
