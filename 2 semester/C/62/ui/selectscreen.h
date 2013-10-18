#ifndef _SELECTSCREEN_H_
#define _SELECTSCREEN_H_

#include "dal/dal.h"
#include "ui/basemenuscreen.h"
#include "ui/controller.h"


template <class D>
class SelectScreen : public BaseMenuScreen {
public:

    typedef void (*SelectScreenCallback)(D*);

    SelectScreen(List<D*>* items, void cb(D*)) {
        _callback = cb;
        _items = items;

        char** strs = new char*[items->length()];
        for (int i = 0; i < items->length(); i++)
            strs[i] = items->get(i)->toString();
            
        BaseMenuScreen::setItems(strs, items->length());
    }
     
    virtual void onSelected(int idx) {
        _callback(_items->get(idx));   
    }
    
    virtual ~SelectScreen() {
        delete _items;
    }
    
private:
    List<D*>* _items;
    char* _title;
    SelectScreenCallback _callback;
};

#endif

