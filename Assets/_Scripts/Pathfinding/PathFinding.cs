using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


    public static class PathFinding
    {
        private static List<Node> path = new List<Node>();
        private static List<Node> checkedNeighbours = new List<Node>();
        


        public static Node FindClosestNode(this List<List<Node>> nodes, Vector2 position)
        {
            Node closestNode = new Node(Vector2.positiveInfinity);
            float closestDistance = float.MaxValue;

            foreach (var nodeList in nodes)
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
        public static List<Node> FindPath(this Node myNode ,Node targetNode)
        {
            
            path.Add(myNode);
            
            return CheckNeighbours(myNode.Neighbours, targetNode);
        }

        private static List<Node> CheckNeighbours(List<Node> neighbours, Node targetNode)
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
