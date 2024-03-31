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
            Console.WriteLine("ID: "+ id +", Nombre:"+ nombre +", Identificación: "+ identificacion);
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
        //Validar que no se repita la identificacion
        string queryValidacion = "SELECT * FROM usuario WHERE identificacion = '"+identificacion+"'";
        MySqlCommand commandValidacion = new MySqlCommand(queryValidacion, connection);
        MySqlDataReader reader = commandValidacion.ExecuteReader();
        if(reader.HasRows){
            Console.WriteLine("Ya existe un usuario con esa identificacion");
            return;
        }
        reader.Close();

        string query = "INSERT INTO usuario (nombre, identificacion) VALUES ('"+nombre+"', '"+identificacion+"')";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        Console.WriteLine("Usuario insertado");
        //recoger el id del usuario insertado
        string queryId = "SELECT * FROM usuario WHERE identificacion = '"+identificacion+"'";
        MySqlCommand commandId = new MySqlCommand(queryId, connection);
        MySqlDataReader readerId = commandId.ExecuteReader();
        readerId.Read();
        int id = int.Parse(readerId[0].ToString());
        readerId.Close();

        //Insertar cuenta bancaria
        CuentaBancariaController cuentaBancariaController = new CuentaBancariaController();
        Console.WriteLine("Ingrese el saldo de la cuenta");
        double saldo = double.Parse(Console.ReadLine());
        cuentaBancariaController.InsertarCuenta(saldo, id, GenerarCodigoUnico() );
        //recogemos el id de la cuenta insertada
        string queryIdCuenta = "SELECT * FROM cuenta_bancaria WHERE titular = "+id;
        MySqlCommand commandIdCuenta = new MySqlCommand(queryIdCuenta, connection);
        MySqlDataReader readerIdCuenta = commandIdCuenta.ExecuteReader();
        readerIdCuenta.Read();
        int idCuentaBancaria = int.Parse(readerIdCuenta[0].ToString());
        readerIdCuenta.Close();

        Console.WriteLine("Ingrese el tipo de cuenta \n1. Ahorros \n2. Corriente");
        int tipo_cuenta = int.Parse(Console.ReadLine());
        switch (tipo_cuenta)
        {
            case 1:
                cuentaBancariaController.InsertarCuentaAhorros(idCuentaBancaria, 0.5);
                break;
            case 2:
                cuentaBancariaController.InsertarCuentaCorriente(idCuentaBancaria, 1000);
                break;
            default:
                Console.WriteLine("Opcion no valida, se le va a generar una cuenta de ahorros por defecto");
                cuentaBancariaController.InsertarCuentaAhorros(idCuentaBancaria, 0.5);
                break;
        }
        connection.Close();
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

    private static readonly Random random = new Random();
    private const string caracteresPermitidos = "0123456789";

    public string GenerarCodigoUnico()
    {
        char[] codigo = new char[10];
        for (int i = 0; i < 10; i++)
        {
            codigo[i] = caracteresPermitidos[random.Next(caracteresPermitidos.Length)];
        }
        return new string(codigo);
    }

    

}