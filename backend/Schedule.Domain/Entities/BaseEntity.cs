using System.ComponentModel.DataAnnotations;

namespace Schedule.domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
