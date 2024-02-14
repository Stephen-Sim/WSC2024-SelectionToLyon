using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Models
{
    public class Graph
    {
        private long V;
        private List<long>[] adj;

        public Graph(long v)
        {
            V = v;
            adj = new List<long>[V];
            for (long i = 0; i < V; ++i)
            {
                adj[i] = new List<long>();
            }
        }

        public void AddEdge(long v, long w)
        {
            adj[v].Add(w);
        }

        private void DFSUtil(long u, long d, bool[] visited, List<long> path, List<List<long>> paths)
        {
            visited[u] = true;
            path.Add(u);

            if (u == d)
            {
                paths.Add(new List<long>(path));
            }
            else
            {
                foreach (long i in adj[u])
                {
                    if (!visited[i])
                    {
                        DFSUtil(i, d, visited, path, paths);
                    }
                }
            }

            path.RemoveAt(path.Count - 1);
            visited[u] = false;
        }

        public List<List<long>> FindAllPaths(long s, long d)
        {
            List<List<long>> paths = new List<List<long>>();

            bool[] visited = new bool[V];

            List<long> path = new List<long>();

            DFSUtil(s, d, visited, path, paths);

            return paths;
        }
    }
}
