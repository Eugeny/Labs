#include "dbs/db.h"
#include "dbs/dbtable.h"
#include "dbs/dbrow.h"
#include "util/util.h"
#include <stdio.h>
#include <string.h>


DB* db;

void log(char* s) {
    printf("* %s\n", s);
}

void showTables() {
    printf("Tables:\n");
    for (int i = 0; i < db->getTables()->length(); i++)
        printf(" %s\n", db->getTables()->getByIndex(i)->getName());
}

void read(char* buf) {
    fgets(buf, 256, stdin);
    buf[strlen(buf)-1] = 0;
}

int ALL(DBRow* r, char* c) { return 1; }
int BY_INDEX(DBRow* r, char* c) { return r->getIndex() == (int)c; }

void select() {
    printf("Table name > ");
    char name[256];
    read(name);
    printf("\n");
    DBTable* t = db->getTable(name);
    if (!t) {
        printf("No such table\n");
    } else {
        for (int i = 0; i < t->getRows()->length(); i++) {
            DBRow* r = t->getRows()->getByIndex(i);
            printf(" %i\t| ", r->getIndex());
            for (int j = 0; j < t->getSchemaSize(); j++)
                printf(" %s |", r->get(j)->toString());
            printf("\n");
        }
    }
}

void insert() {
    printf("Table name > ");
    char n[256];
    read(n);
    printf("\n");
    DBTable* t = db->getTable(n);

    if (!t) {
        printf("No such table\n");
        return;
    }
    
    DBRow* r = new DBRow(t);
    for (int i = 0; i < t->getSchemaSize(); i++) {
        printf("Cell %i", i);
        if (t->getSchema()[i] == DB::CELL_INT)
            printf(" [int] > ");
        if (t->getSchema()[i] == DB::CELL_STRING)
            printf(" [string] > ");

        char* s = new char[256];
        read(s);
        printf("\n");
        
        if (t->getSchema()[i] == DB::CELL_INT) {
            r->set(i, new DBIntCell(atoi(s)));
            delete s;
        }
        if (t->getSchema()[i] == DB::CELL_STRING)
            r->set(i, new DBStringCell(s));
    }
    
    int idx = t->insert(r);
    printf("Inserted index %i\n", idx);
}

void del() {
    printf("Table name > ");
    char n[256];
    read(n);
    printf("\n");

    char i[256];
    printf("Index > ");
    read(i);
    printf("\n");
    
    db->del(n, BY_INDEX, (char*)atoi(i));
}

void commit() {
    db->save();
    log("Saved DB");
}

int main() {
    db = new DB("db");
    log("Loaded DB");
    
    char cmd[256];
    while (true) {
        printf("\n> ");
        read(cmd);
        printf("\n");
        
        if (strcmp(cmd, "tables") == 0)
            showTables();
        if (strcmp(cmd, "select") == 0)
            select();
        if (strcmp(cmd, "insert") == 0)
            insert();
        if (strcmp(cmd, "commit") == 0)
            commit();
        if (strcmp(cmd, "delete") == 0)
            del();
    }
    
    db->save();
    delete db;
    return 0;
}

