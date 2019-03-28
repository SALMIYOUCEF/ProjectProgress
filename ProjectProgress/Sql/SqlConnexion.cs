using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectProgress.Sql
{
    public class SqlConnexion
    {
        public SqlConnection GetConnexion()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ProgressProjectsEntities"].ConnectionString;
                EntityConnectionStringBuilder builderConnection = new EntityConnectionStringBuilder(connectionString);
                return new SqlConnection(builderConnection.ProviderConnectionString);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("erreur connexion bdd", ex);
            }
        }
    }
  
}