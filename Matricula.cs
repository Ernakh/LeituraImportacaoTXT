using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsLeituraImportacaoTxt
{
    public class Matricula
    {
        public int pessoa;
        public int curso;

        public bool gravar(SqlConnection cn, SqlTransaction tran)
        {
            Banco banco = new Banco();

            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;

            command.CommandType = CommandType.Text;

            command.CommandText = "insert into matricula " +
                "values (@pessoa, @curso); ";
            command.Parameters.Add("@pessoa", SqlDbType.VarChar);
            command.Parameters.Add("@curso", SqlDbType.VarChar);
            command.Parameters[0].Value = pessoa;
            command.Parameters[1].Value = curso;

            try
            {
                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                //banco.fecharConexao();
            }
        }
    }
}
