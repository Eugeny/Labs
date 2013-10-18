#include "ui/reportscreen.h"
#include "dal/dal.h"
#include "dal/objects.h"
#include "util/util.h"
#include <ncurses.h>


void ReportScreen::onShown() {
    data = new HashMap<Speciality*, List<Student*>*>();
    List<Speciality*>* specs = DAL::get()->find(DAL::ALL<Speciality>, 0);
    
    for (int i = 0; i < specs->length(); i++) {
        Speciality* spec = specs->get(i);
        List<Student*>* t = DAL::get()->find(Student::BY_SPECIALITY, specs->get(i));
        data->put(
            spec, 
            Util::sorted(t)
        );
        delete t;
    }
    
    dy = 0;
}

int ReportScreen::visible(int x, int y, int dx, int dy) {
    if (x+dx < 1 && dx != 0)
        return 0;
    if (x+dx > WIDTH)
        return 0;
    if (y+dy < 0 && dy != 0)
        return 0;
    if (y+dy > HEIGHT-1)
        return 0;
    return 1;
}

void ReportScreen::print(char* s, int x, int y, int dx, int dy) {
    if (visible(x,y,dx,dy))
        mvprintw(y+dy, x+dx, s);
}

void ReportScreen::draw() {
    int y = 1;

    BaseScreen::draw();
    
    for (int i = 0; i < data->length(); i++) {
        List<Student*>* studs = data->getByIndex(i);
        if (studs->length() == 0)
            continue;
        
        y++;

        Speciality* spec = data->getKeyByIndex(i);
        Faculty* fac = DAL::get()->findOne(DAL::BY_ID<Faculty>, spec->faculty);
        
        attron(COLOR_PAIR(Controller::COLORS_BORDER));
        Controller::get()->fillRect(1, y+dy, WIDTH, 1);
        print(fac->name, 1, y, 0, dy);
        print(spec->name, 10, y, 0, dy);
        attroff(COLOR_PAIR(Controller::COLORS_BORDER));
        
        attron(COLOR_PAIR(Controller::COLORS_HL_G));
        Controller::get()->fillRect(WIDTH-21, y+dy, 5, 1);
        char* b = Util::itoa(SpecialityUtil::getBall(spec));
        print(b, WIDTH-20, y, 0, dy);
        delete b;
        
        attroff(COLOR_PAIR(Controller::COLORS_HL_G));
        attron(COLOR_PAIR(Controller::COLORS_HL_Y));
        int semiBall = SpecialityUtil::getSemiBall(spec);
        b = Util::itoa(semiBall);
        if (semiBall > 0) {
            Controller::get()->fillRect(WIDTH-11, y+dy, 5, 1);
            print(b, WIDTH-10, y, 0, dy);
        }
        attroff(COLOR_PAIR(Controller::COLORS_HL_Y));
                
        y++;
        
        for (int j = 0; j < studs->length(); j++) {
            Student* s = studs->get(j);
            if (s->getSum() < semiBall)
                break;

            attron(COLOR_PAIR(Controller::COLORS_WINDOW));
            Controller::get()->fillRect(1, y+dy, WIDTH, 1);

            char* n = Util::itoa(j);
            print(n, 2, y, 0, dy);
            delete n;
            print(s->name, 5, y, 0, dy);
            print(s->patrName, 20, y, 0, dy);
            print(s->lastName, 35, y, 0, dy);
            
            char* t = Util::itoa(s->getSum());
            print(t, WIDTH-20, y, 0, dy);
            delete t;
            
            y++;
            attroff(COLOR_PAIR(Controller::COLORS_WINDOW));
        }
    }
}

void ReportScreen::onKey(int code) {
    if (code == KEY_UP && dy < 0)
        dy++;
    if (code == KEY_DOWN)
        dy--;
    
    BaseScreen::onKey(code);
}

void ReportScreen::onClosed() {
    delete data;
}
