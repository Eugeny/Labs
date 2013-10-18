all:
	@mkdir -p classes bin/META-INF || true
	javac -d classes org/labs/print/HWPrinter.java org/labs/main/Main.java
	cp classes/* bin/ -r
	echo "Manifest-Version: 1.0\nCreated-By: 1.7.0_06\nMain-Class: org.labs.main.Main\n" > bin/META-INF/MANIFEST.MF
	cd bin && zip -r app.jar *
	mv bin/app.jar .

