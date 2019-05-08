using System;
using System.Collections.Generic;
public class Graph {
    private List < int > [] childNodes;

    public Graph(int size) {
        this.childNodes = new List < int > [size];
        for (int i = 0; i < size; i++) {
            // Assing an empty list of adjacents for each vertex
            this.childNodes[i] = new List < int > ();
        }
    }

    public List<int>[] GetChildNodes()
    {
        return childNodes;
    }

    public int childNodesLength
    {
        get
        {
            return childNodes.Length;
        }
    }

    public Graph(List < int > [] childNodes) {
        this.childNodes = childNodes;
    }


    public int Size {
        get {
            return this.childNodes.Length;
        }
    }


    public void AddEdge(int u, int v) {
        childNodes[u].Add(v);
    }

    public void AddVertex()
    {
        
    }

    public void RemoveVertex(int vertex)
    {
        if(vertex > childNodes.Length || vertex < 0)
        {
            Console.WriteLine("Nie znaleziono wierzchołka");
        } else 
        {
            for(int i = 0; i < childNodes.Length; i++)
            {
                for(int j = 0; j < childNodes[i].Count; j++)
                {
                    if(childNodes[i][j] == vertex)
                    {
                        childNodes[i].Remove(childNodes[i][j]);
                    }
                }
            }

            childNodes[vertex] = new List<int>();
        }
    }

    public void PrintConnectedVertex(int vertex)
    {
        if(vertex < childNodes.Length)
        {
            for(int i = vertex; i < vertex + 1; i++)
            {
                Console.Write(i);
                for(int j = 0; j < childNodes[i].Count; j++)
                {
                    Console.Write($" {childNodes[i][j]} ");
                }
                Console.WriteLine();
            }
        } else 
        {
            Console.WriteLine("Nie ma takiego wierzchołka");
        }

    }

    public void RemoveEdge(int u, int v) {
        childNodes[u].Remove(v);
    }

    public void PrintList(){
        for(int i = 0; i < childNodes.Length; i++)
        {
            Console.Write(i);
            for(int j = 0; j < childNodes[i].Count; j++)
            {
                Console.Write($" {childNodes[i][j]} ");
            }
            Console.WriteLine();
        }
    }

    public void PrintMatrix() {
        int[, ] matrix = new int[childNodes.Length, childNodes.Length];

        for (int i = 0; i < childNodes.Length; i++) {
            if (childNodes[i].Count != 0) {
                //Console.WriteLine($"Krawędź {i}: ");
                for (int j = 0; j < childNodes[i].Count; j++) {
                    matrix[i, childNodes[i][j]] = 1;
                }
            }
        }

        for (int i = 0; i < childNodes.Length; i++) {
            Console.WriteLine();
            for (int j = 0; j < childNodes.Length; j++) {
                Console.Write($" {matrix[i, j]}");
            }
        }
    }

    public void vertices() {
        for(int i = 0; i < childNodes.Length; i++)
        {
            Console.WriteLine(i);
        }
    }

    public void edges(Graph graph) {
        bool[] visited = new bool[graph.Size];
        for (int v = 0; v < graph.Size; v++) {
            if (!visited[v]) {
                Graph.TraverseDFS(v, visited, graph);
                Console.WriteLine();
            }
        }
    }

    public bool HasEdge(int u, int v) {
        bool hasEdge = childNodes[u].Contains(v);
        return hasEdge;
    }

    public IList < int > GetSuccessors(int v) {
        return childNodes[v];
    }

    public static void TraverseDFS(int v, bool[] visited, Graph graph) {
        if (!visited[v]) {
            Console.Write(v + " ");
            visited[v] = true;
            foreach(int child in graph.GetSuccessors(v)) {
                TraverseDFS(child, visited, graph);
            }
        }
    }
}