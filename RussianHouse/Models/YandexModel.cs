using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace RussianHouse.Models
{
    public class YandexContext : DbContext
    {
        public YandexContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<YandexToken> YandexToken { get; set; }
    }

    public class YandexToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Token { get; set; }
        public DateTime Date { get; set; }
    }
}