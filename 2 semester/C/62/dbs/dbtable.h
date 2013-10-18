#ifndef _DBTABLE_H_
#define _DBTABLE_H_

#include "util/hashmap.h"
#include "dbrow.h"
#include <stdio.h>

class DBTable {
public:
    DBTable();
    DBTable(char* name, int* schema, int schemaSize);
    virtual ~DBTable();
    int insert(DBRow* row);
    void del(DBRow* row);
    void read(FILE* f);
    void write(FILE* f);
    int getSchemaSize();
    int* getSchema();
    char* getName();
    char* toString();
    HashMap<int, DBRow*>* getRows() { return rows; }
private:
    int* schema;    
    int schemaSize;
    int nextIndex;
    char* name;
    HashMap<int, DBRow*>* rows;
};

#endif
