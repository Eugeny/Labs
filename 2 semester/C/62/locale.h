#ifndef _LOCALE_H_
#define _LOCALE_H_

#include "util/cstring.h"
#include "util/singleton.h"
#include "util/hashmap.h"

class Locale : public Singleton<Locale>
{
public:
    Locale();
    void load(char* name, char* file);
    char* getString(char* name);
    static char* g(char* name);
    char* locale;
private:    
    HashMap<CString*, char*>* _data;
};

#endif
