#include "dal/dal.h"
#include "dal/objects.h"
#include "ui/mainscreen.h"
#include "ui/inputscreen.h"
#include "ui/selectscreen.h"
#include "ui/scorescreen.h"
#include "ui/reportscreen.h"
#include "ui/ratingscreen.h"
#include "ui/message.h"
#include "ui/basemenuscreen.h"
#include "ui/controller.h"
#include "locale.h"
#include <ncurses.h>


void MainScreen::draw() {
    if (!_items)
        _items = new char*[6];
    _items[0] = Locale::g("mm_submit");
    _items[1] = Locale::g("mm_unsubmit");
    _items[2] = Locale::g("mm_balls");
    _items[3] = Locale::g("mm_report");
    _items[4] = Locale::g("mm_rate_students");
    //_items[4] = Locale::g("mm_rate_facs");
    //_items[5] = Locale::g("mm_rate_specs");
    setItems(_items, 5);
    BaseMenuScreen::draw();
}


// Submission process
Student* subStudent;
void _cb_submit_fac(Faculty*);
void _cb_submit_spec(Speciality*);
void _cb_submit_name(char*);
void _cb_submit_lname(char*);
void _cb_submit_pname(char*);
void _cb_submit_passport(char*);
void _cb_submit_address(char*);
void _cb_submit_school(char*);
void _cb_submit_result0(char*);
void _cb_submit_result1(char*);
void _cb_submit_result2(char*);
void _cb_submit_result3(char*);

void _cb_submit_fac(Faculty* f) {
    SelectScreen<Speciality>* ss = new SelectScreen<Speciality>(
        DAL::get()->find(Speciality::BY_FACULTY, f),
        &_cb_submit_spec
    );
    ss->setTitle(Locale::g("sel_spec"));
    Controller::get()->closeScreen();
    Controller::get()->showScreen(ss);
}

void _cb_submit_spec(Speciality* s) {
    subStudent = new Student();
    subStudent->speciality = s->id;
    Controller::get()->closeScreen();
    Controller::get()->showScreen(
        new InputScreen(
            Locale::g("sel_name"),
            _cb_submit_name, 
            InputScreen::NOT_EMPTY
        ));
}


void _cb_submit_name(char* n) {
    subStudent->name = n;
    Controller::get()->closeScreen();
    Controller::get()->showScreen(
        new InputScreen(
            Locale::g("sel_lname"),
            _cb_submit_lname, 
            InputScreen::NOT_EMPTY
        ));
}

void _cb_submit_lname(char* n) {
    subStudent->lastName = n;
    Controller::get()->closeScreen();
    Controller::get()->showScreen(
        new InputScreen(
            Locale::g("sel_pname"),
            _cb_submit_pname, 
            InputScreen::NOT_EMPTY
        ));
}

void _cb_submit_pname(char* n) {
    subStudent->patrName = n;
    Controller::get()->closeScreen();
    Controller::get()->showScreen(
        new InputScreen(
            Locale::g("sel_passport"),
            _cb_submit_passport, 
            InputScreen::NOT_EMPTY
        ));
}

void _cb_submit_passport(char* n) {
    subStudent->passport = n;
    Controller::get()->closeScreen();
    Controller::get()->showScreen(
        new InputScreen(
            Locale::g("sel_address"),
            _cb_submit_address, 
            InputScreen::NOT_EMPTY
        ));
}

void _cb_submit_address(char* n) {
    subStudent->address = n;
    Controller::get()->closeScreen();
    Controller::get()->showScreen(
        new InputScreen(
            Locale::g("sel_school"),
            _cb_submit_school, 
            InputScreen::NOT_EMPTY
        ));
}

void _cb_submit_school(char* n) {
    subStudent->school = n;
    Controller::get()->closeScreen();
    Controller::get()->showScreen(
        new InputScreen(
            Locale::g("sel_result0"),
            _cb_submit_result0, 
            InputScreen::BALLS
        ));
}

void _cb_submit_result0(char* n) {
    subStudent->results[0] = atoi(n);
    Controller::get()->closeScreen();
    Controller::get()->showScreen(
        new InputScreen(
            Locale::g("sel_result1"),
            _cb_submit_result1, 
            InputScreen::BALLS
        ));
}

void _cb_submit_result1(char* n) {
    subStudent->results[1] = atoi(n);
    Controller::get()->closeScreen();
    Controller::get()->showScreen(
        new InputScreen(
            Locale::g("sel_result2"),
            _cb_submit_result2, 
            InputScreen::BALLS
        ));
}

void _cb_submit_result2(char* n) {
    subStudent->results[2] = atoi(n);
    Controller::get()->closeScreen();
    Controller::get()->showScreen(
        new InputScreen(
            Locale::g("sel_result3"),
            _cb_submit_result3, 
            InputScreen::BALLS
        ));
}


void _cb_submit_result3(char* n) {
    subStudent->results[3] = atoi(n);
    Controller::get()->closeScreen();
    subStudent->password = rand() % 9000 + 1000;
    subStudent->save();
    char* code = new char[256];
    sprintf(code, Locale::g("submitted"), subStudent->password);
    Controller::get()->showScreen(new Message(code));
}


// Unsubmission process
void _cb_unsubmit_pw(char* n) {
    int pw = atoi(n);
    Controller::get()->closeScreen();
    Student* stud = DAL::get()->findOne(Student::BY_PASSWORD, pw);
    if (!stud) {
        Controller::get()->showScreen(new Message(Locale::g("unsubmit_inv")));
    } else {
        stud->del();
        Controller::get()->showScreen(new Message(Locale::g("unsubmit_succ")));
    }
}


// Main menu

void MainScreen::onSelected(int idx) {
    if (idx == 0) {
        SelectScreen<Faculty>* ss = new SelectScreen<Faculty>(
            DAL::get()->find(DAL::ALL<Faculty>, 0),
            &_cb_submit_fac
        );
        ss->setTitle(Locale::g("sel_fac"));
        Controller::get()->showScreen(ss);
    }
    if (idx == 1) {
        Controller::get()->showScreen(
            new InputScreen(
                Locale::g("unsubmit_pw"),
                _cb_unsubmit_pw, 
                InputScreen::NUMERIC
            ));
    }
    if (idx == 2) {
        Controller::get()->showScreen(new ScoreScreen());
    }
    if (idx == 3) {
        Controller::get()->showScreen(new ReportScreen());
    }
    if (idx == 4) {
        Controller::get()->showScreen(new RatingScreen());
    }
}


