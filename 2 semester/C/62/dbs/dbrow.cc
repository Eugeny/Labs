#include "dbs/db.h"
#include "dbs/dbrow.h"
#include "dbs/dbcell.h"
#include "dbs/dbtable.h"
#include "util/util.h"
#include "util/list.h"


DBRow::DBRow(DBTable* t) {
    table = t;
    cells = new DBCell*[t->getSchemaSize()];
    for (int i = 0; i < table->getSchemaSize(); i++) {
        DBCell* c = DB::newCell(table->getSchema()[i]);
        cells[i] = c;
    }
}

DBRow::~DBRow() {
    delete cells;
}

DBCell* DBRow::get(int idx) {
    return cells[idx];
}

void DBRow::set(int idx, DBCell* cell) {
    cells[idx] = cell;
}

void DBRow::read(FILE* f) {
    index = Util::readInt(f);        
    for (int i = 0; i < table->getSchemaSize(); i++) {
        cells[i]->read(f);
    }
}

void DBRow::write(FILE* f) {
    Util::writeInt(f, index);
    for (int i = 0; i < table->getSchemaSize(); i++) {
        cells[i]->write(f);
    }
}

char* DBRow::toString() {
    char* s = new char[1024];
    s[0] = 0;
    sprintf(s, "%i\t: ", index);
    for (int i = 0; i < table->getSchemaSize(); i++) {
        char* c = cells[i]->toString();
        strcat(s, c);
        free(c);
        strcat(s, "\t|");
    }
    return s;
}
