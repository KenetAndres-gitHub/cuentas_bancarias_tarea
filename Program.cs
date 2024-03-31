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
        usuarioController.ListarUsuarios();
    }
}