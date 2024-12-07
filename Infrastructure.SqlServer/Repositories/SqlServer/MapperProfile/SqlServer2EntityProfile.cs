using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.SqlServer.Repositories.SqlServer.DataContext;

namespace Infrastructure.SqlServer.Repositories.SqlServer.MapperProfile
{
    public class SqlServer2EntityProfile : Profile
    {
        public SqlServer2EntityProfile()
        {
            CreateMap<Book, Entities.Book>().ReverseMap();
            CreateMap<Cart, Entities.Cart>().ReverseMap();
            CreateMap<CartItem, Entities.CartItem>().ReverseMap();
            CreateMap<Category, Entities.Category>().ReverseMap();
            CreateMap<Feedback, Entities.Feedback>().ReverseMap();
            CreateMap<OrderItem,  Entities.OrderItem>().ReverseMap();
            CreateMap<Publisher, Entities.Publisher>().ReverseMap();
            CreateMap<User, Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));
            CreateMap<Entities.User, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<Order, Entities.Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<Entities.Order, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());
        }
    }
}
