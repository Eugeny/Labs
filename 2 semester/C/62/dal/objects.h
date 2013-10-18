#ifndef _DAO_OBJECTS_H_
#define _DAO_OBJECTS_H_

#include "dal/dao.h"
#include "dbs/db.h"
#include "dbs/dbcells.h"
#include "util/util.h"
#include <string.h>

extern int BSUIR_FACULTIES_NUM;
extern char* BSUIR_FACULTIES[];
extern int FACULTY_SCHEMA[];


class Faculty : public Dao<Faculty> {
public:
    char* name;
    
    virtual void _reset() {
        name = 0;
    }
    
    virtual void _load(DBRow* row) {
        name = ((DBStringCell*)(row->get(0)))->get();
    }
    
    virtual void _save(DBRow* row) {
        ((DBStringCell*)(row->get(0)))->set(name);
    }
    
    virtual char* toString() {
        char* s = new char[strlen(name)+1];
        strcpy(s, name);
        return s;
    }

    static char* getTableName() { 
        return "faculties"; 
    }

    static int* getSchema() { return FACULTY_SCHEMA; }
    static int getSchemaSize() { return 1; }

    static void makePresets(DAL* dal) {
        for (int i = 0; i < BSUIR_FACULTIES_NUM; i++) {
            Faculty* t = new Faculty();
            t->name = BSUIR_FACULTIES[i];
            t->save();
        }
    }
};



extern int BSUIR_SPECS_NUM;
extern char* BSUIR_SPECS_NAMES[];
extern int BSUIR_SPECS_FACS[];
extern int BSUIR_SPECS_LIMITS[];
extern int SPECIALITY_SCHEMA[];


class Speciality : public Dao<Speciality> {
public:
    char* name;
    int faculty;
    int limit;
    
    virtual void _reset() {
        name = 0;
        faculty = -1;
        limit = 0;
    }
    
    virtual void _load(DBRow* row) {
        name = ((DBStringCell*)(row->get(0)))->get();
        faculty = ((DBIntCell*)(row->get(1)))->get();
        limit = ((DBIntCell*)(row->get(2)))->get();
    }
    
    virtual void _save(DBRow* row) {
        ((DBStringCell*)(row->get(0)))->set(name);
        ((DBIntCell*)(row->get(1)))->set(faculty);
        ((DBIntCell*)(row->get(2)))->set(limit);
    }
    
    virtual char* toString() {
        char* s = new char[strlen(name)+1];
        strcpy(s, name);
        return s;
    }

    
    static char* getTableName() { 
        return "specialities"; 
    }

    static int* getSchema() { return SPECIALITY_SCHEMA; }
    static int getSchemaSize() { return 3; }

    static void makePresets(DAL* dal) {
        for (int i = 0; i < BSUIR_SPECS_NUM; i++) {
            Speciality* t = new Speciality();
            t->name = BSUIR_SPECS_NAMES[i];
            t->faculty = BSUIR_SPECS_FACS[i];
            t->limit = BSUIR_SPECS_LIMITS[i];
            t->save();
        }
    }
    
    static int BY_FACULTY(Speciality* i, Faculty* f) { return i->faculty == f->id; }
};



extern int STUDENT_SCHEMA[];

class Student : public Dao<Student> {
public:
    char* name;
    char* patrName;
    char* lastName;
    int speciality;
    int results[4];
    char* passport;
    char* address;
    char* school;
    int password;
    
    virtual void _reset() {
        name = 0;
        lastName = 0;
        patrName = 0;
        speciality = -1;
        passport = 0;
        address = 0;
        school = 0;
        password = 0;
        results[0] = 0;
        results[1] = 0;
        results[2] = 0;
        results[3] = 0;
    }
    
    
    virtual void _load(DBRow* row) {
        name = ((DBStringCell*)(row->get(0)))->get();
        lastName = ((DBStringCell*)(row->get(1)))->get();
        patrName = ((DBStringCell*)(row->get(2)))->get();
        speciality = ((DBIntCell*)(row->get(3)))->get();
        results[0] = ((DBIntCell*)(row->get(4)))->get();
        results[1] = ((DBIntCell*)(row->get(5)))->get();
        results[2] = ((DBIntCell*)(row->get(6)))->get();
        results[3] = ((DBIntCell*)(row->get(7)))->get();
        passport = ((DBStringCell*)(row->get(8)))->get();
        address = ((DBStringCell*)(row->get(9)))->get();
        school = ((DBStringCell*)(row->get(10)))->get();
        password = ((DBIntCell*)(row->get(11)))->get();
    }
    
    virtual void _save(DBRow* row) {
        ((DBStringCell*)(row->get(0)))->set(name);
        ((DBStringCell*)(row->get(1)))->set(lastName);
        ((DBStringCell*)(row->get(2)))->set(patrName);
        ((DBIntCell*)(row->get(3)))->set(speciality);
        ((DBIntCell*)(row->get(4)))->set(results[0]);
        ((DBIntCell*)(row->get(5)))->set(results[1]);
        ((DBIntCell*)(row->get(6)))->set(results[2]);
        ((DBIntCell*)(row->get(7)))->set(results[3]);
        ((DBStringCell*)(row->get(8)))->set(passport);
        ((DBStringCell*)(row->get(9)))->set(address);
        ((DBStringCell*)(row->get(10)))->set(school);
        ((DBIntCell*)(row->get(11)))->set(password);
    }
    
    virtual char* toString() {
        char* s = new char[strlen(name)+1];
        strcpy(s, name);
        return s;
    }

    int getSum() {
        int sum = 0;
        for (int i = 0; i < 4; sum += results[i++]);
        return sum;
    }
    
    int gt(Student* o) {
        return (this->getSum() > o->getSum());
    }
    
    static char* getTableName() { 
        return "students"; 
    }

    static int* getSchema() { return STUDENT_SCHEMA; }
    static int getSchemaSize() { return 12; }

    static void makePresets(DAL* dal) { }
    
    static int BY_SPECIALITY(Student* s, Speciality* f) { return s->speciality == f->id; }
    static int BY_PASSWORD(Student* s, int pw) { return s->password == pw; }
};



class SpecialityUtil {
public:
    static int getBall(Speciality* spec) {
        List<Student*>* t = DAL::get()->find(Student::BY_SPECIALITY, spec);
        List<Student*>* l = Util::sorted(t);
        delete t;
        
        if (l->length() == 0) {
            delete l;
            return 0;
        }
            
        int p = 0;
        if (spec->limit >= l->length()) {
            p = l->get(l->length()-1)->getSum();
        } else {
            int pos = spec->limit-1;
            while (l->get(pos)->getSum() == l->get(pos+1)->getSum())
                pos--;
            p = l->get(pos)->getSum();
        }

        delete l;        
        return p;
    }

    static int getSemiBall(Speciality* spec) {
        List<Student*>* t = DAL::get()->find(Student::BY_SPECIALITY, spec);
        List<Student*>* l = Util::sorted(t);
        delete t;
        
        if (l->length() == 0) {
            delete l;
            return 0;
        }

        int sp = 0;
        if (spec->limit < l->length()) {
            int pos = spec->limit-1;
            while (l->get(pos)->getSum() == l->get(pos+1)->getSum())
                pos--;
            if (pos < l->length() - 2 && l->get(pos+1)->getSum() == l->get(pos+2)->getSum())
                sp = l->get(pos+1)->getSum(); 
        }

        delete l;        
        return sp;
    }
};
#endif

