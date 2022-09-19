INSERT INTO Person
VALUES ('Qan','Dan','595958950','123456780','qandan155@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','e0fbe9b0-2990-4ad9-98c6-5a94abc81ea7','Client'); 
INSERT INTO Person
VALUES ('Romuald','Traworski','595958999','121456780','romualdo@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','e0fbe9b0-2990-4ad9-98c6-5a94abc81ea7','Client'); 
INSERT INTO Person
VALUES ('Roman','Zurba','995958955','123456180','romanello@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','e0fbe9b0-2990-4ad9-98c6-5a94abc81ea7','Client');
INSERT INTO Person
VALUES ('Jan','Kuna','595958958','123456789','jankuna@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','62ebf753-dfd9-4e45-ad50-ae9174fece3e','Programmer'); 
INSERT INTO Person
VALUES ('Maciej','Lubicz','595958978','123451789','lubiczmaciej@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','62ebf753-dfd9-4e45-ad50-ae9174fece3a','Tester'); 
INSERT INTO Person
VALUES ('Bartosz','Bartomski','115958958','123956789','bartoszbart@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','62ebf753-dfd9-4e45-ad50-ae9174fece3c','Graphician'); 
INSERT INTO Person
VALUES ('Jerzy','Kozak','595951958','923456789','jerzykozak@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','62ebf753-dfd9-4e45-ad50-ae9174fece3f','Positioner'); 
INSERT INTO Person
VALUES ('Andrzej','Tama','595911958','923466789','AndziejTamownik@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','62ebf753-dfd9-4e45-ad50-ae9174fece4f','Boss'); 
 
INSERT INTO Client
VALUES (1, 'No');
INSERT INTO Client
VALUES (2, 'No');
INSERT INTO Client
VALUES (3, 'No');

INSERT INTO Employee
VALUES (4,'Warszawa ul.Wschodnia 10',8000,300,'12345678910',3);
INSERT INTO Employee
VALUES (5,'Warszawa ul.Polnocna 5',8400,300,'12345678912',4);
INSERT INTO Employee
VALUES (6,'Warszawa ul.Zachodnia 15',8200,200,'12345678911',5);
INSERT INTO Employee
VALUES (7,'Warszawa ul.Poludniowa 20',2000,500,'12345678913',1);
INSERT INTO Employee
VALUES (8,'Warszawa ul.Poludniowa 20',2000,500,'12345678913',6);

INSERT INTO Contract
VALUES ('Employment contract');
INSERT INTO Contract
VALUES ('Contract of mandate');
INSERT INTO Contract
VALUES ('B2b agreement');

INSERT INTO EmployeeContract
VALUES(4,1,'2018-03-03','2028-10-10');
INSERT INTO EmployeeContract
VALUES(5,2,'2015-01-01','2025-10-10');
INSERT INTO EmployeeContract
VALUES(6,3,'2016-09-09','2026-10-10');
INSERT INTO EmployeeContract
VALUES(7,1,'2021-01-01','2022-01-01');
INSERT INTO EmployeeContract
VALUES(8,1,'2020-08-10','2030-08-10');

INSERT INTO Programmer
VALUES (4,'Senior');
INSERT INTO Graphician
VALUES (6,'2D graphics');
INSERT INTO Tester
VALUES (5,2);
INSERT INTO Positioner
VALUES (7,'Marketing');
INSERT INTO Boss
VALUES (8);

INSERT INTO Team
VALUES ('Team Alpha');
INSERT INTO Team
VALUES ('Team Beta');

INSERT INTO EmployeeTeam
VALUES (4,1,'2020-10-10 12:00:00',NULL);
INSERT INTO EmployeeTeam
VALUES (5,1,'2020-05-05 12:00:00',NULL);
INSERT INTO EmployeeTeam
VALUES (6,1,'2020-06-15 12:00:00',NULL);
INSERT INTO EmployeeTeam
VALUES (7,1,'2020-01-20 12:00:00',NULL);
INSERT INTO EmployeeTeam
VALUES (4,2,'2020-10-10 12:00:00',NULL);
INSERT INTO EmployeeTeam
VALUES (5,2,'2020-05-05 12:00:00',NULL);
INSERT INTO EmployeeTeam
VALUES (6,2,'2020-06-15 12:00:00',NULL);
INSERT INTO EmployeeTeam
VALUES (7,2,'2020-01-20 12:00:00',NULL);

INSERT INTO Company
VALUES('Fitness And Sport');
INSERT INTO Company
VALUES('Binary Helix');
INSERT INTO Company
VALUES('Sirta Foundation');

INSERT INTO ClientCompany
VALUES(1,1);
INSERT INTO ClientCompany
VALUES(2,2);
INSERT INTO ClientCompany
VALUES(3,3);

INSERT INTO Project
VALUES('Project for Fitness And Sport Company','images/logo.jpg',1);
INSERT INTO Project
VALUES('Project for Binary Helix','images/logo2.png',2);
INSERT INTO Project
VALUES('Project for Sirta Foundation','images/logo3.png',3);

INSERT INTO TeamProject
VALUES(1,1,'2020-12-12 12:00:00',NULL);
INSERT INTO TeamProject
VALUES(2,2,'2020-12-12 12:00:00',NULL);
INSERT INTO TeamProject
VALUES(1,3,'2021-05-05 12:00:00',NULL);

INSERT INTO Service
VALUES('Sketching models', 'Graphician');
INSERT INTO Service
VALUES('Back-end programming', 'Programmer');
INSERT INTO Service
VALUES('Whisper marketing', 'Positioner');
INSERT INTO Service
VALUES('Unit tests', 'Tester');

INSERT INTO ServiceProject
VALUES(1,1,'2020-10-20 12:00:00','2021-12-12','Started','You need to make a 300x200px project logo');
INSERT INTO ServiceProject
VALUES(1,2,'2020-10-21 12:00:00','2021-12-12','Started','You need to program the functionality of assigning a manager to a project');
INSERT INTO ServiceProject
VALUES(1,3,'2020-10-22 12:00:00','2021-12-12','Started','You need to promote the domain on forums and social networks');
INSERT INTO ServiceProject
VALUES(1,4,'2020-10-23 12:00:00','2021-12-12','Started','You have to run unit tests');
INSERT INTO ServiceProject
VALUES(2,1,'2020-10-20 12:00:00','2021-12-12','Started','You need to make a 200x200px project logo');
INSERT INTO ServiceProject
VALUES(2,2,'2020-10-21 12:00:00','2021-12-12','Started','You need to program the functionality of assigning a manager to a project');
INSERT INTO ServiceProject
VALUES(3,1,'2020-10-20 12:00:00','2021-12-12','Started','You need to make a 200x200px project logo');
INSERT INTO ServiceProject
VALUES(3,2,'2020-10-21 12:00:00','2021-12-12','Started','You need to program the functionality of assigning a manager to a project');

INSERT INTO EmployeeClient
VALUES (4,1,'2021-03-10 12:00:00','2021-03-11 14:00:00','room no. 8');
INSERT INTO EmployeeClient
VALUES (5,1,'2021-03-11 12:00:00','2021-03-12 14:00:00','room no. 10');

INSERT INTO Package
VALUES ('Subscription package - Website and positioning',1000,'Subscription');
INSERT INTO Package
VALUES ('One-time package - Website',20000,'Order');

INSERT INTO ProjectPackage
VALUES (1,1,'2021-01-01',NULL);
INSERT INTO ProjectPackage
VALUES (2,2,'2021-03-03',NULL);
INSERT INTO ProjectPackage
VALUES (3,2,'2021-02-02',NULL);

INSERT INTO PackageService
VALUES (1,1);
INSERT INTO PackageService
VALUES (1,2);
INSERT INTO PackageService
VALUES (1,3);
INSERT INTO PackageService
VALUES (1,4);
INSERT INTO PackageService
VALUES (2,1);
INSERT INTO PackageService
VALUES (2,2);