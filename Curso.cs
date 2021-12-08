using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsLeituraImportacaoTxt
{
    public class Curso
    {
        public int id;
        public string nome;


        public int gravar(SqlConnection cn, SqlTransaction tran)
        {
            Banco banco = new Banco();

            SqlCommand command = new SqlCommand();
            
            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into curso " +
                "values (@nome); "
                + "SELECT CAST(scope_identity() AS int)";
            command.Parameters.Add("@nome", SqlDbType.VarChar);
            command.Parameters[0].Value = nome;

            try
            {
                int retorno = (int)command.ExecuteScalar();

                return retorno;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                //banco.fecharConexao();
            }
        }
    }
}