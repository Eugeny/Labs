#include "dbs/dbtable.h"
#include "dbs/dbrow.h"
#include "util/util.h"
#include <stdio.h>


DBTable::DBTable() {
    rows = new HashMap<int, DBRow*>();
}

DBTable::DBTable(char* name, int* schema, int schemaSize) {
    rows = new HashMap<int, DBRow*>();
    this->name = name;
    this->schema = schema;
    this->schemaSize = schemaSize;
}

DBTable::~DBTable() {
    delete rows;
}

void DBTable::read(FILE* f) {
    name = Util::readString(f);
    schemaSize = Util::readInt(f);
    int rowCount = Util::readInt(f);
    nextIndex = Util::readInt(f);
    
    schema = new int[schemaSize];
    for (int i = 0; i < schemaSize; i++) {
        schema[i] = Util::readInt(f);
    }

    for (int i = 0; i < rowCount; i++) {
        DBRow* r = new DBRow(this);
        r->read(f);
        rows->put(r->getIndex(), r);
    }
}

void DBTable::write(FILE* f) {
    Util::writeString(f, name);
    Util::writeInt(f, schemaSize);
    Util::writeInt(f, rows->length());
    Util::writeInt(f, nextIndex);

    for (int i = 0; i < schemaSize; i++) {
        Util::writeInt(f, schema[i]);
    }

    for (int i = 0; i < rows->length(); i++) {
        rows->getByIndex(i)->write(f);
    }
}


int DBTable::getSchemaSize() {
    return schemaSize;
}

int* DBTable::getSchema() {
    return schema;
}

char* DBTable::getName() {
    return name;
}

char* DBTable::toString() {
    char* s = new char[102400];
    sprintf(s, "Table %s:\n", name);
    for (int i = 0; i < rows->length(); i++) {
        char* r = rows->getByIndex(i)->toString();
        strcat(s, r);
        free(r);
        strcat(s, "\n");
    }
    return s;
}

int DBTable::insert(DBRow* r) {
    int idx = nextIndex++;
    r->setIndex(idx);
    rows->put(idx, r);
    return idx;
}

void DBTable::del(DBRow* r) {
    rows->remove(r->getIndex());
}
