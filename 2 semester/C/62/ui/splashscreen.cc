#include "dal/dal.h"
#include "dal/objects.h"
#include "ui/splashscreen.h"
#include "ui/mainscreen.h"
#include "ui/basescreen.h"
#include "ui/controller.h"
#include "util/store.h"
#include "locale.h"
#include <ncurses.h>


void SplashScreen::draw() {
    BaseScreen::draw();
    
	Controller::get()->drawWin(WIDTH/2-15, HEIGHT/2-5, 30, 10, 0);
    
    Controller::mvprintc(HEIGHT/2 - 3, WIDTH/2, Locale::g("appname"));
    Controller::mvprintc(HEIGHT/2 + 3, WIDTH/2, Locale::g("copyright"));

	Controller::get()->endWin();
}    
 
void SplashScreen::work() {
    DAL::get()->model<Faculty>();
    DAL::get()->model<Speciality>();
    DAL::get()->model<Student>();
}

void SplashScreen::onKey(int code) {
    BaseScreen::onKey(code);
    
    Controller::get()->closeScreen();
    Controller::get()->showScreen(new MainScreen());    
}
