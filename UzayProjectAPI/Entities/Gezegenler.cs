using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using UzayProjectAPI.Models;

namespace UzayProjectAPI.Models
{
	public class Gezegenler
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string GezegenAdi { get; set; }


       // [ForeignKey("UzayID")]
        public int UzayID { get; set; }


		[ForeignKey("UzayID")]
		public Uzaylar? Uzaylar { get; set; }
		public ICollection<Uydular> Uydular { get; set; }= new List<Uydular>();

		public Gezegenler(string gezegenAdi)
		{
			GezegenAdi = gezegenAdi;
		}


		//[ForeignKey("CityId")]
		//public City? City { get; set; }

		//public int CityId { get; set; }

	}
}



