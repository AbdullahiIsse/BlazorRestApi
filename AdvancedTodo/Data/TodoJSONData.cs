using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data
{
    public class TodoJsonData : ITodoData
    {
        public async Task<IList<Todo>> GetTodos()
        {
           using HttpClient client = new HttpClient();
            
            HttpResponseMessage httpResponseMessage = await client.GetAsync("https://localhost:5008/Todos");

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("failed to fetch data");
            }
            
            var readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();

            IList<Todo> todo = JsonSerializer.Deserialize<IList<Todo>>(readAsStringAsync,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            return todo;
        }

        public async void AddTodo(Todo todo)
        {
            using HttpClient client = new HttpClient();

            var todoAsJson = JsonSerializer.Serialize(todo);

            HttpContent httpContent = new StringContent(todoAsJson, Encoding.UTF8, "application/json");


            HttpResponseMessage httpResponseMessage = await client.PostAsync("https://localhost:5008/Todos", httpContent);
            
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("failed to add data");
            }
            
        }

        public async void RemoveTodo(int todoId)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage httpResponseMessage = await client.DeleteAsync($"https://localhost:5008/Todos/{todoId}");
            
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("failed to add data");
            }
        }

        public async void Update(Todo todo)
        {
            using HttpClient client = new HttpClient();
          
            var todoAsJson = JsonSerializer.Serialize(todo);

            HttpContent httpContent = new StringContent(todoAsJson, Encoding.UTF8, "application/json");


            HttpResponseMessage httpResponseMessage = await client.PatchAsync($"https://localhost:5008/Todos/{todo.TodoId}", httpContent);
            
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("failed to update data");
            }
        }

        public async Task<Todo> Get(int id)
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage httpResponseMessage = await client.GetAsync($"https://localhost:5008/Todos/{id}");

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("failed to fetch data");
            }
            
            var readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();

            Todo todo = JsonSerializer.Deserialize<Todo>(readAsStringAsync,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            return todo;
        }

       
    }
}