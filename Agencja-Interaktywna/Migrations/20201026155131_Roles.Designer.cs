﻿// <auto-generated />
using System;
using Agencja_Interaktywna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Agencja_Interaktywna.Migrations
{
    [DbContext(typeof(Models.DbContext))]
    [Migration("20201026155131_Roles")]
    partial class Roles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Agencja_Interaktywna.Models.Firma", b =>
                {
                    b.Property<int>("Idfirma")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idfirma")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idfirma")
                        .HasName("firmapk");

                    b.ToTable("firma");
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Firmatag", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Grafik", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Jezykprogramowania", b =>
                {
                    b.Property<int>("Idjezyk")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idjezyk")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idjezyk")
                        .HasName("jezykprogramowaniapk");

                    b.ToTable("jezykprogramowania");
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Klient", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Klientfirma", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Klientpakiet", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Osoba", b =>
                {
                    b.Property<int>("Idosoba")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idosoba")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdresEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("CzyEmailZweryfikowane")
                        .HasColumnType("bit");

                    b.Property<string>("Haslo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnName("imie")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<Guid>("KodAktywacyjny")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NumerTelefonuPrywatny")
                        .HasColumnType("char(9)")
                        .IsFixedLength(true)
                        .HasMaxLength(9)
                        .IsUnicode(false);

                    b.Property<string>("NumerTelefonuSluzbowego")
                        .IsRequired()
                        .HasColumnType("char(9)")
                        .IsFixedLength(true)
                        .HasMaxLength(9)
                        .IsUnicode(false);

                    b.HasKey("Idosoba")
                        .HasName("osobapk");

                    b.ToTable("osoba");
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pakiet", b =>
                {
                    b.Property<int>("Idpakiet")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idpakiet")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pakietusluga", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pozycjoner", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pracownik", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pracownikklient", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pracownikumowa", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pracownikzespol", b =>
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

                    b.Property<bool>("Menadzer")
                        .HasColumnName("menadzer")
                        .HasColumnType("bit");

                    b.HasKey("Dataprzypisaniapracownika", "Idpracownik", "Idzespol")
                        .HasName("pracownikzespolpk");

                    b.HasIndex("Idpracownik");

                    b.HasIndex("Idzespol");

                    b.ToTable("pracownikzespol");
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Programista", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Programistajezyk", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Projekt", b =>
                {
                    b.Property<int>("Idprojekt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idprojekt")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Szef", b =>
                {
                    b.Property<int>("Idpracownik")
                        .HasColumnName("idpracownik")
                        .HasColumnType("int");

                    b.HasKey("Idpracownik")
                        .HasName("szefpk");

                    b.ToTable("szef");
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Tag", b =>
                {
                    b.Property<int>("Idtag")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idtag")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idtag")
                        .HasName("tagpk");

                    b.ToTable("tag");
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Tester", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Umowa", b =>
                {
                    b.Property<int>("Idumowa")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idumowa")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Rodzajumowy")
                        .IsRequired()
                        .HasColumnName("rodzajumowy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idumowa")
                        .HasName("umowapk");

                    b.ToTable("umowa");
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Usluga", b =>
                {
                    b.Property<int>("Idusluga")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idusluga")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Zadanie", b =>
                {
                    b.Property<int>("Idzadanie")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idzadanie")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idzadanie")
                        .HasName("zadaniepk");

                    b.ToTable("zadanie");
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Zadanieprojekt", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Zespol", b =>
                {
                    b.Property<int>("Idzespol")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idzespol")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnName("nazwa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Idzespol")
                        .HasName("zespolpk");

                    b.ToTable("zespol");
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Zespolprojekt", b =>
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

            modelBuilder.Entity("Agencja_Interaktywna.Models.Firmatag", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Firma", "IdfirmaNavigation")
                        .WithMany("Firmatag")
                        .HasForeignKey("Idfirma")
                        .HasConstraintName("firmatagfirmafk")
                        .IsRequired();

                    b.HasOne("Agencja_Interaktywna.Models.Tag", "IdtagNavigation")
                        .WithMany("Firmatag")
                        .HasForeignKey("Idtag")
                        .HasConstraintName("firmatagtagfk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Grafik", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Pracownik", "IdpracownikNavigation")
                        .WithOne("Grafik")
                        .HasForeignKey("Agencja_Interaktywna.Models.Grafik", "Idpracownik")
                        .HasConstraintName("grafikpracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Klient", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Osoba", "IdklientNavigation")
                        .WithOne("Klient")
                        .HasForeignKey("Agencja_Interaktywna.Models.Klient", "Idklient")
                        .HasConstraintName("klientosobafk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Klientfirma", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Firma", "IdfirmaNavigation")
                        .WithMany("Klientfirma")
                        .HasForeignKey("Idfirma")
                        .HasConstraintName("klientfirmafirmafk")
                        .IsRequired();

                    b.HasOne("Agencja_Interaktywna.Models.Klient", "IdklientNavigation")
                        .WithMany("Klientfirma")
                        .HasForeignKey("Idklient")
                        .HasConstraintName("klientfirmaklientfk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Klientpakiet", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Klient", "IdklientNavigation")
                        .WithMany("Klientpakiet")
                        .HasForeignKey("Idklient")
                        .HasConstraintName("klientpakietklientfk")
                        .IsRequired();

                    b.HasOne("Agencja_Interaktywna.Models.Pakiet", "IdpakietNavigation")
                        .WithMany("Klientpakiet")
                        .HasForeignKey("Idpakiet")
                        .HasConstraintName("klientpakietpakietfk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pakietusluga", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Pakiet", "IdpakietNavigation")
                        .WithMany("Pakietusluga")
                        .HasForeignKey("Idpakiet")
                        .HasConstraintName("pakietuslugapakietfk")
                        .IsRequired();

                    b.HasOne("Agencja_Interaktywna.Models.Usluga", "IduslugaNavigation")
                        .WithMany("Pakietusluga")
                        .HasForeignKey("Idusluga")
                        .HasConstraintName("pakietuslugauslugafk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pozycjoner", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Pracownik", "IdpracownikNavigation")
                        .WithOne("Pozycjoner")
                        .HasForeignKey("Agencja_Interaktywna.Models.Pozycjoner", "Idpracownik")
                        .HasConstraintName("pozycjonerpracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pracownik", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Osoba", "IdpracownikNavigation")
                        .WithOne("Pracownik")
                        .HasForeignKey("Agencja_Interaktywna.Models.Pracownik", "Idpracownik")
                        .HasConstraintName("pracownikosobafk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pracownikklient", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Klient", "IdklientNavigation")
                        .WithMany("Pracownikklient")
                        .HasForeignKey("Idklient")
                        .HasConstraintName("pracownikklientklientfk")
                        .IsRequired();

                    b.HasOne("Agencja_Interaktywna.Models.Pracownik", "IdpracownikNavigation")
                        .WithMany("Pracownikklient")
                        .HasForeignKey("Idpracownik")
                        .HasConstraintName("pracownikklientpracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pracownikumowa", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Pracownik", "IdpracownikNavigation")
                        .WithMany("Pracownikumowa")
                        .HasForeignKey("Idpracownik")
                        .HasConstraintName("pracownikumowapracownikfk")
                        .IsRequired();

                    b.HasOne("Agencja_Interaktywna.Models.Umowa", "IdumowaNavigation")
                        .WithMany("Pracownikumowa")
                        .HasForeignKey("Idumowa")
                        .HasConstraintName("pracownikumowaumowafk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Pracownikzespol", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Pracownik", "IdpracownikNavigation")
                        .WithMany("Pracownikzespol")
                        .HasForeignKey("Idpracownik")
                        .HasConstraintName("pracownikzespolpracownikfk")
                        .IsRequired();

                    b.HasOne("Agencja_Interaktywna.Models.Zespol", "IdzespolNavigation")
                        .WithMany("Pracownikzespol")
                        .HasForeignKey("Idzespol")
                        .HasConstraintName("pracownikzespolzespolfk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Programista", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Pracownik", "IdpracownikNavigation")
                        .WithOne("Programista")
                        .HasForeignKey("Agencja_Interaktywna.Models.Programista", "Idpracownik")
                        .HasConstraintName("programistapracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Programistajezyk", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Jezykprogramowania", "IdjezykNavigation")
                        .WithMany("Programistajezyk")
                        .HasForeignKey("Idjezyk")
                        .HasConstraintName("programistajezykjezykprogrfk")
                        .IsRequired();

                    b.HasOne("Agencja_Interaktywna.Models.Programista", "IdpracownikNavigation")
                        .WithMany("Programistajezyk")
                        .HasForeignKey("Idpracownik")
                        .HasConstraintName("programistajezykprogramistafk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Projekt", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Firma", "FirmaIdFirmaNavigation")
                        .WithMany("Projekt")
                        .HasForeignKey("FirmaIdFirma")
                        .HasConstraintName("projekt_firma_fk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Szef", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Pracownik", "IdpracownikNavigation")
                        .WithOne("Szef")
                        .HasForeignKey("Agencja_Interaktywna.Models.Szef", "Idpracownik")
                        .HasConstraintName("szefpracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Tester", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Pracownik", "IdpracownikNavigation")
                        .WithOne("Tester")
                        .HasForeignKey("Agencja_Interaktywna.Models.Tester", "Idpracownik")
                        .HasConstraintName("testerpracownikfk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Zadanieprojekt", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Projekt", "IdprojektNavigation")
                        .WithMany("Zadanieprojekt")
                        .HasForeignKey("Idprojekt")
                        .HasConstraintName("zadanieprojektprojektfk")
                        .IsRequired();

                    b.HasOne("Agencja_Interaktywna.Models.Zadanie", "IdzadanieNavigation")
                        .WithMany("Zadanieprojekt")
                        .HasForeignKey("Idzadanie")
                        .HasConstraintName("zadanieprojektzadaniefk")
                        .IsRequired();
                });

            modelBuilder.Entity("Agencja_Interaktywna.Models.Zespolprojekt", b =>
                {
                    b.HasOne("Agencja_Interaktywna.Models.Projekt", "IdprojektNavigation")
                        .WithMany("Zespolprojekt")
                        .HasForeignKey("Idprojekt")
                        .HasConstraintName("zespolprojektprojektfk")
                        .IsRequired();

                    b.HasOne("Agencja_Interaktywna.Models.Zespol", "IdzespolNavigation")
                        .WithMany("Zespolprojekt")
                        .HasForeignKey("Idzespol")
                        .HasConstraintName("zespolprojektzespolfk")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
