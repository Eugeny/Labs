#include "locale.h"
#include "util/cstring.h"
#include <string.h>
#include <stdio.h>


Locale::Locale() {
    _data = new HashMap<CString*, char*>();
}

void Locale::load(char* name, char* file) {
    FILE* f = fopen(file, "r");
    locale = name;
    char s[256];
    _data->clear();
    while (fgets(s, 256, f)) {
        if (strlen(s)>0) {
            char* v = new char[256];
            while (s[strlen(s)-1] == '\n')
                s[strlen(s)-1] = 0;
            fgets(v, 256, f);
            while (v[strlen(v)-1] == '\n')
                v[strlen(v)-1] = 0;
            _data->put(CString::copy(s), v);
        }
    }
}

char* Locale::getString(char* name) {
    CString* q = new CString((char*)name);
    char* r = _data->get(q);
    free(q);
    return r;
}

char* Locale::g(char* name) {
    return Locale::get()->getString(name);
}
