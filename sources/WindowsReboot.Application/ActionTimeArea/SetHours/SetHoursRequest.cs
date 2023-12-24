using System;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetHours
{
    public class SetHoursRequest : IRequest
    {
        public int Hours { get; set; }
    }
}