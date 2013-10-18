#ifndef _DAO_H_
#define _DAO_H_

#include "dal/dal.h"
#include "dbs/dbrow.h"
#include "dbs/dbcell.h"

template <class T>
class Dao {
public:
    Dao() { 
        _reset(); 
        id = -1; 
    }
     
    virtual void save() {
        if (id >= 0)
            DAL::get()->update(this);
        else
            DAL::get()->create(this);
    }
    
    virtual void del() {
        DAL::get()->del(this);
    }
     
    virtual void _reset() {}
    virtual void _load(DBRow* row) {}
    virtual void _save(DBRow* row) {}
    virtual char* toString() { return 0; }
    
    static char* getTableName() { return T::getTableName(); }
    static int* getSchema() { return T::getSchema(); }
    static int getSchemaSize() { return T::getSchemaSize(); }
    static void makePresets(DAL* dal) { T::makePresets(); }
     
    int id;     
};

#endif
