#include <line.h>
#include <iostream>

using namespace std;

Line::Line() { length = 1; }

Line::Line(int l) { 
	length = l;
}

void Line::print() {
	std::cout << "============================\n";
}

void Line::dump() { cout << "<Line />" << endl; }

Line* Line::clone() {
	return new Line(length);
}

