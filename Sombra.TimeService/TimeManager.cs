using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Events;
using Sombra.TimeService.DAL;

namespace Sombra.TimeService
{
    public class TimeManager
    {
        private readonly Dictionary<TimeInterval, DateTime> _lastCheck = new Dictionary<TimeInterval, DateTime>();
        private readonly CancellationTokenSource _cancellationTokenSource;
        private Task _task;
        private readonly IBus _bus;
        private readonly TimeContext _context;

        public TimeManager(IBus bus, TimeContext context)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _bus = bus;
            _context = context;
        }
        public void Start()
        {
            var now = DateTime.UtcNow;
            var day = _context.TimeEvents.OrderBy(e => e.Created, SortOrder.Desc).FirstOrDefault(e => e.Type == TimeInterval.Day);
            _lastCheck.Add(TimeInterval.Day, day?.Created ?? now);

            var week = _context.TimeEvents.OrderBy(e => e.Created, SortOrder.Desc).FirstOrDefault(e => e.Type == TimeInterval.Week);
            _lastCheck.Add(TimeInterval.Week, day?.Created ?? now);

            var month = _context.TimeEvents.OrderBy(e => e.Created, SortOrder.Desc).FirstOrDefault(e => e.Type == TimeInterval.Month);
            _lastCheck.Add(TimeInterval.Month, day?.Created ?? now);

            _task = Task.Run(Worker, _cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        private async Task Worker()
        {
            while (true)
            {
                var now = DateTime.UtcNow;
                if (now.Subtract(_lastCheck[TimeInterval.Day]).Days > 0)
                {
                    ExtendedConsole.Log("Day has passed!");
                    _lastCheck[TimeInterval.Day] = now;
                    _context.TimeEvents.Add(new TimeEvent
                    {
                        Type = TimeInterval.Day,
                        Created = _lastCheck[TimeInterval.Day]
                    });
                    await _context.SaveChangesAsync();
                    await _bus.PublishAsync(new DayHasPassedEvent(now));
                }

                if (now.Subtract(_lastCheck[TimeInterval.Week]).Days > 7)
                {
                    ExtendedConsole.Log("Week has passed!");
                    _lastCheck[TimeInterval.Week] = now;
                    _context.TimeEvents.Add(new TimeEvent
                    {
                        Type = TimeInterval.Week,
                        Created = _lastCheck[TimeInterval.Week]
                    });
                    await _context.SaveChangesAsync();
                    await _bus.PublishAsync(new WeekHasPassedEvent(now));
                }

                if (now.Subtract(_lastCheck[TimeInterval.Month]).Minutes > (365.25 / 12 * 24 * 60))
                {
                    ExtendedConsole.Log("Month has passed!");
                    _lastCheck[TimeInterval.Month] = now;
                    _context.TimeEvents.Add(new TimeEvent
                    {
                        Type = TimeInterval.Month,
                        Created = _lastCheck[TimeInterval.Month]
                    });
                    await _context.SaveChangesAsync();
                    await _bus.PublishAsync(new MonthHasPassedEvent(now));
                }
                Thread.Sleep(10000);
            }
        }
    }
}
