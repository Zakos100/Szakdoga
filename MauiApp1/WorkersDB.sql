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
	"Ability_reg"	integer,
	PRIMARY KEY("ResourceID")
);
CREATE TABLE IF NOT EXISTS "Timeframe" (
	"TimeframeID"	int NOT NULL,
	"Start"	Time,
	"End"	Time,
	"StartInt"	INT,
	"EndInt"	INT,
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
	FOREIGN KEY("Role") REFERENCES "Roles"("Role"),
	FOREIGN KEY("DeviceID") REFERENCES "Device"("DeviceID"),
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
	"OperationTime"	INTEGER,
	FOREIGN KEY("TimeframeID") REFERENCES "Timeframe"("TimeframeID"),
	FOREIGN KEY("SuitabilityID") REFERENCES "Suitability"("SuitabilityID"),
	FOREIGN KEY("ResourceID") REFERENCES "Resources"("ResourceID"),
	FOREIGN KEY("DeviceID") REFERENCES "Device"("DeviceID"),
	PRIMARY KEY("TaskID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "UserTimeframes" (
	"UserID"	INT,
	"TimeframeID"	INT,
	FOREIGN KEY("TimeframeID") REFERENCES "Timeframe"("TimeframeID"),
	FOREIGN KEY("UserID") REFERENCES "Users"("UserID"),
	PRIMARY KEY("UserID","TimeframeID")
);
INSERT INTO "Address" VALUES (1,3450,'Mezőcsát','Széchenyi utca',47,NULL,NULL,1);
INSERT INTO "Address" VALUES (2,1011,'Budapest','Fő utca',12,2,10,2);
INSERT INTO "Address" VALUES (3,4026,'Debrecen','Kossuth utca',8,1,4,3);
INSERT INTO "Address" VALUES (4,6720,'Szeged','Petőfi Sándor utca',10,3,7,4);
INSERT INTO "Address" VALUES (5,9021,'Győr','Árpád út',20,NULL,NULL,5);
INSERT INTO "Address" VALUES (6,3300,'Eger','Dobó tér',5,1,1,6);
INSERT INTO "Roles" VALUES ('Admin','Rendszergazda','Alkalmazás karbantartása',1);
INSERT INTO "Roles" VALUES ('Worker','Dolgozó','Felhasználó',2);
INSERT INTO "Resources" VALUES (1,5,NULL);
INSERT INTO "Resources" VALUES (2,3,NULL);
INSERT INTO "Resources" VALUES (3,2,NULL);
INSERT INTO "Resources" VALUES (4,4,NULL);
INSERT INTO "Resources" VALUES (5,5,NULL);
INSERT INTO "Timeframe" VALUES (1,'00:00','06:00',0,7);
INSERT INTO "Timeframe" VALUES (2,'00:02','12:00',2,12);
INSERT INTO "Timeframe" VALUES (3,'04:00','20:00',4,20);
INSERT INTO "Timeframe" VALUES (4,'06:00','10:00',6,10);
INSERT INTO "Timeframe" VALUES (5,'01:00','25:00',1,25);
INSERT INTO "Suitability" VALUES (1,'Laptop',3);
INSERT INTO "Suitability" VALUES (2,'Mobile',4);
INSERT INTO "Suitability" VALUES (3,'Laptop',2);
INSERT INTO "Suitability" VALUES (4,'Mobile',3);
INSERT INTO "Suitability" VALUES (5,'Laptop',5);
INSERT INTO "Users" VALUES (1,'akos.zarandi','Zarándi Ákos','admin1234','FA00033333','Admin');
INSERT INTO "Users" VALUES (2,'mate.kovacs','Kovács Máté','worker123','MB00011111','Worker');
INSERT INTO "Users" VALUES (3,'anna.szabo','Szabó Anna','worker123','MB00022222','Worker');
INSERT INTO "Users" VALUES (4,'tamas.nagy','Nagy Tamás','worker123','LP00044444','Worker');
INSERT INTO "Users" VALUES (5,'lili.kiss','Kiss Lili','worker123','LP00055555','Worker');
INSERT INTO "Users" VALUES (6,'bence.toth','Tóth Bence','worker123','MB00033333','Worker');
INSERT INTO "Datas" VALUES ('146532RE','Kemény Katalin','Miskolc',20000923,36203274899,1);
INSERT INTO "Datas" VALUES ('123456AB','Nagy Éva','Debrecen','1995-05-10',36204567890,2);
INSERT INTO "Datas" VALUES ('654321CD','Fekete Ilona','Pécs','1992-07-20',36204561234,3);
INSERT INTO "Datas" VALUES ('789012EF','Varga Katalin','Szeged','1989-11-30',36204569876,4);
INSERT INTO "Datas" VALUES ('345678GH','Bíró Mária','Győr','1998-03-05',36204564321,5);
INSERT INTO "Datas" VALUES ('987654IJ','Szűcs Andrea','Eger','1990-01-15',36204560987,6);
INSERT INTO "Device" VALUES ('FA00033333','Dell G315','Laptop','44-CB-F2-8A-77-92','2001-01-01');
INSERT INTO "Device" VALUES ('FA00034567','Asus','Laptop','53-24-AB-AC-43-93','2014-01-01');
INSERT INTO "Device" VALUES ('MB00011111','iPhone 12','Mobile','A1-B2-C3-D4-E5-F6','2024-01-01');
INSERT INTO "Device" VALUES ('MB00022222','Samsung Galaxy S21','Mobile','12-34-56-78-9A-BC','2023-12-15');
INSERT INTO "Device" VALUES ('LP00044444','Lenovo ThinkPad','Laptop','11-22-33-44-55-66','2024-02-10');
INSERT INTO "Device" VALUES ('LP00055555','HP EliteBook','Laptop','AA-BB-CC-DD-EE-FF','2024-03-05');
INSERT INTO "Device" VALUES ('MB00033333','Google Pixel 6','Mobile','77-88-99-AA-BB-CC','2024-04-01');
INSERT INTO "Tasks" VALUES (1,'2000-01-01','2000-01-01','FA00033333',1,1,1,10);
INSERT INTO "Tasks" VALUES (2,'2025-04-01','2025-04-05','MB00011111',2,2,2,5);
INSERT INTO "Tasks" VALUES (3,'2025-04-02','2025-04-06','MB00022222',3,3,4,15);
INSERT INTO "Tasks" VALUES (4,'2025-04-03','2025-04-07','LP00044444',4,4,3,8);
INSERT INTO "Tasks" VALUES (5,'2025-04-04','2025-04-08','LP00055555',5,5,5,4);
INSERT INTO "Tasks" VALUES (6,'2025-04-05','2025-04-09','MB00033333',1,1,1,12);
INSERT INTO "UserTimeframes" VALUES (1,1);
INSERT INTO "UserTimeframes" VALUES (2,1);
INSERT INTO "UserTimeframes" VALUES (3,1);
INSERT INTO "UserTimeframes" VALUES (4,1);
INSERT INTO "UserTimeframes" VALUES (5,1);
INSERT INTO "UserTimeframes" VALUES (6,1);
INSERT INTO "UserTimeframes" VALUES (1,2);
INSERT INTO "UserTimeframes" VALUES (2,2);
INSERT INTO "UserTimeframes" VALUES (3,2);
INSERT INTO "UserTimeframes" VALUES (4,2);
INSERT INTO "UserTimeframes" VALUES (5,2);
INSERT INTO "UserTimeframes" VALUES (6,2);
INSERT INTO "UserTimeframes" VALUES (1,3);
INSERT INTO "UserTimeframes" VALUES (2,3);
INSERT INTO "UserTimeframes" VALUES (3,3);
INSERT INTO "UserTimeframes" VALUES (4,3);
INSERT INTO "UserTimeframes" VALUES (5,3);
INSERT INTO "UserTimeframes" VALUES (6,3);
INSERT INTO "UserTimeframes" VALUES (1,4);
INSERT INTO "UserTimeframes" VALUES (2,4);
INSERT INTO "UserTimeframes" VALUES (3,4);
INSERT INTO "UserTimeframes" VALUES (4,4);
INSERT INTO "UserTimeframes" VALUES (5,4);
INSERT INTO "UserTimeframes" VALUES (6,4);
INSERT INTO "UserTimeframes" VALUES (1,5);
INSERT INTO "UserTimeframes" VALUES (2,5);
INSERT INTO "UserTimeframes" VALUES (3,5);
INSERT INTO "UserTimeframes" VALUES (4,5);
INSERT INTO "UserTimeframes" VALUES (5,5);
INSERT INTO "UserTimeframes" VALUES (6,5);
COMMIT;
