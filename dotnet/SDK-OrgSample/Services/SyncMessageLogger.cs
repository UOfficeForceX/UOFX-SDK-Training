namespace SDK_FirstSample.Services;

public sealed class SyncMessageLogger
{
    // 儲存成功訊息
    private readonly List<string> _successMessages = ["====================================================同步成功===================================================="];
    // 儲存錯誤訊息
    private readonly List<string> _errorMessages = ["====================================================同步失敗===================================================="];

    public void AddSuccess(string message) => _successMessages.Add(message);

    public void AddError(string message) => _errorMessages.Add(message);

    public void Print()
    {
        // 先列印錯誤，再列印成功訊息
        PrintMessages(_errorMessages);
        PrintMessages(_successMessages);
    }

    private static void PrintMessages(IEnumerable<string> messages)
    {
        foreach (var message in messages)
        {
            Console.WriteLine(message);
        }
    }
}