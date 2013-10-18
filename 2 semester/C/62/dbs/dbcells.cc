#include "dbs/dbcells.h"
#include "util/util.h"
#include <stdio.h>
#include <string.h>


DBStringCell::DBStringCell(char* s) {
    data = s;
}

char* DBStringCell::toString() {
    char* n = new char[strlen(data)+1];
    strcpy(n, data);
    return n;
}

void DBStringCell::read(FILE* f) {
    data = Util::readString(f);
}
    
void DBStringCell::write(FILE* f) {
    Util::writeString(f, data);
}

char* DBStringCell::get() {
    return data;
}

void DBStringCell::set(char* s) {
    data = s;
}

DBStringCell::~DBStringCell() {
    delete data;
}



DBIntCell::DBIntCell(int v) {
    data = v;
}

char* DBIntCell::toString() {
    char* r = new char[256];
    sprintf(r, "%i", data);
    return r;
}

void DBIntCell::read(FILE* f) {
    data = Util::readInt(f);
}
    
void DBIntCell::write(FILE* f) {
    Util::writeInt(f, data);
}

int DBIntCell::get() {
    return data;
}

void DBIntCell::set(int v) {
    data = v;
}

