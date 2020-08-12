using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgencjaInteraktywna.Migrations
{
    public partial class SecondInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "firma",
                columns: table => new
                {
                    idfirma = table.Column<int>(nullable: false),
                    nazwa = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("firmapk", x => x.idfirma);
                });

            migrationBuilder.CreateTable(
                name: "jezykprogramowania",
                columns: table => new
                {
                    idjezyk = table.Column<int>(nullable: false),
                    nazwa = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("jezykprogramowaniapk", x => x.idjezyk);
                });

            migrationBuilder.CreateTable(
                name: "osoba",
                columns: table => new
                {
                    idosoba = table.Column<int>(nullable: false),
                    imie = table.Column<string>(maxLength: 25, nullable: false),
                    Nazwisko = table.Column<string>(maxLength: 50, nullable: false),
                    NumerTelefonuPrywatny = table.Column<string>(unicode: false, fixedLength: true, maxLength: 9, nullable: true),
                    NumerTelefonuSłużbowego = table.Column<string>(unicode: false, fixedLength: true, maxLength: 9, nullable: false),
                    AdresEmail = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("osobapk", x => x.idosoba);
                });

            migrationBuilder.CreateTable(
                name: "pakiet",
                columns: table => new
                {
                    idpakiet = table.Column<int>(nullable: false),
                    nazwa = table.Column<string>(maxLength: 50, nullable: false),
                    Oplata = table.Column<int>(nullable: false),
                    RodzajOplaty = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pakietpk", x => x.idpakiet);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    idtag = table.Column<int>(nullable: false),
                    nazwa = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tagpk", x => x.idtag);
                });

            migrationBuilder.CreateTable(
                name: "umowa",
                columns: table => new
                {
                    idumowa = table.Column<int>(nullable: false),
                    rodzajumowy = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("umowapk", x => x.idumowa);
                });

            migrationBuilder.CreateTable(
                name: "usluga",
                columns: table => new
                {
                    idusluga = table.Column<int>(nullable: false),
                    nazwa = table.Column<string>(maxLength: 50, nullable: false),
                    opis = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("uslugapk", x => x.idusluga);
                });

            migrationBuilder.CreateTable(
                name: "zadanie",
                columns: table => new
                {
                    idzadanie = table.Column<int>(nullable: false),
                    nazwa = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("zadaniepk", x => x.idzadanie);
                });

            migrationBuilder.CreateTable(
                name: "zespol",
                columns: table => new
                {
                    idzespol = table.Column<int>(nullable: false),
                    nazwa = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("zespolpk", x => x.idzespol);
                });

            migrationBuilder.CreateTable(
                name: "projekt",
                columns: table => new
                {
                    idprojekt = table.Column<int>(nullable: false),
                    nazwa = table.Column<string>(maxLength: 50, nullable: false),
                    Firma_IdFirma = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("projektpk", x => x.idprojekt);
                    table.ForeignKey(
                        name: "projekt_firma_fk",
                        column: x => x.Firma_IdFirma,
                        principalTable: "firma",
                        principalColumn: "idfirma",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "klient",
                columns: table => new
                {
                    idklient = table.Column<int>(nullable: false),
                    priorytet = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("klientpk", x => x.idklient);
                    table.ForeignKey(
                        name: "klientosobafk",
                        column: x => x.idklient,
                        principalTable: "osoba",
                        principalColumn: "idosoba",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pracownik",
                columns: table => new
                {
                    idpracownik = table.Column<int>(nullable: false),
                    adreszamieszkania = table.Column<string>(maxLength: 100, nullable: false),
                    Pensja = table.Column<int>(nullable: false),
                    Premia = table.Column<int>(nullable: true),
                    PESEL = table.Column<string>(unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    StazPracy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pracownikpk", x => x.idpracownik);
                    table.ForeignKey(
                        name: "pracownikosobafk",
                        column: x => x.idpracownik,
                        principalTable: "osoba",
                        principalColumn: "idosoba",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "firmatag",
                columns: table => new
                {
                    idfirma = table.Column<int>(nullable: false),
                    idtag = table.Column<int>(nullable: false)
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
                name: "pakietusluga",
                columns: table => new
                {
                    idpakiet = table.Column<int>(nullable: false),
                    idusluga = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pakietuslugapk", x => new { x.idpakiet, x.idusluga });
                    table.ForeignKey(
                        name: "pakietuslugapakietfk",
                        column: x => x.idpakiet,
                        principalTable: "pakiet",
                        principalColumn: "idpakiet",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "pakietuslugauslugafk",
                        column: x => x.idusluga,
                        principalTable: "usluga",
                        principalColumn: "idusluga",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "zadanieprojekt",
                columns: table => new
                {
                    idzadanie = table.Column<int>(nullable: false),
                    idprojekt = table.Column<int>(nullable: false),
                    datarozpoczeciazadania = table.Column<DateTime>(type: "datetime", nullable: false),
                    datazakonczeniazadania = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(maxLength: 50, nullable: true),
                    opis = table.Column<string>(maxLength: 100, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "zespolprojekt",
                columns: table => new
                {
                    idzespol = table.Column<int>(nullable: false),
                    idprojekt = table.Column<int>(nullable: false),
                    dataprzypisaniazespolu = table.Column<DateTime>(type: "date", nullable: false),
                    dataoddaniaprojektu = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("zespolprojektpk", x => new { x.dataprzypisaniazespolu, x.idprojekt, x.idzespol });
                    table.ForeignKey(
                        name: "zespolprojektprojektfk",
                        column: x => x.idprojekt,
                        principalTable: "projekt",
                        principalColumn: "idprojekt",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "zespolprojektzespolfk",
                        column: x => x.idzespol,
                        principalTable: "zespol",
                        principalColumn: "idzespol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "klientfirma",
                columns: table => new
                {
                    idklient = table.Column<int>(nullable: false),
                    idfirma = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("klientfirmapk", x => new { x.idklient, x.idfirma });
                    table.ForeignKey(
                        name: "klientfirmafirmafk",
                        column: x => x.idfirma,
                        principalTable: "firma",
                        principalColumn: "idfirma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "klientfirmaklientfk",
                        column: x => x.idklient,
                        principalTable: "klient",
                        principalColumn: "idklient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "klientpakiet",
                columns: table => new
                {
                    idklient = table.Column<int>(nullable: false),
                    idpakiet = table.Column<int>(nullable: false),
                    datarozpoczeciawspolpracy = table.Column<DateTime>(type: "date", nullable: false),
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
                name: "grafik",
                columns: table => new
                {
                    idpracownik = table.Column<int>(nullable: false),
                    specjalizacja = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("grafikpk", x => x.idpracownik);
                    table.ForeignKey(
                        name: "grafikpracownikfk",
                        column: x => x.idpracownik,
                        principalTable: "pracownik",
                        principalColumn: "idpracownik",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pozycjoner",
                columns: table => new
                {
                    idpracownik = table.Column<int>(nullable: false),
                    pelnionafunkcja = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pozycjonerpk", x => x.idpracownik);
                    table.ForeignKey(
                        name: "pozycjonerpracownikfk",
                        column: x => x.idpracownik,
                        principalTable: "pracownik",
                        principalColumn: "idpracownik",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pracownikklient",
                columns: table => new
                {
                    idpracownik = table.Column<int>(nullable: false),
                    idklient = table.Column<int>(nullable: false),
                    datarozpoczeciaspotkania = table.Column<DateTime>(type: "datetime", nullable: false),
                    datazakonczeniaspotkania = table.Column<DateTime>(type: "datetime", nullable: true),
                    miejscespotkania = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pracownikklientpk", x => new { x.datarozpoczeciaspotkania, x.idpracownik, x.idklient });
                    table.ForeignKey(
                        name: "pracownikklientklientfk",
                        column: x => x.idklient,
                        principalTable: "klient",
                        principalColumn: "idklient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "pracownikklientpracownikfk",
                        column: x => x.idpracownik,
                        principalTable: "pracownik",
                        principalColumn: "idpracownik",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pracownikumowa",
                columns: table => new
                {
                    idpracownik = table.Column<int>(nullable: false),
                    idumowa = table.Column<int>(nullable: false),
                    datapodpisaniaumowy = table.Column<DateTime>(type: "date", nullable: false),
                    datawygasnieciaumowy = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pracownikumowapk", x => new { x.datapodpisaniaumowy, x.idpracownik, x.idumowa });
                    table.ForeignKey(
                        name: "pracownikumowapracownikfk",
                        column: x => x.idpracownik,
                        principalTable: "pracownik",
                        principalColumn: "idpracownik",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "pracownikumowaumowafk",
                        column: x => x.idumowa,
                        principalTable: "umowa",
                        principalColumn: "idumowa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pracownikzespol",
                columns: table => new
                {
                    idpracownik = table.Column<int>(nullable: false),
                    idzespol = table.Column<int>(nullable: false),
                    dataprzypisaniapracownika = table.Column<DateTime>(type: "date", nullable: false),
                    datawypisaniapracownika = table.Column<DateTime>(type: "date", nullable: true),
                    menadżer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pracownikzespolpk", x => new { x.dataprzypisaniapracownika, x.idpracownik, x.idzespol });
                    table.ForeignKey(
                        name: "pracownikzespolpracownikfk",
                        column: x => x.idpracownik,
                        principalTable: "pracownik",
                        principalColumn: "idpracownik",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "pracownikzespolzespolfk",
                        column: x => x.idzespol,
                        principalTable: "zespol",
                        principalColumn: "idzespol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "programista",
                columns: table => new
                {
                    idpracownik = table.Column<int>(nullable: false),
                    poziomzaawansowania = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("programistapk", x => x.idpracownik);
                    table.ForeignKey(
                        name: "programistapracownikfk",
                        column: x => x.idpracownik,
                        principalTable: "pracownik",
                        principalColumn: "idpracownik",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "szef",
                columns: table => new
                {
                    idpracownik = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("szefpk", x => x.idpracownik);
                    table.ForeignKey(
                        name: "szefpracownikfk",
                        column: x => x.idpracownik,
                        principalTable: "pracownik",
                        principalColumn: "idpracownik",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tester",
                columns: table => new
                {
                    idpracownik = table.Column<int>(nullable: false),
                    testerdoswiadczenie = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("testerpk", x => x.idpracownik);
                    table.ForeignKey(
                        name: "testerpracownikfk",
                        column: x => x.idpracownik,
                        principalTable: "pracownik",
                        principalColumn: "idpracownik",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "programistajezyk",
                columns: table => new
                {
                    idpracownik = table.Column<int>(nullable: false),
                    idjezyk = table.Column<int>(nullable: false),
                    staz = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("programistajezykpk", x => new { x.idpracownik, x.idjezyk });
                    table.ForeignKey(
                        name: "programistajezykjezykprogrfk",
                        column: x => x.idjezyk,
                        principalTable: "jezykprogramowania",
                        principalColumn: "idjezyk",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "programistajezykprogramistafk",
                        column: x => x.idpracownik,
                        principalTable: "programista",
                        principalColumn: "idpracownik",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_firmatag_idtag",
                table: "firmatag",
                column: "idtag");

            migrationBuilder.CreateIndex(
                name: "IX_klientfirma_idfirma",
                table: "klientfirma",
                column: "idfirma");

            migrationBuilder.CreateIndex(
                name: "IX_klientpakiet_idklient",
                table: "klientpakiet",
                column: "idklient");

            migrationBuilder.CreateIndex(
                name: "IX_klientpakiet_idpakiet",
                table: "klientpakiet",
                column: "idpakiet");

            migrationBuilder.CreateIndex(
                name: "IX_pakietusluga_idusluga",
                table: "pakietusluga",
                column: "idusluga");

            migrationBuilder.CreateIndex(
                name: "IX_pracownikklient_idklient",
                table: "pracownikklient",
                column: "idklient");

            migrationBuilder.CreateIndex(
                name: "IX_pracownikklient_idpracownik",
                table: "pracownikklient",
                column: "idpracownik");

            migrationBuilder.CreateIndex(
                name: "IX_pracownikumowa_idpracownik",
                table: "pracownikumowa",
                column: "idpracownik");

            migrationBuilder.CreateIndex(
                name: "IX_pracownikumowa_idumowa",
                table: "pracownikumowa",
                column: "idumowa");

            migrationBuilder.CreateIndex(
                name: "IX_pracownikzespol_idpracownik",
                table: "pracownikzespol",
                column: "idpracownik");

            migrationBuilder.CreateIndex(
                name: "IX_pracownikzespol_idzespol",
                table: "pracownikzespol",
                column: "idzespol");

            migrationBuilder.CreateIndex(
                name: "IX_programistajezyk_idjezyk",
                table: "programistajezyk",
                column: "idjezyk");

            migrationBuilder.CreateIndex(
                name: "IX_projekt_Firma_IdFirma",
                table: "projekt",
                column: "Firma_IdFirma");

            migrationBuilder.CreateIndex(
                name: "IX_zadanieprojekt_idzadanie",
                table: "zadanieprojekt",
                column: "idzadanie");

            migrationBuilder.CreateIndex(
                name: "IX_zespolprojekt_idprojekt",
                table: "zespolprojekt",
                column: "idprojekt");

            migrationBuilder.CreateIndex(
                name: "IX_zespolprojekt_idzespol",
                table: "zespolprojekt",
                column: "idzespol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "firmatag");

            migrationBuilder.DropTable(
                name: "grafik");

            migrationBuilder.DropTable(
                name: "klientfirma");

            migrationBuilder.DropTable(
                name: "klientpakiet");

            migrationBuilder.DropTable(
                name: "pakietusluga");

            migrationBuilder.DropTable(
                name: "pozycjoner");

            migrationBuilder.DropTable(
                name: "pracownikklient");

            migrationBuilder.DropTable(
                name: "pracownikumowa");

            migrationBuilder.DropTable(
                name: "pracownikzespol");

            migrationBuilder.DropTable(
                name: "programistajezyk");

            migrationBuilder.DropTable(
                name: "szef");

            migrationBuilder.DropTable(
                name: "tester");

            migrationBuilder.DropTable(
                name: "zadanieprojekt");

            migrationBuilder.DropTable(
                name: "zespolprojekt");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "pakiet");

            migrationBuilder.DropTable(
                name: "usluga");

            migrationBuilder.DropTable(
                name: "klient");

            migrationBuilder.DropTable(
                name: "umowa");

            migrationBuilder.DropTable(
                name: "jezykprogramowania");

            migrationBuilder.DropTable(
                name: "programista");

            migrationBuilder.DropTable(
                name: "zadanie");

            migrationBuilder.DropTable(
                name: "projekt");

            migrationBuilder.DropTable(
                name: "zespol");

            migrationBuilder.DropTable(
                name: "pracownik");

            migrationBuilder.DropTable(
                name: "firma");

            migrationBuilder.DropTable(
                name: "osoba");
        }
    }
}
