using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
namespace Complex.Entity
{
    [Table("TestCase")]
    public class TestCase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestCaseID { get; set; }
        public int? Field1 { get; set; }
        public float? Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public DateTime? Field5 { get; set; } 
    }
}
