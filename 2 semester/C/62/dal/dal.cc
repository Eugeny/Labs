#include "dal/dal.h"
#include "dbs/db.h"


DAL::DAL() {
    db = new DB("db");
    commit();
}

void DAL::commit() {
    db->save();
}

void DAL::dump() {
    puts(db->toString());
}
