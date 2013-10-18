CC = g++
BFLAGS = -g -Wall -I `pwd` -std=c++0x -Wno-write-strings
CFLAGS = -c $(BFLAGS)
LDFLAGS = -lncursesw

SOURCES = \
    main.cc \
    locale.cc \
    \
    dbs/db.cc \
    dbs/dbrow.cc \
    dbs/dbtable.cc \
    dbs/dbcells.cc \
    \
    dal/dal.cc \
    dal/objects.cc \
    \
    util/util.cc \
    util/cstring.cc \
    \
    ui/controller.cc \
    ui/basescreen.cc \
    ui/basemenuscreen.cc \
    \
    ui/splashscreen.cc \
    ui/mainscreen.cc \
    ui/scorescreen.cc \
    ui/ratingscreen.cc \
    ui/reportscreen.cc \

OBJECTS = $(SOURCES:.cc=.o)
EXECUTABLE = app

all: $(SOURCES) $(EXECUTABLE)
	
$(EXECUTABLE): $(OBJECTS) 
	$(CC) $(BFLAGS) $(OBJECTS) $(LDFLAGS) -o $@

%.o: %.cc
	$(CC) $(CFLAGS) $< -o $@
	
clean:
	rm *.o */*.o $(EXECUTABLE) || true

editor: editor.cc $(OBJECTS)
	$(CC) $(BFLAGS) editor.cc dbs/db.o dbs/dbtable.o dbs/dbrow.o dbs/dbcells.o util/util.o util/cstring.o $(LDFLAGS) -o $@
