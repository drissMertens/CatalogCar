using namespace;


class Singleton(T)
{
	public
{
	static T getSingleton()
	{

		if (_instance is null)
			_instance = new T;

		return _instance;
	}
}
private
{
	this()
		{
		_instance = cast(T) this;
	}
	~this()
		{ }

	static T _instance;
}
}