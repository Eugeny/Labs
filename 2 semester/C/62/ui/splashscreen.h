#ifndef _SPLASHSCREEN_H_
#define _SPLASHSCREEN_H_

#include <ui/basescreen.h>
#include <ui/controller.h>

class SplashScreen : public BaseScreen {
public:
    virtual void draw();
    virtual void work();
    virtual void onKey(int code);
};

#endif
