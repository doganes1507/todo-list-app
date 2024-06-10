using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Common.DataObjects.Task;

namespace ToDoListApp.Api.Interfaces;

public interface ITaskController
{
    public ActionResult<List<TaskResponseDto>> GetTasks();
    public ActionResult<TaskResponseDto> GetTask(int id);
    public ActionResult<TaskResponseDto> CreateTask(TaskCreateDto newTask);
    public ActionResult<TaskResponseDto> UpdateTask(int id, TaskUpdateDto newTask);
    public ActionResult<TaskResponseDto> DeleteTask(int id);
}