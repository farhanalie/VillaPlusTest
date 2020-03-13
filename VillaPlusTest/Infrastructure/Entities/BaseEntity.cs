using System.ComponentModel.DataAnnotations;

namespace VillaPlus.API.Infrastructure.Entities
{
    public abstract class BaseEntity
    {
        [Key] 
        public int Id { get; set; }
    }
}
