using Assignment.Models.Contracts;
using Assignment.Services.Contracts;

namespace Assignment.Services;

class PersonService<T> : IPersonService<T> where T : INotifyable
{
	private List<T> _people;
	private List<INotifyable> _subscribed;

	public PersonService()
	{
		_people = [];
		_subscribed = [];
	}

	public IEnumerable<T> GetAllSubscribed()
	{
		return _people.Where(person => _subscribed.Contains(person));
	}

	public IEnumerable<T> GetAll()
	{
		return _people;
	}

	public void Add(T toAdd)
	{
		if (!_people.Contains(toAdd))
			_people.Add(toAdd);
	}

	public void Remove(T toRemove)
	{
		_people.Remove(toRemove);
	}

	public void Subscribe(T toSubscribe)
	{
		if (!_subscribed.Contains(toSubscribe))
			_subscribed.Add(toSubscribe);
	}

	public void Unsubscribe(T toUnsubscribe)
	{
		_subscribed.Remove(toUnsubscribe);
	}
}
