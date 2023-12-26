using DustInTheWind.WindowsReboot.Domain;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetScheduleType
{
    public class SetScheduleTypeRequest : IRequest
    {
        public ScheduleTimeType ScheduleType { get; set; }
    }
}