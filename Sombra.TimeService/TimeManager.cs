using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Events.Time;
using Sombra.TimeService.DAL;

namespace Sombra.TimeService
{
    public class TimeManager
    {
        private readonly Dictionary<TimeInterval, CheckHolder> _lastCheck = new Dictionary<TimeInterval, CheckHolder>();
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
            GetHistory(TimeInterval.Day, TimeSpan.FromDays(1), now);
            GetHistory(TimeInterval.Week, TimeSpan.FromDays(7), now);
            GetHistory(TimeInterval.Month, TimeSpan.FromDays(365.25) / 12, now);
            GetHistory(TimeInterval.Year, TimeSpan.FromDays(365.25), now);

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
                await CheckInterval(TimeInterval.Day, now, time => _bus.PublishAsync(new DayHasPassedEvent(now)));
                await CheckInterval(TimeInterval.Week, now, time => _bus.PublishAsync(new WeekHasPassedEvent(now)));
                await CheckInterval(TimeInterval.Month, now, time => _bus.PublishAsync(new MonthHasPassedEvent(now)));
                await CheckInterval(TimeInterval.Year, now, time => _bus.PublishAsync(new YearHasPassedEvent(now)));
                Thread.Sleep(10000);
            }
        }

        private void GetHistory(TimeInterval interval, TimeSpan span, DateTime now)
        {
            var entry = _context.TimeEvents.OrderBy(e => e.Created, SortOrder.Desc).FirstOrDefault(e => e.Type == interval);
            _lastCheck.Add(interval, new CheckHolder(entry?.Created ?? now, span));
        }

        private async Task CheckInterval(TimeInterval interval, DateTime now, Func<DateTime, Task> publishAction)
        {
            if (IntervalHasPassed(_lastCheck[interval], now))
            {
                ExtendedConsole.Log($"{interval.ToString()} has passed!");
                _lastCheck[interval].Last = now;
                _context.TimeEvents.Add(new TimeEvent
                {
                    Type = interval,
                    Created = now
                });

                await _context.SaveChangesAsync();
                await publishAction(now);
            }
        }

        private static bool IntervalHasPassed(CheckHolder checkHolder, DateTime now)
        {
            return now.Subtract(checkHolder.Last) >= checkHolder.Interval;
        }
    }

    public class CheckHolder
    {
        public CheckHolder(DateTime last, TimeSpan interval)
        {
            Last = last;
            Interval = interval;
        }

        public DateTime Last { get; set; }
        public TimeSpan Interval { get; }
    }
}