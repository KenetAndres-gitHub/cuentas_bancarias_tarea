namespace proyecto.CONTROLADOR;

using System;
using Biblioteca.DATOS;
using MySql.Data.MySqlClient;
using Mysqlx.Expr;

class CuentaBancariaController{
    private Conexion conexion;
    
    public CuentaBancariaController(){
        conexion = Conexion.getInstancia();
    }
    public void ListarCuentas(){
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "SELECT * FROM cuenta_bancaria";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine("Cuenta "+ reader[1].ToString() +", "+ reader[2].ToString() +", "+ reader[3].ToString());
        }
        connection.Close();
    }

    public void InsertarCuenta(double saldo, int titular, string numero_cuenta)
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "INSERT INTO cuenta_bancaria (numero_cuenta, saldo, titular) VALUES ('"+numero_cuenta+"', '"+saldo+"', '"+titular+"')";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Cuenta insertada....");
    }

    public void ActualizarCuenta(int id, string numero_cuenta, double saldo, string titular)
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "UPDATE cuenta_bancaria SET numero_cuenta = '"+numero_cuenta+"', saldo = '"+saldo+"', titular = '"+titular+"' WHERE id = "+id;
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Cuenta actualizada");
    }

    public void EliminarCuenta(int id)
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "DELETE FROM cuenta_bancaria WHERE id = "+id;
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Cuenta eliminada");
    }

    public void ListarCuentasUsuario(int id_usuario)
    {
       var listaCuentas = SetListaUsuario(id_usuario.ToString());
         foreach (var cuenta in listaCuentas)
         {
            Console.WriteLine("NRO CUENTA: "+ cuenta[3] +", SALDO: $"+ cuenta[1]+"---------");
            MySqlConnection connection = conexion.CrearConexion();
            connection.Open();
            ListarTipoCuenta(connection, cuenta[0]);
            connection.Close();
            Console.WriteLine("-------------------------------------------------");
         }
    }

    private List<string[]> SetListaUsuario(string id_usuario)
    {
        List<string[]> datosCuenta = new List<string[]>();

        using (MySqlConnection connection = conexion.CrearConexion())
        {
            connection.Open();
            string query = "SELECT * FROM cuenta_bancaria WHERE titular = @id_usuario";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id_usuario", id_usuario);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] datos = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            datos[i] = reader[i].ToString();
                        }
                        datosCuenta.Add(datos);
                    }
                }
            }
        }

        return datosCuenta;
    }

    private void ListarTipoCuenta(MySqlConnection connection, string id_cuenta)
    {
        string query = "SELECT * FROM cuenta_ahorro WHERE id_cuenta_bancaria = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id_cuenta);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var interes = reader[2].ToString();
            Console.WriteLine("Tipo: Cuenta de ahorro, con interés del "+ interes +"%");
        }
        reader.Close();

        string query2 = "SELECT * FROM cuenta_bancaria.cuenta_corriente WHERE id_cuenta_bancaria = @id";
        MySqlCommand command2 = new MySqlCommand(query2, connection);
        command2.Parameters.AddWithValue("@id", id_cuenta);
        MySqlDataReader reader2 = command2.ExecuteReader();
        while (reader2.Read())
        {
            var sobregiro = reader2[2].ToString();
            Console.WriteLine("Tipo: Cuenta Corriente, sobregiro: $"+ sobregiro);
        }
        reader2.Close();

    }

    public void InsertarCuentaAhorros(int idCuentaBancaria, double interes)
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "INSERT INTO cuenta_ahorro (id_cuenta_bancaria, interes) VALUES ('"+idCuentaBancaria+"', '"+interes+"')";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Cuenta de ahorros insertada con éxito");
       
    }

    public void InsertarCuentaCorriente(int idCuentaBancaria, double sobregiro)
    {
        MySqlConnection connection = conexion.CrearConexion();
        connection.Open();
        string query = "INSERT INTO cuenta_corriente (id_cuenta_bancaria, sobregiro_permitido) VALUES ('"+idCuentaBancaria+"', '"+sobregiro+"')";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Cuenta corriente insertada con éxito");
    }
}