#ifndef _DBCELL_H_
#define _DBCELL_H_

#include <stdio.h>

class DBCell {
public:
    virtual char* toString() { return 0; };
    virtual void read(FILE* f) {};
    virtual void write(FILE* f) {};
    virtual ~DBCell() {}
};

#endif
