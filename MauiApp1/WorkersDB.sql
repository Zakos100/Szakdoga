BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Address" (
	"AddressID"	int NOT NULL,
	"Postcode"	int,
	"City"	char(25),
	"Street"	char(30),
	"House_number"	int,
	"Floor"	int,
	"Door"	int,
	"UserID"	int,
	FOREIGN KEY("UserID") REFERENCES "Users"("UserID"),
	PRIMARY KEY("AddressID")
);
CREATE TABLE IF NOT EXISTS "Roles" (
	"Role"	varchar(15) NOT NULL,
	"Position_name"	char(20),
	"Description"	char(50),
	"Access_value"	int,
	PRIMARY KEY("Role")
);
CREATE TABLE IF NOT EXISTS "Resources" (
	"ResourceID"	int NOT NULL,
	"Ability_req"	int,
	PRIMARY KEY("ResourceID")
);
CREATE TABLE IF NOT EXISTS "Timeframe" (
	"TimeframeID"	int NOT NULL,
	"Start"	Time,
	"End"	Time,
	PRIMARY KEY("TimeframeID")
);
CREATE TABLE IF NOT EXISTS "Suitability" (
	"SuitabilityID"	int NOT NULL,
	"Device_type"	char(25),
	"Ability_min"	int,
	PRIMARY KEY("SuitabilityID")
);
CREATE TABLE IF NOT EXISTS "Users" (
	"UserID"	int NOT NULL,
	"Username"	varchar(20),
	"Fullname"	varchar(50),
	"Password"	varchar(25),
	"DeviceID"	varchar(15),
	"Role"	char(10),
	FOREIGN KEY("DeviceID") REFERENCES "Device"("DeviceID"),
	FOREIGN KEY("Role") REFERENCES "Roles"("Role"),
	PRIMARY KEY("UserID")
);
CREATE TABLE IF NOT EXISTS "Datas" (
	"IDcard_number"	varchar(6) NOT NULL,
	"Mothersname"	char(25),
	"Place_of_birth"	char(25),
	"Date_of_birth"	date,
	"Phone_number"	string,
	"UserID"	int,
	FOREIGN KEY("UserID") REFERENCES "Users"("UserID"),
	PRIMARY KEY("IDcard_number")
);
CREATE TABLE IF NOT EXISTS "Device" (
	"DeviceID"	varchar(15) NOT NULL,
	"Device_name"	varchar(35),
	"Device_type"	char(25),
	"MAC_address"	varchar(20),
	"Last_update"	DATE,
	PRIMARY KEY("DeviceID")
);
CREATE TABLE IF NOT EXISTS "Tasks" (
	"TaskID"	INTEGER NOT NULL,
	"Planned_date"	date,
	"Deadline"	date,
	"DeviceID"	varchar(15),
	"ResourceID"	int,
	"TimeframeID"	int,
	"SuitabilityID"	int,
	FOREIGN KEY("DeviceID") REFERENCES "Device"("DeviceID"),
	FOREIGN KEY("ResourceID") REFERENCES "Resources"("ResourceID"),
	FOREIGN KEY("TimeframeID") REFERENCES "Timeframe"("TimeframeID"),
	FOREIGN KEY("SuitabilityID") REFERENCES "Suitability"("SuitabilityID"),
	PRIMARY KEY("TaskID" AUTOINCREMENT)
);
INSERT INTO "Address" VALUES (1,3450,'Mezőcsát','Széchenyi utca',47,NULL,NULL,1);
INSERT INTO "Roles" VALUES ('Admin','Rendszergazda','Alkalmazás karbantartása',1);
INSERT INTO "Resources" VALUES (1,5);
INSERT INTO "Timeframe" VALUES (1,0,20);
INSERT INTO "Suitability" VALUES (1,'Laptop',3);
INSERT INTO "Users" VALUES (1,'akos.zarandi','Zarándi Ákos','admin1234','FA00033333','Admin');
INSERT INTO "Datas" VALUES ('146532RE','Kemény Katalin','Miskolc',20000923,36203274899,1);
INSERT INTO "Device" VALUES ('FA00033333','Dell G315','Laptop','44-CB-F2-8A-77-92','2001-01-01');
INSERT INTO "Tasks" VALUES (1,'2000-01-01','2000-01-01','FA00033333',1,1,1,20);
COMMIT;
