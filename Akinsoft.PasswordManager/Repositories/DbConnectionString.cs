using System.Configuration;

namespace Akinsoft.PasswordManager.Repositories
{
    public static class DbConnectionString
    {
        public static string ConnectionString { get { return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; } }

    }
}