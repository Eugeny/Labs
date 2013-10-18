#include "ui/controller.h"
#include "ui/screen.h"
#include "util/util.h"
#include <stdio.h>
#include <ncurses.h>
#include <string.h>


Controller::Controller() {
    _screens = new List<Screen*>();
}

void Controller::showScreen(Screen* screen) {
    _needkey = 0;
    _screens->add(screen);
    screen->onShown();
}

void Controller::closeScreen() {
    _needkey = 0;
    delete _screens->get(_screens->length()-1);
    _screens->remove(_screens->length()-1);
}

void Controller::run() {
    _running = 1;

    initscr();    
    raw();
    noecho();
    keypad(stdscr, 1);

    start_color();
    
    init_color(COLOR_RED, 1000, 00, 00);    
    init_color(COLOR_BLUE, 00, 00, 1000);    
    init_color(COLOR_CYAN, 00, 1000, 1000);    
    init_color(COLOR_GREEN, 00, 1000, 00);    
    init_color(COLOR_RED, 1000, 1000, 00);    
    
    init_pair(0, COLOR_WHITE, COLOR_BLACK);
    init_pair(Controller::COLORS_STATUSBAR, COLOR_BLACK, COLOR_WHITE);
    init_pair(Controller::COLORS_BORDER, COLOR_BLUE, COLOR_CYAN);
    init_pair(Controller::COLORS_WINDOW, COLOR_WHITE, COLOR_BLUE);
    init_pair(Controller::COLORS_WINDOW_INV, COLOR_BLUE, COLOR_WHITE);
    init_pair(Controller::COLORS_HL_G, COLOR_GREEN, COLOR_BLACK);
    init_pair(Controller::COLORS_HL_R, COLOR_RED, COLOR_BLACK);
    init_pair(Controller::COLORS_HL_Y, COLOR_YELLOW, COLOR_BLACK);
    
    while (_running) {
        Screen* screen = _screens->get(_screens->length()-1);

        getmaxyx(stdscr, screen->HEIGHT, screen->WIDTH);
        curs_set(0);
        
        attron(COLOR_PAIR(0));
        screen->draw();
        refresh();

        _needkey = 1;
    
        screen->work();
        
        if (_needkey) {
            int k = getch();
            screen->onKey(k);
        }

        if (_screens->length() == 0)
            exit();
    }
    endwin();
}

void Controller::exit() {
    _needkey = 0;
    _running = 0;
}

void Controller::fillRect(int x, int y, int w, int h) {
    for (int i = x; i < x + w; i++)
        for (int j = y; j < y + h; j++)
            mvprintw(j, i, " ");
}

void Controller::drawWin(int x, int y, int w, int h, char* title) {
	attron(COLOR_PAIR(Controller::COLORS_BORDER));
	fillRect(x,y,w,h);
	attroff(COLOR_PAIR(Controller::COLORS_BORDER));
	if (title) {
    	attron(COLOR_PAIR(Controller::COLORS_WINDOW_INV));
	    mvprintc(y, x+w/2, title);
    	attroff(COLOR_PAIR(Controller::COLORS_WINDOW_INV));
	}
	attron(COLOR_PAIR(Controller::COLORS_WINDOW));
	fillRect(x+1,y+1,w-2,h-2);
}

void Controller::endWin() {
	attroff(COLOR_PAIR(Controller::COLORS_WINDOW));
}

void Controller::mvprintc(int y, int x, char* s) {
    int dx = -(Util::slen(s)/2);
    mvprintw(y, x+dx, s);
}
