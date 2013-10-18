#include "ui/basescreen.h"
#include "ui/controller.h"
#include "ui/basemenuscreen.h"
#include "util/util.h"
#include "ncurses.h"

BaseMenuScreen::BaseMenuScreen() {
    _sel = 0;
}

BaseMenuScreen::~BaseMenuScreen() {
    delete _items;
}

void BaseMenuScreen::setItems(char** items, int len) {
    _size = len;
    _items = items;
    _width = 0;
    
    for (int i = 0; i < len; i++) {
        int l = Util::slen(items[i]);
        if (l > _width)
            _width = l;
    }
}

void BaseMenuScreen::setTitle(char* title) {
    _title = title;
    if (Util::slen(_title) > _width) 
        _width = Util::slen(_title);
}

void BaseMenuScreen::draw() {
    BaseScreen::draw();
    Controller::get()->drawWin(WIDTH/2 - _width/2 - 3, HEIGHT/2 - _size/2 - 3, _width + 4, _size + 4, _title);
    for (int i = 0; i < _size; i++) {
        if (_sel == i)
        	attron(COLOR_PAIR(Controller::COLORS_WINDOW_INV));
        else
        	attron(COLOR_PAIR(Controller::COLORS_WINDOW));
        Controller::get()->fillRect(WIDTH/2 - _width/2 - 1, HEIGHT/2 - _size/2 - 1 + i, _width, 1);
        mvprintw(HEIGHT/2 - _size/2 - 1 + i, WIDTH/2 - _width/2 - 1, _items[i]);
    }
    Controller::get()->endWin();   
}

void BaseMenuScreen::onKey(int code) {
    BaseScreen::onKey(code);
    if (code == KEY_UP && _sel > 0)
        _sel--;
    if (code == KEY_DOWN && _sel < _size - 1)
        _sel++;
    if (code == '\n')
        onSelected(_sel);
}
