INSERT INTO Osoba
VALUES ('Qan','Dan','595958950','123456780','qandan155@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','e0fbe9b0-2990-4ad9-98c6-5a94abc81ea7','Klient'); 
INSERT INTO Osoba
VALUES ('Romuald','Traworski','595958999','121456780','romualdo@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','e0fbe9b0-2990-4ad9-98c6-5a94abc81ea7','Klient'); 
INSERT INTO Osoba
VALUES ('Roman','Zurba','995958955','123456180','romanello@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','e0fbe9b0-2990-4ad9-98c6-5a94abc81ea7','Klient');
INSERT INTO Osoba
VALUES ('Jan','Kuna','595958958','123456789','jankuna@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','62ebf753-dfd9-4e45-ad50-ae9174fece3e','Programista'); 
INSERT INTO Osoba
VALUES ('Maciej','Lubicz','595958978','123451789','lubiczmaciej@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','62ebf753-dfd9-4e45-ad50-ae9174fece3a','Tester'); 
INSERT INTO Osoba
VALUES ('Bartosz','Bartomski','115958958','123956789','bartoszbart@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','62ebf753-dfd9-4e45-ad50-ae9174fece3c','Grafik'); 
INSERT INTO Osoba
VALUES ('Jerzy','Kozak','595951958','923456789','jerzykozak@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','62ebf753-dfd9-4e45-ad50-ae9174fece3f','Pozycjoner'); 
INSERT INTO Osoba
VALUES ('Andrzej','Tama','595911958','923466789','AndziejTamownik@wp.pl','VutSf+LocAw3LnENngk6ETkPx88C2wUqcjP+DcptQbM=','true','62ebf753-dfd9-4e45-ad50-ae9174fece4f','Szef'); 
 

INSERT INTO Klient
VALUES (1, 'nie');
INSERT INTO Klient
VALUES (2, 'nie');
INSERT INTO Klient
VALUES (3, 'nie');

INSERT INTO Pracownik
VALUES (4,'Warszawa ul.Wschodnia 10',8000,300,'12345678910',3);
INSERT INTO Pracownik
VALUES (5,'Warszawa ul.Polnocna 5',8400,300,'12345678912',4);
INSERT INTO Pracownik
VALUES (6,'Warszawa ul.Zachodnia 15',8200,200,'12345678911',5);
INSERT INTO Pracownik
VALUES (7,'Warszawa ul.Poludniowa 20',2000,500,'12345678913',1);
INSERT INTO Pracownik
VALUES (8,'Warszawa ul.Poludniowa 20',2000,500,'12345678913',6);

INSERT INTO Programista
VALUES (4,'senior');
INSERT INTO Grafik
VALUES (7,'Grafika 2D');
INSERT INTO Tester
VALUES (5,2);
INSERT INTO Pozycjoner
VALUES (6,'marketing szeptany');
INSERT INTO Szef
VALUES (8);

INSERT INTO Zespol
VALUES ('Zespol Alfa');
INSERT INTO Zespol
VALUES ('Zespol Beta');

INSERT INTO PracownikZespol
VALUES (4,1,'2020-10-10 12:00:00',NULL);
INSERT INTO PracownikZespol
VALUES (5,1,'2020-05-05 12:00:00',NULL);
INSERT INTO PracownikZespol
VALUES (6,1,'2020-06-15 12:00:00',NULL);
INSERT INTO PracownikZespol
VALUES (7,1,'2020-01-20 12:00:00',NULL);
INSERT INTO PracownikZespol
VALUES (4,2,'2020-10-10 12:00:00',NULL);
INSERT INTO PracownikZespol
VALUES (5,2,'2020-05-05 12:00:00',NULL);
INSERT INTO PracownikZespol
VALUES (6,2,'2020-06-15 12:00:00',NULL);
INSERT INTO PracownikZespol
VALUES (7,2,'2020-01-20 12:00:00',NULL);

INSERT INTO Firma
VALUES('Fitness And Sport');
INSERT INTO Firma
VALUES('Binary Helix');
INSERT INTO Firma
VALUES('Sirta Foundation');

INSERT INTO KlientFirma
VALUES(1,1);
INSERT INTO KlientFirma
VALUES(2,2);
INSERT INTO KlientFirma
VALUES(3,3);

INSERT INTO Projekt
VALUES('Projekt dla firmy Fitness And Sport','images/logo.jpg',1);
INSERT INTO Projekt
VALUES('Projekt dla firmy Binary Helix','images/logo2.png',2);
INSERT INTO Projekt
VALUES('Projekt dla firmy Sirta Foundation','images/logo3.png',3);

INSERT INTO ZespolProjekt
VALUES(1,1,'2020-12-12 12:00:00',NULL);
INSERT INTO ZespolProjekt
VALUES(2,2,'2020-12-12 12:00:00',NULL);
INSERT INTO ZespolProjekt
VALUES(1,3,'2021-05-05 12:00:00',NULL);

INSERT INTO Usluga
VALUES('Szkicowanie modelów', 'Grafik');
INSERT INTO Usluga
VALUES('Programowanie back-endu', 'Programista');
INSERT INTO Usluga
VALUES('Marketing szeptany', 'Pozycjoner');
INSERT INTO Usluga
VALUES('Testy jednostkowe', 'Tester');

INSERT INTO UslugaProjekt
VALUES(1,1,'2020-10-20 12:00:00','2021-12-12','Rozpoczęte','Trzeba wykonać logo projektu 300x200px');
INSERT INTO UslugaProjekt
VALUES(1,2,'2020-10-21 12:00:00','2021-12-12','Rozpoczęte','Trzeba oprogramować funkcjonalność przypisanie kierowanika do projektu');
INSERT INTO UslugaProjekt
VALUES(1,3,'2020-10-22 12:00:00','2021-12-12','Rozpoczęte','Trzeba wypromować domenę na forach i portalfach społecznościowych');
INSERT INTO UslugaProjekt
VALUES(1,4,'2020-10-23 12:00:00','2021-12-12','Rozpoczęte','Trzeba przeprowadzić testy jednostkowe');

INSERT INTO UslugaProjekt
VALUES(2,1,'2020-10-20 12:00:00','2021-12-12','Rozpoczęte','Trzeba wykonać logo projektu 200x200px');
INSERT INTO UslugaProjekt
VALUES(2,2,'2020-10-21 12:00:00','2021-12-12','Rozpoczęte','Trzeba oprogramować funkcjonalność przypisanie kierowanika do projektu');

INSERT INTO UslugaProjekt
VALUES(3,1,'2020-10-20 12:00:00','2021-12-12','Rozpoczęte','Trzeba wykonać logo projektu 200x200px');
INSERT INTO UslugaProjekt
VALUES(3,2,'2020-10-21 12:00:00','2021-12-12','Rozpoczęte','Trzeba oprogramować funkcjonalność przypisanie kierowanika do projektu');

INSERT INTO PracownikKlient
VALUES (4,1,'2021-03-10 12:00:00','2021-03-11 14:00:00','pokoj nr 8');
INSERT INTO PracownikKlient
VALUES (5,1,'2021-03-11 12:00:00','2021-03-12 14:00:00','pokoj nr 10');

INSERT INTO Pakiet
VALUES ('Pakiet abonamentowy strona i pozycjonowanie',1000,'abonament');
INSERT INTO Pakiet
VALUES ('Pakiet jednorazowy strona internetowa',20000,'zlecenie');

INSERT INTO ProjektPakiet
VALUES (1,1,'2021-01-01',NULL);
INSERT INTO ProjektPakiet
VALUES (2,2,'2021-03-03',NULL);
INSERT INTO ProjektPakiet
VALUES (3,2,'2021-02-02',NULL);

INSERT INTO PakietUsluga
VALUES (1,1);
INSERT INTO PakietUsluga
VALUES (1,2);
INSERT INTO PakietUsluga
VALUES (1,3);
INSERT INTO PakietUsluga
VALUES (1,4);
INSERT INTO PakietUsluga
VALUES (2,1);
INSERT INTO PakietUsluga
VALUES (2,2);