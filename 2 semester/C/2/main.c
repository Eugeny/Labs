#include <stdio.h>
#include <stdlib.h>
#include <string.h>

const int UNITS_COUNT = 12;

char* units[] = {
    "Километры",
    "Метры",
    "Сантиметры",
    "Миллиметры",
    "Версты",
    "Аршины",
    "Сажени",
    "Вершки",
    "Мили",
    "Ярды",
    "Футы",
    "Дюймы",
};

double ratio[] = {
    1000,
    1,
    0.01,
    0.001,
    106.68,
    0.21336,
    0.07112,
    0.004445,
    1609.344,
    0.9144,
    0.3048,
    0.0254
};

int getUnit(char* hint) {
    int r;
    for (int i=0; i < UNITS_COUNT; i++)
        printf("[%i] %s\n", i, units[i]);
    printf("%s > ", hint);
    scanf("%d", &r);
    return r;
}

int main() {
    double val = 1;
    char c;
    int f,t;
    
    while (1) {
        printf("\nТекущее значение: %.6f\n", val);
        printf("[i] Ввести число | [c] Конвертировать | [r] Отчет | [a] О программе | [e] Выход\n> ");
        c = getchar();
        switch (c) {
        case 'i':
            scanf("%lf", &val);
            break;
        case 'c':
            f = getUnit("Исходные единицы");
            t = getUnit("Целевые единицы");
            val *= ratio[f] / ratio[t];
            break;
        case 'r':
            f = getUnit("Исходные единицы");
            for (int i = 0; i < UNITS_COUNT; i++)
                if (i != f) 
                    printf("%s: %.6f\n", units[i], val * ratio[f] / ratio[i]);
            break;
        case 'a':
            printf("Конвертер v0.0\nНаписан Евгением Паньковым\nООО \"Рога и копыта\" (с) 2011\n");   
            break;
        case 'e':
            return 0;
            break;
        default:
            printf("Неверная команда\n");   
        }
        getchar(); // consume #13
    }
    return 0; 
}
