#ifndef _PAIR_H_
#define _PAIR_H_

template<typename A, typename B>
class Pair {
    public:
        Pair(A a, B b) {
            _a = a;
            _b = b;
        }
        
        void setA(A a) { _a = a; }
        void setB(B b) { _b = b; }
        
        A getA() { return _a; }
        B getB() { return _b; }

    private:
        A _a;
        B _b;
};

#endif
