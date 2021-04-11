using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agencja_Interaktywna.Models
{
    public partial class s16693Context : DbContext
    {
        public s16693Context()
        {
        }

        public s16693Context(DbContextOptions<s16693Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Firma> Firma { get; set; }
        public virtual DbSet<Grafik> Grafik { get; set; }
        public virtual DbSet<JezykProgramowania> JezykProgramowania { get; set; }
        public virtual DbSet<Klient> Klient { get; set; }
        public virtual DbSet<KlientFirma> KlientFirma { get; set; }
        public virtual DbSet<Osoba> Osoba { get; set; }
        public virtual DbSet<Pakiet> Pakiet { get; set; }
        public virtual DbSet<PakietUsluga> PakietUsluga { get; set; }
        public virtual DbSet<Pozycjoner> Pozycjoner { get; set; }
        public virtual DbSet<Pracownik> Pracownik { get; set; }
        public virtual DbSet<PracownikKlient> PracownikKlient { get; set; }
        public virtual DbSet<PracownikUmowa> PracownikUmowa { get; set; }
        public virtual DbSet<PracownikZespol> PracownikZespol { get; set; }
        public virtual DbSet<Programista> Programista { get; set; }
        public virtual DbSet<ProgramistaJezyk> ProgramistaJezyk { get; set; }
        public virtual DbSet<Projekt> Projekt { get; set; }
        public virtual DbSet<ProjektPakiet> ProjektPakiet { get; set; }
        public virtual DbSet<Szef> Szef { get; set; }
        public virtual DbSet<Tester> Tester { get; set; }
        public virtual DbSet<Umowa> Umowa { get; set; }
        public virtual DbSet<Usluga> Usluga { get; set; }
        public virtual DbSet<UslugaProjekt> UslugaProjekt { get; set; }
        public virtual DbSet<Zespol> Zespol { get; set; }
        public virtual DbSet<ZespolProjekt> ZespolProjekt { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s17379;Integrated Security=True;");
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s16693;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Firma>(entity =>
            {
                entity.HasKey(e => e.IdFirma)
                    .HasName("Firma_pk");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Grafik>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Grafik_pk");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.Property(e => e.Specjalizacja)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Grafik)
                    .HasForeignKey<Grafik>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_16_Pracownik");
            });

            modelBuilder.Entity<JezykProgramowania>(entity =>
            {
                entity.HasKey(e => e.IdJezyk)
                    .HasName("JezykProgramowania_pk");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasKey(e => e.IdKlient)
                    .HasName("Klient_pk");

                entity.Property(e => e.IdKlient).ValueGeneratedNever();

                entity.Property(e => e.Priorytet)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdKlientNavigation)
                    .WithOne(p => p.Klient)
                    .HasForeignKey<Klient>(d => d.IdKlient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Klient_Osoba");
            });

            modelBuilder.Entity<KlientFirma>(entity =>
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

            modelBuilder.Entity<Osoba>(entity =>
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

            modelBuilder.Entity<Pakiet>(entity =>
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

            modelBuilder.Entity<PakietUsluga>(entity =>
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

            modelBuilder.Entity<Pozycjoner>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Pozycjoner_pk");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.Property(e => e.PelnionaFunkcja)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Pozycjoner)
                    .HasForeignKey<Pozycjoner>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_15_Pracownik");
            });

            modelBuilder.Entity<Pracownik>(entity =>
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
                    .HasForeignKey<Pracownik>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pracownik_Osoba");
            });

            modelBuilder.Entity<PracownikKlient>(entity =>
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

            modelBuilder.Entity<PracownikUmowa>(entity =>
            {
                entity.HasKey(e => new { e.IdPracownik, e.IdUmowa, e.DataPodpisaniaUmowy })
                    .HasName("PracownikUmowa_pk");

                entity.Property(e => e.DataPodpisaniaUmowy).HasColumnType("date");

                entity.Property(e => e.DataZakonczeniaUmowy).HasColumnType("date");

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

            modelBuilder.Entity<PracownikZespol>(entity =>
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

            modelBuilder.Entity<Programista>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Programista_pk");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.Property(e => e.PoziomZaawansowania)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Programista)
                    .HasForeignKey<Programista>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_17_Pracownik");
            });

            modelBuilder.Entity<ProgramistaJezyk>(entity =>
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

            modelBuilder.Entity<Projekt>(entity =>
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

            modelBuilder.Entity<ProjektPakiet>(entity =>
            {
                entity.HasKey(e => new { e.IdPakiet, e.IdProjekt, e.DataRozpoczeciaWspolpracy })
                    .HasName("ProjektPakiet_pk");

                entity.Property(e => e.DataRozpoczeciaWspolpracy).HasColumnType("date");

                entity.Property(e => e.DataZakonczeniaWspolpracy).HasColumnType("date");

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

            modelBuilder.Entity<Szef>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Szef_pk");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Szef)
                    .HasForeignKey<Szef>(d => d.IdPracownik)
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

            modelBuilder.Entity<Umowa>(entity =>
            {
                entity.HasKey(e => e.IdUmowa)
                    .HasName("Umowa_pk");

                entity.Property(e => e.RodzajUmowy)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Usluga>(entity =>
            {
                entity.HasKey(e => e.IdUsluga)
                    .HasName("Usluga_pk");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UslugaProjekt>(entity =>
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

            modelBuilder.Entity<Zespol>(entity =>
            {
                entity.HasKey(e => e.IdZespol)
                    .HasName("Zespol_pk");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ZespolProjekt>(entity =>
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
