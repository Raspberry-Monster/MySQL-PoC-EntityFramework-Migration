using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MySQL_PoC
{
    public class Repository:DbContext
    {
        public DbSet<ExampleEntity> Examples { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExampleEntity>(t =>
            {
                t.Property(item=>item.NewName)
                .HasColumnType("json")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<List<ExampleJsonType>>(v, JsonSerializerOptions.Default) ??
                         new List<ExampleJsonType>())
                .Metadata.SetValueComparer(
                    new ValueComparer<List<ExampleJsonType>>(
                        (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
            });
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=test;User=root;Password=Test12345678;");
            base.OnConfiguring(optionsBuilder);
        }
    }
    public class ExampleJsonType
    {
        public string Name { get; set; } = string.Empty;
    }
    public class ExampleEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<ExampleJsonType> NewName { get; set; } = [];
    }
}
