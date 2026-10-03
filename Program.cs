using System;
using System.Diagnostics;
using System.IO;

namespace GraficaDeGrafos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Gráfica de Grafos";

            Console.WriteLine("==============================================");
            Console.WriteLine("        SISTEMA DE GRÁFICA DE GRAFOS");
            Console.WriteLine("==============================================");
            Console.WriteLine();

            string archivo = "datos.txt";

            if (!File.Exists(archivo))
            {
                Console.WriteLine("ERROR: No se encontró el archivo datos.txt");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para salir...");
                Console.ReadKey();
                return;
            }

            Grafo grafo1 = new Grafo();
            Grafo grafo2 = new Grafo();

            Stopwatch tiempo = Stopwatch.StartNew();

            bool segundoGrafo = false;

            string[] lineas = File.ReadAllLines(archivo);

            foreach (string linea in lineas)
            {
                string texto = linea.Trim();

                if (string.IsNullOrWhiteSpace(texto))
                {
                    continue;
                }

                if (texto == "[GRAFO1]")
                {
                    segundoGrafo = false;
                    continue;
                }

                if (texto == "[GRAFO2]")
                {
                    segundoGrafo = true;
                    continue;
                }

                string[] partes = texto.Split(',');

                if (partes.Length == 2)
                {
                    string origen = partes[0].Trim();
                    string destino = partes[1].Trim();

                    if (!segundoGrafo)
                    {
                        grafo1.AgregarArista(origen, destino);
                    }
                    else
                    {
                        grafo2.AgregarArista(origen, destino);
                    }
                }
            }

            tiempo.Stop();

            Console.WriteLine("Datos cargados correctamente.");
            Console.WriteLine();

            MostrarMenu(grafo1, grafo2, tiempo.Elapsed);
        }

        static void MostrarMenu(
            Grafo grafo1,
            Grafo grafo2,
            TimeSpan tiempo)
        {
            int opcion;

            do
            {
                Console.WriteLine();
                Console.WriteLine("==============================================");
                Console.WriteLine("                    MENÚ");
                Console.WriteLine("==============================================");
                Console.WriteLine("1. Mostrar Grafo 1 - Red de computadoras");
                Console.WriteLine("2. Mostrar Grafo 2 - Conexiones entre ciudades");
                Console.WriteLine("3. Consultar vértice del Grafo 1");
                Console.WriteLine("4. Consultar vértice del Grafo 2");
                Console.WriteLine("5. Mostrar información general");
                Console.WriteLine("6. Mostrar tiempo de ejecución");
                Console.WriteLine("0. Salir");
                Console.WriteLine("==============================================");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1;
                }

                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        MostrarReporte(
                            grafo1,
                            "GRAFO 1 - RED DE COMPUTADORAS");
                        break;

                    case 2:
                        MostrarReporte(
                            grafo2,
                            "GRAFO 2 - CONEXIONES ENTRE CIUDADES");
                        break;

                    case 3:
                        ConsultarVertice(grafo1);
                        break;

                    case 4:
                        ConsultarVertice(grafo2);
                        break;

                    case 5:
                        MostrarInformacion(grafo1, grafo2);
                        break;

                    case 6:
                        Console.WriteLine("Tiempo de carga y construcción:");
                        Console.WriteLine(
                            $"{tiempo.TotalMilliseconds:F4} ms");
                        break;

                    case 0:
                        Console.WriteLine("Programa finalizado.");
                        break;

                    default:
                        Console.WriteLine(
                            "Opción no válida. Intente nuevamente.");
                        break;
                }

            } while (opcion != 0);
        }

        static void MostrarReporte(
            Grafo grafo,
            string titulo)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine($"       {titulo}");
            Console.WriteLine("==============================================");

            Console.WriteLine();
            Console.WriteLine(
                $"Cantidad de vértices: {grafo.CantidadVertices()}");

            Console.WriteLine(
                $"Cantidad de conexiones: {grafo.CantidadAristas()}");

            Console.WriteLine();
            Console.WriteLine("Vértices y conexiones:");
            Console.WriteLine();

            grafo.MostrarGrafo();

            Console.WriteLine();
            Console.WriteLine("==============================================");
        }

        static void ConsultarVertice(Grafo grafo)
        {
            Console.Write("Ingrese el nombre del vértice: ");

            string? vertice = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(vertice))
            {
                Console.WriteLine("Debe ingresar un vértice.");
                return;
            }

            vertice = vertice.Trim();

            var conexiones = grafo.ObtenerConexiones(vertice);

            Console.WriteLine();

            if (conexiones == null)
            {
                Console.WriteLine(
                    $"El vértice '{vertice}' no existe.");
                return;
            }

            Console.WriteLine($"Vértice consultado: {vertice}");

            if (conexiones.Count == 0)
            {
                Console.WriteLine("No tiene conexiones.");
            }
            else
            {
                Console.WriteLine("Conectado con:");

                foreach (string conexion in conexiones)
                {
                    Console.WriteLine($"- {conexion}");
                }
            }
        }

        static void MostrarInformacion(
            Grafo grafo1,
            Grafo grafo2)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("          INFORMACIÓN DE LOS GRAFOS");
            Console.WriteLine("==============================================");

            Console.WriteLine();
            Console.WriteLine("GRAFO 1 - Red de computadoras");
            Console.WriteLine(
                $"Vértices: {grafo1.CantidadVertices()}");
            Console.WriteLine(
                $"Conexiones: {grafo1.CantidadAristas()}");

            Console.WriteLine();
            Console.WriteLine("GRAFO 2 - Conexiones entre ciudades");
            Console.WriteLine(
                $"Vértices: {grafo2.CantidadVertices()}");
            Console.WriteLine(
                $"Conexiones: {grafo2.CantidadAristas()}");

            Console.WriteLine();
            Console.WriteLine("==============================================");
        }
    }
}
