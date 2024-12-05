﻿using AutoMapper;
using Infrastructure.SqlServer.Repositories;
using Infrastructure.SqlServer.Repositories.SqlServer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;
using UseCases.UnitOfWork;

namespace Infrastructure.SqlServer.UnitOfWork
{
    public class SqlServerCategoryUnitOfWork : ICategoryUnitOfWork
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public ICategoryRepository CategoryRepository { get; }

        public IBookRepository BookRepository { get; }

        public SqlServerCategoryUnitOfWork(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            CategoryRepository = new SqlServerCategoryRepository(context, mapper);
            BookRepository = new SqlServerBookRepository(context, mapper);
        }

        public Task BeginTransactionAsync()
        {
            return _context.Database.BeginTransactionAsync();
        }

        public Task CancelTransactionAsync()
        {
            return _context.Database.RollbackTransactionAsync();
        }

        public Task SaveChangesAsync()
        {
            return _context.Database.CommitTransactionAsync();
        }
    }
}
