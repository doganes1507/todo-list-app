using AutoMapper;
using ToDoListApp.Api.Models;
using ToDoListApp.Common.DataObjects.TaskList;

namespace ToDoListApp.Api.Mappings;

public class TaskListProfile : Profile
{
    public TaskListProfile()
    {
        CreateMap<TaskList, TaskListResponseDto>();
        CreateMap<TaskListRequestDto, TaskList>();
    }
}