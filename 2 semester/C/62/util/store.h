#ifndef _STORE_H_
#define _STORE_H_

#include "dbs/db.h"

class Store {
public:
    static DB* DBS;
};

DB* Store::DBS = 0;

#endif

