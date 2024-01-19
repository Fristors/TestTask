using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskAPI.Models.Database
{
    public class StatusModel
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
    }
}
