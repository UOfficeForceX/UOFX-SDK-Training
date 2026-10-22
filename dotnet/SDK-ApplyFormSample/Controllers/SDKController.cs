using Microsoft.AspNetCore.Mvc;
using Ede.Uofx.OpenApi.Sdk.NetStd.Service;
using Ede.Uofx.OpenApi.Sdk.NetStd.Models;
using Ede.Uofx.OpenApi.Sdk.NetStd.Models.Bpm;

namespace SDK_ErpApplyFormSample.Controllers
{
    [ApiController]
    [Route("api/sdk")]
    [Produces("application/json")]
    public class SDKController : ControllerBase
    {
        [HttpPost("callback")]
        public IActionResult Callback([FromBody] CallBackModel<string> model)
        {
            try
            {
                // 在此可實作與 ERP 系統串接的相關邏輯
                // 例如將表單編號更新至 ERP 系統

                // 將 CallbackModel 印出
                Console.WriteLine(UofxService.Json.Convert(model));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 依據 TraceId 查詢起單結果
        /// </summary>
        /// <param name="traceId">起單後取得的 TraceId</param>
        /// <returns></returns>
        [HttpGet("apply-result/{traceId}")]
        public async Task<IActionResult> GetApplyResult(string traceId)
        {
            try
            {
                var result = await UofxService.BPM.Apply.GetApplyResult(
                    new ApplyFormTraceRequestModel
                    {
                        TraceId = traceId
                    });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(UofxService.Error.ConvertToModel(ex));
            }
        }
    }
}
