using System.Collections.Generic;
using System.Data.SqlClient;
// INTERNAL DLL
using AcessTower.myRepository;
using AcessTower.myDomain;


namespace AcessTower.myAplication
{
    public class myDistributionGroupAplication
    {
        private Contexto db;

        public void Method_APP_InsertOneDistributionGroup(string idUser, string idAcessoGrupo)
        {
            var strQuery = "dbo.SP_INSERT_DISTRIBUTION_GROUP";
            using (db = new Contexto())
            {
                db.Method_RPS_ExecuteProcedureWithParam_A(strQuery,"@ID_USER", "@ID_ACESSO_GRUPO",idUser,idAcessoGrupo);
            }
        }

        public void Method_APP_DeleteOneDistributionGroup(string idDistributionGroup)
        {
            var strQuery = "dbo.SP_DELETE_DISTRIBUTION_GROUP";
            using (db = new Contexto())
            {
                db.Method_RPS_ExecuteProcedureWhitParam_B(strQuery, "@ID_DISTRIBUTION_GROUP", idDistributionGroup);
            }
        }

        public List<myDistributionGroupDto> Method_APP_SelectAll()
        {
            using (db = new Contexto())
            {
                var strQuery = "SELECT * FROM dbo.SelectedUserDistributionGroup AS sudg " +
                    "INNER JOIN User_ AS usr " +
                    "ON usr.ID = sudg.OWNERID " +
                    "INNER JOIN UserDistributionGroup AS udg " + 
                    "ON udg.ID = sudg.USERDISTRIBUTIONGROUPID " +
                    "ORDER BY usr.MIDDLENAME";
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader);
            }
        }

        public List<myDistributionGroupDto> Method_APP_ResetList()
        {
            var distributionGroups = new List<myDistributionGroupDto>();
            var tempObjeto = new myDistributionGroupDto()
            {
                id = null,
                userId = null,
                firstName = null,
                lastName = null,
                jobTitle = null,
                zoneId = null,
                zoneName=null,
            };
            distributionGroups.Add(tempObjeto);
            return distributionGroups;
        }

        public List<myDistributionGroupDto> Method_APP_SelectByJobTitle(string jobTitle)
        {
            using (db = new Contexto())
            {
                var strQuery = string.Format("SELECT * FROM dbo.SelectedUserDistributionGroup AS sudg " +
                    "INNER JOIN User_ AS usr " +
                    "ON usr.ID = sudg.OWNERID " +
                    "INNER JOIN UserDistributionGroup AS udg " +
                    "ON udg.ID = sudg.USERDISTRIBUTIONGROUPID " +
                    "WHERE MIDDLENAME='{0}'",jobTitle);
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader);
            }
        }

        public List<myDistributionGroupDto> Method_APP_SelectByName(string name)
        {
            using (db = new Contexto())
            {
                var strQuery = string.Format("SELECT * FROM dbo.SelectedUserDistributionGroup AS sudg " +
                    "INNER JOIN User_ AS usr " +
                    "ON usr.ID = sudg.OWNERID " +
                    "INNER JOIN UserDistributionGroup AS udg " +
                    "ON udg.ID = sudg.USERDISTRIBUTIONGROUPID " +
                    "WHERE FIRSTNAME LIKE '%{0}%'", name);
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader);
            }
        }

        public List<myDistributionGroupDto> Method_APP_SelectByNameAndJob(string name,string jobTitle)
        {
            using (db = new Contexto())
            {
                var strQuery = string.Format("SELECT * FROM dbo.SelectedUserDistributionGroup AS sudg " +
                    "INNER JOIN User_ AS usr " +
                    "ON usr.ID = sudg.OWNERID " +
                    "INNER JOIN UserDistributionGroup AS udg " +
                    "ON udg.ID = sudg.USERDISTRIBUTIONGROUPID " +
                    "WHERE FIRSTNAME = '{0}'AND MIDDLENAME = '{1}'", name,jobTitle);
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader);
            }
        }

        private List<myDistributionGroupDto> Method_APP_TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var selectGroups = new List<myDistributionGroupDto>();
            while (reader.Read())
            {
                var tempObjeto = new myDistributionGroupDto()
                {
                    id = reader["ID"].ToString(),
                    userId = reader["OWNERID"].ToString(),
                    firstName = reader["FIRSTNAME"].ToString(),
                    lastName = reader["LASTNAME"].ToString(),
                    jobTitle = reader["MIDDLENAME"].ToString(),
                    zoneId = reader["USERDISTRIBUTIONGROUPID"].ToString(),
                    zoneName = reader["NAME_"].ToString(),
                };
                selectGroups.Add(tempObjeto);
            }
            reader.Close();
            return selectGroups;
        }
    }
}
