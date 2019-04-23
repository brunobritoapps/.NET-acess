using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
//
using AcessTower.myRepository;
using AcessTower.myDomain;

namespace AcessTower.myAplication
{
    class myGoupAplication
    {
        private Contexto db;

        public List<myGroupDto> Method_APP_SelectAll()
        {
            using (db = new Contexto())
            {
                var strQuery = "SELECT * FROM dbo.UserDistributionGroup";
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader);
            }
        }

        public int Method_APP_CountAll()
        {
            using (db = new Contexto())
            {
                var strQuery = "SELECT * FROM UserDistributionGroup";
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader).Count;
            }
        }

        public myGroupDto Method_APP_GetTotalAcess()
        {
            using (db = new Contexto())
            {
                var strQuery = "SELECT ID,NAME_ FROM UserDistributionGroup WHERE ID='347B28F6-56CF-4F9D-87BA-3E164E614488'";
                var retornaDataReader = db.Method_RPS_ExecuteCommandWithReturn(strQuery);
                return Method_APP_TransformaReaderEmListaDeObjeto(retornaDataReader).FirstOrDefault();
            }

        }

        private List<myGroupDto> Method_APP_TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var groups = new List<myGroupDto>();
            while (reader.Read())
            {
                var tempObjeto = new myGroupDto()
                {
                    id = reader["ID"].ToString(),
                    nameGroup = reader["NAME_"].ToString(),
                };
                groups.Add(tempObjeto);
            }
            reader.Close();
            return groups;
        }
    }
}
