#include "ui/basescreen.h"
#include "ui/controller.h"
#include <ncurses.h>
#include <locale.h>
#include <stdio.h>
#include <string.h>

void BaseScreen::draw() {
    Controller::get()->fillRect(0, 0, WIDTH, HEIGHT);
    
	attron(COLOR_PAIR(Controller::COLORS_STATUSBAR));
	Controller::get()->fillRect(0, HEIGHT-1, WIDTH, 1);
    if (helpText)
        mvprintw(HEIGHT-1, 1, helpText);
    mvprintw(HEIGHT-1, WIDTH-40, Locale::g("basehelp"));
	attroff(COLOR_PAIR(Controller::COLORS_STATUSBAR));
}

void BaseScreen::setHelpText(char* text) {
    helpText = text;
}

void BaseScreen::onKey(int code) {
    if (code == KEY_F(9)) {
        if (strcmp(Locale::get()->locale, "ru"))
            Locale::get()->load("ru", "locale/ru-RU");
        else
            Locale::get()->load("en", "locale/en-US");
    }
    if (code == 27)
        Controller::get()->exit();
    if (code == KEY_BACKSPACE)
        Controller::get()->closeScreen();
}
