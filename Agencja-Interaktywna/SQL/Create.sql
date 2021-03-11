-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2021-03-11 13:33:25.099

-- tables
-- Table: Firma
CREATE TABLE Firma (
    IdFirma int  NOT NULL IDENTITY,
    Nazwa nvarchar(50)  NOT NULL,
    CONSTRAINT Firma_pk PRIMARY KEY  (IdFirma)
);

-- Table: FirmaTag
CREATE TABLE FirmaTag (
    IdFirma int  NOT NULL,
    IdTag int  NOT NULL,
    CONSTRAINT FirmaTag_pk PRIMARY KEY  (IdFirma,IdTag)
);

-- Table: Grafik
CREATE TABLE Grafik (
    IdPracownik int  NOT NULL,
    Specjalizacja nvarchar(50)  NOT NULL,
    CONSTRAINT Grafik_pk PRIMARY KEY  (IdPracownik)
);

-- Table: JezykProgramowania
CREATE TABLE JezykProgramowania (
    IdJezyk int  NOT NULL IDENTITY,
    Nazwa nvarchar(50)  NOT NULL,
    CONSTRAINT JezykProgramowania_pk PRIMARY KEY  (IdJezyk)
);

-- Table: Klient
CREATE TABLE Klient (
    IdKlient int  NOT NULL,
    Priorytet nvarchar(50)  NOT NULL,
    CONSTRAINT Klient_pk PRIMARY KEY  (IdKlient)
);

-- Table: KlientFirma
CREATE TABLE KlientFirma (
    IdKlient int  NOT NULL,
    IdFirma int  NOT NULL,
    CONSTRAINT KlientFirma_pk PRIMARY KEY  (IdKlient,IdFirma)
);

-- Table: KlientPakiet
CREATE TABLE KlientPakiet (
    IdKlient int  NOT NULL,
    IdPakiet int  NOT NULL,
    DataRozpoczeciaWspolpracy date  NOT NULL,
    DataZakonczeniaWspolpracy date  NULL,
    CONSTRAINT KlientPakiet_pk PRIMARY KEY  (IdKlient,IdPakiet,DataRozpoczeciaWspolpracy)
);

-- Table: Osoba
CREATE TABLE Osoba (
    IdOsoba int  NOT NULL IDENTITY,
    Imie nvarchar(25)  NOT NULL,
    Nazwisko nvarchar(50)  NOT NULL,
    NumerTelefonuPrywatny char(9)  NULL,
    NumerTelefonuSluzbowego char(9)  NOT NULL,
    AdresEmail nvarchar(50)  NOT NULL,
    Haslo nvarchar(max)  NOT NULL,
    CzyEmailZweryfikowany bit  NOT NULL,
    KodAktywacyjny uniqueidentifier  NOT NULL,
    Rola nvarchar(25)  NOT NULL,
    CONSTRAINT Osoba_pk PRIMARY KEY  (IdOsoba)
);

-- Table: Pakiet
CREATE TABLE Pakiet (
    IdPakiet int  NOT NULL IDENTITY,
    Nazwa nvarchar(50)  NOT NULL,
    Oplata int  NOT NULL,
    RodzajOplaty nvarchar(50)  NOT NULL,
    CONSTRAINT Pakiet_pk PRIMARY KEY  (IdPakiet)
);

-- Table: PakietUsluga
CREATE TABLE PakietUsluga (
    IdPakiet int  NOT NULL,
    IdUsluga int  NOT NULL,
    CONSTRAINT PakietUsluga_pk PRIMARY KEY  (IdPakiet,IdUsluga)
);

-- Table: Pozycjoner
CREATE TABLE Pozycjoner (
    IdPracownik int  NOT NULL,
    PelnionaFunkcja nvarchar(50)  NOT NULL,
    CONSTRAINT Pozycjoner_pk PRIMARY KEY  (IdPracownik)
);

-- Table: Pracownik
CREATE TABLE Pracownik (
    IdPracownik int  NOT NULL,
    AdresZamieszkania nvarchar(100)  NOT NULL,
    Pensja int  NOT NULL,
    Premia int  NOT NULL,
    Pesel char(11)  NOT NULL,
    StazPracy int  NOT NULL,
    CONSTRAINT Pracownik_pk PRIMARY KEY  (IdPracownik)
);

-- Table: PracownikKlient
CREATE TABLE PracownikKlient (
    IdPracownik int  NOT NULL,
    IdKlient int  NOT NULL,
    DataRozpoczeciaSpotkania date  NOT NULL,
    DataZakonczeniaSpotkania date  NOT NULL,
    MiejsceSpotkania nvarchar(50)  NOT NULL,
    CONSTRAINT PracownikKlient_pk PRIMARY KEY  (IdKlient,IdPracownik,DataRozpoczeciaSpotkania)
);

-- Table: PracownikUmowa
CREATE TABLE PracownikUmowa (
    IdPracownik int  NOT NULL,
    IdUmowa int  NOT NULL,
    DataPodpisaniaUmowy date  NOT NULL,
    DataZakonczeniaUmowy date  NOT NULL,
    CONSTRAINT PracownikUmowa_pk PRIMARY KEY  (IdPracownik,IdUmowa,DataPodpisaniaUmowy)
);

-- Table: PracownikZespol
CREATE TABLE PracownikZespol (
    IdPracownik int  NOT NULL,
    IdZespol int  NOT NULL,
    DataPrzypisaniaPracownika date  NOT NULL,
    DataWypisaniaPracownika date  NULL,
    CONSTRAINT PracownikZespol_pk PRIMARY KEY  (IdPracownik,IdZespol,DataPrzypisaniaPracownika)
);

-- Table: Programista
CREATE TABLE Programista (
    IdPracownik int  NOT NULL,
    PoziomZaawansowania nvarchar(50)  NOT NULL,
    CONSTRAINT Programista_pk PRIMARY KEY  (IdPracownik)
);

-- Table: ProgramistaJezyk
CREATE TABLE ProgramistaJezyk (
    IdPracownik int  NOT NULL,
    IdJezyk int  NOT NULL,
    Staz int  NOT NULL,
    CONSTRAINT ProgramistaJezyk_pk PRIMARY KEY  (IdPracownik,IdJezyk)
);

-- Table: Projekt
CREATE TABLE Projekt (
    IdProjekt int  NOT NULL IDENTITY,
    Nazwa nvarchar(50)  NOT NULL,
    IdFirma int  NOT NULL,
    CONSTRAINT Projekt_pk PRIMARY KEY  (IdProjekt)
);

-- Table: Szef
CREATE TABLE Szef (
    IdPracownik int  NOT NULL,
    CONSTRAINT Szef_pk PRIMARY KEY  (IdPracownik)
);

-- Table: Tag
CREATE TABLE Tag (
    IdTag int  NOT NULL IDENTITY,
    Nazwa nvarchar(50)  NOT NULL,
    CONSTRAINT Tag_pk PRIMARY KEY  (IdTag)
);

-- Table: Tester
CREATE TABLE Tester (
    IdPracownik int  NOT NULL,
    TesterDoswiadczenie int  NOT NULL,
    CONSTRAINT Tester_pk PRIMARY KEY  (IdPracownik)
);

-- Table: Umowa
CREATE TABLE Umowa (
    IdUmowa int  NOT NULL IDENTITY,
    RodzajUmowy nvarchar(50)  NOT NULL,
    CONSTRAINT Umowa_pk PRIMARY KEY  (IdUmowa)
);

-- Table: Usluga
CREATE TABLE Usluga (
    IdUsluga int  NOT NULL IDENTITY,
    Nazwa nvarchar(50)  NOT NULL,
    Opis nvarchar(100)  NOT NULL,
    CONSTRAINT Usluga_pk PRIMARY KEY  (IdUsluga)
);

-- Table: Zadanie
CREATE TABLE Zadanie (
    IdZadanie int  NOT NULL IDENTITY,
    Nazwa nvarchar(50)  NOT NULL,
    CONSTRAINT Zadanie_pk PRIMARY KEY  (IdZadanie)
);

-- Table: ZadanieProjekt
CREATE TABLE ZadanieProjekt (
    IdProjekt int  NOT NULL,
    IdZadanie int  NOT NULL,
    DataPrzypisaniaZadania date  NOT NULL,
    DataZakonczeniaZadania date  NULL,
    Status nvarchar(30)  NOT NULL,
    Opis nvarchar(max)  NOT NULL,
    CONSTRAINT ZadanieProjekt_pk PRIMARY KEY  (IdProjekt,IdZadanie,DataPrzypisaniaZadania)
);

-- Table: Zespol
CREATE TABLE Zespol (
    IdZespol int  NOT NULL IDENTITY,
    Nazwa nvarchar(50)  NOT NULL,
    CONSTRAINT Zespol_pk PRIMARY KEY  (IdZespol)
);

-- Table: ZespolProjekt
CREATE TABLE ZespolProjekt (
    IdZespol int  NOT NULL,
    IdProjekt int  NOT NULL,
    DataPrzypisaniaZespolu date  NOT NULL,
    DataWypisaniaZespolu date  NOT NULL,
    CONSTRAINT ZespolProjekt_pk PRIMARY KEY  (IdZespol,IdProjekt,DataPrzypisaniaZespolu)
);

-- foreign keys
-- Reference: FirmaTag_Firma (table: FirmaTag)
ALTER TABLE FirmaTag ADD CONSTRAINT FirmaTag_Firma
    FOREIGN KEY (IdFirma)
    REFERENCES Firma (IdFirma);

-- Reference: FirmaTag_Tag (table: FirmaTag)
ALTER TABLE FirmaTag ADD CONSTRAINT FirmaTag_Tag
    FOREIGN KEY (IdTag)
    REFERENCES Tag (IdTag);

-- Reference: KlientFirma_Firma (table: KlientFirma)
ALTER TABLE KlientFirma ADD CONSTRAINT KlientFirma_Firma
    FOREIGN KEY (IdFirma)
    REFERENCES Firma (IdFirma);

-- Reference: KlientFirma_Klient (table: KlientFirma)
ALTER TABLE KlientFirma ADD CONSTRAINT KlientFirma_Klient
    FOREIGN KEY (IdKlient)
    REFERENCES Klient (IdKlient);

-- Reference: KlientPakiet_Klient (table: KlientPakiet)
ALTER TABLE KlientPakiet ADD CONSTRAINT KlientPakiet_Klient
    FOREIGN KEY (IdKlient)
    REFERENCES Klient (IdKlient);

-- Reference: KlientPakiet_Pakiet (table: KlientPakiet)
ALTER TABLE KlientPakiet ADD CONSTRAINT KlientPakiet_Pakiet
    FOREIGN KEY (IdPakiet)
    REFERENCES Pakiet (IdPakiet);

-- Reference: Klient_Osoba (table: Klient)
ALTER TABLE Klient ADD CONSTRAINT Klient_Osoba
    FOREIGN KEY (IdKlient)
    REFERENCES Osoba (IdOsoba);

-- Reference: PakietUsluga_Pakiet (table: PakietUsluga)
ALTER TABLE PakietUsluga ADD CONSTRAINT PakietUsluga_Pakiet
    FOREIGN KEY (IdPakiet)
    REFERENCES Pakiet (IdPakiet);

-- Reference: PakietUsluga_Usluga (table: PakietUsluga)
ALTER TABLE PakietUsluga ADD CONSTRAINT PakietUsluga_Usluga
    FOREIGN KEY (IdUsluga)
    REFERENCES Usluga (IdUsluga);

-- Reference: PracownikKlient_Klient (table: PracownikKlient)
ALTER TABLE PracownikKlient ADD CONSTRAINT PracownikKlient_Klient
    FOREIGN KEY (IdKlient)
    REFERENCES Klient (IdKlient);

-- Reference: PracownikKlient_Pracownik (table: PracownikKlient)
ALTER TABLE PracownikKlient ADD CONSTRAINT PracownikKlient_Pracownik
    FOREIGN KEY (IdPracownik)
    REFERENCES Pracownik (IdPracownik);

-- Reference: PracownikUmowa_Pracownik (table: PracownikUmowa)
ALTER TABLE PracownikUmowa ADD CONSTRAINT PracownikUmowa_Pracownik
    FOREIGN KEY (IdPracownik)
    REFERENCES Pracownik (IdPracownik);

-- Reference: PracownikUmowa_Umowa (table: PracownikUmowa)
ALTER TABLE PracownikUmowa ADD CONSTRAINT PracownikUmowa_Umowa
    FOREIGN KEY (IdUmowa)
    REFERENCES Umowa (IdUmowa);

-- Reference: Pracownik_Osoba (table: Pracownik)
ALTER TABLE Pracownik ADD CONSTRAINT Pracownik_Osoba
    FOREIGN KEY (IdPracownik)
    REFERENCES Osoba (IdOsoba);

-- Reference: Pracownik_Szef (table: Szef)
ALTER TABLE Szef ADD CONSTRAINT Pracownik_Szef
    FOREIGN KEY (IdPracownik)
    REFERENCES Pracownik (IdPracownik);

-- Reference: Projekt_Firma (table: Projekt)
ALTER TABLE Projekt ADD CONSTRAINT Projekt_Firma
    FOREIGN KEY (IdFirma)
    REFERENCES Firma (IdFirma);

-- Reference: Table_14_Pracownik (table: Tester)
ALTER TABLE Tester ADD CONSTRAINT Table_14_Pracownik
    FOREIGN KEY (IdPracownik)
    REFERENCES Pracownik (IdPracownik);

-- Reference: Table_15_Pracownik (table: Pozycjoner)
ALTER TABLE Pozycjoner ADD CONSTRAINT Table_15_Pracownik
    FOREIGN KEY (IdPracownik)
    REFERENCES Pracownik (IdPracownik);

-- Reference: Table_16_Pracownik (table: Grafik)
ALTER TABLE Grafik ADD CONSTRAINT Table_16_Pracownik
    FOREIGN KEY (IdPracownik)
    REFERENCES Pracownik (IdPracownik);

-- Reference: Table_17_Pracownik (table: Programista)
ALTER TABLE Programista ADD CONSTRAINT Table_17_Pracownik
    FOREIGN KEY (IdPracownik)
    REFERENCES Pracownik (IdPracownik);

-- Reference: Table_18_JezykProgramowania (table: ProgramistaJezyk)
ALTER TABLE ProgramistaJezyk ADD CONSTRAINT Table_18_JezykProgramowania
    FOREIGN KEY (IdJezyk)
    REFERENCES JezykProgramowania (IdJezyk);

-- Reference: Table_18_Programista (table: ProgramistaJezyk)
ALTER TABLE ProgramistaJezyk ADD CONSTRAINT Table_18_Programista
    FOREIGN KEY (IdPracownik)
    REFERENCES Programista (IdPracownik);

-- Reference: Table_22_Pracownik (table: PracownikZespol)
ALTER TABLE PracownikZespol ADD CONSTRAINT Table_22_Pracownik
    FOREIGN KEY (IdPracownik)
    REFERENCES Pracownik (IdPracownik);

-- Reference: Table_22_Zespol (table: PracownikZespol)
ALTER TABLE PracownikZespol ADD CONSTRAINT Table_22_Zespol
    FOREIGN KEY (IdZespol)
    REFERENCES Zespol (IdZespol);

-- Reference: Table_24_Projekt (table: ZespolProjekt)
ALTER TABLE ZespolProjekt ADD CONSTRAINT Table_24_Projekt
    FOREIGN KEY (IdProjekt)
    REFERENCES Projekt (IdProjekt);

-- Reference: Table_24_Zespol (table: ZespolProjekt)
ALTER TABLE ZespolProjekt ADD CONSTRAINT Table_24_Zespol
    FOREIGN KEY (IdZespol)
    REFERENCES Zespol (IdZespol);

-- Reference: Table_26_Projekt (table: ZadanieProjekt)
ALTER TABLE ZadanieProjekt ADD CONSTRAINT Table_26_Projekt
    FOREIGN KEY (IdProjekt)
    REFERENCES Projekt (IdProjekt);

-- Reference: Table_26_Zadanie (table: ZadanieProjekt)
ALTER TABLE ZadanieProjekt ADD CONSTRAINT Table_26_Zadanie
    FOREIGN KEY (IdZadanie)
    REFERENCES Zadanie (IdZadanie);

-- End of file.

