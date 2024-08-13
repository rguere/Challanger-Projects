using System;
using System.Collections.Generic;

class MainClass
{
    public static string GraphChallenge(string[] strArr)
    {
        // Número de nodos
        int N = int.Parse(strArr[0]);

        // Primer nodo
        string startNode = strArr[1];

        // Último nodo
        string endNode = strArr[N];

        // El camino más corto
        string varOcg = "-1";
        var graph = new Dictionary<string, List<string>>();

        // Construyendo el grafo (asegúrate de agregar todos los nodos al diccionario)
        for (int i = 1; i <= N; i++)
        {
            graph[strArr[i]] = new List<string>();
        }

        // Se agregan las conexiones
        for (int i = N + 1; i < strArr.Length; i++)
        {
            string[] nodes = strArr[i].Split('-');

            // Asegúrate de que ambos nodos existan en el diccionario antes de agregar la conexión
            if (!graph.ContainsKey(nodes[0]))
            {
                graph[nodes[0]] = new List<string>();
            }
            if (!graph.ContainsKey(nodes[1]))
            {
                graph[nodes[1]] = new List<string>();
            }

            graph[nodes[0]].Add(nodes[1]);
            graph[nodes[1]].Add(nodes[0]);
        }

        // BFS para encontrar el camino más corto
        var queue = new Queue<Tuple<string, List<string>>>();
        var visited = new HashSet<string>();
        queue.Enqueue(new Tuple<string, List<string>>(startNode, new List<string> { startNode }));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            string currentNode = current.Item1;
            List<string> path = current.Item2;

            if (currentNode == endNode)
            {
                // __define-ocg__: Encontramos el camino más corto
                varOcg = string.Join("-", path);
                return varOcg;
            }

            foreach (string neighbor in graph[currentNode])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    var newPath = new List<string>(path) { neighbor };
                    queue.Enqueue(new Tuple<string, List<string>>(neighbor, newPath));
                }
            }
        }

        // Devolverá -1 si no encuentra un camino
        return varOcg;
    }

    static void Main()
    {
        // Define el arreglo de entrada directamente en el código
        string[] inputArray = new string[] { "5", "A", "B", "C", "D", "F", "A-B", "A-C", "B-C", "C-D", "D-F" };

        // Llama al método con el arreglo y muestra el resultado
        Console.WriteLine(GraphChallenge(inputArray));
    }
}