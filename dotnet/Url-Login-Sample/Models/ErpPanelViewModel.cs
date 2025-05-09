using Ede.Uofx.PubApi.Sdk.NetStd.Models;
using Ede.Uofx.PubApi.Sdk.NetStd.Models.Bpm;

namespace Url_Login_Sample.Models
{
    public class ErpPanelViewModel
    {
        public AllCanApplyFormViewModel allCanApplyForm { get; set; }
        public SearchByPage<SearchFormByApplyResultModel> applyForm { get; set; }
        public SearchByPage<SearchFormByAwaitingResultModel> awaitingForm { get; set; }
    }
}
