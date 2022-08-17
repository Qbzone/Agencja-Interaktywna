using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Interactive_Agency.Migrations
{
    public partial class DateUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "grafikpracownikfk",
                table: "grafik");

            migrationBuilder.DropForeignKey(
                name: "klientosobafk",
                table: "klient");

            migrationBuilder.DropForeignKey(
                name: "klientfirmafirmafk",
                table: "klientfirma");

            migrationBuilder.DropForeignKey(
                name: "klientfirmaklientfk",
                table: "klientfirma");

            migrationBuilder.DropForeignKey(
                name: "pakietuslugapakietfk",
                table: "pakietusluga");

            migrationBuilder.DropForeignKey(
                name: "pakietuslugauslugafk",
                table: "pakietusluga");

            migrationBuilder.DropForeignKey(
                name: "pozycjonerpracownikfk",
                table: "pozycjoner");

            migrationBuilder.DropForeignKey(
                name: "pracownikosobafk",
                table: "pracownik");

            migrationBuilder.DropForeignKey(
                name: "pracownikklientklientfk",
                table: "pracownikklient");

            migrationBuilder.DropForeignKey(
                name: "pracownikklientpracownikfk",
                table: "pracownikklient");

            migrationBuilder.DropForeignKey(
                name: "pracownikumowapracownikfk",
                table: "pracownikumowa");

            migrationBuilder.DropForeignKey(
                name: "pracownikumowaumowafk",
                table: "pracownikumowa");

            migrationBuilder.DropForeignKey(
                name: "pracownikzespolpracownikfk",
                table: "pracownikzespol");

            migrationBuilder.DropForeignKey(
                name: "pracownikzespolzespolfk",
                table: "pracownikzespol");

            migrationBuilder.DropForeignKey(
                name: "programistapracownikfk",
                table: "programista");

            migrationBuilder.DropForeignKey(
                name: "programistajezykjezykprogrfk",
                table: "programistajezyk");

            migrationBuilder.DropForeignKey(
                name: "programistajezykprogramistafk",
                table: "programistajezyk");

            migrationBuilder.DropForeignKey(
                name: "projekt_firma_fk",
                table: "projekt");

            migrationBuilder.DropForeignKey(
                name: "szefpracownikfk",
                table: "szef");

            migrationBuilder.DropForeignKey(
                name: "testerpracownikfk",
                table: "tester");

            migrationBuilder.DropForeignKey(
                name: "zespolprojektprojektfk",
                table: "zespolprojekt");

            migrationBuilder.DropForeignKey(
                name: "zespolprojektzespolfk",
                table: "zespolprojekt");

            migrationBuilder.DropTable(
                name: "firmatag");

            migrationBuilder.DropTable(
                name: "klientpakiet");

            migrationBuilder.DropTable(
                name: "zadanieprojekt");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "zadanie");

            migrationBuilder.DropPrimaryKey(
                name: "zespolprojektpk",
                table: "zespolprojekt");

            migrationBuilder.DropIndex(
                name: "IX_zespolprojekt_idzespol",
                table: "zespolprojekt");

            migrationBuilder.DropPrimaryKey(
                name: "zespolpk",
                table: "zespol");

            migrationBuilder.DropPrimaryKey(
                name: "uslugapk",
                table: "usluga");

            migrationBuilder.DropPrimaryKey(
                name: "umowapk",
                table: "umowa");

            migrationBuilder.DropPrimaryKey(
                name: "testerpk",
                table: "tester");

            migrationBuilder.DropPrimaryKey(
                name: "szefpk",
                table: "szef");

            migrationBuilder.DropPrimaryKey(
                name: "projektpk",
                table: "projekt");

            migrationBuilder.DropIndex(
                name: "IX_projekt_Firma_IdFirma",
                table: "projekt");

            migrationBuilder.DropPrimaryKey(
                name: "programistajezykpk",
                table: "programistajezyk");

            migrationBuilder.DropPrimaryKey(
                name: "programistapk",
                table: "programista");

            migrationBuilder.DropPrimaryKey(
                name: "pracownikzespolpk",
                table: "pracownikzespol");

            migrationBuilder.DropIndex(
                name: "IX_pracownikzespol_idpracownik",
                table: "pracownikzespol");

            migrationBuilder.DropPrimaryKey(
                name: "pracownikumowapk",
                table: "pracownikumowa");

            migrationBuilder.DropIndex(
                name: "IX_pracownikumowa_idpracownik",
                table: "pracownikumowa");

            migrationBuilder.DropPrimaryKey(
                name: "pracownikklientpk",
                table: "pracownikklient");

            migrationBuilder.DropIndex(
                name: "IX_pracownikklient_idklient",
                table: "pracownikklient");

            migrationBuilder.DropPrimaryKey(
                name: "pracownikpk",
                table: "pracownik");

            migrationBuilder.DropPrimaryKey(
                name: "pozycjonerpk",
                table: "pozycjoner");

            migrationBuilder.DropPrimaryKey(
                name: "pakietuslugapk",
                table: "pakietusluga");

            migrationBuilder.DropPrimaryKey(
                name: "pakietpk",
                table: "pakiet");

            migrationBuilder.DropPrimaryKey(
                name: "osobapk",
                table: "osoba");

            migrationBuilder.DropPrimaryKey(
                name: "klientfirmapk",
                table: "klientfirma");

            migrationBuilder.DropPrimaryKey(
                name: "klientpk",
                table: "klient");

            migrationBuilder.DropPrimaryKey(
                name: "jezykprogramowaniapk",
                table: "jezykprogramowania");

            migrationBuilder.DropPrimaryKey(
                name: "grafikpk",
                table: "grafik");

            migrationBuilder.DropPrimaryKey(
                name: "firmapk",
                table: "firma");

            migrationBuilder.DropColumn(
                name: "dataoddaniaprojektu",
                table: "zespolprojekt");

            migrationBuilder.DropColumn(
                name: "opis",
                table: "usluga");

            migrationBuilder.DropColumn(
                name: "Firma_IdFirma",
                table: "projekt");

            migrationBuilder.DropColumn(
                name: "menadzer",
                table: "pracownikzespol");

            migrationBuilder.DropColumn(
                name: "datawygasnieciaumowy",
                table: "pracownikumowa");

            migrationBuilder.DropColumn(
                name: "CzyEmailZweryfikowane",
                table: "osoba");

            migrationBuilder.RenameTable(
                name: "zespolprojekt",
                newName: "ZespolProjekt");

            migrationBuilder.RenameTable(
                name: "zespol",
                newName: "Zespol");

            migrationBuilder.RenameTable(
                name: "usluga",
                newName: "Usluga");

            migrationBuilder.RenameTable(
                name: "umowa",
                newName: "Umowa");

            migrationBuilder.RenameTable(
                name: "tester",
                newName: "Tester");

            migrationBuilder.RenameTable(
                name: "szef",
                newName: "Szef");

            migrationBuilder.RenameTable(
                name: "projekt",
                newName: "Projekt");

            migrationBuilder.RenameTable(
                name: "programistajezyk",
                newName: "ProgramistaJezyk");

            migrationBuilder.RenameTable(
                name: "programista",
                newName: "Programista");

            migrationBuilder.RenameTable(
                name: "pracownikzespol",
                newName: "PracownikZespol");

            migrationBuilder.RenameTable(
                name: "pracownikumowa",
                newName: "PracownikUmowa");

            migrationBuilder.RenameTable(
                name: "pracownikklient",
                newName: "PracownikKlient");

            migrationBuilder.RenameTable(
                name: "pracownik",
                newName: "Pracownik");

            migrationBuilder.RenameTable(
                name: "pozycjoner",
                newName: "Pozycjoner");

            migrationBuilder.RenameTable(
                name: "pakietusluga",
                newName: "PakietUsluga");

            migrationBuilder.RenameTable(
                name: "pakiet",
                newName: "Pakiet");

            migrationBuilder.RenameTable(
                name: "osoba",
                newName: "Osoba");

            migrationBuilder.RenameTable(
                name: "klientfirma",
                newName: "KlientFirma");

            migrationBuilder.RenameTable(
                name: "klient",
                newName: "Klient");

            migrationBuilder.RenameTable(
                name: "jezykprogramowania",
                newName: "JezykProgramowania");

            migrationBuilder.RenameTable(
                name: "grafik",
                newName: "Grafik");

            migrationBuilder.RenameTable(
                name: "firma",
                newName: "Firma");

            migrationBuilder.RenameColumn(
                name: "idzespol",
                table: "ZespolProjekt",
                newName: "IdZespol");

            migrationBuilder.RenameColumn(
                name: "idprojekt",
                table: "ZespolProjekt",
                newName: "IdProjekt");

            migrationBuilder.RenameColumn(
                name: "dataprzypisaniazespolu",
                table: "ZespolProjekt",
                newName: "DataPrzypisaniaZespolu");

            migrationBuilder.RenameIndex(
                name: "IX_zespolprojekt_idprojekt",
                table: "ZespolProjekt",
                newName: "IX_ZespolProjekt_IdProjekt");

            migrationBuilder.RenameColumn(
                name: "nazwa",
                table: "Zespol",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "idzespol",
                table: "Zespol",
                newName: "IdZespol");

            migrationBuilder.RenameColumn(
                name: "nazwa",
                table: "Usluga",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "idusluga",
                table: "Usluga",
                newName: "IdUsluga");

            migrationBuilder.RenameColumn(
                name: "rodzajumowy",
                table: "Umowa",
                newName: "RodzajUmowy");

            migrationBuilder.RenameColumn(
                name: "idumowa",
                table: "Umowa",
                newName: "IdUmowa");

            migrationBuilder.RenameColumn(
                name: "testerdoswiadczenie",
                table: "Tester",
                newName: "TesterDoswiadczenie");

            migrationBuilder.RenameColumn(
                name: "idpracownik",
                table: "Tester",
                newName: "IdPracownik");

            migrationBuilder.RenameColumn(
                name: "idpracownik",
                table: "Szef",
                newName: "IdPracownik");

            migrationBuilder.RenameColumn(
                name: "nazwa",
                table: "Projekt",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "idprojekt",
                table: "Projekt",
                newName: "IdProjekt");

            migrationBuilder.RenameColumn(
                name: "staz",
                table: "ProgramistaJezyk",
                newName: "Staz");

            migrationBuilder.RenameColumn(
                name: "idjezyk",
                table: "ProgramistaJezyk",
                newName: "IdJezyk");

            migrationBuilder.RenameColumn(
                name: "idpracownik",
                table: "ProgramistaJezyk",
                newName: "IdPracownik");

            migrationBuilder.RenameIndex(
                name: "IX_programistajezyk_idjezyk",
                table: "ProgramistaJezyk",
                newName: "IX_ProgramistaJezyk_IdJezyk");

            migrationBuilder.RenameColumn(
                name: "poziomzaawansowania",
                table: "Programista",
                newName: "PoziomZaawansowania");

            migrationBuilder.RenameColumn(
                name: "idpracownik",
                table: "Programista",
                newName: "IdPracownik");

            migrationBuilder.RenameColumn(
                name: "datawypisaniapracownika",
                table: "PracownikZespol",
                newName: "DataWypisaniaPracownika");

            migrationBuilder.RenameColumn(
                name: "idzespol",
                table: "PracownikZespol",
                newName: "IdZespol");

            migrationBuilder.RenameColumn(
                name: "idpracownik",
                table: "PracownikZespol",
                newName: "IdPracownik");

            migrationBuilder.RenameColumn(
                name: "dataprzypisaniapracownika",
                table: "PracownikZespol",
                newName: "DataPrzypisaniaPracownika");

            migrationBuilder.RenameIndex(
                name: "IX_pracownikzespol_idzespol",
                table: "PracownikZespol",
                newName: "IX_PracownikZespol_IdZespol");

            migrationBuilder.RenameColumn(
                name: "idumowa",
                table: "PracownikUmowa",
                newName: "IdUmowa");

            migrationBuilder.RenameColumn(
                name: "idpracownik",
                table: "PracownikUmowa",
                newName: "IdPracownik");

            migrationBuilder.RenameColumn(
                name: "datapodpisaniaumowy",
                table: "PracownikUmowa",
                newName: "DataPodpisaniaUmowy");

            migrationBuilder.RenameIndex(
                name: "IX_pracownikumowa_idumowa",
                table: "PracownikUmowa",
                newName: "IX_PracownikUmowa_IdUmowa");

            migrationBuilder.RenameColumn(
                name: "miejscespotkania",
                table: "PracownikKlient",
                newName: "MiejsceSpotkania");

            migrationBuilder.RenameColumn(
                name: "datazakonczeniaspotkania",
                table: "PracownikKlient",
                newName: "DataZakonczeniaSpotkania");

            migrationBuilder.RenameColumn(
                name: "idklient",
                table: "PracownikKlient",
                newName: "IdKlient");

            migrationBuilder.RenameColumn(
                name: "idpracownik",
                table: "PracownikKlient",
                newName: "IdPracownik");

            migrationBuilder.RenameColumn(
                name: "datarozpoczeciaspotkania",
                table: "PracownikKlient",
                newName: "DataRozpoczeciaSpotkania");

            migrationBuilder.RenameIndex(
                name: "IX_pracownikklient_idpracownik",
                table: "PracownikKlient",
                newName: "IX_PracownikKlient_IdPracownik");

            migrationBuilder.RenameColumn(
                name: "PESEL",
                table: "Pracownik",
                newName: "Pesel");

            migrationBuilder.RenameColumn(
                name: "adreszamieszkania",
                table: "Pracownik",
                newName: "AdresZamieszkania");

            migrationBuilder.RenameColumn(
                name: "idpracownik",
                table: "Pracownik",
                newName: "IdPracownik");

            migrationBuilder.RenameColumn(
                name: "pelnionafunkcja",
                table: "Pozycjoner",
                newName: "PelnionaFunkcja");

            migrationBuilder.RenameColumn(
                name: "idpracownik",
                table: "Pozycjoner",
                newName: "IdPracownik");

            migrationBuilder.RenameColumn(
                name: "idusluga",
                table: "PakietUsluga",
                newName: "IdUsluga");

            migrationBuilder.RenameColumn(
                name: "idpakiet",
                table: "PakietUsluga",
                newName: "IdPakiet");

            migrationBuilder.RenameIndex(
                name: "IX_pakietusluga_idusluga",
                table: "PakietUsluga",
                newName: "IX_PakietUsluga_IdUsluga");

            migrationBuilder.RenameColumn(
                name: "nazwa",
                table: "Pakiet",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "idpakiet",
                table: "Pakiet",
                newName: "IdPakiet");

            migrationBuilder.RenameColumn(
                name: "imie",
                table: "Osoba",
                newName: "Imie");

            migrationBuilder.RenameColumn(
                name: "idosoba",
                table: "Osoba",
                newName: "IdOsoba");

            migrationBuilder.RenameColumn(
                name: "idfirma",
                table: "KlientFirma",
                newName: "IdFirma");

            migrationBuilder.RenameColumn(
                name: "idklient",
                table: "KlientFirma",
                newName: "IdKlient");

            migrationBuilder.RenameIndex(
                name: "IX_klientfirma_idfirma",
                table: "KlientFirma",
                newName: "IX_KlientFirma_IdFirma");

            migrationBuilder.RenameColumn(
                name: "priorytet",
                table: "Klient",
                newName: "Priorytet");

            migrationBuilder.RenameColumn(
                name: "idklient",
                table: "Klient",
                newName: "IdKlient");

            migrationBuilder.RenameColumn(
                name: "nazwa",
                table: "JezykProgramowania",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "idjezyk",
                table: "JezykProgramowania",
                newName: "IdJezyk");

            migrationBuilder.RenameColumn(
                name: "specjalizacja",
                table: "Grafik",
                newName: "Specjalizacja");

            migrationBuilder.RenameColumn(
                name: "idpracownik",
                table: "Grafik",
                newName: "IdPracownik");

            migrationBuilder.RenameColumn(
                name: "nazwa",
                table: "Firma",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "idfirma",
                table: "Firma",
                newName: "IdFirma");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPrzypisaniaZespolu",
                table: "ZespolProjekt",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataWypisaniaZespolu",
                table: "ZespolProjekt",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Klasyfikacja",
                table: "Usluga",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdFirma",
                table: "Projekt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Projekt",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataWypisaniaPracownika",
                table: "PracownikZespol",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPrzypisaniaPracownika",
                table: "PracownikZespol",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPodpisaniaUmowy",
                table: "PracownikUmowa",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataZakonczeniaUmowy",
                table: "PracownikUmowa",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Premia",
                table: "Pracownik",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CzyEmailZweryfikowany",
                table: "Osoba",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Rola",
                table: "Osoba",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "ZespolProjekt_pk",
                table: "ZespolProjekt",
                columns: new[] { "IdZespol", "IdProjekt", "DataPrzypisaniaZespolu" });

            migrationBuilder.AddPrimaryKey(
                name: "Zespol_pk",
                table: "Zespol",
                column: "IdZespol");

            migrationBuilder.AddPrimaryKey(
                name: "Usluga_pk",
                table: "Usluga",
                column: "IdUsluga");

            migrationBuilder.AddPrimaryKey(
                name: "Umowa_pk",
                table: "Umowa",
                column: "IdUmowa");

            migrationBuilder.AddPrimaryKey(
                name: "Tester_pk",
                table: "Tester",
                column: "IdPracownik");

            migrationBuilder.AddPrimaryKey(
                name: "Szef_pk",
                table: "Szef",
                column: "IdPracownik");

            migrationBuilder.AddPrimaryKey(
                name: "Projekt_pk",
                table: "Projekt",
                column: "IdProjekt");

            migrationBuilder.AddPrimaryKey(
                name: "ProgramistaJezyk_pk",
                table: "ProgramistaJezyk",
                columns: new[] { "IdPracownik", "IdJezyk" });

            migrationBuilder.AddPrimaryKey(
                name: "Programista_pk",
                table: "Programista",
                column: "IdPracownik");

            migrationBuilder.AddPrimaryKey(
                name: "PracownikZespol_pk",
                table: "PracownikZespol",
                columns: new[] { "IdPracownik", "IdZespol", "DataPrzypisaniaPracownika" });

            migrationBuilder.AddPrimaryKey(
                name: "PracownikUmowa_pk",
                table: "PracownikUmowa",
                columns: new[] { "IdPracownik", "IdUmowa", "DataPodpisaniaUmowy" });

            migrationBuilder.AddPrimaryKey(
                name: "PracownikKlient_pk",
                table: "PracownikKlient",
                columns: new[] { "IdKlient", "IdPracownik", "DataRozpoczeciaSpotkania" });

            migrationBuilder.AddPrimaryKey(
                name: "Pracownik_pk",
                table: "Pracownik",
                column: "IdPracownik");

            migrationBuilder.AddPrimaryKey(
                name: "Pozycjoner_pk",
                table: "Pozycjoner",
                column: "IdPracownik");

            migrationBuilder.AddPrimaryKey(
                name: "PakietUsluga_pk",
                table: "PakietUsluga",
                columns: new[] { "IdPakiet", "IdUsluga" });

            migrationBuilder.AddPrimaryKey(
                name: "Pakiet_pk",
                table: "Pakiet",
                column: "IdPakiet");

            migrationBuilder.AddPrimaryKey(
                name: "Osoba_pk",
                table: "Osoba",
                column: "IdOsoba");

            migrationBuilder.AddPrimaryKey(
                name: "KlientFirma_pk",
                table: "KlientFirma",
                columns: new[] { "IdKlient", "IdFirma" });

            migrationBuilder.AddPrimaryKey(
                name: "Klient_pk",
                table: "Klient",
                column: "IdKlient");

            migrationBuilder.AddPrimaryKey(
                name: "JezykProgramowania_pk",
                table: "JezykProgramowania",
                column: "IdJezyk");

            migrationBuilder.AddPrimaryKey(
                name: "Grafik_pk",
                table: "Grafik",
                column: "IdPracownik");

            migrationBuilder.AddPrimaryKey(
                name: "Firma_pk",
                table: "Firma",
                column: "IdFirma");

            migrationBuilder.CreateTable(
                name: "ProjektPakiet",
                columns: table => new
                {
                    IdProjekt = table.Column<int>(nullable: false),
                    IdPakiet = table.Column<int>(nullable: false),
                    DataRozpoczeciaWspolpracy = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataZakonczeniaWspolpracy = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProjektPakiet_pk", x => new { x.IdPakiet, x.IdProjekt, x.DataRozpoczeciaWspolpracy });
                    table.ForeignKey(
                        name: "ProjektPakiet_Pakiet",
                        column: x => x.IdPakiet,
                        principalTable: "Pakiet",
                        principalColumn: "IdPakiet",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ProjektPakiet_Projekt",
                        column: x => x.IdProjekt,
                        principalTable: "Projekt",
                        principalColumn: "IdProjekt",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UslugaProjekt",
                columns: table => new
                {
                    IdProjekt = table.Column<int>(nullable: false),
                    IdUsluga = table.Column<int>(nullable: false),
                    DataPrzypisaniaZadania = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataZakonczeniaZadania = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(maxLength: 30, nullable: false),
                    Opis = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ZadanieProjekt_pk", x => new { x.IdProjekt, x.IdUsluga, x.DataPrzypisaniaZadania });
                    table.ForeignKey(
                        name: "Table_26_Projekt",
                        column: x => x.IdProjekt,
                        principalTable: "Projekt",
                        principalColumn: "IdProjekt",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Table_26_Zadanie",
                        column: x => x.IdUsluga,
                        principalTable: "Usluga",
                        principalColumn: "IdUsluga",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projekt_IdFirma",
                table: "Projekt",
                column: "IdFirma");

            migrationBuilder.CreateIndex(
                name: "IX_ProjektPakiet_IdProjekt",
                table: "ProjektPakiet",
                column: "IdProjekt");

            migrationBuilder.CreateIndex(
                name: "IX_UslugaProjekt_IdUsluga",
                table: "UslugaProjekt",
                column: "IdUsluga");

            migrationBuilder.AddForeignKey(
                name: "Table_16_Pracownik",
                table: "Grafik",
                column: "IdPracownik",
                principalTable: "Pracownik",
                principalColumn: "IdPracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Klient_Osoba",
                table: "Klient",
                column: "IdKlient",
                principalTable: "Osoba",
                principalColumn: "IdOsoba",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "KlientFirma_Firma",
                table: "KlientFirma",
                column: "IdFirma",
                principalTable: "Firma",
                principalColumn: "IdFirma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "KlientFirma_Klient",
                table: "KlientFirma",
                column: "IdKlient",
                principalTable: "Klient",
                principalColumn: "IdKlient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "PakietUsluga_Pakiet",
                table: "PakietUsluga",
                column: "IdPakiet",
                principalTable: "Pakiet",
                principalColumn: "IdPakiet",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "PakietUsluga_Usluga",
                table: "PakietUsluga",
                column: "IdUsluga",
                principalTable: "Usluga",
                principalColumn: "IdUsluga",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_15_Pracownik",
                table: "Pozycjoner",
                column: "IdPracownik",
                principalTable: "Pracownik",
                principalColumn: "IdPracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Pracownik_Osoba",
                table: "Pracownik",
                column: "IdPracownik",
                principalTable: "Osoba",
                principalColumn: "IdOsoba",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "PracownikKlient_Klient",
                table: "PracownikKlient",
                column: "IdKlient",
                principalTable: "Klient",
                principalColumn: "IdKlient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "PracownikKlient_Pracownik",
                table: "PracownikKlient",
                column: "IdPracownik",
                principalTable: "Pracownik",
                principalColumn: "IdPracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "PracownikUmowa_Pracownik",
                table: "PracownikUmowa",
                column: "IdPracownik",
                principalTable: "Pracownik",
                principalColumn: "IdPracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "PracownikUmowa_Umowa",
                table: "PracownikUmowa",
                column: "IdUmowa",
                principalTable: "Umowa",
                principalColumn: "IdUmowa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_22_Pracownik",
                table: "PracownikZespol",
                column: "IdPracownik",
                principalTable: "Pracownik",
                principalColumn: "IdPracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_22_Zespol",
                table: "PracownikZespol",
                column: "IdZespol",
                principalTable: "Zespol",
                principalColumn: "IdZespol",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_17_Pracownik",
                table: "Programista",
                column: "IdPracownik",
                principalTable: "Pracownik",
                principalColumn: "IdPracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_18_JezykProgramowania",
                table: "ProgramistaJezyk",
                column: "IdJezyk",
                principalTable: "JezykProgramowania",
                principalColumn: "IdJezyk",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_18_Programista",
                table: "ProgramistaJezyk",
                column: "IdPracownik",
                principalTable: "Programista",
                principalColumn: "IdPracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Projekt_Firma",
                table: "Projekt",
                column: "IdFirma",
                principalTable: "Firma",
                principalColumn: "IdFirma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Pracownik_Szef",
                table: "Szef",
                column: "IdPracownik",
                principalTable: "Pracownik",
                principalColumn: "IdPracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_14_Pracownik",
                table: "Tester",
                column: "IdPracownik",
                principalTable: "Pracownik",
                principalColumn: "IdPracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_24_Projekt",
                table: "ZespolProjekt",
                column: "IdProjekt",
                principalTable: "Projekt",
                principalColumn: "IdProjekt",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_24_Zespol",
                table: "ZespolProjekt",
                column: "IdZespol",
                principalTable: "Zespol",
                principalColumn: "IdZespol",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Table_16_Pracownik",
                table: "Grafik");

            migrationBuilder.DropForeignKey(
                name: "Klient_Osoba",
                table: "Klient");

            migrationBuilder.DropForeignKey(
                name: "KlientFirma_Firma",
                table: "KlientFirma");

            migrationBuilder.DropForeignKey(
                name: "KlientFirma_Klient",
                table: "KlientFirma");

            migrationBuilder.DropForeignKey(
                name: "PakietUsluga_Pakiet",
                table: "PakietUsluga");

            migrationBuilder.DropForeignKey(
                name: "PakietUsluga_Usluga",
                table: "PakietUsluga");

            migrationBuilder.DropForeignKey(
                name: "Table_15_Pracownik",
                table: "Pozycjoner");

            migrationBuilder.DropForeignKey(
                name: "Pracownik_Osoba",
                table: "Pracownik");

            migrationBuilder.DropForeignKey(
                name: "PracownikKlient_Klient",
                table: "PracownikKlient");

            migrationBuilder.DropForeignKey(
                name: "PracownikKlient_Pracownik",
                table: "PracownikKlient");

            migrationBuilder.DropForeignKey(
                name: "PracownikUmowa_Pracownik",
                table: "PracownikUmowa");

            migrationBuilder.DropForeignKey(
                name: "PracownikUmowa_Umowa",
                table: "PracownikUmowa");

            migrationBuilder.DropForeignKey(
                name: "Table_22_Pracownik",
                table: "PracownikZespol");

            migrationBuilder.DropForeignKey(
                name: "Table_22_Zespol",
                table: "PracownikZespol");

            migrationBuilder.DropForeignKey(
                name: "Table_17_Pracownik",
                table: "Programista");

            migrationBuilder.DropForeignKey(
                name: "Table_18_JezykProgramowania",
                table: "ProgramistaJezyk");

            migrationBuilder.DropForeignKey(
                name: "Table_18_Programista",
                table: "ProgramistaJezyk");

            migrationBuilder.DropForeignKey(
                name: "Projekt_Firma",
                table: "Projekt");

            migrationBuilder.DropForeignKey(
                name: "Pracownik_Szef",
                table: "Szef");

            migrationBuilder.DropForeignKey(
                name: "Table_14_Pracownik",
                table: "Tester");

            migrationBuilder.DropForeignKey(
                name: "Table_24_Projekt",
                table: "ZespolProjekt");

            migrationBuilder.DropForeignKey(
                name: "Table_24_Zespol",
                table: "ZespolProjekt");

            migrationBuilder.DropTable(
                name: "ProjektPakiet");

            migrationBuilder.DropTable(
                name: "UslugaProjekt");

            migrationBuilder.DropPrimaryKey(
                name: "ZespolProjekt_pk",
                table: "ZespolProjekt");

            migrationBuilder.DropPrimaryKey(
                name: "Zespol_pk",
                table: "Zespol");

            migrationBuilder.DropPrimaryKey(
                name: "Usluga_pk",
                table: "Usluga");

            migrationBuilder.DropPrimaryKey(
                name: "Umowa_pk",
                table: "Umowa");

            migrationBuilder.DropPrimaryKey(
                name: "Tester_pk",
                table: "Tester");

            migrationBuilder.DropPrimaryKey(
                name: "Szef_pk",
                table: "Szef");

            migrationBuilder.DropPrimaryKey(
                name: "Projekt_pk",
                table: "Projekt");

            migrationBuilder.DropIndex(
                name: "IX_Projekt_IdFirma",
                table: "Projekt");

            migrationBuilder.DropPrimaryKey(
                name: "ProgramistaJezyk_pk",
                table: "ProgramistaJezyk");

            migrationBuilder.DropPrimaryKey(
                name: "Programista_pk",
                table: "Programista");

            migrationBuilder.DropPrimaryKey(
                name: "PracownikZespol_pk",
                table: "PracownikZespol");

            migrationBuilder.DropPrimaryKey(
                name: "PracownikUmowa_pk",
                table: "PracownikUmowa");

            migrationBuilder.DropPrimaryKey(
                name: "PracownikKlient_pk",
                table: "PracownikKlient");

            migrationBuilder.DropPrimaryKey(
                name: "Pracownik_pk",
                table: "Pracownik");

            migrationBuilder.DropPrimaryKey(
                name: "Pozycjoner_pk",
                table: "Pozycjoner");

            migrationBuilder.DropPrimaryKey(
                name: "PakietUsluga_pk",
                table: "PakietUsluga");

            migrationBuilder.DropPrimaryKey(
                name: "Pakiet_pk",
                table: "Pakiet");

            migrationBuilder.DropPrimaryKey(
                name: "Osoba_pk",
                table: "Osoba");

            migrationBuilder.DropPrimaryKey(
                name: "KlientFirma_pk",
                table: "KlientFirma");

            migrationBuilder.DropPrimaryKey(
                name: "Klient_pk",
                table: "Klient");

            migrationBuilder.DropPrimaryKey(
                name: "JezykProgramowania_pk",
                table: "JezykProgramowania");

            migrationBuilder.DropPrimaryKey(
                name: "Grafik_pk",
                table: "Grafik");

            migrationBuilder.DropPrimaryKey(
                name: "Firma_pk",
                table: "Firma");

            migrationBuilder.DropColumn(
                name: "DataWypisaniaZespolu",
                table: "ZespolProjekt");

            migrationBuilder.DropColumn(
                name: "Klasyfikacja",
                table: "Usluga");

            migrationBuilder.DropColumn(
                name: "IdFirma",
                table: "Projekt");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Projekt");

            migrationBuilder.DropColumn(
                name: "DataZakonczeniaUmowy",
                table: "PracownikUmowa");

            migrationBuilder.DropColumn(
                name: "CzyEmailZweryfikowany",
                table: "Osoba");

            migrationBuilder.DropColumn(
                name: "Rola",
                table: "Osoba");

            migrationBuilder.RenameTable(
                name: "ZespolProjekt",
                newName: "zespolprojekt");

            migrationBuilder.RenameTable(
                name: "Zespol",
                newName: "zespol");

            migrationBuilder.RenameTable(
                name: "Usluga",
                newName: "usluga");

            migrationBuilder.RenameTable(
                name: "Umowa",
                newName: "umowa");

            migrationBuilder.RenameTable(
                name: "Tester",
                newName: "tester");

            migrationBuilder.RenameTable(
                name: "Szef",
                newName: "szef");

            migrationBuilder.RenameTable(
                name: "Projekt",
                newName: "projekt");

            migrationBuilder.RenameTable(
                name: "ProgramistaJezyk",
                newName: "programistajezyk");

            migrationBuilder.RenameTable(
                name: "Programista",
                newName: "programista");

            migrationBuilder.RenameTable(
                name: "PracownikZespol",
                newName: "pracownikzespol");

            migrationBuilder.RenameTable(
                name: "PracownikUmowa",
                newName: "pracownikumowa");

            migrationBuilder.RenameTable(
                name: "PracownikKlient",
                newName: "pracownikklient");

            migrationBuilder.RenameTable(
                name: "Pracownik",
                newName: "pracownik");

            migrationBuilder.RenameTable(
                name: "Pozycjoner",
                newName: "pozycjoner");

            migrationBuilder.RenameTable(
                name: "PakietUsluga",
                newName: "pakietusluga");

            migrationBuilder.RenameTable(
                name: "Pakiet",
                newName: "pakiet");

            migrationBuilder.RenameTable(
                name: "Osoba",
                newName: "osoba");

            migrationBuilder.RenameTable(
                name: "KlientFirma",
                newName: "klientfirma");

            migrationBuilder.RenameTable(
                name: "Klient",
                newName: "klient");

            migrationBuilder.RenameTable(
                name: "JezykProgramowania",
                newName: "jezykprogramowania");

            migrationBuilder.RenameTable(
                name: "Grafik",
                newName: "grafik");

            migrationBuilder.RenameTable(
                name: "Firma",
                newName: "firma");

            migrationBuilder.RenameColumn(
                name: "DataPrzypisaniaZespolu",
                table: "zespolprojekt",
                newName: "dataprzypisaniazespolu");

            migrationBuilder.RenameColumn(
                name: "IdProjekt",
                table: "zespolprojekt",
                newName: "idprojekt");

            migrationBuilder.RenameColumn(
                name: "IdZespol",
                table: "zespolprojekt",
                newName: "idzespol");

            migrationBuilder.RenameIndex(
                name: "IX_ZespolProjekt_IdProjekt",
                table: "zespolprojekt",
                newName: "IX_zespolprojekt_idprojekt");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "zespol",
                newName: "nazwa");

            migrationBuilder.RenameColumn(
                name: "IdZespol",
                table: "zespol",
                newName: "idzespol");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "usluga",
                newName: "nazwa");

            migrationBuilder.RenameColumn(
                name: "IdUsluga",
                table: "usluga",
                newName: "idusluga");

            migrationBuilder.RenameColumn(
                name: "RodzajUmowy",
                table: "umowa",
                newName: "rodzajumowy");

            migrationBuilder.RenameColumn(
                name: "IdUmowa",
                table: "umowa",
                newName: "idumowa");

            migrationBuilder.RenameColumn(
                name: "TesterDoswiadczenie",
                table: "tester",
                newName: "testerdoswiadczenie");

            migrationBuilder.RenameColumn(
                name: "IdPracownik",
                table: "tester",
                newName: "idpracownik");

            migrationBuilder.RenameColumn(
                name: "IdPracownik",
                table: "szef",
                newName: "idpracownik");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "projekt",
                newName: "nazwa");

            migrationBuilder.RenameColumn(
                name: "IdProjekt",
                table: "projekt",
                newName: "idprojekt");

            migrationBuilder.RenameColumn(
                name: "Staz",
                table: "programistajezyk",
                newName: "staz");

            migrationBuilder.RenameColumn(
                name: "IdJezyk",
                table: "programistajezyk",
                newName: "idjezyk");

            migrationBuilder.RenameColumn(
                name: "IdPracownik",
                table: "programistajezyk",
                newName: "idpracownik");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramistaJezyk_IdJezyk",
                table: "programistajezyk",
                newName: "IX_programistajezyk_idjezyk");

            migrationBuilder.RenameColumn(
                name: "PoziomZaawansowania",
                table: "programista",
                newName: "poziomzaawansowania");

            migrationBuilder.RenameColumn(
                name: "IdPracownik",
                table: "programista",
                newName: "idpracownik");

            migrationBuilder.RenameColumn(
                name: "DataWypisaniaPracownika",
                table: "pracownikzespol",
                newName: "datawypisaniapracownika");

            migrationBuilder.RenameColumn(
                name: "DataPrzypisaniaPracownika",
                table: "pracownikzespol",
                newName: "dataprzypisaniapracownika");

            migrationBuilder.RenameColumn(
                name: "IdZespol",
                table: "pracownikzespol",
                newName: "idzespol");

            migrationBuilder.RenameColumn(
                name: "IdPracownik",
                table: "pracownikzespol",
                newName: "idpracownik");

            migrationBuilder.RenameIndex(
                name: "IX_PracownikZespol_IdZespol",
                table: "pracownikzespol",
                newName: "IX_pracownikzespol_idzespol");

            migrationBuilder.RenameColumn(
                name: "DataPodpisaniaUmowy",
                table: "pracownikumowa",
                newName: "datapodpisaniaumowy");

            migrationBuilder.RenameColumn(
                name: "IdUmowa",
                table: "pracownikumowa",
                newName: "idumowa");

            migrationBuilder.RenameColumn(
                name: "IdPracownik",
                table: "pracownikumowa",
                newName: "idpracownik");

            migrationBuilder.RenameIndex(
                name: "IX_PracownikUmowa_IdUmowa",
                table: "pracownikumowa",
                newName: "IX_pracownikumowa_idumowa");

            migrationBuilder.RenameColumn(
                name: "MiejsceSpotkania",
                table: "pracownikklient",
                newName: "miejscespotkania");

            migrationBuilder.RenameColumn(
                name: "DataZakonczeniaSpotkania",
                table: "pracownikklient",
                newName: "datazakonczeniaspotkania");

            migrationBuilder.RenameColumn(
                name: "DataRozpoczeciaSpotkania",
                table: "pracownikklient",
                newName: "datarozpoczeciaspotkania");

            migrationBuilder.RenameColumn(
                name: "IdPracownik",
                table: "pracownikklient",
                newName: "idpracownik");

            migrationBuilder.RenameColumn(
                name: "IdKlient",
                table: "pracownikklient",
                newName: "idklient");

            migrationBuilder.RenameIndex(
                name: "IX_PracownikKlient_IdPracownik",
                table: "pracownikklient",
                newName: "IX_pracownikklient_idpracownik");

            migrationBuilder.RenameColumn(
                name: "Pesel",
                table: "pracownik",
                newName: "PESEL");

            migrationBuilder.RenameColumn(
                name: "AdresZamieszkania",
                table: "pracownik",
                newName: "adreszamieszkania");

            migrationBuilder.RenameColumn(
                name: "IdPracownik",
                table: "pracownik",
                newName: "idpracownik");

            migrationBuilder.RenameColumn(
                name: "PelnionaFunkcja",
                table: "pozycjoner",
                newName: "pelnionafunkcja");

            migrationBuilder.RenameColumn(
                name: "IdPracownik",
                table: "pozycjoner",
                newName: "idpracownik");

            migrationBuilder.RenameColumn(
                name: "IdUsluga",
                table: "pakietusluga",
                newName: "idusluga");

            migrationBuilder.RenameColumn(
                name: "IdPakiet",
                table: "pakietusluga",
                newName: "idpakiet");

            migrationBuilder.RenameIndex(
                name: "IX_PakietUsluga_IdUsluga",
                table: "pakietusluga",
                newName: "IX_pakietusluga_idusluga");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "pakiet",
                newName: "nazwa");

            migrationBuilder.RenameColumn(
                name: "IdPakiet",
                table: "pakiet",
                newName: "idpakiet");

            migrationBuilder.RenameColumn(
                name: "Imie",
                table: "osoba",
                newName: "imie");

            migrationBuilder.RenameColumn(
                name: "IdOsoba",
                table: "osoba",
                newName: "idosoba");

            migrationBuilder.RenameColumn(
                name: "IdFirma",
                table: "klientfirma",
                newName: "idfirma");

            migrationBuilder.RenameColumn(
                name: "IdKlient",
                table: "klientfirma",
                newName: "idklient");

            migrationBuilder.RenameIndex(
                name: "IX_KlientFirma_IdFirma",
                table: "klientfirma",
                newName: "IX_klientfirma_idfirma");

            migrationBuilder.RenameColumn(
                name: "Priorytet",
                table: "klient",
                newName: "priorytet");

            migrationBuilder.RenameColumn(
                name: "IdKlient",
                table: "klient",
                newName: "idklient");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "jezykprogramowania",
                newName: "nazwa");

            migrationBuilder.RenameColumn(
                name: "IdJezyk",
                table: "jezykprogramowania",
                newName: "idjezyk");

            migrationBuilder.RenameColumn(
                name: "Specjalizacja",
                table: "grafik",
                newName: "specjalizacja");

            migrationBuilder.RenameColumn(
                name: "IdPracownik",
                table: "grafik",
                newName: "idpracownik");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "firma",
                newName: "nazwa");

            migrationBuilder.RenameColumn(
                name: "IdFirma",
                table: "firma",
                newName: "idfirma");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataprzypisaniazespolu",
                table: "zespolprojekt",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<DateTime>(
                name: "dataoddaniaprojektu",
                table: "zespolprojekt",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "opis",
                table: "usluga",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Firma_IdFirma",
                table: "projekt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datawypisaniapracownika",
                table: "pracownikzespol",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataprzypisaniapracownika",
                table: "pracownikzespol",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<bool>(
                name: "menadzer",
                table: "pracownikzespol",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datapodpisaniaumowy",
                table: "pracownikumowa",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<DateTime>(
                name: "datawygasnieciaumowy",
                table: "pracownikumowa",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Premia",
                table: "pracownik",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "CzyEmailZweryfikowane",
                table: "osoba",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "zespolprojektpk",
                table: "zespolprojekt",
                columns: new[] { "dataprzypisaniazespolu", "idprojekt", "idzespol" });

            migrationBuilder.AddPrimaryKey(
                name: "zespolpk",
                table: "zespol",
                column: "idzespol");

            migrationBuilder.AddPrimaryKey(
                name: "uslugapk",
                table: "usluga",
                column: "idusluga");

            migrationBuilder.AddPrimaryKey(
                name: "umowapk",
                table: "umowa",
                column: "idumowa");

            migrationBuilder.AddPrimaryKey(
                name: "testerpk",
                table: "tester",
                column: "idpracownik");

            migrationBuilder.AddPrimaryKey(
                name: "szefpk",
                table: "szef",
                column: "idpracownik");

            migrationBuilder.AddPrimaryKey(
                name: "projektpk",
                table: "projekt",
                column: "idprojekt");

            migrationBuilder.AddPrimaryKey(
                name: "programistajezykpk",
                table: "programistajezyk",
                columns: new[] { "idpracownik", "idjezyk" });

            migrationBuilder.AddPrimaryKey(
                name: "programistapk",
                table: "programista",
                column: "idpracownik");

            migrationBuilder.AddPrimaryKey(
                name: "pracownikzespolpk",
                table: "pracownikzespol",
                columns: new[] { "dataprzypisaniapracownika", "idpracownik", "idzespol" });

            migrationBuilder.AddPrimaryKey(
                name: "pracownikumowapk",
                table: "pracownikumowa",
                columns: new[] { "datapodpisaniaumowy", "idpracownik", "idumowa" });

            migrationBuilder.AddPrimaryKey(
                name: "pracownikklientpk",
                table: "pracownikklient",
                columns: new[] { "datarozpoczeciaspotkania", "idpracownik", "idklient" });

            migrationBuilder.AddPrimaryKey(
                name: "pracownikpk",
                table: "pracownik",
                column: "idpracownik");

            migrationBuilder.AddPrimaryKey(
                name: "pozycjonerpk",
                table: "pozycjoner",
                column: "idpracownik");

            migrationBuilder.AddPrimaryKey(
                name: "pakietuslugapk",
                table: "pakietusluga",
                columns: new[] { "idpakiet", "idusluga" });

            migrationBuilder.AddPrimaryKey(
                name: "pakietpk",
                table: "pakiet",
                column: "idpakiet");

            migrationBuilder.AddPrimaryKey(
                name: "osobapk",
                table: "osoba",
                column: "idosoba");

            migrationBuilder.AddPrimaryKey(
                name: "klientfirmapk",
                table: "klientfirma",
                columns: new[] { "idklient", "idfirma" });

            migrationBuilder.AddPrimaryKey(
                name: "klientpk",
                table: "klient",
                column: "idklient");

            migrationBuilder.AddPrimaryKey(
                name: "jezykprogramowaniapk",
                table: "jezykprogramowania",
                column: "idjezyk");

            migrationBuilder.AddPrimaryKey(
                name: "grafikpk",
                table: "grafik",
                column: "idpracownik");

            migrationBuilder.AddPrimaryKey(
                name: "firmapk",
                table: "firma",
                column: "idfirma");

            migrationBuilder.CreateTable(
                name: "klientpakiet",
                columns: table => new
                {
                    datarozpoczeciawspolpracy = table.Column<DateTime>(type: "date", nullable: false),
                    idklient = table.Column<int>(type: "int", nullable: false),
                    idpakiet = table.Column<int>(type: "int", nullable: false),
                    datazakonczeniawspolpracy = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("klientpakietpk", x => new { x.datarozpoczeciawspolpracy, x.idklient, x.idpakiet });
                    table.ForeignKey(
                        name: "klientpakietklientfk",
                        column: x => x.idklient,
                        principalTable: "klient",
                        principalColumn: "idklient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "klientpakietpakietfk",
                        column: x => x.idpakiet,
                        principalTable: "pakiet",
                        principalColumn: "idpakiet",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    idtag = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tagpk", x => x.idtag);
                });

            migrationBuilder.CreateTable(
                name: "zadanie",
                columns: table => new
                {
                    idzadanie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("zadaniepk", x => x.idzadanie);
                });

            migrationBuilder.CreateTable(
                name: "firmatag",
                columns: table => new
                {
                    idfirma = table.Column<int>(type: "int", nullable: false),
                    idtag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("firmatagpk", x => new { x.idfirma, x.idtag });
                    table.ForeignKey(
                        name: "firmatagfirmafk",
                        column: x => x.idfirma,
                        principalTable: "firma",
                        principalColumn: "idfirma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "firmatagtagfk",
                        column: x => x.idtag,
                        principalTable: "tag",
                        principalColumn: "idtag",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "zadanieprojekt",
                columns: table => new
                {
                    idprojekt = table.Column<int>(type: "int", nullable: false),
                    idzadanie = table.Column<int>(type: "int", nullable: false),
                    datarozpoczeciazadania = table.Column<DateTime>(type: "datetime", nullable: false),
                    datazakonczeniazadania = table.Column<DateTime>(type: "datetime", nullable: true),
                    opis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("zadanieprojektpk", x => new { x.idprojekt, x.idzadanie, x.datarozpoczeciazadania });
                    table.ForeignKey(
                        name: "zadanieprojektprojektfk",
                        column: x => x.idprojekt,
                        principalTable: "projekt",
                        principalColumn: "idprojekt",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "zadanieprojektzadaniefk",
                        column: x => x.idzadanie,
                        principalTable: "zadanie",
                        principalColumn: "idzadanie",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_zespolprojekt_idzespol",
                table: "zespolprojekt",
                column: "idzespol");

            migrationBuilder.CreateIndex(
                name: "IX_projekt_Firma_IdFirma",
                table: "projekt",
                column: "Firma_IdFirma");

            migrationBuilder.CreateIndex(
                name: "IX_pracownikzespol_idpracownik",
                table: "pracownikzespol",
                column: "idpracownik");

            migrationBuilder.CreateIndex(
                name: "IX_pracownikumowa_idpracownik",
                table: "pracownikumowa",
                column: "idpracownik");

            migrationBuilder.CreateIndex(
                name: "IX_pracownikklient_idklient",
                table: "pracownikklient",
                column: "idklient");

            migrationBuilder.CreateIndex(
                name: "IX_firmatag_idtag",
                table: "firmatag",
                column: "idtag");

            migrationBuilder.CreateIndex(
                name: "IX_klientpakiet_idklient",
                table: "klientpakiet",
                column: "idklient");

            migrationBuilder.CreateIndex(
                name: "IX_klientpakiet_idpakiet",
                table: "klientpakiet",
                column: "idpakiet");

            migrationBuilder.CreateIndex(
                name: "IX_zadanieprojekt_idzadanie",
                table: "zadanieprojekt",
                column: "idzadanie");

            migrationBuilder.AddForeignKey(
                name: "grafikpracownikfk",
                table: "grafik",
                column: "idpracownik",
                principalTable: "pracownik",
                principalColumn: "idpracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "klientosobafk",
                table: "klient",
                column: "idklient",
                principalTable: "osoba",
                principalColumn: "idosoba",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "klientfirmafirmafk",
                table: "klientfirma",
                column: "idfirma",
                principalTable: "firma",
                principalColumn: "idfirma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "klientfirmaklientfk",
                table: "klientfirma",
                column: "idklient",
                principalTable: "klient",
                principalColumn: "idklient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pakietuslugapakietfk",
                table: "pakietusluga",
                column: "idpakiet",
                principalTable: "pakiet",
                principalColumn: "idpakiet",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pakietuslugauslugafk",
                table: "pakietusluga",
                column: "idusluga",
                principalTable: "usluga",
                principalColumn: "idusluga",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pozycjonerpracownikfk",
                table: "pozycjoner",
                column: "idpracownik",
                principalTable: "pracownik",
                principalColumn: "idpracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pracownikosobafk",
                table: "pracownik",
                column: "idpracownik",
                principalTable: "osoba",
                principalColumn: "idosoba",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pracownikklientklientfk",
                table: "pracownikklient",
                column: "idklient",
                principalTable: "klient",
                principalColumn: "idklient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pracownikklientpracownikfk",
                table: "pracownikklient",
                column: "idpracownik",
                principalTable: "pracownik",
                principalColumn: "idpracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pracownikumowapracownikfk",
                table: "pracownikumowa",
                column: "idpracownik",
                principalTable: "pracownik",
                principalColumn: "idpracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pracownikumowaumowafk",
                table: "pracownikumowa",
                column: "idumowa",
                principalTable: "umowa",
                principalColumn: "idumowa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pracownikzespolpracownikfk",
                table: "pracownikzespol",
                column: "idpracownik",
                principalTable: "pracownik",
                principalColumn: "idpracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pracownikzespolzespolfk",
                table: "pracownikzespol",
                column: "idzespol",
                principalTable: "zespol",
                principalColumn: "idzespol",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "programistapracownikfk",
                table: "programista",
                column: "idpracownik",
                principalTable: "pracownik",
                principalColumn: "idpracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "programistajezykjezykprogrfk",
                table: "programistajezyk",
                column: "idjezyk",
                principalTable: "jezykprogramowania",
                principalColumn: "idjezyk",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "programistajezykprogramistafk",
                table: "programistajezyk",
                column: "idpracownik",
                principalTable: "programista",
                principalColumn: "idpracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "projekt_firma_fk",
                table: "projekt",
                column: "Firma_IdFirma",
                principalTable: "firma",
                principalColumn: "idfirma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "szefpracownikfk",
                table: "szef",
                column: "idpracownik",
                principalTable: "pracownik",
                principalColumn: "idpracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "testerpracownikfk",
                table: "tester",
                column: "idpracownik",
                principalTable: "pracownik",
                principalColumn: "idpracownik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "zespolprojektprojektfk",
                table: "zespolprojekt",
                column: "idprojekt",
                principalTable: "projekt",
                principalColumn: "idprojekt",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "zespolprojektzespolfk",
                table: "zespolprojekt",
                column: "idzespol",
                principalTable: "zespol",
                principalColumn: "idzespol",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
