﻿// <auto-generated />
using System;
using AgencjaInteraktywna;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgencjaInteraktywna.Migrations
{
    [DbContext(typeof(s16693Context))]
    partial class s16693ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AgencjaInteraktywna.Firma", b =>
                {
                    b.Property<int>("Idfirma")
                        .HasColumnName("idfirma")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idfirma")
                        .HasName("firmapk");

                    b.ToTable("firma");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Firmatag", b =>
                {
                    b.Property<int>("Idfirma")
                        .HasColumnName("idfirma")
                        .HasColumnType("int");

                    b.Property<int>("Idtag")
                        .HasColumnName("idtag")
                        .HasColumnType("int");

                    b.HasKey("Idfirma", "Idtag")
                        .HasName("firmatagpk");

                    b.HasIndex("Idtag");

                    b.ToTable("firmatag");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Grafik", b =>
                {
                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.Property<string>("Specjalizacja")
                        .IsRequired()
                        .HasColumnName("specjalizacja")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idpracownik")
                        .HasName("grafikpk");

                    b.ToTable("grafik");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Jezykprogramowania", b =>
                {
                    b.Property<int>("Idjezyk")
                        .HasColumnName("idjezyk")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idjezyk")
                        .HasName("jezykprogramowaniapk");

                    b.ToTable("jezykprogramowania");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Klient", b =>
                {
                    b.Property<int>("Idklient")
                        .HasColumnName("idklient")
                        .HasColumnType("int");

                    b.Property<string>("Priorytet")
                        .IsRequired()
                        .HasColumnName("priorytet")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idklient")
                        .HasName("klientpk");

                    b.ToTable("klient");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Klientfirma", b =>
                {
                    b.Property<int>("Idklient")
                        .HasColumnName("idklient")
                        .HasColumnType("int");

                    b.Property<int>("Idfirma")
                        .HasColumnName("idfirma")
                        .HasColumnType("int");

                    b.HasKey("Idklient", "Idfirma")
                        .HasName("klientfirmapk");

                    b.HasIndex("Idfirma");

                    b.ToTable("klientfirma");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Klientpakiet", b =>
                {
                    b.Property<DateTime>("Datarozpoczeciawspolpracy")
                        .HasColumnName("datarozpoczeciawspolpracy")
                        .HasColumnType("date");

                    b.Property<int>("Idklient")
                        .HasColumnName("idklient")
                        .HasColumnType("int");

                    b.Property<int>("Idpakiet")
                        .HasColumnName("idpakiet")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Datazakonczeniawspolpracy")
                        .HasColumnName("datazakonczeniawspolpracy")
                        .HasColumnType("date");

                    b.HasKey("Datarozpoczeciawspolpracy", "Idklient", "Idpakiet")
                        .HasName("klientpakietpk");

                    b.HasIndex("Idklient");

                    b.HasIndex("Idpakiet");

                    b.ToTable("klientpakiet");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Osoba", b =>
                {
                    b.Property<int>("Idosoba")
                        .HasColumnName("idosoba")
                        .HasColumnType("int");

                    b.Property<string>("AdresEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnName("imie")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NumerTelefonuPrywatny")
                        .HasColumnType("char(9)")
                        .IsFixedLength(true)
                        .HasMaxLength(9)
                        .IsUnicode(false);

                    b.Property<string>("NumerTelefonuSłużbowego")
                        .IsRequired()
                        .HasColumnType("char(9)")
                        .IsFixedLength(true)
                        .HasMaxLength(9)
                        .IsUnicode(false);

                    b.HasKey("Idosoba")
                        .HasName("osobapk");

                    b.ToTable("osoba");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pakiet", b =>
                {
                    b.Property<int>("Idpakiet")
                        .HasColumnName("idpakiet")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Oplata")
                        .HasColumnType("int");

                    b.Property<string>("RodzajOplaty")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idpakiet")
                        .HasName("pakietpk");

                    b.ToTable("pakiet");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pakietusluga", b =>
                {
                    b.Property<int>("Idpakiet")
                        .HasColumnName("idpakiet")
                        .HasColumnType("int");

                    b.Property<int>("Idusluga")
                        .HasColumnName("idusluga")
                        .HasColumnType("int");

                    b.HasKey("Idpakiet", "Idusluga")
                        .HasName("pakietuslugapk");

                    b.HasIndex("Idusluga");

                    b.ToTable("pakietusluga");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pozycjoner", b =>
                {
                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.Property<string>("Pelnionafunkcja")
                        .IsRequired()
                        .HasColumnName("pelnionafunkcja")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idpracownik")
                        .HasName("pozycjonerpk");

                    b.ToTable("pozycjoner");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pracownik", b =>
                {
                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.Property<string>("Adreszamieszkania")
                        .IsRequired()
                        .HasColumnName("adreszamieszkania")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Pensja")
                        .HasColumnType("int");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnName("PESEL")
                        .HasColumnType("char(11)")
                        .IsFixedLength(true)
                        .HasMaxLength(11)
                        .IsUnicode(false);

                    b.Property<int?>("Premia")
                        .HasColumnType("int");

                    b.Property<int>("StazPracy")
                        .HasColumnType("int");

                    b.HasKey("Idpracownik")
                        .HasName("pracownikpk");

                    b.ToTable("pracownik");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pracownikklient", b =>
                {
                    b.Property<DateTime>("Datarozpoczeciaspotkania")
                        .HasColumnName("datarozpoczeciaspotkania")
                        .HasColumnType("datetime");

                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.Property<int>("Idklient")
                        .HasColumnName("idklient")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Datazakonczeniaspotkania")
                        .HasColumnName("datazakonczeniaspotkania")
                        .HasColumnType("datetime");

                    b.Property<string>("Miejscespotkania")
                        .IsRequired()
                        .HasColumnName("miejscespotkania")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Datarozpoczeciaspotkania", "Idpracownik", "Idklient")
                        .HasName("pracownikklientpk");

                    b.HasIndex("Idklient");

                    b.HasIndex("Idpracownik");

                    b.ToTable("pracownikklient");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pracownikumowa", b =>
                {
                    b.Property<DateTime>("Datapodpisaniaumowy")
                        .HasColumnName("datapodpisaniaumowy")
                        .HasColumnType("date");

                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.Property<int>("Idumowa")
                        .HasColumnName("idumowa")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datawygasnieciaumowy")
                        .HasColumnName("datawygasnieciaumowy")
                        .HasColumnType("date");

                    b.HasKey("Datapodpisaniaumowy", "Idpracownik", "Idumowa")
                        .HasName("pracownikumowapk");

                    b.HasIndex("Idpracownik");

                    b.HasIndex("Idumowa");

                    b.ToTable("pracownikumowa");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pracownikzespol", b =>
                {
                    b.Property<DateTime>("Dataprzypisaniapracownika")
                        .HasColumnName("dataprzypisaniapracownika")
                        .HasColumnType("date");

                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.Property<int>("Idzespol")
                        .HasColumnName("idzespol")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Datawypisaniapracownika")
                        .HasColumnName("datawypisaniapracownika")
                        .HasColumnType("date");

                    b.Property<bool>("Menadżer")
                        .HasColumnName("menadżer")
                        .HasColumnType("bit");

                    b.HasKey("Dataprzypisaniapracownika", "Idpracownik", "Idzespol")
                        .HasName("pracownikzespolpk");

                    b.HasIndex("Idpracownik");

                    b.HasIndex("Idzespol");

                    b.ToTable("pracownikzespol");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Programista", b =>
                {
                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.Property<string>("Poziomzaawansowania")
                        .IsRequired()
                        .HasColumnName("poziomzaawansowania")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idpracownik")
                        .HasName("programistapk");

                    b.ToTable("programista");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Programistajezyk", b =>
                {
                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.Property<int>("Idjezyk")
                        .HasColumnName("idjezyk")
                        .HasColumnType("int");

                    b.Property<int>("Staz")
                        .HasColumnName("staz")
                        .HasColumnType("int");

                    b.HasKey("Idpracownik", "Idjezyk")
                        .HasName("programistajezykpk");

                    b.HasIndex("Idjezyk");

                    b.ToTable("programistajezyk");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Projekt", b =>
                {
                    b.Property<int>("Idprojekt")
                        .HasColumnName("idprojekt")
                        .HasColumnType("int");

                    b.Property<int>("FirmaIdFirma")
                        .HasColumnName("Firma_IdFirma")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idprojekt")
                        .HasName("projektpk");

                    b.HasIndex("FirmaIdFirma");

                    b.ToTable("projekt");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Szef", b =>
                {
                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.HasKey("Idpracownik")
                        .HasName("szefpk");

                    b.ToTable("szef");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Tag", b =>
                {
                    b.Property<int>("Idtag")
                        .HasColumnName("idtag")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idtag")
                        .HasName("tagpk");

                    b.ToTable("tag");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Tester", b =>
                {
                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.Property<int>("Testerdoswiadczenie")
                        .HasColumnName("testerdoswiadczenie")
                        .HasColumnType("int");

                    b.HasKey("Idpracownik")
                        .HasName("testerpk");

                    b.ToTable("tester");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Umowa", b =>
                {
                    b.Property<int>("Idumowa")
                        .HasColumnName("idumowa")
                        .HasColumnType("int");

                    b.Property<string>("Rodzajumowy")
                        .IsRequired()
                        .HasColumnName("rodzajumowy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idumowa")
                        .HasName("umowapk");

                    b.ToTable("umowa");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Usluga", b =>
                {
                    b.Property<int>("Idusluga")
                        .HasColumnName("idusluga")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Opis")
                        .HasColumnName("opis")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Idusluga")
                        .HasName("uslugapk");

                    b.ToTable("usluga");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Zadanie", b =>
                {
                    b.Property<int>("Idzadanie")
                        .HasColumnName("idzadanie")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idzadanie")
                        .HasName("zadaniepk");

                    b.ToTable("zadanie");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Zadanieprojekt", b =>
                {
                    b.Property<int>("Idprojekt")
                        .HasColumnName("idprojekt")
                        .HasColumnType("int");

                    b.Property<int>("Idzadanie")
                        .HasColumnName("idzadanie")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datarozpoczeciazadania")
                        .HasColumnName("datarozpoczeciazadania")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("Datazakonczeniazadania")
                        .HasColumnName("datazakonczeniazadania")
                        .HasColumnType("datetime");

                    b.Property<string>("Opis")
                        .HasColumnName("opis")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idprojekt", "Idzadanie", "Datarozpoczeciazadania")
                        .HasName("zadanieprojektpk");

                    b.HasIndex("Idzadanie");

                    b.ToTable("zadanieprojekt");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Zespol", b =>
                {
                    b.Property<int>("Idzespol")
                        .HasColumnName("idzespol")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idzespol")
                        .HasName("zespolpk");

                    b.ToTable("zespol");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Zespolprojekt", b =>
                {
                    b.Property<DateTime>("Dataprzypisaniazespolu")
                        .HasColumnName("dataprzypisaniazespolu")
                        .HasColumnType("date");

                    b.Property<int>("Idprojekt")
                        .HasColumnName("idprojekt")
                        .HasColumnType("int");

                    b.Property<int>("Idzespol")
                        .HasColumnName("idzespol")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Dataoddaniaprojektu")
                        .HasColumnName("dataoddaniaprojektu")
                        .HasColumnType("date");

                    b.HasKey("Dataprzypisaniazespolu", "Idprojekt", "Idzespol")
                        .HasName("zespolprojektpk");

                    b.HasIndex("Idprojekt");

                    b.HasIndex("Idzespol");

                    b.ToTable("zespolprojekt");
                });

            modelBuilder.Entity("AgencjaInteraktywna.Firmatag", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Firma", "IdfirmaNavigation")
                        .WithMany("Firmatag")
                        .HasForeignKey("Idfirma")
                        .HasConstraintName("firmatagfirmafk")
                        .IsRequired();

                    b.HasOne("AgencjaInteraktywna.Tag", "IdtagNavigation")
                        .WithMany("Firmatag")
                        .HasForeignKey("Idtag")
                        .HasConstraintName("firmatagtagfk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Grafik", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Pracownik", "IdpracownikNavigation")
                        .WithOne("Grafik")
                        .HasForeignKey("AgencjaInteraktywna.Grafik", "Idpracownik")
                        .HasConstraintName("grafikpracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Klient", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Osoba", "IdklientNavigation")
                        .WithOne("Klient")
                        .HasForeignKey("AgencjaInteraktywna.Klient", "Idklient")
                        .HasConstraintName("klientosobafk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Klientfirma", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Firma", "IdfirmaNavigation")
                        .WithMany("Klientfirma")
                        .HasForeignKey("Idfirma")
                        .HasConstraintName("klientfirmafirmafk")
                        .IsRequired();

                    b.HasOne("AgencjaInteraktywna.Klient", "IdklientNavigation")
                        .WithMany("Klientfirma")
                        .HasForeignKey("Idklient")
                        .HasConstraintName("klientfirmaklientfk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Klientpakiet", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Klient", "IdklientNavigation")
                        .WithMany("Klientpakiet")
                        .HasForeignKey("Idklient")
                        .HasConstraintName("klientpakietklientfk")
                        .IsRequired();

                    b.HasOne("AgencjaInteraktywna.Pakiet", "IdpakietNavigation")
                        .WithMany("Klientpakiet")
                        .HasForeignKey("Idpakiet")
                        .HasConstraintName("klientpakietpakietfk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pakietusluga", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Pakiet", "IdpakietNavigation")
                        .WithMany("Pakietusluga")
                        .HasForeignKey("Idpakiet")
                        .HasConstraintName("pakietuslugapakietfk")
                        .IsRequired();

                    b.HasOne("AgencjaInteraktywna.Usluga", "IduslugaNavigation")
                        .WithMany("Pakietusluga")
                        .HasForeignKey("Idusluga")
                        .HasConstraintName("pakietuslugauslugafk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pozycjoner", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Pracownik", "IdpracownikNavigation")
                        .WithOne("Pozycjoner")
                        .HasForeignKey("AgencjaInteraktywna.Pozycjoner", "Idpracownik")
                        .HasConstraintName("pozycjonerpracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pracownik", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Osoba", "IdpracownikNavigation")
                        .WithOne("Pracownik")
                        .HasForeignKey("AgencjaInteraktywna.Pracownik", "Idpracownik")
                        .HasConstraintName("pracownikosobafk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pracownikklient", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Klient", "IdklientNavigation")
                        .WithMany("Pracownikklient")
                        .HasForeignKey("Idklient")
                        .HasConstraintName("pracownikklientklientfk")
                        .IsRequired();

                    b.HasOne("AgencjaInteraktywna.Pracownik", "IdpracownikNavigation")
                        .WithMany("Pracownikklient")
                        .HasForeignKey("Idpracownik")
                        .HasConstraintName("pracownikklientpracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pracownikumowa", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Pracownik", "IdpracownikNavigation")
                        .WithMany("Pracownikumowa")
                        .HasForeignKey("Idpracownik")
                        .HasConstraintName("pracownikumowapracownikfk")
                        .IsRequired();

                    b.HasOne("AgencjaInteraktywna.Umowa", "IdumowaNavigation")
                        .WithMany("Pracownikumowa")
                        .HasForeignKey("Idumowa")
                        .HasConstraintName("pracownikumowaumowafk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Pracownikzespol", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Pracownik", "IdpracownikNavigation")
                        .WithMany("Pracownikzespol")
                        .HasForeignKey("Idpracownik")
                        .HasConstraintName("pracownikzespolpracownikfk")
                        .IsRequired();

                    b.HasOne("AgencjaInteraktywna.Zespol", "IdzespolNavigation")
                        .WithMany("Pracownikzespol")
                        .HasForeignKey("Idzespol")
                        .HasConstraintName("pracownikzespolzespolfk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Programista", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Pracownik", "IdpracownikNavigation")
                        .WithOne("Programista")
                        .HasForeignKey("AgencjaInteraktywna.Programista", "Idpracownik")
                        .HasConstraintName("programistapracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Programistajezyk", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Jezykprogramowania", "IdjezykNavigation")
                        .WithMany("Programistajezyk")
                        .HasForeignKey("Idjezyk")
                        .HasConstraintName("programistajezykjezykprogrfk")
                        .IsRequired();

                    b.HasOne("AgencjaInteraktywna.Programista", "IdpracownikNavigation")
                        .WithMany("Programistajezyk")
                        .HasForeignKey("Idpracownik")
                        .HasConstraintName("programistajezykprogramistafk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Projekt", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Firma", "FirmaIdFirmaNavigation")
                        .WithMany("Projekt")
                        .HasForeignKey("FirmaIdFirma")
                        .HasConstraintName("projekt_firma_fk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Szef", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Pracownik", "IdpracownikNavigation")
                        .WithOne("Szef")
                        .HasForeignKey("AgencjaInteraktywna.Szef", "Idpracownik")
                        .HasConstraintName("szefpracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Tester", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Pracownik", "IdpracownikNavigation")
                        .WithOne("Tester")
                        .HasForeignKey("AgencjaInteraktywna.Tester", "Idpracownik")
                        .HasConstraintName("testerpracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Zadanieprojekt", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Projekt", "IdprojektNavigation")
                        .WithMany("Zadanieprojekt")
                        .HasForeignKey("Idprojekt")
                        .HasConstraintName("zadanieprojektprojektfk")
                        .IsRequired();

                    b.HasOne("AgencjaInteraktywna.Zadanie", "IdzadanieNavigation")
                        .WithMany("Zadanieprojekt")
                        .HasForeignKey("Idzadanie")
                        .HasConstraintName("zadanieprojektzadaniefk")
                        .IsRequired();
                });

            modelBuilder.Entity("AgencjaInteraktywna.Zespolprojekt", b =>
                {
                    b.HasOne("AgencjaInteraktywna.Projekt", "IdprojektNavigation")
                        .WithMany("Zespolprojekt")
                        .HasForeignKey("Idprojekt")
                        .HasConstraintName("zespolprojektprojektfk")
                        .IsRequired();

                    b.HasOne("AgencjaInteraktywna.Zespol", "IdzespolNavigation")
                        .WithMany("Zespolprojekt")
                        .HasForeignKey("Idzespol")
                        .HasConstraintName("zespolprojektzespolfk")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
