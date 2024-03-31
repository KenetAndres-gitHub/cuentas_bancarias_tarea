using Biblioteca.DATOS;
using MySql.Data.MySqlClient;

namespace proyecto.CONTROLADOR;
class UsuarioController
{
   //la tabla usuario tiene los campos: id, nombre e identificacion
    private Conexion conexion;
    public UsuarioController()
    {
        conexion = Conexion.getInstancia();
    }

    public void ListarUsuarios()
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "SELECT * FROM usuario";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var id = reader[0].ToString();
            var nombre = reader[1].ToString();
            var identificacion = reader[2].ToString();
            Console.WriteLine("Usuario "+ id +", "+ nombre +", "+ identificacion);
            //Cuenta usuario
            CuentaBancariaController cuentaBancariaController = new CuentaBancariaController();
            cuentaBancariaController.ListarCuentasUsuario(int.Parse(id));
        }
        connection.Close();
    }

    public void InsertarUsuario(string nombre, string identificacion)
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "INSERT INTO usuario (nombre, identificacion) VALUES ('"+nombre+"', '"+identificacion+"')";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Usuario insertado");
    }

    public void ActualizarUsuario(int id, string nombre, string identificacion)
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "UPDATE usuario SET nombre = '"+nombre+"', identificacion = '"+identificacion+"' WHERE id = "+id;
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Usuario actualizado");
    }

    

}