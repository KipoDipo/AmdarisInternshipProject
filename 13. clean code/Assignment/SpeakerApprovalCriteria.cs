namespace BusinessLayer;

public class SpeakerApprovalCriteria
{
	public List<string> Technologies { get; }
	public List<string> Domains { get; }
	public List<string> Employers { get; }

	public SpeakerApprovalCriteria(List<string> technologies, List<string> domains, List<string> employers)
	{
		Technologies = technologies ?? new List<string>();
		Domains = domains ?? new List<string>();
		Employers = employers ?? new List<string>();
	}
}
