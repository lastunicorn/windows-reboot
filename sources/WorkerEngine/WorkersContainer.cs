﻿// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.WorkerEngine
{
    public class WorkersContainer
    {
        private readonly IWorkerProvider workerProvider;
        private List<IWorker> workers;

        public WorkersContainer(IWorkerProvider workerProvider)
        {
            this.workerProvider = workerProvider ?? throw new ArgumentNullException(nameof(workerProvider));
        }

        public void Start()
        {
            if (workers == null)
            {
                IEnumerable<IWorker> newWorkers = workerProvider.GetNewWorkers();
                workers = new List<IWorker>(newWorkers);
            }

            foreach (IWorker worker in workers)
                worker.Start();
        }

        public void Stop()
        {
            if (workers == null)
                return;

            foreach (IWorker worker in workers)
                worker.Stop();
        }

        public T GetOne<T>()
        {
            return (T)workers.FirstOrDefault(x => x is T);
        }
    }
}