#ifndef _BASEMENUSCREEN_H_
#define _BASEMENUSCREEN_H_

#include <ui/basescreen.h>
#include <ui/controller.h>

class BaseMenuScreen : public BaseScreen {
public:
    BaseMenuScreen();
    virtual ~BaseMenuScreen();
    void setItems(char** items, int size);
    void setTitle(char* title);
    virtual void draw();
    virtual void onKey(int code);
    virtual void onSelected(int idx) {};
protected:
    char** _items;
    char* _title;
    int _size, _sel, _width;
};

#endif
