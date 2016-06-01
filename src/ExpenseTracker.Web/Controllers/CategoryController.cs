using ExpenseTracker.Data.Infrastructure;
using ExpenseTracker.Data.Repositories;
using ExpenseTracker.Entities;
using ExpenseTracker.Web.Infrastructure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Web.Controllers
{
    public class CategoryController : ExpenseTrackerCtrlBase
    {
        IEntityBaseRepository<Category> _categoryRepository;

        public CategoryController(IEntityBaseRepository<Category> billerRepository, IUnitofWork unitOfWork) : base(unitOfWork)
        {
            _categoryRepository = billerRepository;
        }

        [HttpGet]
        public IEnumerable<Category> Search(string filter)
        {
            IEnumerable<Category> result;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToLower();
                result = _categoryRepository.FindBy(c => c.Name.ToLower().Contains(filter) || c.Description.ToLower().Contains(filter)).AsEnumerable();
            }
            else
                result = _categoryRepository.GetAll().AsEnumerable();

            return result;
        }

        [HttpPost]
        public int Add([FromBody]Category category)
        {
            _categoryRepository.Add(category);
            _unitofWork.Commit();
            return category.ID;
        }

        [HttpPost]
        public void Edit([FromBody] Category category)
        {
            _categoryRepository.Edit(category);
            _unitofWork.Commit();
        }

        [HttpPost]
        public void Delete([FromBody] IEnumerable<Category> categories)
        {
            categories.ToList().ForEach(c => _categoryRepository.Delete(c));
            _unitofWork.Commit();
        }
    }
}
