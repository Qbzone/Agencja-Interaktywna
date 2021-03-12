INSERT INTO Osoba
VALUES ('Jan','Kuna','595958958','123456789','jankuna@wp.pl','QWER123!@','true','62ebf753-dfd9-4e45-ad50-ae9174fece3e','Programista'); 
INSERT INTO Osoba
VALUES ('Maciej','Lubicz','595958978','123451789','lubiczmaciej@wp.pl','QWER128!@','true','62ebf753-dfd9-4e45-ad50-ae9174fece3a','Tester'); 
INSERT INTO Osoba
VALUES ('Bartosz','Bartomski','115958958','123956789','bartoszbart@wp.pl','QWER188!@','true','62ebf753-dfd9-4e45-ad50-ae9174fece3c','Grafik'); 
INSERT INTO Osoba
VALUES ('Jerzy','Kozak','595951958','923456789','jerzykozak@wp.pl','QWER773!@','true','62ebf753-dfd9-4e45-ad50-ae9174fece3f','Pozycjoner'); 


INSERT INTO Pracownik
VALUES (2,'Warszawa ul.Wschodnia 10',8000,300,'12345678910',3);
INSERT INTO Pracownik
VALUES (3,'Warszawa ul.Polnocna 5',8400,300,'12345678912',4);
INSERT INTO Pracownik
VALUES (4,'Warszawa ul.Zachodnia 15',8200,200,'12345678911',5);
INSERT INTO Pracownik
VALUES (5,'Warszawa ul.Poludniowa 20',2000,500,'12345678913',1);

INSERT INTO Programista
VALUES (2,'senior');
INSERT INTO Grafik
VALUES (4,'Grafika 2D');
INSERT INTO Tester
VALUES (3,2);
INSERT INTO Pozycjoner
VALUES (5,'marketing szeptany');

INSERT INTO Zespol
VALUES ('Zespol ogorkow');

INSERT INTO PracownikZespol
VALUES (2,1,'2020-10-10','');
INSERT INTO PracownikZespol
VALUES (3,1,'2020-05-05','');
INSERT INTO PracownikZespol
VALUES (4,1,'2020-06-15','');
INSERT INTO PracownikZespol
VALUES (5,1,'2020-01-20','');

INSERT INTO Firma
VALUES('Firma krzak');

INSERT INTO KlientFirma
VALUES(1,1);

INSERT INTO Projekt
VALUES('Projekt dla firmy krzak','C:\Users\kewin\Desktop\pepsi.png',1);
INSERT INTO Projekt
VALUES('Projekt dla firmy krzak2','C:\Users\kewin\Desktop\pepsi.png',1);

INSERT INTO ZespolProjekt
VALUES(1,1,'2020-12-12','');
INSERT INTO ZespolProjekt
VALUES(1,2,'2020-12-12','');

INSERT INTO Zadanie
VALUES('Posadz marchew');
INSERT INTO Zadanie
VALUES('Posadz ogorki');
INSERT INTO Zadanie
VALUES('Posadz rzodkiew');
INSERT INTO Zadanie
VALUES('Posadz szczypiorek');

INSERT INTO ZadanieProjekt
VALUES(1,1,'2020-10-20','','Rozpoczete','Trzeba posadzic marchew');
INSERT INTO ZadanieProjekt
VALUES(1,2,'2020-10-21','','Rozpoczete','Trzeba posadzic ogorki');
INSERT INTO ZadanieProjekt
VALUES(1,3,'2020-10-22','','Rozpoczete','Trzeba posadzic rzodkiew');
INSERT INTO ZadanieProjekt
VALUES(1,4,'2020-10-23','','Rozpoczete','Trzeba posadzic szczypiorek');

INSERT INTO ZadanieProjekt
VALUES(2,1,'2020-10-20','','Rozpoczete','Trzeba posadzic marchew');
INSERT INTO ZadanieProjekt
VALUES(2,2,'2020-10-21','','Rozpoczete','Trzeba posadzic ogorki');

INSERT INTO PracownikKlient
VALUES (2,1,'2021-03-10','2021-03-11','pokoj nr 8');
INSERT INTO PracownikKlient
VALUES (3,1,'2021-03-11','2021-03-12','pokoj nr 10');

INSERT INTO Pakiet
VALUES ('Pakiet usług sadzenia warzyw',1000,'abonament');
INSERT INTO Pakiet
VALUES ('Pakiet usług sadzenia warzyw i roślin',2000,'abonament');

INSERT INTO ProjektPakiet
VALUES (1,1,'2021-01-01','');
INSERT INTO ProjektPakiet
VALUES (1,2,'2021-01-01 12:40:00','');