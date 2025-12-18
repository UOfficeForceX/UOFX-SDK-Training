using Microsoft.AspNetCore.Mvc;
using SDK_FirstSample.Models;
using System.Text.Json;
using Ede.Uofx.PubApi.Sdk.NetStd.Service;
using Newtonsoft.Json;
using SDK_FirstSample.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SDK_FirstSample.Controllers
{
    [ApiController]
    [Route("api/sdk")]
    [Produces("application/json")]
    public class SDKController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ERPService _erpService;

        public SDKController(IConfiguration configuration, ERPService erpService)
        {
            // 從 appsettings.json 中讀取 ConnectionString
            _connectionString = configuration.GetConnectionString("Default");
            _erpService = erpService;
        }

        [HttpPost("callback")]
        public IActionResult Callback([Bind] JsonElement requestBody)
        {
            try
            {
                // 解密 api request model
                var callbackModel = UofxService.DecodeCallBack<FormApplyResponseModel>(requestBody.ToString());
                // 將 CallbackModel 印出
                Console.WriteLine(JsonConvert.SerializeObject(callbackModel));
                // 取得客製資訊
                var customDataObj = JsonConvert.DeserializeObject<dynamic>(callbackModel.CustomData);
                // 確認是否起單成功
                if (callbackModel?.UofxData?.FormSn != null)
                {
                    // 建立更新 BPM model
                    var model = new UpdateBpmModel
                    {
                        PurchaseID = customDataObj?.PurchaseID,
                        BpmID = callbackModel?.UofxData?.FormSn
                    };
                    // 更新採購單簽核單號
                    _erpService.UpdateBpm(model);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
