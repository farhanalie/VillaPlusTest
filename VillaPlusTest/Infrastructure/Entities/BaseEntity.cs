using System;
using System.ComponentModel.DataAnnotations;

namespace VillaPlusTest.Infrastructure.Entities
{
    public abstract class BaseEntity
    {
        [Key] 
        public int Id { get; set; }
    }
}
