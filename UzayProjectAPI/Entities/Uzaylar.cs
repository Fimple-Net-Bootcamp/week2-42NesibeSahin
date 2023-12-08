using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace UzayProjectAPI.Models
{
    public class Uzaylar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string UzayAdi { get; set; }

        //public virtual ICollection<Gezegenler> Gezegenlers { get; set; }

		public ICollection<Gezegenler> Gezegenler { get; set; }
		public Uzaylar(string uzayAdi)
		{
			UzayAdi = uzayAdi;

		}
        public Uzaylar()
        {
            
        }
    }
}
