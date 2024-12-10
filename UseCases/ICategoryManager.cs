﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.TaskResults;

namespace UseCases
{
    public interface ICategoryManager
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task<AtomicTaskResult> SuspendAsync(int id);
        Task<AtomicTaskResult> ActivateAsync(int id);
    }
}
