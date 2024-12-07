using AutoMapper;
using Infrastructure.SqlServer.Repositories.SqlServer.DataContext;
using Infrastructure.SqlServer.Repositories.SqlServer.MapperProfile;
using Infrastructure.SqlServer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UseCases;
using UseCases.DTO;

namespace Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var service = new ServiceCollection();
            service.AddAutoMapper(typeof(SqlServer2EntityProfile));
            service.AddDbContext<BookDbContext>(options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IBookDb;Integrated Security=True;").UseLazyLoadingProxies());
            var serviceProvider = service.BuildServiceProvider();
            var orderManager = new OrderManager(new SqlServerOrderUnitOfWork(serviceProvider.GetRequiredService<BookDbContext>(), serviceProvider.GetRequiredService<IMapper>()));

            IEnumerable<OrderDTO> orderDTOs = await orderManager.GetAllAsync();
            foreach (var orderDTO in orderDTOs)
            {
                Console.WriteLine(orderDTO.Order.DeliveryPhone);
                foreach (var orderItem in orderDTO.OrderItems)
                {
                    Console.WriteLine(orderItem.Quantity);
                }
            }
        }
    }
}
