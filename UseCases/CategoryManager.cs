using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.UnitOfWork;

namespace UseCases
{
    public class CategoryManager
    {
        private readonly ICategoryUnitOfWork _categoryUnitOfWork;

        public CategoryManager(ICategoryUnitOfWork categoryUnitOfWork)
        {
            _categoryUnitOfWork = categoryUnitOfWork;
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return _categoryUnitOfWork.CategoryRepository.GetAllAsync();
        }

        public Task<Category?> GetByIdAsync(int id)
        {
            return _categoryUnitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Category category)
        {
            return _categoryUnitOfWork.CategoryRepository.AddAsync(category);
        }

        public Task UpdateAsync(Category category)
        {
            return _categoryUnitOfWork.CategoryRepository.UpdateAsync(category);
        }

        public async Task<SuspendEntityResult> SuspendAsync(int id)
        {
            try
            {
                await _categoryUnitOfWork.BeginTransactionAsync();

                var foundCategory = await _categoryUnitOfWork.CategoryRepository.GetByIdAsync(id);

                if (foundCategory == null)
                {
                    return SuspendEntityResult.NotFound;
                }

                foundCategory.Status = EntityStatus.Suspended;

                var books = await _categoryUnitOfWork.BookRepository.GetAllAsync();
                foreach (var book in books)
                {
                    if (book.CategoryId == foundCategory.Id)
                    {
                        book.Status = EntityStatus.Suspended;
                        await _categoryUnitOfWork.BookRepository.UpdateAsync(book);
                    }
                }

                await _categoryUnitOfWork.CategoryRepository.UpdateAsync(foundCategory);

                await _categoryUnitOfWork.SaveChangesAsync();

                return SuspendEntityResult.Success;
            }
            catch (Exception ex)
            {
                return new SuspendEntityResult(SuspendEntityResultCodes.Error, ex.Message);
            }
        }
    }
}
