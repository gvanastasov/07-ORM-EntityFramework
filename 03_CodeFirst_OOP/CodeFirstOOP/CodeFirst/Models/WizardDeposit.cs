using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Models
{
    public class WizardDeposit
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [Required, 
            StringLength(50)]
        public string LastName { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        [Required,
            Range(0, int.MaxValue)]
        public int Age { get; set; }

        [StringLength(100)]
        public string MagicWandCreator { get; set; }

        [Range(1, int.MaxValue)]
        public int MagicWandSize { get; set; }

        [StringLength(20)]
        public string DepositGroup { get; set; }

        public DateTime? DepositStartDate { get; set; }

        public float DepositAmount { get; set; }

        public float DepositInterest { get; set; }

        public float DepositCharge { get; set; }

        public DateTime? DepositExpirationDate { get; set; }

        public bool IsDepositExpired { get; set; }
    }
}
