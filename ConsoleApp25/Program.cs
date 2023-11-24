using System;
using System.Diagnostics.Eventing.Reader;

namespace Laboratorio_sem14
{
    class Program
    {
        const int MaxEncuestas = 1000;
        static int[] edades = new int[MaxEncuestas];
        static char[] vacunado = new char[MaxEncuestas];
        static int contador = 0;

        static void Main()
        {
            int opcion;

            do
            {
                MostrarMenu();
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        RealizarEncuesta();
                        break;
                    case 2:
                        MostrarDatosEncuesta();
                        break;
                    case 3:
                        MostrarResultados();
                        break;
                    case 4:
                        BuscarPorEdad();
                        break;
                    case 5:
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }

            } while (opcion != 5);
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("================================");
            Console.WriteLine("Encuesta Covid-19");
            Console.WriteLine("================================");
            Console.WriteLine("1: Realizar Encuesta");
            Console.WriteLine("2: Mostrar Datos de la encuesta");
            Console.WriteLine("3: Mostrar Resultados");
            Console.WriteLine("4: Buscar Personas por edad");
            Console.WriteLine("5: Salir");
            Console.WriteLine("================================");
            Console.Write("Ingrese una opción: ");
        }

        static int LeerOpcion()
        {
            return int.Parse(Console.ReadLine());
        }

        static void RealizarEncuesta()
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("Encuesta de vacunación");
            Console.WriteLine("===================================");
            Console.Write("¿Qué edad tienes?: ");
            int edad = int.Parse(Console.ReadLine());

            Console.WriteLine("Te has vacunado");
            Console.WriteLine("1: Sí  2: No");
            Console.Write("Ingrese una opción: ");
            char respuesta = Console.ReadKey().KeyChar;
            Console.WriteLine();

            edades[contador] = edad;
            vacunado[contador] = (respuesta == '1') ? 'S' : 'N';
            contador++;

            Console.WriteLine("===================================\n");
            Console.WriteLine("¡Gracias por participar!");
            Console.WriteLine("\n===================================");
            Console.Write("Presione una tecla ...");
            Console.ReadKey();
        }

        static void MostrarDatosEncuesta()
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("Datos de la encuesta");
            Console.WriteLine("===================================\n");
            Console.WriteLine("ID   | Edad | Vacunado");
            Console.WriteLine("=======================");

            for (int i = 0; i < contador; i++)
            {
                Console.WriteLine($"{i:D4} |  {edades[i]:D2}  |   {(vacunado[i] == 'S' ? "Si" : "No")}");
            }

            Console.WriteLine("\n===================================");
            Console.Write("Presione una tecla ...");
            Console.ReadKey();
        }

        static void MostrarResultados()
        {
            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine("Resultados de la encuesta");
            Console.WriteLine("=========================================");

            int totalVacunados = 0;
            int totalNoVacunados = 0;
            int[] rangosEdad = new int[6];
            Console.WriteLine(" ");

            for (int i = 0; i < contador; i++)
            {
                if (vacunado[i] == 'S')
                    totalVacunados++;
                else
                    totalNoVacunados++;

                AsignarRangoEdad(edades[i], rangosEdad);
            }
            MostrarTotalesYRangos(totalVacunados, totalNoVacunados, rangosEdad);

            Console.WriteLine("\n\n=========================================");
            Console.Write("Presione una tecla ...");
            Console.ReadKey();
        }

        static void AsignarRangoEdad(int edad, int[] rangosEdad)
        {
            if (edad >= 1 && edad <= 20)
                rangosEdad[0]++;
            else if (edad >= 21 && edad <= 30)
                rangosEdad[1]++;
            else if (edad >= 31 && edad <= 40)
                rangosEdad[2]++;
            else if (edad >= 41 && edad <= 50)
                rangosEdad[3]++;
            else if (edad >= 51 && edad <= 60)
                rangosEdad[4]++;
            else if (edad > 60)
                rangosEdad[5]++;
        }

        static void MostrarTotalesYRangos(int totalVacunados, int totalNoVacunados, int[] rangosEdad)
        {
            Console.WriteLine($"{totalVacunados:D2} personas se han vacunado");
            Console.WriteLine($"{totalNoVacunados:D2} personas no se han vacunado\n");
            Console.WriteLine("Existen: ");

            for (int i = 0; i < rangosEdad.Length; i++)
            {
                if (i == 6)
                {
                    Console.Write($"{rangosEdad[i]:D2} personas de más de 61 años");
                }
                if (i == 5)
                {
                    Console.Write($"{rangosEdad[i]:D2} personas de más de 61 años");
                }

                else
                {
                    int rangoInicio = i * 10 + 1;
                    int rangoFin = rangoInicio + 9;

                    Console.WriteLine($"{rangosEdad[i]:D2} personas entre {rangoInicio:D2} y {rangoFin:D2} años");
                }
            }
        }

        static void BuscarPorEdad()
        {
            Console.Clear();
            Console.WriteLine("================================");
            Console.WriteLine("Buscar a personas por edad");
            Console.WriteLine("================================");
            Console.Write("¿Qué edad quieres buscar?: ");
            int edadBuscada = int.Parse(Console.ReadLine());

            int vacunados = 0;
            int noVacunados = 0;

            for (int i = 0; i < contador; i++)
            {
                if (edades[i] == edadBuscada)
                {
                    ContarVacunadosYNoVacunados(i, ref vacunados, ref noVacunados);
                }
            }

            MostrarResultadosBusqueda(edadBuscada, vacunados, noVacunados);

            Console.WriteLine("================================");
            Console.Write("Presione una tecla ...");
            Console.ReadKey();
        }

        static void ContarVacunadosYNoVacunados(int i, ref int vacunados, ref int noVacunados)
        {
            if (vacunado[i] == 'S')
                vacunados++;
            else
                noVacunados++;
        }

        static void MostrarResultadosBusqueda(int edadBuscada, int vacunados, int noVacunados)
        {
            Console.WriteLine($"{vacunados:D2} se vacunaron a la edad de {edadBuscada} años");
            Console.WriteLine($"{noVacunados:D2} no se vacunaron a la edad de {edadBuscada} años");
        }
    }
}
