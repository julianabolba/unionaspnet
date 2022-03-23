using MongoDB.Driver;

namespace Unionmongo.Models
{
    public class ContextoMongodb
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }
        private IMongoDatabase _database { get; }

        public ContextoMongodb()
        
            {
            try
            {
                MongoClientSettings setting = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));

                if (IsSSL)
                {
                    setting.SslSettings = new SslSettings
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    };
                }
                var mongoCliente = new MongoClient(setting);
                _database = mongoCliente.GetDatabase(DatabaseName); 
            }
            catch (Exception)
            {

                throw new Exception("Não conseguiu conectar");
            }
            }
        public IMongoCollection<Cliente> Cliente
        {
            get
            {
                return _database.GetCollection<Cliente>("Cliente");
            }
        }
         public IMongoCollection<Profissional> Profissional
        {
            get
            {
                return _database.GetCollection<Profissional>("Profissional");
            }
        }
    }
}
            