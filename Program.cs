namespace proyecto;
using System;
using Biblioteca.DATOS;
using MySql.Data.MySqlClient;
using proyecto.CONTROLADOR;

class Program
{
    static void Main()
    {
        BancoController bancoController = new BancoController();
        bancoController.ListarBancos();
        //usuarios
        UsuarioController usuarioController = new UsuarioController();

        //menu de opciones while
        int opcion = 0;
        while (opcion != 3)
        {
            Console.WriteLine("---------------------MENU---------------------");
            Console.WriteLine("1. Listar usuarios");
            Console.WriteLine("2. Insertar usuario");
            Console.WriteLine("3. Salir");
            Console.WriteLine("Ingrese una opcion");
            Console.WriteLine("----------------------------------------------");

            opcion = int.Parse(Console.ReadLine());
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("LISTA DE USUARIOS");
                    usuarioController.ListarUsuarios();
                    break;
                case 2:
                    Console.WriteLine("Ingrese el nombre del usuario");
                    string nombre = Console.ReadLine();
                    Console.WriteLine("Ingrese la identificacion del usuario");
                    string identificacion = Console.ReadLine();
                    usuarioController.InsertarUsuario(nombre, identificacion);
                    break;
                case 3:
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opcion no valida");
                    break;
            }

        }
    }
}