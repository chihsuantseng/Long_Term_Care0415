using System.ComponentModel.DataAnnotations;

namespace Long_Term_Care.Models
{
    public class Physicals
    {
        [Key]
        public int PhysicalId { get; set; }

        [Required]
        public DateOnly ExaminDate { get; set; }

        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int SBP { get; set; }
        [Required]
        public int DBP { get; set; }
        [Required]
        public int Waist { get; set; }
        [Required]
        public float WBC { get; set; }
        [Required]
        public float RBC { get; set; }
        [Required]
        public float HB { get; set; }
        [Required]
        public float HCT { get; set; }
        [Required]
        public int PLT { get; set; }
        [Required]
        public int HDL { get; set; }
        [Required]
        public int LDL { get; set; }
        [Required]
        public int TG { get; set; }
        [Required]
        public int CHOL { get; set; }
        [Required]
        public int ALT { get; set; }
        [Required]
        public int AST { get; set; }
        [Required]
        public float CREA { get; set; }
        [Required]
        public int BUN { get; set; }
        [Required]
        public float Ca { get; set; }
        [Required]
        public float HbA1C { get; set; }
        [Required]
        public int GLU { get; set; }
        [Required]
        public int UIBC { get; set; }
        [Required]
        public int Fe { get; set; }
    }
}
