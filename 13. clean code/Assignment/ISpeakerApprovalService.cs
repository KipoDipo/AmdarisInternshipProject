namespace BusinessLayer;

public interface ISpeakerApprovalService
{
	bool Approve(ISpeaker speaker, SpeakerApprovalCriteria criteria);
}