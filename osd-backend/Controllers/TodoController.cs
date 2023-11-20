using IDO.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using osd_backend.Models;

namespace osd_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public TodoController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get() {
            var todos = _context.Todos.ToList();
            if (todos.Count == 0)
            {
                return NotFound("No Todos yet.");
            }

            return Ok(todos);
        }

        [HttpGet("{userId}")]
        public ActionResult Get(int userId)
        {
            var todos = _context.Todos.Where(t => t.UserId == userId).ToList();

            if (todos.Count == 0)
            {
                return NotFound($"No Todos found for User with Id {userId}.");
            }

            var todosTodo = todos.Where(t => t.Status == Todo.StatusEnum.Todo.ToString()).ToList();
            var todosDoing = todos.Where(t => t.Status == Todo.StatusEnum.Doing.ToString()).ToList();
            var todosDone = todos.Where(t => t.Status == Todo.StatusEnum.Done.ToString()).ToList();

            var result = new
            {
                TodosTodo = todosTodo,
                TodosDoing = todosDoing,
                TodosDone = todosDone
            };

            return Ok(result);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Todo todo)
        {
            var user = _context.Users.Find(todo.UserId);

            if (user == null)
            {
                return NotFound($"User with Id {todo.UserId} not found.");
            }

            _context.Todos.Add(todo);
            _context.SaveChangesAsync();

            return Ok($"Todo created successfully for User with Id {todo.UserId}.");
        }

        [HttpPut]
        public ActionResult Put([FromBody] Todo model)
        {
            if (model == null || model.Id == 0)
            {
                return BadRequest("Todo data is invalid");
            }

            try
            {
                var todo = _context.Todos.Find(model.Id);
                if(todo == null)
                {
                    return BadRequest($"Todo not found with id {model.Id}");
                }

                todo.Status = model.Status;
                todo.Category = model.Category;
                todo.DueDate = model.DueDate;
                todo.Title = model.Title;
                todo.Estimate = model.Estimate;
                todo.Importance = model.Importance;

                _context.SaveChangesAsync();

                return Ok("Todo details updated");

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
