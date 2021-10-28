using System.Collections.Generic;
using System.Threading.Tasks;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data
{
    public interface ITodoData
    {


        Task<IList<Todo>> GetTodos();
        void AddTodo(Todo todo);
        void RemoveTodo(int todoId);

        void Update(Todo todo);

        Task<Todo> Get(int id);






    }
}