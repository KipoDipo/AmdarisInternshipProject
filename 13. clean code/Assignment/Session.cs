namespace BusinessLayer;

public interface ISession
{
	string Title { get; set; }
	string Description { get; set; }
	bool Approved { get; set; }
}
