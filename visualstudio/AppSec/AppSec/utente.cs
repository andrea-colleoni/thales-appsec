namespace AppSec
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("utente")]
    public partial class utente
    {
        public int id { get; set; }

        [StringLength(30)]
        public string nome { get; set; }

        [StringLength(30)]
        public string cognome { get; set; }

        [StringLength(30)]
        public string email { get; set; }

        [StringLength(30)]
        public string password { get; set; }
    }
}
