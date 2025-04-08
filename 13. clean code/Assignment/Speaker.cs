namespace BusinessLayer;

/// <summary>
/// Represents a single speaker
/// </summary>
public class Speaker : ISpeaker
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public int? Experience { get; set; }
	public bool HasBlog { get; set; }
	public string Employer { get; set; }
	public string BlogURL { get; set; }
	public IWebBrowser Browser { get; set; }
	public List<string> Certifications { get; set; }
	public int RegistrationFee { get; set; }
	public List<ISession> Sessions { get; set; }

	private readonly ISpeakerValidator _validator;
	private readonly ISpeakerApprovalService _approvalService;
	private readonly IRegistrationFeeCalculator _feeCalculator;

	public Speaker(ISpeakerValidator validator, ISpeakerApprovalService approvalService, IRegistrationFeeCalculator feeCalculator)
	{
		_validator = validator;
		_approvalService = approvalService;
		_feeCalculator = feeCalculator;
	}

	/// <summary>
	/// Register a speaker
	/// </summary>
	/// <returns>speakerID</returns>
	public int? Register(IRepository repository, SpeakerApprovalCriteria criteria)
	{
		int? speakerId = null;
		
		_validator.Validate(this, criteria);

		if (!_approvalService.Approve(this, criteria))
		{
			throw new NoSessionsApprovedException("No sessions approved.");
		}

		RegistrationFee = _feeCalculator.Calculate(this);

		//Now, save the speaker and sessions to the db.
		try
		{
			speakerId = repository.SaveSpeaker(this);
		}
		catch (Exception e)
		{
			//in case the db call fails 
		}

		//if we got this far, the speaker is registered.
		return speakerId;
	}

	#region Custom Exceptions
	public class SpeakerDoesntMeetRequirementsException : Exception
	{
		public SpeakerDoesntMeetRequirementsException(string message)
			: base(message)
		{
		}

		public SpeakerDoesntMeetRequirementsException(string format, params object[] args)
			: base(string.Format(format, args)) { }
	}

	public class NoSessionsApprovedException : Exception
	{
		public NoSessionsApprovedException(string message)
			: base(message)
		{
		}
	}
	#endregion
}