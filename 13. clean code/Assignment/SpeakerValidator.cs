using static BusinessLayer.Speaker;

namespace BusinessLayer;

public class SpeakerValidator : ISpeakerValidator
{
	public void Validate(ISpeaker speaker, SpeakerApprovalCriteria criteria)
	{
		if (string.IsNullOrWhiteSpace(speaker.FirstName))
		{
			throw new ArgumentNullException("First Name is required");
		}
		if (string.IsNullOrWhiteSpace(speaker.LastName))
		{
			throw new ArgumentNullException("Last name is required.");
		}
		if (string.IsNullOrWhiteSpace(speaker.Email))
		{
			throw new ArgumentNullException("Email is required.");
		}
		if (speaker.Sessions.Count() == 0)
		{
			throw new ArgumentException("Can't register speaker with no sessions to present.");
		}

		bool meetsRequirements = speaker.Experience > 10 || speaker.HasBlog || speaker.Certifications.Count() > 3 || criteria.Employers.Contains(speaker.Employer);

		if (!meetsRequirements)
		{
			//need to get just the domain from the email
			string emailDomain = speaker.Email.Split('@').Last();

			if (criteria.Domains.Contains(emailDomain) || (speaker.Browser.Name == WebBrowser.BrowserName.InternetExplorer.ToString() && speaker.Browser.MajorVersion < 9))
			{
				throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.");
			}
		}

	}
}
