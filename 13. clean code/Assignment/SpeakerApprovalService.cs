namespace BusinessLayer;

class SpeakerApprovalService : ISpeakerApprovalService
{
	public bool Approve(ISpeaker speaker, SpeakerApprovalCriteria criteria)
	{
		bool result = false;
		foreach (var session in speaker.Sessions)
		{
			foreach (var tech in criteria.Technologies)
			{
				if (session.Title.Contains(tech) || session.Description.Contains(tech))
				{
					session.Approved = false;
					break;
				}
				else
				{
					session.Approved = true;
					result = true;
				}
			}
		}

		return result;
	}
}
