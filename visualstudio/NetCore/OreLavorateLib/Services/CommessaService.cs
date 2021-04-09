using Microsoft.Extensions.Logging;
using OreLavorateLib.Context;

namespace OreLavorateLib.Services
{
    public interface ICommessaService
    {

    }
    public class CommessaService: ICommessaService
    {
        private readonly OrelavorateContext _ctx;
        private readonly ILogger<CommessaService> _log;
        public CommessaService(OrelavorateContext ctx, ILogger<CommessaService> log)
        {
            _ctx = ctx;
            _log = log;
        }
    }
}
