using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DATOS
{
    internal class CRUDMySql
    {
        public static object MessageBox { get; private set; }

        public static bool guardarDatos(String StoreProcedure, MySqlParameter[] datosIngreso)
        {
            bool guardar=false;

            MySqlConnection SqlCon = new MySqlConnection();
            try
            {
                SqlCon = Conexion.getInstanciaLog().CrearConexion();
                string sql_tarea = StoreProcedure;
                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);
                Comando.CommandTimeout = 20;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddRange(datosIngreso);
                SqlCon.Open();
                Comando.ExecuteNonQuery();
                guardar = true;

            }
            catch (Exception ex)
            {/*MessageBox.Show("CONEXION INTERNET FALLIDA CARGAR  CLIENTES");*/ guardar = false; }
            finally { if (SqlCon.State == ConnectionState.Open) SqlCon.Close(); }


            return guardar;
        }
        public static DataTable buscarDatos(String StoreProcedure, MySqlParameter[] datosIngreso)
        {
            MySqlDataReader Resultado;
            DataTable dDatos = new DataTable();
            MySqlConnection SqlCon = new MySqlConnection();

            try
            {
                SqlCon = Conexion.getInstanciaLog().CrearConexion();
                string sql_tarea = StoreProcedure;
                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);
                Comando.CommandTimeout = 20;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddRange(datosIngreso);
                SqlCon.Open();
                Resultado = Comando.ExecuteReader();
                dDatos.Load(Resultado);
            }
            catch (Exception ex)
            { Console.WriteLine("ERROR: EXECUTE STORE BDD:  "+ ex.Message); }
            finally { if (SqlCon.State == ConnectionState.Open) SqlCon.Close(); }

            return dDatos;
        }
        public static string  InsertaDatos(String StoreProcedure, MySqlParameter[] datosIngreso)
        {
            string guardar = "";

            MySqlConnection SqlCon = new MySqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = StoreProcedure;
                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);
                Comando.CommandTimeout = 20;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddRange(datosIngreso);
                SqlCon.Open();
                guardar = Comando.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {/*MessageBox.Show("CONEXION INTERNET FALLIDA CARGAR  CLIENTES");*/ guardar = "ERROR"; }
            finally { if (SqlCon.State == ConnectionState.Open) SqlCon.Close(); }


            return guardar;
        }
    }
}
