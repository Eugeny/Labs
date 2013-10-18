#include "dal/dal.h"
#include "dal/objects.h"
#include "locale.h"
#include "ui/controller.h"
#include "ui/splashscreen.h"
#include "/usr/include/locale.h"


int main()
{
    setlocale(LC_CTYPE, "en_US.UTF-8");
    Locale::get()->load("ru", "locale/ru-RU");
  
    Controller::get()->showScreen(new SplashScreen());
    Controller::get()->run();
    
 	return 0;
}
