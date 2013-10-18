#ifndef _MAINSCREEN_H_
#define _MAINSCREEN_H_

#include <ui/basemenuscreen.h>
#include <ui/controller.h>

class MainScreen : public BaseMenuScreen {
public:
    virtual void draw();
    virtual void onSelected(int idx);
};

#endif
