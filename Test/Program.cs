using Microsoft.Extensions.DependencyInjection;
using Infrastructure.SqlServer.Repositories.SqlServer.DataContext;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new ServiceCollection();
            service.AddAutoMapper(typeof(SqlServer2EntityProfile))
        }
    }
}
