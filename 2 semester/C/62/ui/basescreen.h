#ifndef _BASESCREEN_H_
#define _BASESCREEN_H_

#include <ui/screen.h>
#include <ui/controller.h>

class BaseScreen : public Screen {
public:
    virtual void draw();
    virtual void onKey(int code);
    void setHelpText(char* text);
private:
    char* helpText;    
};

#endif
