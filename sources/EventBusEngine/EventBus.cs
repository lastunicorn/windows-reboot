// Windows Reboot
// Copyright (C) 2009-2023 Dust in the Wind
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
using System.Threading;
using System.Threading.Tasks;

namespace DustInTheWind.EventBusEngine
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<object>> subscribersByEvent = new Dictionary<Type, List<object>>();

        public void Subscribe<TEvent>(Func<TEvent, CancellationToken, Task> action)
        {
            List<object> actions = GetBucket<TEvent>() ?? CreateBucket<TEvent>();
            actions.Add(action);
        }

        public void Subscribe<TEvent>(Action<TEvent> action)
        {
            List<object> actions = GetBucket<TEvent>() ?? CreateBucket<TEvent>();
            actions.Add(action);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> action)
        {
            List<object> actions = GetBucket<TEvent>();

            if (actions == null)
                return;

            if (actions.Contains(actions))
                actions.Remove(action);
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        {
            List<object> bucket = GetBucket<TEvent>();

            if (bucket == null)
                return;

            IEnumerable<Task> tasks = bucket
                .Select(x =>
                {
                    if (x is Func<TEvent, CancellationToken, Task> asyncAction)
                        return asyncAction(@event, cancellationToken);

                    if (x is Action<TEvent> syncAction)
                        return Task.Run(() =>
                        {
                            syncAction(@event);
                        }, cancellationToken);

                    return Task.CompletedTask;
                });

            await Task.WhenAll(tasks);
        }

        public void Publish<TEvent>(TEvent @event)
        {
            List<object> bucket = GetBucket<TEvent>();

            if (bucket == null)
                return;

            foreach (object o in bucket)
            {
                if (o is Func<TEvent, CancellationToken, Task> asyncAction)
                    asyncAction(@event, CancellationToken.None).Wait();
                else if (o is Action<TEvent> syncAction)
                    syncAction(@event);
            }
        }

        private List<object> GetBucket<TEvent>()
        {
            return subscribersByEvent.ContainsKey(typeof(TEvent))
                ? subscribersByEvent[typeof(TEvent)]
                : null;
        }

        private List<object> CreateBucket<TEvent>()
        {
            List<object> actions = new List<object>();
            subscribersByEvent.Add(typeof(TEvent), actions);
            return actions;
        }
    }
}