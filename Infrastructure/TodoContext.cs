using Microsoft.EntityFrameworkCore;
using todo_list_app1.Models;

namespace todo_list_app1.Infrastructure
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
        public DbSet<TodoList> TodoList { get; set; }
    }
}
