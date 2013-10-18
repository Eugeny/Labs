#ifndef _COMPARABLE_H_
#define _COMPARABLE_H_

class Comparable {
    public:  
        virtual int compareTo(Comparable* other) {
            return 0;
        }
        
        int operator== (Comparable &b) {
            return this->compareTo(&b);
        }
        
        int operator!= (Comparable &b) {
            return !(*this==b);
        }
};

#endif

