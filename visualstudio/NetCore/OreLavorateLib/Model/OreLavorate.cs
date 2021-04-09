using System;
using System.Collections.Generic;

#nullable disable

namespace OreLavorateLib.Model
{
    public partial class OreLavorate
    {
        public int IdOreLavorate { get; set; }
        public int IdCommessa { get; set; }
        public string Username { get; set; }
        public DateTime Data { get; set; }
        public double NumeroOre { get; set; }

        public virtual Commessa IdCommessaNavigation { get; set; }
        public virtual Utente UsernameNavigation { get; set; }
    }
}
