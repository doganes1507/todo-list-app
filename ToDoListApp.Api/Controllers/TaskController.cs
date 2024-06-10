using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Api.Interfaces;
using ToDoListApp.Api.Models;
using ToDoListApp.Common.DataObjects.Task;
using Task = ToDoListApp.Api.Models.Task;

namespace ToDoListApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/tasks")]
public class TaskController(IRepository<Task> taskRepository, IRepository<TaskList> taskListRepository) : ControllerBase, ITaskController
{
    [HttpGet]
    public ActionResult<List<TaskResponseDto>> GetTasks()
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var response = taskRepository
            .FindAll(task => task.TaskList.UserId == int.Parse(idFromToken))
            .Select(task => new TaskResponseDto(
                task.Id,
                task.Title,
                task.Description,
                task.CreationDate,
                task.DueDate,
                task.IsCompleted,
                task.TaskListId
            ))
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskResponseDto> GetTask(int id)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var task = taskRepository.GetById(id);
        if (task is null)
            return NotFound();

        if (task.TaskList.UserId != int.Parse(idFromToken))
            return Forbid();

        var response = new TaskResponseDto(
            task.Id,
            task.Title,
            task.Description,
            task.CreationDate,
            task.DueDate,
            task.IsCompleted,
            task.TaskListId
        );

        return Ok(response);
    }

    [HttpPost]
    public ActionResult<TaskResponseDto> CreateTask(TaskCreateDto newTask)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var targetTaskList = taskListRepository.GetById(newTask.TaskListId);
        if (targetTaskList is null)
            return NotFound();

        if (targetTaskList.UserId != int.Parse(idFromToken))
            return Forbid();

        var task = new Task
        {
            Title = newTask.Title,
            Description = newTask.Description,
            CreationDate = DateTime.Now,
            DueDate = newTask.DueDate,
            IsCompleted = false,
            TaskListId = newTask.TaskListId
        };
        
        taskRepository.Add(task);

        var response = new TaskResponseDto(
            task.Id,
            task.Title,
            task.Description,
            task.CreationDate, 
            task.DueDate,
            task.IsCompleted,
            task.TaskListId
        );

        return Ok(response);
    }

    [HttpPut("{id}")]
    public ActionResult<TaskResponseDto> UpdateTask(int id, TaskUpdateDto newTask)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var task = taskRepository.GetById(id);
        if (task is null)
            return NotFound();

        if (task.TaskList.UserId != int.Parse(idFromToken))
            return Forbid();

        task.Title = newTask.Title;
        task.Description = task.Description;
        task.DueDate = newTask.DueDate;
        task.IsCompleted = newTask.IsCompleted;
        
        var response = new TaskResponseDto(
            task.Id,
            task.Title,
            task.Description,
            task.CreationDate, 
            task.DueDate,
            task.IsCompleted,
            task.TaskListId
        );

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult<TaskResponseDto> DeleteTask(int id)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var task = taskRepository.GetById(id);
        if (task is null)
            return NotFound();

        if (task.TaskList.UserId != int.Parse(idFromToken))
            return Forbid();
        
        taskRepository.Remove(task);
        
        var response = new TaskResponseDto(
            task.Id,
            task.Title,
            task.Description,
            task.CreationDate, 
            task.DueDate,
            task.IsCompleted,
            task.TaskListId
        );

        return Ok(response);
    }
}