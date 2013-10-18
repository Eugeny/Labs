#include "util/cstring.h"
#include "util/comparable.h"
#include <string.h>

char* CString::data() {
    return _data;
}

int CString::compareTo(Comparable* other) {
    CString* o = (CString*)other;
//std::cout << "|"<<_data << " " <<o->data()  <<"|"<<strcmp(_data, o->data())<<std::endl;
    return !((bool)strcmp(_data, o->data()));
} 


