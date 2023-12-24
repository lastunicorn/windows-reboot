using System;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetDailyTime
{
    public class SetDailyTimeRequest : IRequest
    {
        public TimeSpan Time { get; set; }
    }
}