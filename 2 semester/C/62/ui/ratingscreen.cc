#include "ui/ratingscreen.h"
#include "dal/dal.h"
#include "dal/objects.h"
#include "util/util.h"
#include <ncurses.h>


void RatingScreen::onShown() {
    List<Student*>* t = DAL::get()->find(DAL::ALL<Student>, 0);
    data = Util::sorted(t);
    delete t;
    dy = 0;
}

int RatingScreen::visible(int x, int y, int dx, int dy) {
    if (x+dx < 1 && dx != 0)
        return 0;
    if (x+dx > WIDTH)
        return 0;
    if (y+dy < 2 && dy != 0)
        return 0;
    if (y+dy > HEIGHT-1)
        return 0;
    return 1;
}

void RatingScreen::print(char* s, int x, int y, int dx, int dy) {
    if (visible(x,y,dx,dy))
        mvprintw(y+dy, x+dx, s);
}

void RatingScreen::draw() {
    int y = 1;

    BaseScreen::draw();
    
    for (int i = 0; i < data->length(); i++) {
        y++;
        Student* s = data->get(i);
        print(s->name, 1, y, 0, dy);
        print(s->patrName, 15, y, 0, dy);
        print(s->lastName, 30, y, 0, dy);
        
        char* sums = Util::itoa(s->getSum());
        
        print(sums, 45, y, 0 ,dy);
        delete sums;
        
        for (int x=0; x<4; x++) {
            char* ss = Util::itoa(s->results[x]);
            print(ss, 50+5*x, y, 0, dy);
            delete ss;
        }
        
        Speciality* spec = DAL::get()->findOne(DAL::BY_ID<Speciality>, s->speciality);
        Faculty* fac = DAL::get()->findOne(DAL::BY_ID<Faculty>, spec->faculty);
        print(fac->name, 75, y, 0 , dy);
        print(spec->name, 85, y, 0 , dy);
        delete spec;
        delete fac;
    }
}

void RatingScreen::onKey(int code) {
    if (code == KEY_UP && dy < 0)
        dy++;
    if (code == KEY_DOWN)
        dy--;
    
    BaseScreen::onKey(code);
}

void RatingScreen::onClosed() {
    delete data;
}
