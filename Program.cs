using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace TP09
{
    class Program
    {
        static int Main(string[] args){

            List <archivosGuardar> ListaArchivoGuardar = new List<archivosGuardar>();

            Console.WriteLine("Por favor ingrese un path para listar contenidos: ");
            string? buffUno, pathDirectory;
            do{
            Console.WriteLine("Ingrese el path de una carpeta: ");
            buffUno = Console.ReadLine();
            } while (string.IsNullOrEmpty(buffUno));

            string path = @"C:\Users\Usuario\Desktop\index.csv";
            pathDirectory = buffUno;

            if (Directory.Exists(pathDirectory))
            {

                if(!File.Exists(path)){
                File.Create(path);
                }

                FileStream Fstream = new FileStream(path, FileMode.Open);
                StreamWriter Streamw= new StreamWriter(Fstream);

                List<string> archivos = Directory.GetFiles(pathDirectory).ToList();
                List<string> subcarpetas = Directory.GetDirectories(pathDirectory).ToList();

                int i = 0;
                string[] acortado;
                string[] acortadoFormato;

                foreach (string item in archivos)
                {

                    archivosGuardar archivoGuardar = new archivosGuardar();
                    acortado = item.Split("\\");
                    Console.WriteLine(acortado[acortado.Length-1]);
                    acortadoFormato = acortado[acortado.Length-1].Split(".");

                    archivoGuardar.Indice = i;
                    archivoGuardar.Formato = acortadoFormato[1];
                    archivoGuardar.NombreArchivo = acortadoFormato[0];

                    ListaArchivoGuardar.Add(archivoGuardar);

                    i++;
                }

                foreach (var item in subcarpetas)
                {

                    archivosGuardar archivoGuardar = new archivosGuardar();
                    acortado = item.Split("\\");
                    Console.WriteLine(acortado[acortado.Length-1]);
                    acortadoFormato = acortado[acortado.Length-1].Split(".");

                    archivoGuardar.Indice = i;
                    archivoGuardar.Formato = "Carpeta";
                    archivoGuardar.NombreArchivo = acortadoFormato[0];

                    ListaArchivoGuardar.Add(archivoGuardar);
                    
                    i++;
                }

                SerializarListaArchivos(ListaArchivoGuardar);

                Streamw.Close();

            }else{
                Console.WriteLine("\nLa direccion de la carpeta no existe.");
            }

            return 0;
        }

        public static void SerializarListaArchivos(List<archivosGuardar> archivoGuardar){

        //Creamos una ruta y donde estará nuestro JSON
        string path;
        path = @"C:\Users\Usuario\Desktop\ArchivoJSON\guardar.JSON";

        using (var NuevoArchivoJson = new FileStream(path, FileMode.Create))
        {
        using(StreamWriter sw = new StreamWriter(NuevoArchivoJson)){
            string? serializarArchivos = JsonSerializer.Serialize(archivoGuardar);
            sw.WriteLine(serializarArchivos);
            sw.Close();
        }
        }
    }

    }
}
