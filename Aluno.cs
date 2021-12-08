using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsLeituraImportacaoTxt
{
    public class Aluno : Pessoa
    {
        public string matrícula;
        public Curso curso;

        public bool gravar()
        {
            Banco banco = new Banco();

            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            int curso = this.curso.gravar(cn, tran);

            if (curso == 0)
            {
                return false;
            }

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into pessoa " +
                "values (@nome, @fone, @cidade, @rg, @cpf);"
                + "SELECT CAST(scope_identity() AS int)";
            command.Parameters.Add("@nome", SqlDbType.VarChar);
            command.Parameters.Add("@fone", SqlDbType.VarChar);
            command.Parameters.Add("@cidade", SqlDbType.VarChar);
            command.Parameters.Add("@rg", SqlDbType.VarChar);
            command.Parameters.Add("@cpf", SqlDbType.VarChar);
            command.Parameters[0].Value = nome;
            command.Parameters[1].Value = fone;
            command.Parameters[2].Value = cidade;
            command.Parameters[3].Value = rg;
            command.Parameters[4].Value = cpf;


            try
            {
                int pessoaId = (int)command.ExecuteScalar();

                Matricula m = new Matricula();
                m.curso = curso;
                m.pessoa = pessoaId;
                if (!m.gravar(cn, tran))
                {
                    tran.Rollback();
                    return false;
                }

                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                banco.fecharConexao();
            }
        }
    }
}
