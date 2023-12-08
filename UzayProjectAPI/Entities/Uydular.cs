using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UzayProjectAPI.Models
{
   
    public class Uydular
	{
        internal string Status;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string UyduAdi { get; set; }
        public decimal Derece { get; set; }

		[ForeignKey("GezegenID")]
		public Gezegenler? Gezegen { get; set; }

		public int GezegenID { get; set; }

		
		public Uydular(string uyduAdi)
		{
			UyduAdi = uyduAdi;
		}
	}
}
