CREATE TABLE "Planes" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Model"	TEXT,
	"Year"	INTEGER,
	"Country"	TEXT,
	"Capacity"	INTEGER,
	"Routes"	TEXT,
	"Crew"	TEXT,
	PRIMARY KEY("ID" AUTOINCREMENT)
);