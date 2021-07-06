using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Menu
{
    class Program
    {
        static Dictionary<string, string> agenda = new Dictionary<string, string>();

        static string nombreArchivo = @"C:\Users\LIBRE3\Desktop\html gus\Comunidad IT\Clase 16\agenda-paginas-web\paginas.csv";

        static void Main(string[] args)
        {
            //1
            FileStream stream = new FileStream(nombreArchivo,
                 FileMode.Open
                 ,FileAccess.Read);
            //2
            StreamReader reader = new StreamReader(stream);
            //3
            while (reader.Peek() > -1)
            {
                string renglon = reader.ReadLine();
                string clave = renglon.Split(";")[0];
                string valor = renglon.Split(";")[1];
                agenda.Add(clave,valor);
            }
            reader.Close();

            string opUsuario = "";
            do
            {
                Console.Clear();
                ImprimirMenu();
                opUsuario = Console.ReadLine();

                switch(opUsuario)
                {
                    case "1":
                        agendarPagina();
                        break;
                    case "2":
                        buscarDir();
                        break;
                    case "3":
                        buscarUrl();
                        break;    
                    case "0":
                        Console.WriteLine("¡Hasta la próxima!");
                        break;
                    default:
                        Console.WriteLine("Opción incorrecta");
                        Console.ReadKey();
                        break;
                }
            } while (opUsuario != "0");
            Console.ResetColor();
            Console.ReadKey();
        }
        static void ImprimirMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1- Agendar página");
            Console.WriteLine("2- Buscar y visitar página (por Nombre)");
            Console.WriteLine("3- Buscar y visitar página (por URL)");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("0- Salir");
            Console.WriteLine("(Ingrese número de opción)");
        }
        static void agendarPagina()
        {
            Console.WriteLine("Agendar");
            Console.WriteLine("Escriba el nombre de la página");
            string pagina = Console.ReadLine();
            Console.WriteLine("Escriba URL de la página");
            string url = Console.ReadLine();
            
            using(var w = File.AppendText(nombreArchivo))
            {
                var line = string.Format("{0};{1}", pagina, url);
                w.WriteLine(line);
                w.Flush();
            }
            agenda.Add(pagina,url);
            Console.WriteLine("¡La página se agendó con éxito!\nPresione:\n 0 - para volver al menú...");
            Console.ReadKey();
        }
        static void buscarDir()
        {
            try
            {
                Console.WriteLine("Buscar página por Nombre");
                Console.WriteLine("Escriba el Nombre de la página:");
                string busqueda = Console.ReadLine();
                Console.WriteLine(agenda[busqueda]);
                Console.WriteLine("Presione:\n 1 - para visitar la página\n 0 - para volver al menú...");
                int opBuscar = int.Parse(Console.ReadLine());
                if(opBuscar==1)
                {
                    visitar(agenda[busqueda]);
                }
            }        
            catch (System.Exception)
            {
                Console.WriteLine("La página no existe en la agenda");
                Console.ReadKey();
            }        
        }
        static void buscarUrl()
        {
            Console.WriteLine("Buscar por URL");
            Console.WriteLine("Escriba una parte de la URL para buscar");
            string buscar = Console.ReadLine();
            bool encontrado = false;
            foreach(KeyValuePair<string, string> kvp in agenda)
            {
                encontrado = kvp.Value.Contains(buscar);
                if(encontrado==true)
                {
                    visitar(kvp.Value);
                }
            }
            if(encontrado==false)
            {
                    Console.WriteLine("La página no está agendada...\nPresione:\n 0 - para volver al menú...");
            }
            Console.ReadKey();
        }
        static void visitar(string url)
        {
            Console.WriteLine("Visitar");
            Console.WriteLine("");
            string ruta = "C:\\Program Files (x86)\\Google\\Chrome\\Application";
            var procesoExterno = new ProcessStartInfo(ruta)
            {
                Arguments = url,
                UseShellExecute = true,
                WorkingDirectory = ruta,
                FileName = "chrome.exe",
                Verb = "OPEN"
            };
            Process.Start(procesoExterno);
            Console.ReadKey();
        }
    }
}
