using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Homework
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Content { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        [ForeignKey("Course")]
        public int CourceId { get; set; }
        public virtual Course Course { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
