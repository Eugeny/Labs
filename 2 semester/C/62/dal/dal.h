#ifndef _DAL_H_
#define _DAL_H_

#include "dbs/db.h"
#include "dbs/dbrow.h"
#include "util/list.h"
#include "util/singleton.h"




class DAL : public Singleton<DAL> {
public:
    DAL();
    void commit();
    void dump();
    
    template <class D>
    void model() {
        if (!db->getTable(D::getTableName())) {
            db->createTable(
                D::getTableName(),
                D::getSchema(),
                D::getSchemaSize()
            );
            
            D::makePresets(this);
        }        
    }
    
    template <class D, typename A>
    List<D*>* find(int predicate(D* obj, A arg), A arg) {
        List<DBRow*>* r = db->select(D::getTableName(), _select_all, 0);
        
        List<D*>* res = new List<D*>();
        for (int i = 0; i < r->length(); i++) {
            D* item = new D();
            item->_load(r->get(i));
            item->id = r->get(i)->getIndex();
            if (predicate(item, arg))
                res->add(item);
            else 
                delete item;
        }
        
        return res;
    }
    
    template <class D, typename A>
    D* findOne(int predicate(D* obj, A arg), A arg) {
        List<D*>* l = find(predicate, arg);
        D* r = (l->length()>0)?(l->get(0)):0;
        delete l;
        return r;
    }

    template <class D>
    void create(D* obj) {
        DBRow* r = new DBRow(db->getTable(D::getTableName()));
        obj->_save(r);
        obj->id = db->insert(D::getTableName(), r);
        commit();
    }
    
    template <class D>
    void update(D* obj) {
        DBRow* r = db->selectOne(D::getTableName(), obj->id);
        obj->_save(r);
        commit();
    }

    template <class D>
    void del(D* obj) {
        db->del(D::getTableName(), _select_by_id, (char*)(obj->id));
        obj->id = -1;
        commit();
    }
    
    template <class D>
    static int ALL(D* o, int a) { return 1; }

    template <class D>
    static int BY_ID(D* item, int a) { return item->id == a; }    
    
private:
    DB* db;
    static int _select_all(DBRow* r, char* a) { return 1; }
    static int _select_by_id(DBRow* r, char* a) { return r->getIndex() == (int)a; }    
};

#endif 
