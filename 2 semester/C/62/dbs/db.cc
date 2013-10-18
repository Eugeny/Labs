#include "dbs/db.h"
#include "dbs/dbcells.h"
#include "util/util.h"


DB::DB(char* path) {
    file = path;
    
    FILE* f = fopen(path, "rb");
    int tableCount = Util::readInt(f);
    
    tables = new HashMap<CString*, DBTable*>();
    
    for (int i = 0; i < tableCount; i++) {
        DBTable* t = new DBTable();
        t->read(f);
        tables->put(new CString(t->getName()), t);
    }
    
    fclose(f);
}

DBCell* DB::newCell(int type) {
    if (type == DB::CELL_STRING)
        return new DBStringCell(0);
    if (type == DB::CELL_INT)
        return new DBIntCell(0);
    return 0;
}

void DB::createTable(char* name, int* schema, int schemaSize) {
    DBTable* t = new DBTable(name, schema, schemaSize);
    tables->put(new CString(t->getName()), t);
}

void DB::dropTable(char* name) {
    CString* n = new CString(name);
    tables->remove(n);
    free(n);
}

void DB::save() {
    FILE* f = fopen(file, "w");

    Util::writeInt(f, tables->length());
    for (int i = 0; i < tables->length(); i++) {
        tables->getByIndex(i)->write(f);                
    }
           
    fclose(f);
}

char* DB::toString() {
    char* s = new char[1024*1024];
    sprintf(s, "DB dump:\n");
    for (int i = 0; i < tables->length(); i++) {
        char* r = tables->getByIndex(i)->toString();
        strcat(s, r);
        free(r);
        strcat(s, "\n");
    }
    return s;
}

DBTable* DB::getTable(char* name) {
    CString* s = new CString(name);
    DBTable* r = tables->get(s);
    free(s);
    return r;
}

int DB::insert(char* table, DBRow* r) {
    DBTable* t = getTable(table);
    return t->insert(r);
}

List<DBRow*>* DB::select(char* table, int func(DBRow*, char*), char* arg) {
    DBTable* t = getTable(table);
    List<DBRow*>* res = new List<DBRow*>();
    HashMap<int, DBRow*>* r = t->getRows();   
    
    for (int i = 0; i < r->length(); i++) {
        DBRow* row = r->getByIndex(i);
        if (func(row, arg))
            res->add(row);
    }
    
    return res;
}

DBRow* DB::selectOne(char* table, int id) {
    DBTable* t = getTable(table);
    return t->getRows()->get(id);
}

void DB::del(char* table, int func(DBRow*, char*), char* arg) {
    DBTable* t = getTable(table);
    HashMap<int, DBRow*>* r = t->getRows();   
    
    for (int i = 0; i < r->length(); i++) {
        DBRow* row = r->getByIndex(i);
        if (func(row, arg)) {
            t->del(row);
            i--;
        }   
    }
}


char* DB::dumpList(List<DBRow*>* rows) {
    char* s = new char[1024];
    sprintf(s, "Rows dump:\n");
    for (int i = 0; i < rows->length(); i++) {
        char* r = rows->get(i)->toString();
        strcat(s, r);
        free(r);
        strcat(s, "\n");
    }
    return s;
}

