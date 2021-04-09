using System;
using System.Collections.Generic;

#nullable disable

namespace OreLavorateLib.Model
{
    public partial class Utente
    {
        public Utente()
        {
            OreLavorates = new HashSet<OreLavorate>();
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<OreLavorate> OreLavorates { get; set; }
    }
}
