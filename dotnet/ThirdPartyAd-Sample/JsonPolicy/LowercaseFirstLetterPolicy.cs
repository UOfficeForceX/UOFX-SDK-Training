using System.Text.Json;

namespace Ede.Uofx.ThirdPartyAd.Sample.JsonPolicy
{
    /// <summary>
    /// 第一個字元轉小寫的命名規則，例如：UserName -> userName
    /// </summary>
    public class LowercaseFirstLetterPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (string.IsNullOrEmpty(name) || char.IsLower(name[0]))
            {
                return name;
            }

            return char.ToLower(name[0]) + name.Substring(1);
        }
    }
}
