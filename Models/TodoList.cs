using System.ComponentModel.DataAnnotations;

namespace todo_list_app1.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
