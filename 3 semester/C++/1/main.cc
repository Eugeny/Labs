#include <iostream>
#include <string.h>
#include <stdio.h>



char* strclone(char* s) {
    char* r = new char[strlen(s)+1];
    memcpy(r, s, strlen(s)+1);
    return r;
}


using namespace std;

class Person {
    public:
        Person();
        Person(char *fn, char *ln, int age);
        ~Person();
        Person *clone();
        char *toString();

        char *getFirstName();
        char *getLastName();
        int   getAge();
        void  setFirstName(char* name);
        void  setLastName(char* name);
        void  setAge(int age);
    private:
        char *firstName, *lastName;
        int  age;
};


Person::Person() {
    cout << "Person::Person()" << endl;
    firstName = new char[256];
    lastName =  new char[256];
}

Person::~Person() {
    cout << "Person::~Person()" << endl;
    delete firstName;
    delete lastName;
}

Person::Person(char *fn, char *ln, int age) {
    cout << "Person::Person(char*, char*)" << endl;
    firstName = new char[256];
    lastName =  new char[256];
    this->age = age;
    setFirstName(fn);
    setLastName(ln);
}

char *Person::toString() {
    char* s = new char[256];
    sprintf(s, "%s %s %i", firstName, lastName, age);
    return s;
}

Person *Person::clone() {
    Person *n = new Person();
    n->setFirstName(firstName);
    n->setLastName(lastName);
    n->setAge(age);
    return n;
}

char *Person::getFirstName() {
    return strclone(firstName);
}

char *Person::getLastName() {
    return strclone(lastName);
}

int Person::getAge() {
    return age;
}

void Person::setFirstName(char* name) {
    strcpy(firstName, name);
}

void Person::setLastName(char* name) {
    strcpy(lastName, name);
}

void Person::setAge(int age) {
    this->age = age;
}


int main() {
    Person *p1 = new Person("Q", "W", 25);
    cout << p1->toString() << endl;

    Person *p3 = new Person("E", "rtyui", 23);
    cout << p3->toString() << endl;
    delete p3;

    Person *p2 = p1->clone();
    cout << p2->toString() << endl;
    delete p2;
    delete p1;
}
