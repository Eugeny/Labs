
CREATE TABLE Author
(
	id                   CHAR(18) NOT NULL,
	name                 VARCHAR(20) NULL
);



ALTER TABLE Author
ADD PRIMARY KEY (id);



CREATE TABLE Book
(
	id                   INTEGER NOT NULL,
	author_id            CHAR(18) NOT NULL,
	name                 VARCHAR(20) NULL,
	isbn                 VARCHAR(20) NULL,
	series_id            CHAR(18) NULL
);



ALTER TABLE Book
ADD PRIMARY KEY (id);



CREATE TABLE BookShelf
(
	id                   CHAR(18) NOT NULL,
	name                 CHAR(18) NULL,
	reader_id            CHAR(18) NOT NULL
);



ALTER TABLE BookShelf
ADD PRIMARY KEY (id);



CREATE TABLE Reader
(
	id                   CHAR(18) NOT NULL,
	name                 CHAR(18) NULL
);



ALTER TABLE Reader
ADD PRIMARY KEY (id);



CREATE TABLE Review
(
	id                   CHAR(18) NOT NULL,
	text                 VARCHAR(20) NULL,
	rating               INTEGER NULL,
	reader_id            CHAR(18) NOT NULL,
	book_id              INTEGER NOT NULL
);



ALTER TABLE Review
ADD PRIMARY KEY (id);



CREATE TABLE Series
(
	id                   CHAR(18) NOT NULL,
	name                 CHAR(18) NULL,
	author_id            CHAR(18) NOT NULL
);



ALTER TABLE Series
ADD PRIMARY KEY (id);



CREATE TABLE Store
(
	id                   CHAR(18) NOT NULL,
	name                 VARCHAR(20) NULL,
	network_id           CHAR(18) NULL
);



ALTER TABLE Store
ADD PRIMARY KEY (id);



CREATE TABLE StoreListing
(
	id                   INTEGER NOT NULL,
	book_id              INTEGER NOT NULL,
	store_id             CHAR(18) NOT NULL,
	price                INTEGER NULL
);



ALTER TABLE StoreListing
ADD PRIMARY KEY (id);



CREATE TABLE StoreNetwork
(
	id                   CHAR(18) NOT NULL,
	name                 CHAR(18) NULL
);



ALTER TABLE StoreNetwork
ADD PRIMARY KEY (id);



ALTER TABLE Book
ADD CONSTRAINT FOREIGN KEY (author_id) REFERENCES Author (id);



ALTER TABLE Book
ADD CONSTRAINT FOREIGN KEY (series_id) REFERENCES Series (id);



ALTER TABLE BookShelf
ADD CONSTRAINT FOREIGN KEY (reader_id) REFERENCES Reader (id);



ALTER TABLE Review
ADD CONSTRAINT FOREIGN KEY (book_id) REFERENCES Book (id);



ALTER TABLE Review
ADD CONSTRAINT FOREIGN KEY (reader_id) REFERENCES Reader (id);



ALTER TABLE Series
ADD CONSTRAINT FOREIGN KEY (author_id) REFERENCES Author (id);



ALTER TABLE Store
ADD CONSTRAINT FOREIGN KEY (network_id) REFERENCES StoreNetwork (id);



ALTER TABLE StoreListing
ADD CONSTRAINT FOREIGN KEY (book_id) REFERENCES Book (id);



ALTER TABLE StoreListing
ADD CONSTRAINT FOREIGN KEY (store_id) REFERENCES Store (id);


