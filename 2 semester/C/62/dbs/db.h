#ifndef _DB_H_
#define _DB_H_

#include "dbs/dbcell.h"
#include "dbs/dbcells.h"
#include "dbs/dbtable.h"
#include "util/list.h"
#include "util/cstring.h"

class DB {
public:
    DB(char* path);
    void save();
    
    void createTable(char* name, int* schema, int schemaSize);
    int insert(char* table, DBRow* data);
    void dropTable(char* name);
    List<DBRow*>* select(char* table, int func(DBRow*, char*), char* arg);
    DBRow* selectOne(char* table, int id);
    void del(char* table, int func(DBRow*, char*), char* arg);
        
    char* toString();
    char* dumpList(List<DBRow*>* l);
    
    DBTable* getTable(char* name);
    HashMap<CString*, DBTable*>* getTables() { return tables; }
    
    static DBCell* newCell(int type);
    static const int CELL_STRING = 1;
    static const int CELL_INT = 2;
private:
    char* file;    
    HashMap<CString*, DBTable*>* tables;
};

#endif
