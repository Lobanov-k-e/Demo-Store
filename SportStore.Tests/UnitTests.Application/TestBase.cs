using SportStore.Application.Interfaces;
using SportStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Tests.UnitTests.Application
{
    class TestBase
    {
        protected ApplicationContext _context;

        public TestBase()
        {
            _context = DbContextFactory.Create();
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(_context);
        }
    }
}
