using Assignment.Models.Contracts;

namespace Assignment.Services.Contracts;

interface IPersonService<T> where T : INotifyable
{
	IEnumerable<T> GetAllSubscribed();
	IEnumerable<T> GetAll();

	void Add(T toAdd);
	void Remove(T toRemove);

	void Subscribe(T toSubscribe);
	void Unsubscribe(T toUnsubscribe);
}