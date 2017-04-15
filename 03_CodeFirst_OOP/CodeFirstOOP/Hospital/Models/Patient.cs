using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Patient
    {
        public Patient()
        {
            this.Visitations = new HashSet<Visitation>();
            this.Diagnoses = new HashSet<Diagnose>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ProfilePictureURL { get; set; }
        public string InsuranceInformation { get; set; }

        public virtual ICollection<Visitation> Visitations { get; set; }
        public virtual ICollection<Diagnose> Diagnoses { get; set; }
        public virtual ICollection<Medicament> PrescribedMedicaments { get; set; }
    }
}
