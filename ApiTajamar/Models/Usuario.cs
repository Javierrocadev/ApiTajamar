using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TajamarProyecto.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }

        [Column("IDCLASE")]
        public int? IdClase { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("ROLE")]
        public string Role { get; set; }

        [Column("LINKEDIN")]
        public string Linkedin { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }


        [Column("EMP_1ID")]
        public int? Emp_1Id { get; set; }

        [Column("EMP_2ID")]
        public int? Emp_2Id { get; set; }

        [Column("EMP_3ID")]
        public int? Emp_3Id { get; set; }

        [Column("EMP_4ID")]
        public int? Emp_4Id { get; set; }

        [Column("EMP_5ID")]
        public int? Emp_5Id { get; set; }

        [Column("EMP_6ID")]
        public int? Emp_6Id { get; set; }
    }
}
