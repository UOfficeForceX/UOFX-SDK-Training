using Microsoft.AspNetCore.Mvc;
using SDK_FirstSample.Models;
using Microsoft.Data.SqlClient;
using SDK_FirstSample.Service;

namespace SDK_FirstSample.Controllers
{
    [ApiController]
    [Route("api/northwind")]
    [Produces("application/json")]
    public class ERPController : ControllerBase
    {

        private readonly ERPService _erpService;

        public ERPController(ERPService erpService)
        {
            _erpService = erpService;
        }

        // 新增採購單
        [HttpPost("purchase/add")]
        public IActionResult InsertPurchase([Bind] PurchaseModel model)
        {
            try
            {
                return Ok(new { purchaseId = _erpService.InsertPurchase(model) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 更新採購單狀態
        [HttpPost("purchase/update-status")]
        public IActionResult UpdatePurchaseStatus([Bind] UpdatePurchaseStatusModel model)
        {
            try
            {
                return Ok(new { purchaseId = model.PurchaseID, status = _erpService.UpdatePurchaseStatus(model) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 更新採購單簽核單號
        [HttpPost("purchase/update-bpm")]
        public IActionResult UpdateBpm([Bind] UpdateBpmModel model)
        {
            try
            {
                return Ok(new { purchaseId = model.PurchaseID, bpmId = _erpService.UpdateBpm(model) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
