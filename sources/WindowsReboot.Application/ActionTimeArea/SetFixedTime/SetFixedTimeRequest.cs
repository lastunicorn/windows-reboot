using System;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetFixedTime
{
    public class SetFixedTimeRequest : IRequest
    {
        public TimeSpan Time { get; set; }
    }
}