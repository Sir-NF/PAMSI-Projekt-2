using System;
using System.Collections;
using System.Collections.Generic;

namespace SuperTurboHashTable {
    class Program {
        static void Main(string[] args) {
            
            while (true) {
                Console.WriteLine("1. HashTablice");
                Console.WriteLine("2. Grafy");
                int choice = int.Parse(Console.ReadLine());
                switch (choice) {
                    case 1:
                        int collisionSolveWay;
                        do {
                            Console.WriteLine("Podaj sposób wykrywania kolizji (1-3)");
                            Console.WriteLine("1. Linkowanie");
                            Console.WriteLine("2. Próbkowanie liniowe");
                            Console.WriteLine("3. Podwójne haszowanie");
                            collisionSolveWay = int.Parse(Console.ReadLine());
                        } while (Hasz.HashTable.CollisionSolveWayValid(collisionSolveWay) == false);

                        Hasz.HashTable hashTable = new Hasz.HashTable(collisionSolveWay);

                        Console.WriteLine("Podaj ilość elementów które chcesz wygenerować w HashTable: ");
                        int valueCount = int.Parse(Console.ReadLine());

                        for (int i = 0; i < valueCount; i++) {
                            hashTable.AddElement();
                        }

                        hashTable.PrintAllElementsAndIndexes();

                        bool inHashMenu = true;
                        while (inHashMenu) {
                            Console.WriteLine("1. Wyszukaj klucza po wartości");
                            Console.WriteLine("2. Wyszukaj wartości po kluczu");
                            Console.WriteLine("3. Dodaj element do tablicy");
                            Console.WriteLine("4. Usuń element z tablicy");
                            Console.WriteLine("5. Wyświetl wszystkie elementy");
                            Console.WriteLine("6. Powrót do poprzedniego menu");
                            int choose = int.Parse(Console.ReadLine());
                            int valueOrKey;
                            switch (choose) {
                                case 1:
                                    Console.WriteLine("Podaj wartość: ");
                                    valueOrKey = int.Parse(Console.ReadLine());
                                    hashTable.PrintKey(valueOrKey);
                                    break;
                                case 2:
                                    Console.WriteLine("Podaj klucz: ");
                                    valueOrKey = int.Parse(Console.ReadLine());
                                    hashTable.PrintValue(valueOrKey);
                                    break;
                                case 3:
                                    hashTable.AddElement();
                                    break;
                                case 4:
                                    Console.WriteLine("Podaj klucz: ");
                                    valueOrKey = int.Parse(Console.ReadLine());
                                    hashTable.RemoveElement(valueOrKey);
                                    break;
                                case 5:
                                    hashTable.PrintAllElementsAndIndexes();
                                    break;
                                case 6:
                                    inHashMenu = false;
                                    break;
                            }                            
                        }  
                        break; 
                        case 2:
                            bool inGraphMenu = true;
                            Graph graph = new Graph(new List < int > [] {
                                new List < int > () {
                                        1,
                                        3,
                                        6
                                    }, // successors of vertice 0
                                    new List < int > () {
                                        0,
                                        3
                                    }, // successors of vertice 1
                                    new List < int > () {
                                        5
                                    }, // successors of vertice 2
                                    new List < int > () {
                                        0,
                                        1
                                    }, // successors of vertice 3
                                    new List < int > () {}, // successors of vertice 4
                                    new List < int > () {
                                        2
                                    }, // successors of vertice 5
                                    new List < int > () {
                                        0
                                    } // successors of vertice 6
                            });
                            Console.WriteLine("Graf stworzony");
                            while (inGraphMenu) {
                                Console.WriteLine();
                                Console.WriteLine("1. Wygeneruj i wyświetl listę sąsiedztwa");
                                Console.WriteLine("2. Wygeneruj i wyświetl macierz sąsiedztwa");
                                Console.WriteLine("3. Wyświetl wszystkie krawędzie");
                                Console.WriteLine("4. Wyświetl wszystkie wierzchołki");
                                Console.WriteLine("5. Usuń wierzchołek");
                                Console.WriteLine("6. Usuń krawędź (kierunkowo)");
                                Console.WriteLine("7. Dodaj wierzchołek");
                                Console.WriteLine("8. Dodaj krawędź (kierunkowo)");
                                Console.WriteLine("9. Wyświetl sąsiednie wierzchołki");
                                Console.WriteLine("10. Powrót do poprzedniego menu");
                                int option = int.Parse(Console.ReadLine());
                                switch (option) {
                                    case 1:
                                        graph.PrintList();
                                        break;
                                    case 2:
                                        graph.PrintMatrix();
                                        break;
                                    case 3:
                                        graph.edges(graph);
                                        break;
                                    case 4:
                                        graph.vertices();
                                        break;
                                    case 5:
                                        Console.WriteLine("Podaj który wierzchołek chcesz usunąć: ");
                                        int value = int.Parse(Console.ReadLine());
                                        graph.RemoveVertex(value);
                                        break;
                                    case 6:
                                        Console.WriteLine("Podaj startowy wierzchołek: ");
                                        int u = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Podaj końcowy wierzchołek: ");
                                        int v = int.Parse(Console.ReadLine());
                                        graph.RemoveEdge(u, v);
                                        Console.WriteLine("Usunięto");
                                        break;
                                    case 7:
                                        List < int > [] vertices = new List < int > [graph.childNodesLength + 1];
                                        List < int > [] tempList = graph.GetChildNodes();
                                        for (int i = 0; i < tempList.Length; i++) {
                                            vertices[i] = tempList[i];
                                        }
                                        vertices[vertices.Length - 1] = new List < int > ();
                                        Graph newGraph = new Graph(vertices);
                                        graph = newGraph;
                                        Console.WriteLine("Wierzchołek dodano");
                                        break;
                                    case 8:
                                        Console.WriteLine("Podaj startowy wierzchołek: ");
                                        int p = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Podaj końcowy wierzchołek: ");
                                        int k = int.Parse(Console.ReadLine());
                                        graph.AddEdge(p, k);
                                        Console.WriteLine("Dodano");
                                        break;
                                    case 9:
                                        Console.WriteLine("Podaj wierzchołek");
                                        int vertex = int.Parse(Console.ReadLine());
                                        graph.PrintConnectedVertex(vertex);
                                        break;
                                    case 10:
                                        inGraphMenu = false;
                                        break;
                                }
                            }
                            break;

                }
            }
        }
    }
}