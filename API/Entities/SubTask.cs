using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class SubTask
    {
        public int Id { get; set; }

        public int TasksId { get; set; }
        public string Description { get; set; }

        public TaskStatus Status { get; set; }
    }
}