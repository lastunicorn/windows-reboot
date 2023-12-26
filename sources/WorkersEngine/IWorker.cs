﻿// Windows Reboot
// Copyright (C) 2009-2015 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace DustInTheWind.WorkersEngine
{
    /// <summary>
    /// A worker is a peace of code that runs continuously until is stopped.
    /// It may be an observer that subscribes itself to an event and does something when the event is triggered.
    /// It may also be a loop that processes some data from a queue.
    /// </summary>
    public interface IWorker
    {
        void Start();
        void Stop();
    }
}