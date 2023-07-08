using Serilog.Formatting.Json;
using Serilog;

namespace WebApplication5
{
    public class LoggerConfig
    {
        public static void Configure()
        {
            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.File(new JsonFormatter(), "log.txt")
            //    .CreateLogger();
        }
    }

}
