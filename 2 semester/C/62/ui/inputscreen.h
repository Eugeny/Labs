#ifndef _INPUTSCREEN_H_
#define _INPUTSCREEN_H_

#include "dal/dal.h"
#include "ui/basescreen.h"
#include "ui/controller.h"
#include <ncurses.h>
#include <string.h>


class InputScreen : public BaseScreen {
public:

    typedef void (*InputScreenCallback)(char*);
    typedef char* (*InputScreenValidator)(char*);

    InputScreen(char* title, InputScreenCallback cb, InputScreenValidator v) {
        _callback = cb;
        _validator = v;
        _title = title;
        _text = new char[256];
        _text[0] = 0;
        _err = "";
    }
     
    virtual void onKey(int code) {
        if (code == '\n' && !_err) {
            _callback(_text);
            return;
        }
        if (
            (code >= 'a' && code <= 'z') ||
            (code >= 'A' && code <= 'Z') ||
            (code >= "а"[1] && code <= "я"[1]) ||
            (code >= "А"[1] && code <= "Я"[1]) ||
            (code >= '0' && code <= '9') ||
            (code >= 120 && code <= 200) ||
            code == ' ' || code == 209 || code == 208) {
                _text[strlen(_text)+1] = 0;
                _text[strlen(_text)] = code;
        }
        //printf("%i ", code);
        if (code == KEY_BACKSPACE) {
            if (strlen(_text) > 0)
                _text[strlen(_text)-1] = 0;
        } else 
            BaseScreen::onKey(code);
        _err = _validator(_text);
    }
    
    virtual void draw() {
        BaseScreen::draw();
        int _width = 30;
        int _size = 2;
        Controller::get()->drawWin(WIDTH/2 - _width/2 - 3, HEIGHT/2 - _size/2 - 3, _width + 4, _size + 4, _title);
        attron(COLOR_PAIR(Controller::COLORS_WINDOW));
        if (_err)
            mvprintw(HEIGHT/2 - _size/2, WIDTH/2 - _width/2 - 1, _err);
      	attron(COLOR_PAIR(Controller::COLORS_WINDOW_INV));
        Controller::get()->fillRect(WIDTH/2 - _width/2 - 1, HEIGHT/2 - _size/2 - 1, _width, 1);
        mvprintw(HEIGHT/2 - _size/2 - 1, WIDTH/2 - _width/2 - 1, _text);
        curs_set(1);
        Controller::get()->endWin();   
    }

    static char* NOT_EMPTY(char* t) {   
        if (strlen(t) == 0)
            return "Введите текст";
        return 0;
    }

    static char* NUMERIC(char* t) {
        char* endptr = 0;
        strtol(t, &endptr, 10);
        if (endptr && *endptr != 0)
            return "Введите число";
        return 0;
    }

    static char* BALLS(char* t) {
        char* endptr = 0;
        int r = strtol(t, &endptr, 10);
        if (endptr && *endptr != 0)
            return "Введите число";
        if (r < 0 || r > 100)
            return "Введите число от 0 до 100";
        return 0;
    }
        
private:
    char* _title;
    char* _text;
    char* _err;
    InputScreenValidator _validator;
    InputScreenCallback _callback;
};

#endif

