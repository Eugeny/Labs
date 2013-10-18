#ifndef _SINGLETON_H_
#define _SINGLETON_H_

template <class T>
class Singleton
{
public:
	static T* get() {
	    if (!_instance)
	        _instance = new T;
	    return _instance;
    }
private:
    static T* _instance;
};

template <class T> T* Singleton<T>::_instance = 0;

#endif // _SINGLETON_H_
