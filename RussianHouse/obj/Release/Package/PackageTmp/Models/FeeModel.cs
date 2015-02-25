using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace RussianHouse.Models
{
    public class FeeContext : DbContext
    {
        public FeeContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Fee> YandexToken { get; set; }
    }

    public class Fee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int Num { get; set; }
        public string FeeText { get; set; }
    }
}