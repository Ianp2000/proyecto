
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace WebAPI.Models
{
    public class Arte
    {
        [Key]
        public int ArteId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ArteOwner { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string ArteLink { get; set; }

        [Column(TypeName = "nvarchar(8)")]
        public string ArteDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ArteTag { get; set; }

        [Column(TypeName = "bit")]
        public bool? Like { get; set; }



    }

}
