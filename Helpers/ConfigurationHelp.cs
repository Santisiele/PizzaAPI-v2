using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Pizzas.API.Helpers{

    public class ConfigurationHelper{

        public static IConfiguration GetConfiguration(){
            IConfiguration config;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // VER ERROR DE PARAMETROS
                .AddJson("appsettings.json",optional : true , reloadOnCharge:true);
                config=builder.Build();
                return config;
            }
        }
    }