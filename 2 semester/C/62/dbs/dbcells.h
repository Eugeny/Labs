#ifndef _DBCELLS_H_
#define _DBCELLS_H_

#include "dbs/dbcell.h"

class DBStringCell : public DBCell{
public:
    DBStringCell(char* data);
    virtual char* toString();
    char* get();
    void set(char* s);
    virtual void read(FILE* f);
    virtual void write(FILE* f);
    virtual ~DBStringCell();
private:
    char* data;    
};

class DBIntCell : public DBCell{
public:
    DBIntCell(int data);
    virtual char* toString();
    int get();
    void set(int v);
    virtual void read(FILE* f);
    virtual void write(FILE* f);
private:
    int data;    
};

#endif
