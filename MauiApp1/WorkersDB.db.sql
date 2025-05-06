BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Address" (
	"Address_ID"	int NOT NULL,
	"Postcode"	int,
	"City"	char(25),
	"Street"	char(30),
	"House_number"	int,
	"Floor"	int,
	"Door"	int,
	"User_ID"	int,
	FOREIGN KEY("User_ID") REFERENCES "Users"("User_ID"),
	PRIMARY KEY("Address_ID")
);
CREATE TABLE IF NOT EXISTS "Roles" (
	"Role"	varchar(15) NOT NULL,
	"Position_name"	char(20),
	"Description"	char(50),
	"Access_value"	int,
	PRIMARY KEY("Role")
);
CREATE TABLE IF NOT EXISTS "Resources" (
	"Resource_ID"	int NOT NULL,
	"Ability_req"	int,
	PRIMARY KEY("Resource_ID")
);
CREATE TABLE IF NOT EXISTS "Timeframes" (
	"Timeframe_ID"	int NOT NULL,
	"Start"	Time,
	"End"	Time,
	"StartInt"	INT,
	"EndInt"	INT,
	PRIMARY KEY("Timeframe_ID")
);
CREATE TABLE IF NOT EXISTS "Suitability" (
	"Suitability_ID"	int NOT NULL,
	"Device_type"	char(25),
	"Ability_min"	int,
	PRIMARY KEY("Suitability_ID")
);
CREATE TABLE IF NOT EXISTS "Users" (
	"User_ID"	int NOT NULL,
	"Username"	varchar(20),
	"Fullname"	varchar(50),
	"Password"	varchar(25),
	"Device_ID"	varchar(15),
	"Role"	char(10),
	FOREIGN KEY("Role") REFERENCES "Roles"("Role"),
	FOREIGN KEY("Device_ID") REFERENCES "Devices"("Device_ID"),
	PRIMARY KEY("User_ID")
);
CREATE TABLE IF NOT EXISTS "Data" (
	"IDcard_number"	varchar(6) NOT NULL,
	"Mothersname"	char(25),
	"Place_of_birth"	char(25),
	"Date_of_birth"	date,
	"Phone_number"	string,
	"User_ID"	int,
	FOREIGN KEY("User_ID") REFERENCES "Users"("User_ID"),
	PRIMARY KEY("IDcard_number")
);
CREATE TABLE IF NOT EXISTS "Devices" (
	"Device_ID"	varchar(15) NOT NULL,
	"Device_name"	varchar(35),
	"Device_type"	char(25),
	"MAC_address"	varchar(20),
	"Last_update"	DATE,
	PRIMARY KEY("Device_ID")
);
CREATE TABLE IF NOT EXISTS "Tasks" (
	"Task_ID"	INTEGER NOT NULL,
	"Planned_date"	date,
	"Deadline"	date,
	"Device_ID"	varchar(15),
	"Resource_ID"	int,
	"Timeframe_ID"	int,
	"Suitability_ID"	int,
	"OperationTime"	INTEGER,
	FOREIGN KEY("Timeframe_ID") REFERENCES "Timeframes"("Timeframe_ID"),
	FOREIGN KEY("Suitability_ID") REFERENCES "Suitability"("Suitability_ID"),
	FOREIGN KEY("Device_ID") REFERENCES "Devices"("Device_ID"),
	FOREIGN KEY("Resource_ID") REFERENCES "Resources"("Resource_ID"),
	PRIMARY KEY("Task_ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "UserTimeframes" (
	"User_ID"	INT,
	"Timeframe_ID"	INT,
	FOREIGN KEY("Timeframe_ID") REFERENCES "Timeframes"("Timeframe_ID"),
	FOREIGN KEY("User_ID") REFERENCES "Users"("User_ID"),
	PRIMARY KEY("User_ID","Timeframe_ID")
);
INSERT INTO "Address" ("Address_ID","Postcode","City","Street","House_number","Floor","Door","User_ID") VALUES (1,3450,'Mezőcsát','Széchenyi utca',47,NULL,NULL,1);
INSERT INTO "Address" ("Address_ID","Postcode","City","Street","House_number","Floor","Door","User_ID") VALUES (2,1011,'Budapest','Fő utca',12,2,10,2);
INSERT INTO "Address" ("Address_ID","Postcode","City","Street","House_number","Floor","Door","User_ID") VALUES (3,4026,'Debrecen','Kossuth utca',8,1,4,3);
INSERT INTO "Address" ("Address_ID","Postcode","City","Street","House_number","Floor","Door","User_ID") VALUES (4,6720,'Szeged','Petőfi Sándor utca',10,3,7,4);
INSERT INTO "Address" ("Address_ID","Postcode","City","Street","House_number","Floor","Door","User_ID") VALUES (5,9021,'Győr','Árpád út',20,NULL,NULL,5);
INSERT INTO "Address" ("Address_ID","Postcode","City","Street","House_number","Floor","Door","User_ID") VALUES (6,3300,'Eger','Dobó tér',5,1,1,6);
INSERT INTO "Roles" ("Role","Position_name","Description","Access_value") VALUES ('Admin','Rendszergazda','Alkalmazás karbantartása',1);
INSERT INTO "Roles" ("Role","Position_name","Description","Access_value") VALUES ('Worker','Dolgozó','Felhasználó',2);
INSERT INTO "Resources" ("Resource_ID","Ability_req") VALUES (1,5);
INSERT INTO "Resources" ("Resource_ID","Ability_req") VALUES (2,3);
INSERT INTO "Resources" ("Resource_ID","Ability_req") VALUES (3,2);
INSERT INTO "Resources" ("Resource_ID","Ability_req") VALUES (4,4);
INSERT INTO "Resources" ("Resource_ID","Ability_req") VALUES (5,5);
INSERT INTO "Timeframes" ("Timeframe_ID","Start","End","StartInt","EndInt") VALUES (1,'00:00','06:00',0,7);
INSERT INTO "Timeframes" ("Timeframe_ID","Start","End","StartInt","EndInt") VALUES (2,'00:02','12:00',2,12);
INSERT INTO "Timeframes" ("Timeframe_ID","Start","End","StartInt","EndInt") VALUES (3,'04:00','20:00',4,20);
INSERT INTO "Timeframes" ("Timeframe_ID","Start","End","StartInt","EndInt") VALUES (4,'06:00','10:00',6,10);
INSERT INTO "Timeframes" ("Timeframe_ID","Start","End","StartInt","EndInt") VALUES (5,'01:00','25:00',1,25);
INSERT INTO "Suitability" ("Suitability_ID","Device_type","Ability_min") VALUES (1,'Laptop',3);
INSERT INTO "Suitability" ("Suitability_ID","Device_type","Ability_min") VALUES (2,'Mobile',3);
INSERT INTO "Suitability" ("Suitability_ID","Device_type","Ability_min") VALUES (3,'Laptop',2);
INSERT INTO "Suitability" ("Suitability_ID","Device_type","Ability_min") VALUES (4,'Mobile',2);
INSERT INTO "Suitability" ("Suitability_ID","Device_type","Ability_min") VALUES (5,'Laptop',5);
INSERT INTO "Users" ("User_ID","Username","Fullname","Password","Device_ID","Role") VALUES (1,'akos.zarandi','Zarándi Ákos','admin1234','FA00033333','Admin');
INSERT INTO "Users" ("User_ID","Username","Fullname","Password","Device_ID","Role") VALUES (2,'mate.kovacs','Kovács Máté','worker123','MB00011111','Worker');
INSERT INTO "Users" ("User_ID","Username","Fullname","Password","Device_ID","Role") VALUES (3,'anna.szabo','Szabó Anna','worker123','MB00022222','Worker');
INSERT INTO "Users" ("User_ID","Username","Fullname","Password","Device_ID","Role") VALUES (4,'tamas.nagy','Nagy Tamás','worker123','LP00044444','Worker');
INSERT INTO "Users" ("User_ID","Username","Fullname","Password","Device_ID","Role") VALUES (5,'lili.kiss','Kiss Lili','admin1234','LP00055555','Admin');
INSERT INTO "Users" ("User_ID","Username","Fullname","Password","Device_ID","Role") VALUES (6,'bence.toth','Tóth Bence','worker123','MB00033333','Worker');
INSERT INTO "Data" ("IDcard_number","Mothersname","Place_of_birth","Date_of_birth","Phone_number","User_ID") VALUES ('146532RE','Kemény Katalin','Miskolc',20000923,36203274899,1);
INSERT INTO "Data" ("IDcard_number","Mothersname","Place_of_birth","Date_of_birth","Phone_number","User_ID") VALUES ('123456AB','Nagy Éva','Debrecen','1995-05-10',36204567890,2);
INSERT INTO "Data" ("IDcard_number","Mothersname","Place_of_birth","Date_of_birth","Phone_number","User_ID") VALUES ('654321CD','Fekete Ilona','Pécs','1992-07-20',36204561234,3);
INSERT INTO "Data" ("IDcard_number","Mothersname","Place_of_birth","Date_of_birth","Phone_number","User_ID") VALUES ('789012EF','Varga Katalin','Szeged','1989-11-30',36204569876,4);
INSERT INTO "Data" ("IDcard_number","Mothersname","Place_of_birth","Date_of_birth","Phone_number","User_ID") VALUES ('345678GH','Bíró Mária','Győr','1998-03-05',36204564321,5);
INSERT INTO "Data" ("IDcard_number","Mothersname","Place_of_birth","Date_of_birth","Phone_number","User_ID") VALUES ('987654IJ','Szűcs Andrea','Eger','1990-01-15',36204560987,6);
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('FA00033333','Dell G315','Laptop','44-CB-F2-8A-77-92','2001-01-01');
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('FA00034567','Asus','Laptop','53-24-AB-AC-43-93','2014-01-01');
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('MB00011111','iPhone 12','Mobile','A1-B2-C3-D4-E5-F6','2024-01-01');
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('MB00022222','Samsung Galaxy S21','Mobile','12-34-56-78-9A-BC','2023-12-15');
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('LP00044444','Lenovo ThinkPad','Laptop','11-22-33-44-55-66','2024-02-10');
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('LP00055555','HP EliteBook','Laptop','AA-BB-CC-DD-EE-FF','2024-03-05');
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('MB00033333','Google Pixel 6','Mobile','77-88-99-AA-BB-CC','2024-04-01');
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('MB00066666','Samsung Galaxy A52','Mobile','00-16-3E-11-22-33','2024-04-15');
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('LP00077777','Acer Aspire 5','Laptop','44-88-77-66-55-44','2023-11-30');
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('FA00088888','MSI Modern 14','Laptop','AA-BB-CC-DD-EE-11','2024-01-20');
INSERT INTO "Devices" ("Device_ID","Device_name","Device_type","MAC_address","Last_update") VALUES ('MB00099999','Xiaomi Mi 11','Mobile','99-88-77-66-55-44','2024-03-10');
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (1,'2025-06-01','2025-06-08','FA00033333',1,1,1,10);
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (2,'2025-04-30','2025-05-07','MB00011111',2,2,2,5);
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (3,'2025-04-02','2025-04-09','MB00022222',3,3,4,15);
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (4,'2025-07-17','2025-07-24','LP00044444',4,4,3,8);
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (5,'2025-09-22','2025-09-29','LP00055555',5,5,5,4);
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (6,'2025-04-05','2025-04-12','FA00034567',1,1,1,12);
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (7,'2025-04-12','2025-04-23','LP00077777',5,1,3,15);
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (8,'2025-04-06','2025-04-16','FA00088888',1,4,5,20);
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (9,'2025-04-28','2025-05-12','MB00033333',1,3,1,5);
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (10,'2025-04-28','2025-05-04','MB00066666',5,4,4,5);
INSERT INTO "Tasks" ("Task_ID","Planned_date","Deadline","Device_ID","Resource_ID","Timeframe_ID","Suitability_ID","OperationTime") VALUES (11,'2025-04-09','2025-04-18','FA00034567',3,2,2,10);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (1,1);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (2,1);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (3,1);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (4,1);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (5,1);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (6,1);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (1,2);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (2,2);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (3,2);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (4,2);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (5,2);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (6,2);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (1,3);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (2,3);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (3,3);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (4,3);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (5,3);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (6,3);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (1,4);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (2,4);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (3,4);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (4,4);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (5,4);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (6,4);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (1,5);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (2,5);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (3,5);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (4,5);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (5,5);
INSERT INTO "UserTimeframes" ("User_ID","Timeframe_ID") VALUES (6,5);
COMMIT;
