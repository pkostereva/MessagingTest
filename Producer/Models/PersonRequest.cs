using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Producer.Models
{
    public class PersonRequest
    {
        [MaxLength(50)]
        [DefaultValue("Иванов")]
        public string LastName { get; set; }

        [MaxLength(50)]
        [DefaultValue("Иван")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [DefaultValue("Иванович")]
        public string MiddleName { get; set; }
    }
}
