namespace BusinessLayer;

public interface ISpeaker
{
	string BlogURL { get; set; }
	IWebBrowser Browser { get; set; }
	List<string> Certifications { get; set; }
	string Email { get; set; }
	string Employer { get; set; }
	int? Experience { get; set; }
	string FirstName { get; set; }
	bool HasBlog { get; set; }
	string LastName { get; set; }
	int RegistrationFee { get; set; }
	List<ISession> Sessions { get; set; }

	int? Register(IRepository repository, SpeakerApprovalCriteria criteria);
}