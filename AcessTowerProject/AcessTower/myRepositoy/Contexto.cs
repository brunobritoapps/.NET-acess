using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AcessTower.myRepository
{
    public class Contexto : IDisposable
    {
        private readonly SqlConnection conexao;

        public Contexto()
        {
            conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["myStringConnAcessTower"].ConnectionString);
            conexao.Open();
        }

        public void Method_RPS_ExecuteProcedureWithParam_A(string strQuery,string parametroName1, string parametroName2,string valor1,string valor2)
        {
            var cmdQuery = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.StoredProcedure,
                Connection = conexao
            };
            cmdQuery.Parameters.AddWithValue(parametroName1, valor1);
            cmdQuery.Parameters.AddWithValue(parametroName2, valor2);
            cmdQuery.ExecuteNonQuery();
        }

        public void Method_RPS_ExecuteProcedureWhitParam_B(string strQuery, string parametroName1, string valor1)
        {
            var cmdQuery = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.StoredProcedure,
                Connection = conexao
            };
            cmdQuery.Parameters.AddWithValue(parametroName1, valor1);
            cmdQuery.ExecuteNonQuery();
        }

        public void Method_RPS_ExecuteCommand(string strQuery)
        {
            var cmdQuery = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = conexao
            };
            cmdQuery.ExecuteNonQuery();
        }

        public SqlDataReader Method_RPS_ExecuteCommandWithReturn(string strQuery)
        {
            var cmdQuery = new SqlCommand(strQuery, conexao);
            return cmdQuery.ExecuteReader();
        }

        public void Dispose()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }
    }
}
