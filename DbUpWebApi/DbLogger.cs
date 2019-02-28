using DbUp.Engine.Output;
using Microsoft.Extensions.Logging;

namespace DbUpWebApi
{
    public class DbLogger<T> : IUpgradeLog where T : class
    {
        private readonly ILogger<T> _logger;

        public DbLogger(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void WriteInformation(string format, params object[] args)
        {
            _logger.LogInformation(format, args);
        }

        public void WriteError(string format, params object[] args)
        {
            _logger.LogError(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            _logger.LogWarning(format, args);
        }
    }
}