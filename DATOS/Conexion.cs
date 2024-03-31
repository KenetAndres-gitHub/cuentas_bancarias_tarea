using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DATOS
{
    public class Conexion
    {
        private protected string servidor;
        private protected string puerto;
        private protected string usuario;
        private protected string password;
        private protected string cadenaConexion;
        private protected static Conexion Con = null;
        private protected static Conexion ConLog = null;
        
        private Conexion()
        {
           
                this.servidor = "localhost";
                this.puerto = "3306";
                this.usuario = "root";
                this.password = "kenbokCh2969";
                this.cadenaConexion = "server=" + servidor + ";port=" + puerto + ";user id=" + usuario + ";password=" + password +
                "; database=cuenta_bancaria;";
            
        }
        public MySqlConnection CrearConexion()
        {
            MySqlConnection Cadena = new MySqlConnection();
            try
            { Cadena.ConnectionString = cadenaConexion; }
            catch (Exception ex)
            { Cadena = null; throw ex; }
            return Cadena;
        }
        public static Conexion getInstancia()
        {
            if (Con == null)
            { Con = new Conexion(); }
            return Con;
        }
        public static Conexion getInstanciaLog()
        {
            if (ConLog == null)
            { ConLog = new Conexion(); }
            return ConLog;
        }
        
    }
}
