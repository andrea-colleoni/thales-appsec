using System;
using System.Collections.Generic;

#nullable disable

namespace OreLavorateLib.Model
{
    public partial class Commessa
    {
        public Commessa()
        {
            OreLavorates = new HashSet<OreLavorate>();
        }

        public int IdCommessa { get; set; }
        public string Titolo { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime? DataFine { get; set; }

        public virtual ICollection<OreLavorate> OreLavorates { get; set; }
    }
}
