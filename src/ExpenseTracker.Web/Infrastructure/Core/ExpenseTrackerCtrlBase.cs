using ExpenseTracker.Data.Infrastructure;
using ExpenseTracker.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExpenseTracker.Web.Infrastructure.Core
{
    public class ExpenseTrackerCtrlBase : Controller
    {
        protected readonly IUnitofWork _unitofWork;

        public ExpenseTrackerCtrlBase(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        protected ActionResult CreateHttpResponse(HttpRequestMessage request, Func<ActionResult> function)
        {
            ActionResult response = null;

            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                return (new BadRequestResult());
                //response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = new StatusCodeResult((int)HttpStatusCode.InternalServerError);
                    //request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                //Error _error = new Error()
                //{
                //    Message = ex.Message,
                //    StackTrace = ex.StackTrace,
                //    DateCreated = DateTime.Now
                //};

                //_errorsRepository.Add(_error);
                _unitofWork.Commit();
            }
            catch { }
        }

    }
}
