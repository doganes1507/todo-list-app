using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Api.Interfaces;
using ToDoListApp.Api.Models;
using ToDoListApp.Common.DataObjects.Task;
using ToDoListApp.Common.DataObjects.TaskList;

namespace ToDoListApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/tasklists")]
public class TaskListController(IRepository<TaskList> taskListRepository, IMapper mapper)
    : ControllerBase, ITaskListController
{
    [HttpGet]
    public ActionResult<List<TaskListResponseDto>> GetTaskLists()
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var response = taskListRepository
            .FindAll(taskList => taskList.UserId == int.Parse(idFromToken))
            .Select(mapper.Map<TaskListResponseDto>)
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

        var response = mapper.Map<TaskListResponseDto>(taskList);
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

        var response = taskList.Tasks.Select(mapper.Map<TaskResponseDto>).ToList();

        return Ok(response);
    }

    [HttpPost]
    public ActionResult<TaskListResponseDto> CreateTaskList(TaskListRequestDto newTaskList)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var taskList = mapper.Map<TaskList>(newTaskList);
        taskList.UserId = int.Parse(idFromToken);

        taskListRepository.Add(taskList);

        var response = mapper.Map<TaskListResponseDto>(taskList);
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

        mapper.Map(newTaskList, taskList);
        taskListRepository.Update(taskList);

        var response = mapper.Map<TaskListResponseDto>(taskList);
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

        var response = mapper.Map<TaskListResponseDto>(taskList);
        return Ok(response);
    }
}