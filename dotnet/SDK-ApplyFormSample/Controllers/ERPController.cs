using Ede.Uofx.OpenApi.Sdk.NetStd.Service;
using Microsoft.AspNetCore.Mvc;
using SDK_ErpApplyFormSample.Models;

namespace SDK_ErpApplyFormSample.Controllers
{
    [ApiController]
    [Route("api/northwind")]
    [Produces("application/json")]
    public class ERPController : ControllerBase
    {
        // 更新採購單單據結果
        [HttpPost("purchase/update-result")]
        public IActionResult UpdatePurchaseStatus([FromBody] UpdatePurchaseResultModel model)
        {
            try
            {
                // 在此可實作更新採購單單據結果的相關邏輯
                // 例如呼叫 ERP 系統 API、更新資料庫或等相關處理

                Console.WriteLine(UofxService.Json.Convert(model));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
