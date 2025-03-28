namespace BusinessLayer;

public interface ISpeakerValidator
{
	void Validate(ISpeaker speaker, SpeakerApprovalCriteria criteria);
}