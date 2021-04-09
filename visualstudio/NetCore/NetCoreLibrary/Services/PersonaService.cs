using NetCoreLibrary;
using NetCoreLibrary.Context;
using NetCoreLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreLibrary.Services
{
    public interface IPersonaService
    {
        Persona PersonaDiTest();
    }

    public class PersonaServiceMock: IPersonaService
    {
        public Persona PersonaDiTest()
        {
            var p = new Persona();
            p.Nome = "Mario (mock)";
            p.Cognome = "Rossi";
            p.Email = "mario@rossi.it";

            return p;
        }
    }

    public class PersonaService : IPersonaService
    {
        private PersoneContext ctx;
        public PersonaService(PersoneContext ctx)
        {
            this.ctx = ctx;
        }

        public Persona PersonaDiTest()
        {
            var p = ctx.Persone.Find("mario@rossi.it");
            return p;
        }
    }

}
