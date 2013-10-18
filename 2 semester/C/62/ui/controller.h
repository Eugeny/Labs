#ifndef _CONTROLLER_H_
#define _CONTROLLER_H_

#include "util/singleton.h"
#include "util/list.h"
#include "ui/screen.h"

class Controller : public Singleton<Controller> {
public:
    Controller();
    void showScreen(Screen* screen);
    void closeScreen();
    void run();    
    void exit();
    
    void fillRect(int x, int y, int w, int h);
    void drawWin(int x, int y, int w, int h, char* title);
    void endWin();
    static void mvprintc(int y, int x, char* s);
    
    static const int COLORS_STATUSBAR = 1;
    static const int COLORS_WINDOW = 2;
    static const int COLORS_WINDOW_INV = 4;
    static const int COLORS_BORDER = 3;
    static const int COLORS_HL_G = 5;
    static const int COLORS_HL_R = 6;
    static const int COLORS_HL_Y = 7;
    
private:
    List<Screen*>* _screens;    
    int _running;
    int _needkey;
};

#endif
