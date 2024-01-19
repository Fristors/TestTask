using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskAPI.Models.Database
{
    public class TaskModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status_ID { get; set; }
        [Required]
        public StatusModel Statuss { get; set; } //= new StatusModel();

        public TaskModel() { }
    }
}
