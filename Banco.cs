using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WinFormsLeituraImportacaoTxt
{
    public class Banco
    {
        /*
     create login usuario with password='senha';
     create user usuario from login usuario;
     exec sp_addrolemember 'DB_DATAREADER', 'usuario';
     exec sp_addrolemember 'DB_DATAWRITER', 'usuario';
  */


        /*
         
         create table pessoa
	 (
		id integer primary key identity,
		nome varchar(50),
		fone varchar(50),
		cidade varchar(50),
		rg varchar(50),
		cpf varchar(50),
	 )

	 create table curso
	 (
		id integer primary key identity,
		nome varchar(50)
	 )

	  create table matricula
	 (
		id integer primary key identity,
		fk_pessoa int FOREIGN KEY REFERENCES pessoa(id),
		fk_curso int FOREIGN KEY REFERENCES curso(id)
	 )

         */

        private string stringConexao = "Data Source=localhost; Initial Catalog=desafio1; User ID=usuario; password=senha;language=Portuguese";

        private SqlConnection cn;

        private void conexao()//vincular a string com o cn, bm inicia o CN
        {
            cn = new SqlConnection(stringConexao);
        }

        public SqlConnection abrirConexao()
        {
            try
            {
                //tentar fazer algo
                conexao();
                cn.Open();

                return cn;
            }
            catch (Exception ex)
            {
                //faz algo se deu erro
                return null;
            }
        }

        public void fecharConexao()
        {
            try
            {
                cn.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public DataTable executarConsultaGenerica(string sql)
        {
            try
            {
                abrirConexao();

                SqlCommand command = new SqlCommand(sql, cn);
                command.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adapter.Fill(dt);//adaptar preenche o datatable com os dados do command

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                fecharConexao();
            }
        }
    }
}
