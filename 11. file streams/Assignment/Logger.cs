namespace Assignment;
public class Logger
{
	private static void Log(string methodName, string status)
	{
		File.AppendAllText($"Log_{DateTime.Now:yyyy-MM-dd}.txt", $"{methodName} {DateTime.Now} [{status}] {Environment.NewLine}");
	}
	public static void LogSuccess(string methodName)
	{
		Log(methodName, "success");
	}
	public static void LogFailure(string methodName)
	{
		Log(methodName, "failure");
	}
}
