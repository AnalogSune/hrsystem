using System.Threading.Tasks;
using API.Data;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Helper
{
    public class DayOffJob : IJob
    {
        private ILogger<DayOffJob> _logger;
        private DataContext _context;

        public DayOffJob(ILogger<DayOffJob> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var today = DateTime.Today;
            foreach (var user in _context.Users)
            {
                var days = today.Subtract(user.DaysOffLastUpdated).Days;
                if (days >= 30)
                {
                    user.DaysOffLastUpdated.AddMonths(1);
                    user.DaysOffLeft += 1.75;
                }
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Days off updated!");
        }
    }
}