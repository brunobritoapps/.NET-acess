using System.Collections.Generic;
using System.Data.SqlClient;

// INTERNAL DLL
using AcessTower.myRepository;
using AcessTower.myDomain;


namespace AcessTower.myAplication
{
    public class myUserAplication
    {
        private Contexto db;
      
        //CONSULTA TODOS USUARIOS
        public List<myUserDto> Method_APP_SelectAll()
        {
            using (db = new Contexto())
            {
                var strQuery = "SELECT * FROM dbo.User_ ORDER BY MIDDLENAME";
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader);
            }
        }

        //POPULA COMBO JOB TITLE
        public List<string> Method_APP_SelectAll_JobTitle()
        {
        using (db = new Contexto())
            {
                var strQuery = "SELECT DISTINCT MIDDLENAME FROM dbo.User_ ORDER BY MIDDLENAME";
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeString(retornaDataReader,"MIDDLENAME");
        }
        }

        // CONSULTA NUMERO DE RESULTADOS DA CONSULTA
        public int Method_APP_CountAll()
            {
                using (db = new Contexto())
                {
                    var strQuery = "SELECT * FROM dbo.User_";
                    var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                    return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader).Count;
                }
            }

        //RESETA LISTA
        public List<myUserDto> Method_APP_ResetList()
        {
            var users = new List<myUserDto>();
            var tempObjeto = new myUserDto()
            {
                id =null,
                firstName = null,
                lastName = null,
                jobTitle = null,
            };
            users.Add(tempObjeto);
            return users;
        }

        // CONSULTA POR FIRSTNOME E RETORNA UMA LISTA OU UNICO OBJETO DE USUARIO
        public List<myUserDto> Method_APP_SelectByName(string name)
        {
            using (db = new Contexto())
            {
                var strQuery = string.Format("SELECT * FROM dbo.User_ WHERE FIRSTNAME LIKE '%{0}%' ", name);
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader);
            }
        }

        // CONSULTA POR JOBTITLE E RETORNA UMA LISTA DE USUARIOS EM OBJETOS
        public List<myUserDto> Method_APP_SelectByJobTitle(string jobTitle)
        {
            using (db = new Contexto())
            {
                var strQuery =string.Format("SELECT * FROM dbo.User_ WHERE MIDDLENAME='{0}' ORDER BY FIRSTNAME",jobTitle);
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader);
            }
        }

        // CONSULTA POR FIRST NAME E JOB E RETORNA UMA LISTA OU UNICO OBJETO DE USUARIO
        public List<myUserDto> Method_APP_SelectByNameAndJob(string jobTitle, string firstName)
        {
            using (db = new Contexto())
            {
                var strQuery = string.Format("SELECT * FROM dbo.User_ WHERE  FIRSTNAME='{0}' AND MIDDLENAME='{1}'", jobTitle,firstName);
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader);
            }
        }
                
        private List<myUserDto> Method_APP_TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var users = new List<myUserDto>();
            while (reader.Read())
            {
                var tempObjeto = new myUserDto()
                {
                    id = reader["ID"].ToString(),
                    firstName = reader["FIRSTNAME"].ToString(),
                    lastName = reader["LASTNAME"].ToString(),
                    jobTitle = reader["MIDDLENAME"].ToString(),
                };
                users.Add(tempObjeto);
            }
            reader.Close();
            return users;
        }

        private List<string> Method_APP_TransformaReaderEmListaDeString(SqlDataReader reader, string campoBusca)
        {
            var campoList = new List<string>();
            while (reader.Read())
            {
                string tempString = null;
                tempString = reader[campoBusca].ToString();
                campoList.Add(tempString);
            }
            reader.Close();
            return campoList;
        }

    }
}
