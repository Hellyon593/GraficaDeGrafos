using System;
using System.Collections.Generic;

namespace GraficaDeGrafos
{
    public class Grafo
    {
        private Dictionary<string, List<string>> adyacencia;

        public Grafo()
        {
            adyacencia = new Dictionary<string, List<string>>();
        }

        public void AgregarVertice(string vertice)
        {
            if (!adyacencia.ContainsKey(vertice))
            {
                adyacencia[vertice] = new List<string>();
            }
        }

        public void AgregarArista(string origen, string destino)
        {
            AgregarVertice(origen);
            AgregarVertice(destino);

            if (!adyacencia[origen].Contains(destino))
            {
                adyacencia[origen].Add(destino);
            }

            if (!adyacencia[destino].Contains(origen))
            {
                adyacencia[destino].Add(origen);
            }
        }

        public IEnumerable<string> ObtenerVertices()
        {
            return adyacencia.Keys;
        }

        public List<string>? ObtenerConexiones(string vertice)
        {
            if (adyacencia.ContainsKey(vertice))
            {
                return adyacencia[vertice];
            }

            return null;
        }

        public int CantidadVertices()
        {
            return adyacencia.Count;
        }

        public int CantidadAristas()
        {
            int total = 0;

            foreach (var conexiones in adyacencia.Values)
            {
                total += conexiones.Count;
            }

            return total / 2;
        }

        public void MostrarGrafo()
        {
            foreach (var vertice in adyacencia)
            {
                Console.Write($"{vertice.Key} -> ");

                if (vertice.Value.Count == 0)
                {
                    Console.WriteLine("Sin conexiones");
                }
                else
                {
                    Console.WriteLine(
                        string.Join(", ", vertice.Value));
                }
            }
        }
    }
}