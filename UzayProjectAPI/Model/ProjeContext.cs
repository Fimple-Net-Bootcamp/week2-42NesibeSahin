using Microsoft.EntityFrameworkCore;
using UzayProjectAPI.Models;

namespace UzayProjectAPI.Model

{
    public class ProjeContext:DbContext
    {
		public DbSet<Uzaylar> Uzaylar { get; set; } = null!;
        public DbSet<Gezegenler> Gezegenler { get; set; }= null!;
		public DbSet<Uydular> Uydular { get; set; }= null!;


		public ProjeContext(DbContextOptions<ProjeContext> options) : base(options)
        {

        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Uzaylar>().HasData(
				new Uzaylar("Uzay A")
				{
					ID = 1,
					UzayAdi = "Uzay A",

				},
				new Uzaylar("Uzay B")
				{
					ID = 2,
					UzayAdi = "Uzay B",

				},
				new Uzaylar("Uzay C")
				{
					ID = 3,
					UzayAdi = "Uzay C",
				}
			);


			modelBuilder.Entity<Gezegenler>().HasData(
				new Gezegenler("Gezegen A")
				{
					ID = 1,
					GezegenAdi="Gezegen A",
					UzayID = 1,
				},
				 new Gezegenler("Gezegen B")
				 {
					 ID = 2,
					 GezegenAdi = "Gezegen B",
					 UzayID = 1,
				 },
				 new Gezegenler("Gezegen C")
				 {
					 ID = 3,
					 GezegenAdi = "Gezegen C",
					 UzayID = 2,
				 },
				 new Gezegenler("Gezegen D")
				 {
					 ID = 4,
					 GezegenAdi = "Gezegen D",
					 UzayID = 2,
				 },
				 new Gezegenler("Gezegen E")
				 {
					 ID = 5,
					 GezegenAdi = "Gezegen E",
					 UzayID = 3,
				 }
			);


			modelBuilder.Entity<Uydular>().HasData(
				new Uydular("Uydular A")
				{
					ID = 1,
					UyduAdi = "Uydular A",
					Derece = 1,
					GezegenID = 1,
				},
				 new Uydular("Uydular B")
				 {
					 ID = 2,
					 UyduAdi = "Uydular B",
					 Derece = 2,
					 GezegenID = 1,
				 },
				 new Uydular("Uydular C")
				 {
					 ID = 3,
					 UyduAdi = "Uydular C",
					 Derece = 3,
					 GezegenID = 2,
				 },
				 new Uydular("Uydular D")
				 {
					 ID = 4,
					 UyduAdi = "Uydular D",
					 Derece = 4,
					 GezegenID = 2,
				 },
				 new Uydular("Uydular E")
				 {
					 ID = 5,
					 UyduAdi = "Uydular E",
					 Derece = 5,
					 GezegenID = 3,
				 },
				  new Uydular("Uydular F")
				  {
					  ID = 6,
					  UyduAdi = "Uydular F",
					  Derece = 6,
					  GezegenID = 2,
				  },
				 new Uydular("Uydular G")
				 {
					 ID = 7,
					 UyduAdi = "Uydular G",
					 Derece = 7,
					 GezegenID = 2,
				 },
				 new Uydular("Uydular H")
				 {
					 ID = 8,
					 UyduAdi = "Uydular H",
					 Derece = 8,
					 GezegenID = 3,
				 }
			);
			base.OnModelCreating(modelBuilder);
		}
	}
}


