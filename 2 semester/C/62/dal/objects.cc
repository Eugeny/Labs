#include "dbs/db.h"

int BSUIR_FACULTIES_NUM = 2;

char* BSUIR_FACULTIES[2] = {
    "ФКП",
    "ФКСиС",
};

int FACULTY_SCHEMA[1] = {
    DB::CELL_STRING, // Name
};    



int BSUIR_SPECS_NUM = 11;
char* BSUIR_SPECS_NAMES[11] = {
    "ЭоСиТ",
    "ТОБ",
    "МиКПРС",
    "ПиПРС",
    "МЭ",
    "ЭВС",
    "ИПОИТ",
    "ЭСБ",
    "Инф",
    "ПОИТ",
    "ВМСиС"
};

int BSUIR_SPECS_FACS[11] = {
    0,0,0,0,0,0,0,0,
    1,1,1
};

int BSUIR_SPECS_LIMITS[11] = {
    3,3,3,3,3,3,3,3,
    3,3,3
};

int SPECIALITY_SCHEMA[3] = {
    DB::CELL_STRING, // Name
    DB::CELL_INT, // Faculty ID
    DB::CELL_INT // Limit
};




    
int STUDENT_SCHEMA[12] = {
    DB::CELL_STRING, // Name
    DB::CELL_STRING, // Patronymic name
    DB::CELL_STRING, // Last name
    DB::CELL_INT, // Speciality ID
    DB::CELL_INT, // School result
    DB::CELL_INT, // Result 1
    DB::CELL_INT, // Result 2
    DB::CELL_INT, // Result 3
    DB::CELL_STRING, // Passport
    DB::CELL_STRING, // Address
    DB::CELL_STRING, // School
    DB::CELL_INT // Password
 };


