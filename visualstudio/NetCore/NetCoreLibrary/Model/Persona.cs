using System.ComponentModel.DataAnnotations;

namespace NetCoreLibrary.Model
{
    public class Persona
    {
        //[Key]
        public string Email { get; set; }
        //[StringLength(100)]
        public string Nome { get; set; }
        //[StringLength(100)]
        public string Cognome { get; set; }

    }
}
