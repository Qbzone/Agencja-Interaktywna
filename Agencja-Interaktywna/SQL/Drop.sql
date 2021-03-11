-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2021-03-11 13:33:25.099

-- foreign keys
ALTER TABLE FirmaTag DROP CONSTRAINT FirmaTag_Firma;

ALTER TABLE FirmaTag DROP CONSTRAINT FirmaTag_Tag;

ALTER TABLE KlientFirma DROP CONSTRAINT KlientFirma_Firma;

ALTER TABLE KlientFirma DROP CONSTRAINT KlientFirma_Klient;

ALTER TABLE KlientPakiet DROP CONSTRAINT KlientPakiet_Klient;

ALTER TABLE KlientPakiet DROP CONSTRAINT KlientPakiet_Pakiet;

ALTER TABLE Klient DROP CONSTRAINT Klient_Osoba;

ALTER TABLE PakietUsluga DROP CONSTRAINT PakietUsluga_Pakiet;

ALTER TABLE PakietUsluga DROP CONSTRAINT PakietUsluga_Usluga;

ALTER TABLE PracownikKlient DROP CONSTRAINT PracownikKlient_Klient;

ALTER TABLE PracownikKlient DROP CONSTRAINT PracownikKlient_Pracownik;

ALTER TABLE PracownikUmowa DROP CONSTRAINT PracownikUmowa_Pracownik;

ALTER TABLE PracownikUmowa DROP CONSTRAINT PracownikUmowa_Umowa;

ALTER TABLE Pracownik DROP CONSTRAINT Pracownik_Osoba;

ALTER TABLE Szef DROP CONSTRAINT Pracownik_Szef;

ALTER TABLE Projekt DROP CONSTRAINT Projekt_Firma;

ALTER TABLE Tester DROP CONSTRAINT Table_14_Pracownik;

ALTER TABLE Pozycjoner DROP CONSTRAINT Table_15_Pracownik;

ALTER TABLE Grafik DROP CONSTRAINT Table_16_Pracownik;

ALTER TABLE Programista DROP CONSTRAINT Table_17_Pracownik;

ALTER TABLE ProgramistaJezyk DROP CONSTRAINT Table_18_JezykProgramowania;

ALTER TABLE ProgramistaJezyk DROP CONSTRAINT Table_18_Programista;

ALTER TABLE PracownikZespol DROP CONSTRAINT Table_22_Pracownik;

ALTER TABLE PracownikZespol DROP CONSTRAINT Table_22_Zespol;

ALTER TABLE ZespolProjekt DROP CONSTRAINT Table_24_Projekt;

ALTER TABLE ZespolProjekt DROP CONSTRAINT Table_24_Zespol;

ALTER TABLE ZadanieProjekt DROP CONSTRAINT Table_26_Projekt;

ALTER TABLE ZadanieProjekt DROP CONSTRAINT Table_26_Zadanie;

-- tables
DROP TABLE Firma;

DROP TABLE FirmaTag;

DROP TABLE Grafik;

DROP TABLE JezykProgramowania;

DROP TABLE Klient;

DROP TABLE KlientFirma;

DROP TABLE KlientPakiet;

DROP TABLE Osoba;

DROP TABLE Pakiet;

DROP TABLE PakietUsluga;

DROP TABLE Pozycjoner;

DROP TABLE Pracownik;

DROP TABLE PracownikKlient;

DROP TABLE PracownikUmowa;

DROP TABLE PracownikZespol;

DROP TABLE Programista;

DROP TABLE ProgramistaJezyk;

DROP TABLE Projekt;

DROP TABLE Szef;

DROP TABLE Tag;

DROP TABLE Tester;

DROP TABLE Umowa;

DROP TABLE Usluga;

DROP TABLE Zadanie;

DROP TABLE ZadanieProjekt;

DROP TABLE Zespol;

DROP TABLE ZespolProjekt;

-- End of file.

