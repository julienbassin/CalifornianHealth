using CalendarApi.Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Test.Infrastructure
{
    public class DatabaseTest : IDisposable
    {
        protected readonly CalendarDBContext _context;
        public DatabaseTest()
        {
            var options = new DbContextOptionsBuilder<CalendarDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new CalendarDBContext(options);
            _context.Database.EnsureCreated();
            DatabaseInitializer.Initialize(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }        
    }
}
