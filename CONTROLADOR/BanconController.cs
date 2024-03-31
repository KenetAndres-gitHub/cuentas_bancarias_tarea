namespace proyecto.CONTROLADOR;

using System;
using Biblioteca.DATOS;
using MySql.Data.MySqlClient;

class BancoController
{
    
    private Conexion conexion;
    
    public BancoController(){
       conexion = Conexion.getInstancia();
    }
    public void ListarBancos()
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "SELECT * FROM banco";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine("Banco "+ reader[1].ToString() +", "+ reader[2].ToString());
        }
        connection.Close();
    }

    public void InsertarBanco(string nombre, string direccion)
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "INSERT INTO banco (nombre, direccion) VALUES ('"+nombre+"', '"+direccion+"')";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Banco insertado");
    }

    public void ActualizarBanco(int id, string nombre, string direccion)
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "UPDATE banco SET nombre = '"+nombre+"', direccion = '"+direccion+"' WHERE id = "+id;
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Banco actualizado");
    }

    public void EliminarBanco(int id)
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "DELETE FROM banco WHERE id = "+id;
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Banco eliminado");
    }
}