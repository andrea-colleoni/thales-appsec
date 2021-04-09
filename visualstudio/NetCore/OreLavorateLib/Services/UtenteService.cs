using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OreLavorateLib.Context;
using OreLavorateLib.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OreLavorateLib.Services
{
    public interface IUtenteService
    {
        bool Login(string username, string password);
        Task<List<Utente>> all();
        Task save(Utente utente);
        Task<Utente> byUsername(string username);
        Task delete(string username);
        Task<bool> exists(string username);
    }
    public class UtenteService: IUtenteService
    {
        private readonly OrelavorateContext _ctx;
        private readonly ILogger<UtenteService> _log;
        public UtenteService(OrelavorateContext ctx, ILogger<UtenteService> log)
        {
            _ctx = ctx;
            _log = log;
        }

        public async Task save(Utente utente)
        {
            _log.LogInformation($"saving user {utente.Username}");
            var dbUtente = await byUsername(utente.Username);
            if (dbUtente == null)
            {
                _log.LogInformation($"new user {utente.Username} => adding");
                _ctx.Utentes.Add(utente);
            }
            else
            {
                _log.LogInformation($"existing user {utente.Username} => updating");
                _ctx.Utentes.Attach(utente);
                _ctx.Entry(utente).State = EntityState.Modified;
            }
            await _ctx.SaveChangesAsync();
        }

        public async Task<List<Utente>> all()
        {
            _log.LogInformation($"retrieving all users");
            return await _ctx.Utentes.ToListAsync();
        }

        public async Task<Utente> byUsername(string username)
        {
            _log.LogInformation($"get user by username => {username}");
            return await _ctx.Utentes.FindAsync(username);
        }

        public bool Login(string username, string password)
        {
            _log.LogInformation($"trying to login {username}");
            var u = _ctx.Utentes.Find(username);
            return (u != null && u.Password == password);
        }

        public async Task delete(string username)
        {
            _log.LogInformation($"deleting user {username}");
            var u = _ctx.Utentes.Find(username);
            if (u != null)
            {
                _ctx.Remove(u);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<bool> exists(string username)
        {
            return await _ctx.Utentes.AnyAsync(u => u.Username == username);
        }
    }
}
