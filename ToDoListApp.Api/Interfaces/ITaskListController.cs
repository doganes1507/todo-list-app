using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Common.DataObjects.TaskList;
using ToDoListApp.Common.DataObjects.Task;

namespace ToDoListApp.Api.Interfaces;

public interface ITaskListController
{
    public ActionResult<List<TaskListResponseDto>> GetTaskLists();
    public ActionResult<TaskListResponseDto> GetTaskList(int id);
    public ActionResult<List<TaskResponseDto>> GetTasksFromTaskList(int id);
    public ActionResult<TaskListResponseDto> CreateTaskList(TaskListRequestDto newTaskList);
    public ActionResult<TaskListResponseDto> UpdateTaskList(int id, TaskListRequestDto newTaskList);
    public ActionResult<TaskListResponseDto> DeleteTaskList(int id);
}