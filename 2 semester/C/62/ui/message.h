#ifndef _MESSAGESCREEN_H_
#define _MESSAGESCREEN_H_

#include <ui/basemenuscreen.h>
#include <ui/controller.h>

class Message : public BaseMenuScreen {
public:
    
    Message(char* msg) {
        char** _items = new char*[1];
        _items[0] = msg;
        setItems(_items, 1);
        setTitle("");
    }
    
    virtual void onSelected(int idx) {
        Controller::get()->closeScreen();
    }
    
};

#endif
