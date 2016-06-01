using AutoMapper;
using ExpenseTracker.Data.Infrastructure;
using ExpenseTracker.Data.Repositories;
using ExpenseTracker.Entities;
using ExpenseTracker.Web.Infrastructure.Core;
using ExpenseTracker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Web.Controllers
{
    public class BillerController : ExpenseTrackerCtrlBase
    {
        IEntityBaseRepository<Biller> _billerRepository;


        public BillerController(IEntityBaseRepository<Biller> billerRepository, IUnitofWork unitofWork) : base(unitofWork)
        {
            _billerRepository = billerRepository;
        }

        [HttpGet]
        public IEnumerable<BillerViewModel> Get()
        {
            var billers = _billerRepository.GetAll().ToList().AsEnumerable();
            return Mapper.Map<IEnumerable<BillerViewModel>>(billers);
        }

        [HttpGet]
        public PaginationSet<BillerViewModel> Search(int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            IEnumerable<Biller> billers;
            int totalBillers = new int();

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower().Trim();
                billers = _billerRepository.FindBy(b => b.Name.ToLower().Contains(filter) || b.Address.ToLower().Contains(filter))
                                               .OrderBy(b => b.ID)
                                               .Skip(currentPage * currentPageSize)
                                               .Take(currentPageSize)
                                               .AsEnumerable();
                totalBillers = _billerRepository.FindBy(b => b.Name.ToLower().Contains(filter) || b.Address.ToLower().Contains(filter)).Count();
            }
            else
            {
                billers = _billerRepository.GetAll().OrderBy(b => b.ID)
                                                     .Skip(currentPage * currentPageSize)
                                                     .Take(currentPageSize)
                                                     .AsEnumerable();
                totalBillers = _billerRepository.GetAll().Count();
            }

            IEnumerable<BillerViewModel> billersVM = Mapper.Map<IEnumerable<Biller>, IEnumerable<BillerViewModel>>(billers);

            PaginationSet<BillerViewModel> pagedSet = new PaginationSet<BillerViewModel>()
            {
                Page = currentPage,
                TotalCount = totalBillers,
                TotalPages = (int)Math.Ceiling((decimal)totalBillers / currentPageSize),
                Items = billersVM
            };

            return pagedSet;
        }

        [HttpPost]
        public void Update([FromBody] BillerViewModel billerViewModel)
        {
            var biller = Mapper.Map<Biller>(billerViewModel);
            _billerRepository.Edit(biller);
            _unitofWork.Commit();
        }

        [HttpPost]
        public int Add([FromBody] BillerViewModel billerViewModel)
        {
            var biller = Mapper.Map<Biller>(billerViewModel);
            _billerRepository.Add(biller);
            _unitofWork.Commit();
            return biller.ID;
        }

        [HttpPost]
        public void Delete([FromBody] IEnumerable<BillerViewModel> billersViewModel)
        {
            var billers = Mapper.Map<IEnumerable<Biller>>(billersViewModel);
            foreach (var biller in billers)
            {
                _billerRepository.Delete(biller);
            }
            _unitofWork.Commit();
        }

    }
}
