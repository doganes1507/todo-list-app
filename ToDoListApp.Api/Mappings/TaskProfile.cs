using AutoMapper;
using ToDoListApp.Common.DataObjects.Task;
using Task = ToDoListApp.Api.Models.Task;

namespace ToDoListApp.Api.Mappings;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskResponseDto>();
        CreateMap<TaskCreateDto, Task>()
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(_ => false));
        CreateMap<TaskUpdateDto, Task>();
    }
}