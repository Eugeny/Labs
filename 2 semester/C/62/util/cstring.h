#ifndef _CSTRING_H_
#define _CSTRING_H_

#include "util/comparable.h"
#include <string.h>

class CString : public Comparable {
    public:
        CString(char* inner) {
            _data = inner;
        }
        
        static CString* copy(char* data) {
            char* d = new char[strlen(data)+1];
            strcpy(d, data);
            return new CString(d);
        }
        
        char* data();
        virtual int compareTo(Comparable* other);
    private:
        char* _data;
};

#endif
