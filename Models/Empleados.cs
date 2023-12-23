using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppNomina.Models
{
    public class Empleados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="#")]
        public int ID { get; set; }

        [Display(Name ="Empleado")]
        public string NombreCompleto { get; set; }

        [Display(Name = "H Normales")]
        public decimal HNormales { get; set; }

        [Display(Name = "H Extras")]
        public decimal HExtras { get; set; }

        [Display(Name = "Salario Bruto")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true, NullDisplayText = "CRC 0.00")]
        public decimal SalarioBruto { get; set; }

        [Display(Name = "Salario Neto")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true, NullDisplayText = "CRC 0.00")]
        public decimal SalarioNeto { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true, NullDisplayText = "CRC 0.00")]
        public decimal Deducciones { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

    }
}
