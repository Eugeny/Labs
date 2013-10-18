#include "scorescreen.h"
#include "dal/dal.h"
#include "dal/objects.h"
#include <ncurses.h>

void ScoreScreen::onShown() {
    data = new HashMap<Faculty*, HashMap<Speciality*, int*>*>();
    List<Faculty*>* facs = DAL::get()->find(DAL::ALL<Faculty>, 0);
    for (int i = 0; i < facs->length(); i++) {
        HashMap<Speciality*, int*>* d = new HashMap<Speciality*, int*>();
        Faculty* fac = facs->get(i);
        data->put(fac, d);
        
        List<Speciality*>* specs = DAL::get()->find(Speciality::BY_FACULTY, fac);
        for (int j = 0; j < specs->length(); j++) {
            Speciality* spec = specs->get(j);
            int* ints = new int[40];
            d->put(spec, ints);
            
            for (int k = 0; k < 40; k++)
                ints[k] = 0;
                
            List<Student*>* studs = DAL::get()->find(Student::BY_SPECIALITY, spec);
            for (int k = 0; k < studs->length(); k++) {
                Student* stud = studs->get(k);
                int s = 
                    stud->results[0] + 
                    stud->results[1] + 
                    stud->results[2] + 
                    stud->results[3];
                if (s == 400) s--;
                ints[s/10]++;
            }
            delete studs;
            
        }
        delete specs;
    }
    delete facs;
    
    dx = 0;
    dy = 0;
}

void ScoreScreen::onClosed() {
    delete data;
}

const int CELL_W = 5;
const int SPEC_W = 10;

int ScoreScreen::visible(int x, int y, int dx, int dy) {
    if (x+dx < SPEC_W && dx != 0)
        return 0;
    if (x+dx > WIDTH-CELL_W)
        return 0;
    if (y+dy < 2 && dy != 0)
        return 0;
    if (y+dy > HEIGHT-1)
        return 0;
    return 1;
}

void ScoreScreen::print(char* s, int x, int y, int dx, int dy) {
    if (visible(x,y,dx,dy))
        mvprintw(y+dy, x+dx, s);
}

void ScoreScreen::draw() {
    int y = 2;

    BaseScreen::draw();
        
    for (int i = 39; i >= 0; i--) {
        char* s = new char[10];
        sprintf(s, "%i+", i*10);
        print(s, SPEC_W + (39-i)*CELL_W, 0, dx, 0);
        delete s;
    }
    
    
    for (int i = 0; i < data->length(); i++) {
        print(data->getKeyByIndex(i)->name, 0, y++, 0, dy);
        HashMap<Speciality*, int*>* specs = data->getByIndex(i);
        
        for (int j = 0; j < specs->length(); j++) {
            Speciality* spec = specs->getKeyByIndex(j);
            int* ints = specs->getByIndex(j);
            int sum = 0;
            
            for (int k = 39; k >= 0; k--) {
                char* s = new char[10];
                sprintf(s, "%i", ints[k]);
                
                if (ints[k] > 0 && sum <= spec->limit)
                    attron(COLOR_PAIR(Controller::COLORS_HL_G));
                if (ints[k] > 0 && sum > spec->limit)
                    attron(COLOR_PAIR(Controller::COLORS_HL_R));
                if (ints[k] > 0 && sum <= spec->limit && sum + ints[k] > spec->limit)
                    attron(COLOR_PAIR(Controller::COLORS_HL_Y));
                
                print(s, SPEC_W + (39-k)*CELL_W, y, dx, dy);
                
                if (ints[k] > 0 && sum <= spec->limit)
                    attroff(COLOR_PAIR(Controller::COLORS_HL_G));
                if (ints[k] > 0 && sum > spec->limit)
                    attroff(COLOR_PAIR(Controller::COLORS_HL_R));
                if (ints[k] > 0 && sum <= spec->limit && sum + ints[k] > spec->limit)
                    attroff(COLOR_PAIR(Controller::COLORS_HL_Y));
                
                
                sum += ints[k];
                delete s;
            }
            print(spec->name, 1, y++, 0, dy);
        }
    }
}

void ScoreScreen::onKey(int code) {
    if (code == KEY_UP && dy < 0)
        dy++;
    if (code == KEY_DOWN)
        dy--;
    if (code == KEY_RIGHT)
        dx--;
    if (code == KEY_LEFT && dx < 0)
        dx++;
    
    BaseScreen::onKey(code);
}
