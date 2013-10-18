#ifndef _DBROW_H_
#define _DBROW_H_

#include "dbs/dbcell.h"
#include "util/list.h"

class DBTable;

class DBRow {
public:
    DBRow(DBTable* t);
    virtual ~DBRow();
    DBCell* get(int idx);
    void set(int idx, DBCell* cell);
    void read(FILE* f);
    void write(FILE* f);
    int getIndex() { return index; }
    void setIndex(int idx) { index = idx; }
    char* toString();
private:
    int index;
    DBTable* table; 
    DBCell** cells;
};

#endif
