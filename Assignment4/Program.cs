using System;
using assignment4;

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new KanbanContextFactory();
            var kanbanContext = factory.CreateDbContext(args);
            KanbanContextFactory.Seed(kanbanContext);
        }
    }
}
