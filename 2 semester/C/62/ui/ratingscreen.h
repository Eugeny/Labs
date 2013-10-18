#ifndef _RATINGSCREEN_H_
#define _RATINGSCREEN_H_

#include "dal/objects.h"
#include "ui/basemenuscreen.h"
#include "ui/controller.h"
#include "util/hashmap.h"

class RatingScreen : public BaseScreen {
public:
    virtual void onShown();
    virtual void onClosed();
    virtual void onKey(int);
    virtual void draw();
private:
    List<Student*>* data;
    int visible(int x, int y, int dx, int dy);
    void print(char* s, int x, int y, int dx, int dy);    
    int dy;
};

#endif
