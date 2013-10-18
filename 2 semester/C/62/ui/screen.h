#ifndef _SCREEN_H_
#define _SCREEN_H_

class Screen {
public:
    virtual void draw() {};
    virtual void work() {};
    virtual void onKey(int code) {};
    virtual void onShown() {};
    virtual void onClosed() {};
    
    int WIDTH;
    int HEIGHT;
};

#endif
