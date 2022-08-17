using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agencja_Interaktywna.Models
{
    public partial class InteractiveAgencyContext : DbContext
    {
        public InteractiveAgencyContext()
        {
        }

        public InteractiveAgencyContext(DbContextOptions<InteractiveAgencyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Firma { get; set; }
        public virtual DbSet<Graphician> Grafik { get; set; }
        public virtual DbSet<ProgrammingLanguage> JezykProgramowania { get; set; }
        public virtual DbSet<Client> Klient { get; set; }
        public virtual DbSet<ClientCompany> KlientFirma { get; set; }
        public virtual DbSet<Person> Osoba { get; set; }
        public virtual DbSet<Package> Pakiet { get; set; }
        public virtual DbSet<PackageService> PakietUsluga { get; set; }
        public virtual DbSet<Positioner> Pozycjoner { get; set; }
        public virtual DbSet<Employee> Pracownik { get; set; }
        public virtual DbSet<EmployeeClient> PracownikKlient { get; set; }
        public virtual DbSet<EmployeeContract> PracownikUmowa { get; set; }
        public virtual DbSet<EmployeeTeam> PracownikZespol { get; set; }
        public virtual DbSet<Programmer> Programista { get; set; }
        public virtual DbSet<ProgrammerLanguage> ProgramistaJezyk { get; set; }
        public virtual DbSet<Project> Projekt { get; set; }
        public virtual DbSet<ProjectPackage> ProjektPakiet { get; set; }
        public virtual DbSet<Boss> Szef { get; set; }
        public virtual DbSet<Tester> Tester { get; set; }
        public virtual DbSet<Contract> Umowa { get; set; }
        public virtual DbSet<Service> Usluga { get; set; }
        public virtual DbSet<ServiceProject> UslugaProjekt { get; set; }
        public virtual DbSet<Team> Zespol { get; set; }
        public virtual DbSet<TeamProject> ZespolProjekt { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Interactive_Agency;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.IdFirma)
                    .HasName("Firma_pk");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Graphician>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Grafik_pk");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.Property(e => e.Specjalizacja)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Grafik)
                    .HasForeignKey<Graphician>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_16_Pracownik");
            });

            modelBuilder.Entity<ProgrammingLanguage>(entity =>
            {
                entity.HasKey(e => e.IdJezyk)
                    .HasName("JezykProgramowania_pk");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdKlient)
                    .HasName("Klient_pk");

                entity.Property(e => e.IdKlient).ValueGeneratedNever();

                entity.Property(e => e.Priorytet)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdKlientNavigation)
                    .WithOne(p => p.Klient)
                    .HasForeignKey<Client>(d => d.IdKlient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Klient_Osoba");
            });

            modelBuilder.Entity<ClientCompany>(entity =>
            {
                entity.HasKey(e => new { e.IdKlient, e.IdFirma })
                    .HasName("KlientFirma_pk");

                entity.HasOne(d => d.IdFirmaNavigation)
                    .WithMany(p => p.KlientFirma)
                    .HasForeignKey(d => d.IdFirma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("KlientFirma_Firma");

                entity.HasOne(d => d.IdKlientNavigation)
                    .WithMany(p => p.KlientFirma)
                    .HasForeignKey(d => d.IdKlient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("KlientFirma_Klient");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdOsoba)
                    .HasName("Osoba_pk");

                entity.Property(e => e.AdresEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Haslo).IsRequired();

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NumerTelefonuPrywatny)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NumerTelefonuSluzbowego)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Rola)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.IdPakiet)
                    .HasName("Pakiet_pk");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RodzajOplaty)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PackageService>(entity =>
            {
                entity.HasKey(e => new { e.IdPakiet, e.IdUsluga })
                    .HasName("PakietUsluga_pk");

                entity.HasOne(d => d.IdPakietNavigation)
                    .WithMany(p => p.PakietUsluga)
                    .HasForeignKey(d => d.IdPakiet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PakietUsluga_Pakiet");

                entity.HasOne(d => d.IdUslugaNavigation)
                    .WithMany(p => p.PakietUsluga)
                    .HasForeignKey(d => d.IdUsluga)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PakietUsluga_Usluga");
            });

            modelBuilder.Entity<Positioner>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Pozycjoner_pk");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.Property(e => e.PelnionaFunkcja)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Pozycjoner)
                    .HasForeignKey<Positioner>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_15_Pracownik");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Pracownik_pk");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.Property(e => e.AdresZamieszkania)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Pracownik)
                    .HasForeignKey<Employee>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pracownik_Osoba");
            });

            modelBuilder.Entity<EmployeeClient>(entity =>
            {
                entity.HasKey(e => new { e.IdKlient, e.IdPracownik, e.DataRozpoczeciaSpotkania })
                    .HasName("PracownikKlient_pk");

                entity.Property(e => e.DataRozpoczeciaSpotkania).HasColumnType("datetime");

                entity.Property(e => e.DataZakonczeniaSpotkania).HasColumnType("datetime");

                entity.Property(e => e.MiejsceSpotkania)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdKlientNavigation)
                    .WithMany(p => p.PracownikKlient)
                    .HasForeignKey(d => d.IdKlient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikKlient_Klient");

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithMany(p => p.PracownikKlient)
                    .HasForeignKey(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikKlient_Pracownik");
            });

            modelBuilder.Entity<EmployeeContract>(entity =>
            {
                entity.HasKey(e => new { e.IdPracownik, e.IdUmowa, e.DataPodpisaniaUmowy })
                    .HasName("PracownikUmowa_pk");

                entity.Property(e => e.DataPodpisaniaUmowy).HasColumnType("datetime");

                entity.Property(e => e.DataZakonczeniaUmowy).HasColumnType("datetime");

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithMany(p => p.PracownikUmowa)
                    .HasForeignKey(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikUmowa_Pracownik");

                entity.HasOne(d => d.IdUmowaNavigation)
                    .WithMany(p => p.PracownikUmowa)
                    .HasForeignKey(d => d.IdUmowa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikUmowa_Umowa");
            });

            modelBuilder.Entity<EmployeeTeam>(entity =>
            {
                entity.HasKey(e => new { e.IdPracownik, e.IdZespol, e.DataPrzypisaniaPracownika })
                    .HasName("PracownikZespol_pk");

                entity.Property(e => e.DataPrzypisaniaPracownika).HasColumnType("datetime");

                entity.Property(e => e.DataWypisaniaPracownika).HasColumnType("datetime");

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithMany(p => p.PracownikZespol)
                    .HasForeignKey(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_22_Pracownik");

                entity.HasOne(d => d.IdZespolNavigation)
                    .WithMany(p => p.PracownikZespol)
                    .HasForeignKey(d => d.IdZespol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_22_Zespol");
            });

            modelBuilder.Entity<Programmer>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Programista_pk");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.Property(e => e.PoziomZaawansowania)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Programista)
                    .HasForeignKey<Programmer>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_17_Pracownik");
            });

            modelBuilder.Entity<ProgrammerLanguage>(entity =>
            {
                entity.HasKey(e => new { e.IdPracownik, e.IdJezyk })
                    .HasName("ProgramistaJezyk_pk");

                entity.HasOne(d => d.IdJezykNavigation)
                    .WithMany(p => p.ProgramistaJezyk)
                    .HasForeignKey(d => d.IdJezyk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_18_JezykProgramowania");

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithMany(p => p.ProgramistaJezyk)
                    .HasForeignKey(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_18_Programista");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdProjekt)
                    .HasName("Projekt_pk");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdFirmaNavigation)
                    .WithMany(p => p.Projekt)
                    .HasForeignKey(d => d.IdFirma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Projekt_Firma");
            });

            modelBuilder.Entity<ProjectPackage>(entity =>
            {
                entity.HasKey(e => new { e.IdPakiet, e.IdProjekt, e.DataRozpoczeciaWspolpracy })
                    .HasName("ProjektPakiet_pk");

                entity.Property(e => e.DataRozpoczeciaWspolpracy).HasColumnType("datetime");

                entity.Property(e => e.DataZakonczeniaWspolpracy).HasColumnType("datetime");

                entity.HasOne(d => d.IdPakietNavigation)
                    .WithMany(p => p.ProjektPakiet)
                    .HasForeignKey(d => d.IdPakiet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProjektPakiet_Pakiet");

                entity.HasOne(d => d.IdProjektNavigation)
                    .WithMany(p => p.ProjektPakiet)
                    .HasForeignKey(d => d.IdProjekt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProjektPakiet_Projekt");
            });

            modelBuilder.Entity<Boss>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Szef_pk");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Szef)
                    .HasForeignKey<Boss>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pracownik_Szef");
            });

            modelBuilder.Entity<Tester>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Tester_pk");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Tester)
                    .HasForeignKey<Tester>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_14_Pracownik");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.IdUmowa)
                    .HasName("Umowa_pk");

                entity.Property(e => e.RodzajUmowy)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.IdUsluga)
                    .HasName("Usluga_pk");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Klasyfikacja)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ServiceProject>(entity =>
            {
                entity.HasKey(e => new { e.IdProjekt, e.IdUsluga, e.DataPrzypisaniaZadania })
                    .HasName("ZadanieProjekt_pk");

                entity.Property(e => e.DataPrzypisaniaZadania).HasColumnType("datetime");

                entity.Property(e => e.DataZakonczeniaZadania).HasColumnType("datetime");

                entity.Property(e => e.Opis).IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.IdProjektNavigation)
                    .WithMany(p => p.UslugaProjekt)
                    .HasForeignKey(d => d.IdProjekt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_26_Projekt");

                entity.HasOne(d => d.IdUslugaNavigation)
                    .WithMany(p => p.UslugaProjekt)
                    .HasForeignKey(d => d.IdUsluga)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_26_Zadanie");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.IdZespol)
                    .HasName("Zespol_pk");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TeamProject>(entity =>
            {
                entity.HasKey(e => new { e.IdZespol, e.IdProjekt, e.DataPrzypisaniaZespolu })
                    .HasName("ZespolProjekt_pk");

                entity.Property(e => e.DataPrzypisaniaZespolu).HasColumnType("datetime");

                entity.Property(e => e.DataWypisaniaZespolu).HasColumnType("datetime");

                entity.HasOne(d => d.IdProjektNavigation)
                    .WithMany(p => p.ZespolProjekt)
                    .HasForeignKey(d => d.IdProjekt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_24_Projekt");

                entity.HasOne(d => d.IdZespolNavigation)
                    .WithMany(p => p.ZespolProjekt)
                    .HasForeignKey(d => d.IdZespol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_24_Zespol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}