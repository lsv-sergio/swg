using swg.Core.Dto;
using swg.Core.Services;
using swg.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace swg.Controllers {
    public class HomeController : Controller
    {
        private readonly IOperationStorage _operationService;
        private readonly IResultStorage _storage;
        private readonly IOperationLogger _logger;

        public HomeController(IOperationStorage operationService, IResultStorage storage, IOperationLogger logger) {
            _operationService = operationService;
            _storage = storage;
            _logger = logger; 
        }

        public ActionResult Index()
        {
            var model = new OperationModel();
            model.Operations = _operationService.GetAllOperationNames().Select(x => new { value = x, text = x });
            return View(model);
        }

        public async Task<JsonResult> MakeOperation(string operationName, int arg1, int arg2) {
            var operationCreator = _operationService.GetCreatorByOperationName(operationName);
            if (operationCreator == null) {
                return Json(new { result = ""});
            }
            var operation = operationCreator.CreateOperation();
            var result = operation.Execute(arg1, arg2);
            var storeKey = await _storage.SaveResultToStorage(result);
            if (_logger != null) {
                await _logger.WriteOperationLogAsync(OperationLogParameter.Create(operation, arg1, arg2, result, this.HttpContext.Session.SessionID));
            }

            return Json(new { StoreResultKey = storeKey });
        }

        public async Task<JsonResult> GetResult(Guid key) {
            var result = await _storage.GetResultByKeyAsync(key);
            return Json(new { StoredResult = result }, JsonRequestBehavior.AllowGet);
        }
    }
}