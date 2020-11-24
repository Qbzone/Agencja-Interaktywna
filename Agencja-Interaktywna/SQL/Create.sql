CREATE TABLE firma 
    (
    idfirma   INTEGER NOT NULL IDENTITY (1,1),
    nazwa     nvarchar (50) NOT null )

ALTER TABLE Firma ADD constraint firmapk PRIMARY KEY CLUSTERED (IdFirma)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE firmatag (
    idfirma   INTEGER NOT NULL,
    idtag     INTEGER NOT NULL
)

ALTER TABLE FirmaTag ADD constraint firmatagpk PRIMARY KEY CLUSTERED (IdFirma, IdTag)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE grafik 
    (
    idpracownik     INTEGER NOT NULL,
    specjalizacja   nvarchar (50) NOT null )

ALTER TABLE Grafik ADD constraint grafikpk PRIMARY KEY CLUSTERED (IdPracownik)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE jezykprogramowania 
    (
    idjezyk   INTEGER NOT NULL IDENTITY (1,1),
    nazwa     nvarchar (50) NOT null )

ALTER TABLE JezykProgramowania ADD constraint jezykprogramowaniapk PRIMARY KEY CLUSTERED (IdJezyk)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE klient 
    (
    idklient    INTEGER NOT NULL,
    priorytet   nvarchar (50) NOT null )

ALTER TABLE Klient ADD constraint klientpk PRIMARY KEY CLUSTERED (IdKlient)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE klientfirma (
    idklient   INTEGER NOT NULL,
    idfirma    INTEGER NOT NULL
)

ALTER TABLE KlientFirma ADD constraint klientfirmapk PRIMARY KEY CLUSTERED (IdKlient, IdFirma)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE klientpakiet (
    idklient                    INTEGER NOT NULL,
    idpakiet                    INTEGER NOT NULL,
    datarozpoczeciawspolpracy   DATE NOT NULL,
    datazakonczeniawspolpracy   DATE
)

ALTER TABLE KlientPakiet ADD constraint klientpakietpk PRIMARY KEY CLUSTERED (DataRozpoczeciaWspolpracy, IdKlient, IdPakiet)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE osoba 
    (
    idosoba   INTEGER NOT NULL IDENTITY (1,1),
    imie      nvarchar (25) NOT NULL , 
     Nazwisko NVARCHAR (50) NOT NULL , 
     NumerTelefonuPrywatny CHAR (9) , 
     NumerTelefonuSluzbowego CHAR (9) NOT NULL , 
     AdresEmail NVARCHAR (50) NOT null,
	 Haslo NVARCHAR (MAX) NOT NULL,
	 CzyEmailZweryfikowane BIT NOT NULL,
	 KodAktywacyjny UNIQUEIDENTIFIER NOT NULL,
     Rola NVARCHAR (25) NOT NULL,
	 )

ALTER TABLE Osoba ADD constraint osobapk PRIMARY KEY CLUSTERED (IdOsoba)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE pakiet 
    (
    idpakiet   INTEGER NOT NULL IDENTITY (1,1),
    nazwa      nvarchar (50) NOT NULL , 
     Oplata INTEGER NOT NULL , 
     RodzajOplaty NVARCHAR (50) NOT null )

ALTER TABLE Pakiet ADD constraint pakietpk PRIMARY KEY CLUSTERED (IdPakiet)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE pakietusluga (
    idpakiet   INTEGER NOT NULL,
    idusluga   INTEGER NOT NULL
)

ALTER TABLE PakietUsluga ADD constraint pakietuslugapk PRIMARY KEY CLUSTERED (IdPakiet, IdUsluga)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE pozycjoner 
    (
    idpracownik       INTEGER NOT NULL,
    pelnionafunkcja   nvarchar (50) NOT null )

ALTER TABLE Pozycjoner ADD constraint pozycjonerpk PRIMARY KEY CLUSTERED (IdPracownik)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE pracownik 
    (
    idpracownik         INTEGER NOT NULL,
    adreszamieszkania   nvarchar (100) NOT NULL , 
     Pensja INTEGER NOT NULL , 
     Premia INTEGER , 
     PESEL CHAR (11) NOT NULL , 
     StazPracy INTEGER NOT null )

ALTER TABLE Pracownik ADD constraint pracownikpk PRIMARY KEY CLUSTERED (IdPracownik)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE pracownikklient 
    (
    idpracownik                INTEGER NOT NULL,
    idklient                   INTEGER NOT NULL,
    datarozpoczeciaspotkania   datetime NOT NULL,
    datazakonczeniaspotkania   datetime,
    miejscespotkania           nvarchar (50) NOT null )

ALTER TABLE PracownikKlient ADD constraint pracownikklientpk PRIMARY KEY CLUSTERED (DataRozpoczeciaSpotkania, IdPracownik, IdKlient)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE pracownikumowa (
    idpracownik            INTEGER NOT NULL,
    idumowa                INTEGER NOT NULL,
    datapodpisaniaumowy    DATE NOT NULL,
    datawygasnieciaumowy   DATE NOT NULL
)

ALTER TABLE PracownikUmowa ADD constraint pracownikumowapk PRIMARY KEY CLUSTERED (DataPodpisaniaUmowy, IdPracownik, IdUmowa)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE pracownikzespol (
    idpracownik                 INTEGER NOT NULL,
    idzespol                    INTEGER NOT NULL,
    dataprzypisaniapracownika   DATE NOT NULL,
    datawypisaniapracownika     DATE,
    menadzer                    bit NOT NULL
)

ALTER TABLE PracownikZespol ADD constraint pracownikzespolpk PRIMARY KEY CLUSTERED (DataPrzypisaniaPracownika, IdPracownik, IdZespol)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE programista 
    (
    idpracownik           INTEGER NOT NULL,
    poziomzaawansowania   nvarchar (50) NOT null )

ALTER TABLE Programista ADD constraint programistapk PRIMARY KEY CLUSTERED (IdPracownik)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE programistajezyk (
    idpracownik   INTEGER NOT NULL,
    idjezyk       INTEGER NOT NULL,
    staz          INTEGER NOT NULL
)

ALTER TABLE ProgramistaJezyk ADD constraint programistajezykpk PRIMARY KEY CLUSTERED (IdPracownik, IdJezyk)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE projekt 
    (
    idprojekt   INTEGER NOT NULL IDENTITY (1,1),
    nazwa       nvarchar (50) NOT NULL , 
     Firma_IdFirma INTEGER NOT null )

ALTER TABLE Projekt ADD constraint projektpk PRIMARY KEY CLUSTERED (IdProjekt)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE szef (
    idpracownik INTEGER NOT NULL
)

ALTER TABLE Szef ADD constraint szefpk PRIMARY KEY CLUSTERED (IdPracownik)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE tag 
    (
    idtag   INTEGER NOT NULL IDENTITY (1,1),
    nazwa   nvarchar (50) NOT null )

ALTER TABLE Tag ADD constraint tagpk PRIMARY KEY CLUSTERED (IdTag)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE tester (
    idpracownik           INTEGER NOT NULL,
    testerdoswiadczenie   INTEGER NOT NULL
)

ALTER TABLE Tester ADD constraint testerpk PRIMARY KEY CLUSTERED (IdPracownik)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE umowa 
    (
    idumowa       INTEGER NOT NULL IDENTITY (1,1),
    rodzajumowy   nvarchar (50) NOT null )

ALTER TABLE Umowa ADD constraint umowapk PRIMARY KEY CLUSTERED (IdUmowa)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE usluga 
    (
    idusluga   INTEGER NOT NULL IDENTITY (1,1),
    nazwa      nvarchar (50) NOT NULL , opis nvarchar(100) )

ALTER TABLE Usluga ADD constraint uslugapk PRIMARY KEY CLUSTERED (IdUsluga)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE zadanie 
    (
    idzadanie   INTEGER NOT NULL IDENTITY (1,1),
    nazwa       nvarchar (50) NOT null )

ALTER TABLE Zadanie ADD constraint zadaniepk PRIMARY KEY CLUSTERED (IdZadanie)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE zadanieprojekt 
    (
    idzadanie                INTEGER NOT NULL,
    idprojekt                INTEGER NOT NULL,
    datarozpoczeciazadania   datetime NOT NULL,
    datazakonczeniazadania   datetime,
    status                   nvarchar (50) , opis nvarchar(100) )

ALTER TABLE ZadanieProjekt ADD constraint zadanieprojektpk PRIMARY KEY CLUSTERED (IdProjekt, IdZadanie, DataRozpoczeciaZadania)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE zespol 
    (
    idzespol   INTEGER NOT NULL IDENTITY (1,1),
    nazwa      nvarchar (50) NOT null )

ALTER TABLE Zespol ADD constraint zespolpk PRIMARY KEY CLUSTERED (IdZespol)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

CREATE TABLE zespolprojekt (
    idzespol                 INTEGER NOT NULL,
    idprojekt                INTEGER NOT NULL,
    dataprzypisaniazespolu   DATE NOT NULL,
    dataoddaniaprojektu      DATE
)

ALTER TABLE ZespolProjekt ADD constraint zespolprojektpk PRIMARY KEY CLUSTERED (DataPrzypisaniaZespolu, IdProjekt, IdZespol)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )

ALTER TABLE FirmaTag
    ADD CONSTRAINT firmatagfirmafk FOREIGN KEY ( idfirma )
        REFERENCES firma ( idfirma )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE FirmaTag
    ADD CONSTRAINT firmatagtagfk FOREIGN KEY ( idtag )
        REFERENCES tag ( idtag )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE Grafik
    ADD CONSTRAINT grafikpracownikfk FOREIGN KEY ( idpracownik )
        REFERENCES pracownik ( idpracownik )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE KlientFirma
    ADD CONSTRAINT klientfirmafirmafk FOREIGN KEY ( idfirma )
        REFERENCES firma ( idfirma )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE KlientFirma
    ADD CONSTRAINT klientfirmaklientfk FOREIGN KEY ( idklient )
        REFERENCES klient ( idklient )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE Klient
    ADD CONSTRAINT klientosobafk FOREIGN KEY ( idklient )
        REFERENCES osoba ( idosoba )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE KlientPakiet
    ADD CONSTRAINT klientpakietklientfk FOREIGN KEY ( idklient )
        REFERENCES klient ( idklient )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE KlientPakiet
    ADD CONSTRAINT klientpakietpakietfk FOREIGN KEY ( idpakiet )
        REFERENCES pakiet ( idpakiet )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE PakietUsluga
    ADD CONSTRAINT pakietuslugapakietfk FOREIGN KEY ( idpakiet )
        REFERENCES pakiet ( idpakiet )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE PakietUsluga
    ADD CONSTRAINT pakietuslugauslugafk FOREIGN KEY ( idusluga )
        REFERENCES usluga ( idusluga )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE Pozycjoner
    ADD CONSTRAINT pozycjonerpracownikfk FOREIGN KEY ( idpracownik )
        REFERENCES pracownik ( idpracownik )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE PracownikKlient
    ADD CONSTRAINT pracownikklientklientfk FOREIGN KEY ( idklient )
        REFERENCES klient ( idklient )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE PracownikKlient
    ADD CONSTRAINT pracownikklientpracownikfk FOREIGN KEY ( idpracownik )
        REFERENCES pracownik ( idpracownik )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE Pracownik
    ADD CONSTRAINT pracownikosobafk FOREIGN KEY ( idpracownik )
        REFERENCES osoba ( idosoba )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE PracownikUmowa
    ADD CONSTRAINT pracownikumowapracownikfk FOREIGN KEY ( idpracownik )
        REFERENCES pracownik ( idpracownik )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE PracownikUmowa
    ADD CONSTRAINT pracownikumowaumowafk FOREIGN KEY ( idumowa )
        REFERENCES umowa ( idumowa )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE PracownikZespol
    ADD CONSTRAINT pracownikzespolpracownikfk FOREIGN KEY ( idpracownik )
        REFERENCES pracownik ( idpracownik )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE PracownikZespol
    ADD CONSTRAINT pracownikzespolzespolfk FOREIGN KEY ( idzespol )
        REFERENCES zespol ( idzespol )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE ProgramistaJezyk
    ADD CONSTRAINT programistajezykjezykprogrfk FOREIGN KEY ( idjezyk )
        REFERENCES jezykprogramowania ( idjezyk )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE ProgramistaJezyk
    ADD CONSTRAINT programistajezykprogramistafk FOREIGN KEY ( idpracownik )
        REFERENCES programista ( idpracownik )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE Programista
    ADD CONSTRAINT programistapracownikfk FOREIGN KEY ( idpracownik )
        REFERENCES pracownik ( idpracownik )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE Projekt
    ADD CONSTRAINT projekt_firma_fk FOREIGN KEY ( firma_idfirma )
        REFERENCES firma ( idfirma )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE Szef
    ADD CONSTRAINT szefpracownikfk FOREIGN KEY ( idpracownik )
        REFERENCES pracownik ( idpracownik )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE Tester
    ADD CONSTRAINT testerpracownikfk FOREIGN KEY ( idpracownik )
        REFERENCES pracownik ( idpracownik )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE ZadanieProjekt
    ADD CONSTRAINT zadanieprojektprojektfk FOREIGN KEY ( idprojekt )
        REFERENCES projekt ( idprojekt )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE ZadanieProjekt
    ADD CONSTRAINT zadanieprojektzadaniefk FOREIGN KEY ( idzadanie )
        REFERENCES zadanie ( idzadanie )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE ZespolProjekt
    ADD CONSTRAINT zespolprojektprojektfk FOREIGN KEY ( idprojekt )
        REFERENCES projekt ( idprojekt )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE ZespolProjekt
    ADD CONSTRAINT zespolprojektzespolfk FOREIGN KEY ( idzespol )
        REFERENCES zespol ( idzespol )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE FirmaTag
    ADD CONSTRAINT firmatagfirmafk FOREIGN KEY ( idfirma )
        REFERENCES firma ( idfirma )
ON DELETE NO ACTION 
    ON UPDATE no action

ALTER TABLE FirmaTag
    ADD CONSTRAINT firmatagtagfk FOREIGN KEY ( idtag )
        REFERENCES tag ( idtag )
ON DELETE NO ACTION 
    ON UPDATE no action