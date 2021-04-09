using Microsoft.Extensions.Logging;
using OreLavorateLib.Context;

namespace OreLavorateLib.Services
{
    public interface IOreLavorateService
    {

    }
    public class OreLavorateService: IOreLavorateService
    {
        private readonly OrelavorateContext _ctx;
        private readonly ILogger<OreLavorateService> _log;
        public OreLavorateService(OrelavorateContext ctx, ILogger<OreLavorateService> log)
        {
            _ctx = ctx;
            _log = log;
        }
    }
}
