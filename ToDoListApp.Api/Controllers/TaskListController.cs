using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Api.Interfaces;
using ToDoListApp.Api.Models;
using ToDoListApp.Common.DataObjects.Task;
using ToDoListApp.Common.DataObjects.TaskList;
using Task = ToDoListApp.Api.Models.Task;

namespace ToDoListApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/tasklists")]
public class TaskListController(IRepository<TaskList> taskListRepository) : ControllerBase, ITaskListController
{
    [HttpGet]
    public ActionResult<List<TaskListResponseDto>> GetTaskLists()
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var response = taskListRepository
            .FindAll(taskList => taskList.UserId == int.Parse(idFromToken))
            .Select(taskList =>
                new TaskListResponseDto(taskList.Id, taskList.Title, taskList.Description, taskList.UserId))
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskListResponseDto> GetTaskList(int id)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var taskList = taskListRepository.GetById(id);
        if (taskList is null)
            return NotFound();

        if (taskList.UserId != int.Parse(idFromToken))
            return Forbid();

        var response = new TaskListResponseDto(taskList.Id, taskList.Title, taskList.Description, taskList.UserId);
        return Ok(response);
    }

    [HttpGet("{id}/tasks")]
    public ActionResult<List<TaskResponseDto>> GetTasksFromTaskList(int id)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var taskList = taskListRepository.GetById(id);
        if (taskList is null)
            return NotFound();

        if (taskList.UserId != int.Parse(idFromToken))
            return Forbid();
        
        var response = taskList.Tasks.Select(task =>
            new TaskResponseDto(
                task.Id,
                task.Title,
                task.Description,
                task.CreationDate,
                task.DueDate,
                task.IsCompleted,
                task.TaskListId
            )
        ).ToList();
        
        return Ok(response);
    }

    [HttpPost]
    public ActionResult<TaskListResponseDto> CreateTaskList(TaskListRequestDto newTaskList)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var taskList = new TaskList
        {
            Title = newTaskList.Title,
            Description = newTaskList.Description,
            UserId = int.Parse(idFromToken)
        };

        taskListRepository.Add(taskList);

        var response = new TaskListResponseDto(taskList.Id, taskList.Title, taskList.Description, taskList.UserId);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public ActionResult<TaskListResponseDto> UpdateTaskList(int id, TaskListRequestDto newTaskList)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var taskList = taskListRepository.GetById(id);
        if (taskList is null)
            return NotFound();

        if (taskList.UserId != int.Parse(idFromToken))
            return Forbid();

        taskList.Title = newTaskList.Title;
        taskList.Description = newTaskList.Description;

        var response = new TaskListResponseDto(taskList.Id, taskList.Title, taskList.Description, taskList.UserId);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult<TaskListResponseDto> DeleteTaskList(int id)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var taskList = taskListRepository.GetById(id);
        if (taskList is null)
            return NotFound();

        if (taskList.UserId != int.Parse(idFromToken))
            return Forbid();

        taskListRepository.Remove(taskList);

        var response = new TaskListResponseDto(taskList.Id, taskList.Title, taskList.Description, taskList.UserId);
        return Ok(response);
    }
}