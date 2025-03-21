using Application.Modules;

namespace Application.Courses.Vm;

public class CourseGetByIdVm : CourseGetAllVm
{
    public List<ModuleGetAllVm> Modules { get; set; } = [];
}