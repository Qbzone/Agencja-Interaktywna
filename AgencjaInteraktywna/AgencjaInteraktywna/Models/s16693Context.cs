using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AgencjaInteraktywna
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
        public virtual DbSet<Firmatag> Firmatag { get; set; }
        public virtual DbSet<Grafik> Grafik { get; set; }
        public virtual DbSet<Jezykprogramowania> Jezykprogramowania { get; set; }
        public virtual DbSet<Klient> Klient { get; set; }
        public virtual DbSet<Klientfirma> Klientfirma { get; set; }
        public virtual DbSet<Klientpakiet> Klientpakiet { get; set; }
        public virtual DbSet<Osoba> Osoba { get; set; }
        public virtual DbSet<Pakiet> Pakiet { get; set; }
        public virtual DbSet<Pakietusluga> Pakietusluga { get; set; }
        public virtual DbSet<Pozycjoner> Pozycjoner { get; set; }
        public virtual DbSet<Pracownik> Pracownik { get; set; }
        public virtual DbSet<Pracownikklient> Pracownikklient { get; set; }
        public virtual DbSet<Pracownikumowa> Pracownikumowa { get; set; }
        public virtual DbSet<Pracownikzespol> Pracownikzespol { get; set; }
        public virtual DbSet<Programista> Programista { get; set; }
        public virtual DbSet<Programistajezyk> Programistajezyk { get; set; }
        public virtual DbSet<Projekt> Projekt { get; set; }
        public virtual DbSet<Szef> Szef { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Tester> Tester { get; set; }
        public virtual DbSet<Umowa> Umowa { get; set; }
        public virtual DbSet<Usluga> Usluga { get; set; }
        public virtual DbSet<Zadanie> Zadanie { get; set; }
        public virtual DbSet<Zadanieprojekt> Zadanieprojekt { get; set; }
        public virtual DbSet<Zespol> Zespol { get; set; }
        public virtual DbSet<Zespolprojekt> Zespolprojekt { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s16693;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Firma>(entity =>
            {
                entity.HasKey(e => e.Idfirma)
                    .HasName("firmapk");

                entity.ToTable("firma");

                entity.Property(e => e.Idfirma)
                    .HasColumnName("idfirma")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Firmatag>(entity =>
            {
                entity.HasKey(e => new { e.Idfirma, e.Idtag })
                    .HasName("firmatagpk");

                entity.ToTable("firmatag");

                entity.Property(e => e.Idfirma).HasColumnName("idfirma");

                entity.Property(e => e.Idtag).HasColumnName("idtag");

                entity.HasOne(d => d.IdfirmaNavigation)
                    .WithMany(p => p.Firmatag)
                    .HasForeignKey(d => d.Idfirma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("firmatagfirmafk");

                entity.HasOne(d => d.IdtagNavigation)
                    .WithMany(p => p.Firmatag)
                    .HasForeignKey(d => d.Idtag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("firmatagtagfk");
            });

            modelBuilder.Entity<Grafik>(entity =>
            {
                entity.HasKey(e => e.Idpracownik)
                    .HasName("grafikpk");

                entity.ToTable("grafik");

                entity.Property(e => e.Idpracownik)
                    .HasColumnName("idpracownik")
                    .ValueGeneratedNever();

                entity.Property(e => e.Specjalizacja)
                    .IsRequired()
                    .HasColumnName("specjalizacja")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdpracownikNavigation)
                    .WithOne(p => p.Grafik)
                    .HasForeignKey<Grafik>(d => d.Idpracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grafikpracownikfk");
            });

            modelBuilder.Entity<Jezykprogramowania>(entity =>
            {
                entity.HasKey(e => e.Idjezyk)
                    .HasName("jezykprogramowaniapk");

                entity.ToTable("jezykprogramowania");

                entity.Property(e => e.Idjezyk)
                    .HasColumnName("idjezyk")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasKey(e => e.Idklient)
                    .HasName("klientpk");

                entity.ToTable("klient");

                entity.Property(e => e.Idklient)
                    .HasColumnName("idklient")
                    .ValueGeneratedNever();

                entity.Property(e => e.Priorytet)
                    .IsRequired()
                    .HasColumnName("priorytet")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdklientNavigation)
                    .WithOne(p => p.Klient)
                    .HasForeignKey<Klient>(d => d.Idklient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("klientosobafk");
            });

            modelBuilder.Entity<Klientfirma>(entity =>
            {
                entity.HasKey(e => new { e.Idklient, e.Idfirma })
                    .HasName("klientfirmapk");

                entity.ToTable("klientfirma");

                entity.Property(e => e.Idklient).HasColumnName("idklient");

                entity.Property(e => e.Idfirma).HasColumnName("idfirma");

                entity.HasOne(d => d.IdfirmaNavigation)
                    .WithMany(p => p.Klientfirma)
                    .HasForeignKey(d => d.Idfirma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("klientfirmafirmafk");

                entity.HasOne(d => d.IdklientNavigation)
                    .WithMany(p => p.Klientfirma)
                    .HasForeignKey(d => d.Idklient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("klientfirmaklientfk");
            });

            modelBuilder.Entity<Klientpakiet>(entity =>
            {
                entity.HasKey(e => new { e.Datarozpoczeciawspolpracy, e.Idklient, e.Idpakiet })
                    .HasName("klientpakietpk");

                entity.ToTable("klientpakiet");

                entity.Property(e => e.Datarozpoczeciawspolpracy)
                    .HasColumnName("datarozpoczeciawspolpracy")
                    .HasColumnType("date");

                entity.Property(e => e.Idklient).HasColumnName("idklient");

                entity.Property(e => e.Idpakiet).HasColumnName("idpakiet");

                entity.Property(e => e.Datazakonczeniawspolpracy)
                    .HasColumnName("datazakonczeniawspolpracy")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdklientNavigation)
                    .WithMany(p => p.Klientpakiet)
                    .HasForeignKey(d => d.Idklient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("klientpakietklientfk");

                entity.HasOne(d => d.IdpakietNavigation)
                    .WithMany(p => p.Klientpakiet)
                    .HasForeignKey(d => d.Idpakiet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("klientpakietpakietfk");
            });

            modelBuilder.Entity<Osoba>(entity =>
            {
                entity.HasKey(e => e.Idosoba)
                    .HasName("osobapk");

                entity.ToTable("osoba");

                entity.Property(e => e.Idosoba)
                    .HasColumnName("idosoba")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdresEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasColumnName("imie")
                    .HasMaxLength(25);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NumerTelefonuPrywatny)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NumerTelefonuSłużbowego)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Pakiet>(entity =>
            {
                entity.HasKey(e => e.Idpakiet)
                    .HasName("pakietpk");

                entity.ToTable("pakiet");

                entity.Property(e => e.Idpakiet)
                    .HasColumnName("idpakiet")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(50);

                entity.Property(e => e.RodzajOplaty)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Pakietusluga>(entity =>
            {
                entity.HasKey(e => new { e.Idpakiet, e.Idusluga })
                    .HasName("pakietuslugapk");

                entity.ToTable("pakietusluga");

                entity.Property(e => e.Idpakiet).HasColumnName("idpakiet");

                entity.Property(e => e.Idusluga).HasColumnName("idusluga");

                entity.HasOne(d => d.IdpakietNavigation)
                    .WithMany(p => p.Pakietusluga)
                    .HasForeignKey(d => d.Idpakiet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pakietuslugapakietfk");

                entity.HasOne(d => d.IduslugaNavigation)
                    .WithMany(p => p.Pakietusluga)
                    .HasForeignKey(d => d.Idusluga)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pakietuslugauslugafk");
            });

            modelBuilder.Entity<Pozycjoner>(entity =>
            {
                entity.HasKey(e => e.Idpracownik)
                    .HasName("pozycjonerpk");

                entity.ToTable("pozycjoner");

                entity.Property(e => e.Idpracownik)
                    .HasColumnName("idpracownik")
                    .ValueGeneratedNever();

                entity.Property(e => e.Pelnionafunkcja)
                    .IsRequired()
                    .HasColumnName("pelnionafunkcja")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdpracownikNavigation)
                    .WithOne(p => p.Pozycjoner)
                    .HasForeignKey<Pozycjoner>(d => d.Idpracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pozycjonerpracownikfk");
            });

            modelBuilder.Entity<Pracownik>(entity =>
            {
                entity.HasKey(e => e.Idpracownik)
                    .HasName("pracownikpk");

                entity.ToTable("pracownik");

                entity.Property(e => e.Idpracownik)
                    .HasColumnName("idpracownik")
                    .ValueGeneratedNever();

                entity.Property(e => e.Adreszamieszkania)
                    .IsRequired()
                    .HasColumnName("adreszamieszkania")
                    .HasMaxLength(100);

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasColumnName("PESEL")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdpracownikNavigation)
                    .WithOne(p => p.Pracownik)
                    .HasForeignKey<Pracownik>(d => d.Idpracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pracownikosobafk");
            });

            modelBuilder.Entity<Pracownikklient>(entity =>
            {
                entity.HasKey(e => new { e.Datarozpoczeciaspotkania, e.Idpracownik, e.Idklient })
                    .HasName("pracownikklientpk");

                entity.ToTable("pracownikklient");

                entity.Property(e => e.Datarozpoczeciaspotkania)
                    .HasColumnName("datarozpoczeciaspotkania")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idpracownik).HasColumnName("idpracownik");

                entity.Property(e => e.Idklient).HasColumnName("idklient");

                entity.Property(e => e.Datazakonczeniaspotkania)
                    .HasColumnName("datazakonczeniaspotkania")
                    .HasColumnType("datetime");

                entity.Property(e => e.Miejscespotkania)
                    .IsRequired()
                    .HasColumnName("miejscespotkania")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdklientNavigation)
                    .WithMany(p => p.Pracownikklient)
                    .HasForeignKey(d => d.Idklient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pracownikklientklientfk");

                entity.HasOne(d => d.IdpracownikNavigation)
                    .WithMany(p => p.Pracownikklient)
                    .HasForeignKey(d => d.Idpracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pracownikklientpracownikfk");
            });

            modelBuilder.Entity<Pracownikumowa>(entity =>
            {
                entity.HasKey(e => new { e.Datapodpisaniaumowy, e.Idpracownik, e.Idumowa })
                    .HasName("pracownikumowapk");

                entity.ToTable("pracownikumowa");

                entity.Property(e => e.Datapodpisaniaumowy)
                    .HasColumnName("datapodpisaniaumowy")
                    .HasColumnType("date");

                entity.Property(e => e.Idpracownik).HasColumnName("idpracownik");

                entity.Property(e => e.Idumowa).HasColumnName("idumowa");

                entity.Property(e => e.Datawygasnieciaumowy)
                    .HasColumnName("datawygasnieciaumowy")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdpracownikNavigation)
                    .WithMany(p => p.Pracownikumowa)
                    .HasForeignKey(d => d.Idpracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pracownikumowapracownikfk");

                entity.HasOne(d => d.IdumowaNavigation)
                    .WithMany(p => p.Pracownikumowa)
                    .HasForeignKey(d => d.Idumowa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pracownikumowaumowafk");
            });

            modelBuilder.Entity<Pracownikzespol>(entity =>
            {
                entity.HasKey(e => new { e.Dataprzypisaniapracownika, e.Idpracownik, e.Idzespol })
                    .HasName("pracownikzespolpk");

                entity.ToTable("pracownikzespol");

                entity.Property(e => e.Dataprzypisaniapracownika)
                    .HasColumnName("dataprzypisaniapracownika")
                    .HasColumnType("date");

                entity.Property(e => e.Idpracownik).HasColumnName("idpracownik");

                entity.Property(e => e.Idzespol).HasColumnName("idzespol");

                entity.Property(e => e.Datawypisaniapracownika)
                    .HasColumnName("datawypisaniapracownika")
                    .HasColumnType("date");

                entity.Property(e => e.Menadżer).HasColumnName("menadżer");

                entity.HasOne(d => d.IdpracownikNavigation)
                    .WithMany(p => p.Pracownikzespol)
                    .HasForeignKey(d => d.Idpracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pracownikzespolpracownikfk");

                entity.HasOne(d => d.IdzespolNavigation)
                    .WithMany(p => p.Pracownikzespol)
                    .HasForeignKey(d => d.Idzespol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pracownikzespolzespolfk");
            });

            modelBuilder.Entity<Programista>(entity =>
            {
                entity.HasKey(e => e.Idpracownik)
                    .HasName("programistapk");

                entity.ToTable("programista");

                entity.Property(e => e.Idpracownik)
                    .HasColumnName("idpracownik")
                    .ValueGeneratedNever();

                entity.Property(e => e.Poziomzaawansowania)
                    .IsRequired()
                    .HasColumnName("poziomzaawansowania")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdpracownikNavigation)
                    .WithOne(p => p.Programista)
                    .HasForeignKey<Programista>(d => d.Idpracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("programistapracownikfk");
            });

            modelBuilder.Entity<Programistajezyk>(entity =>
            {
                entity.HasKey(e => new { e.Idpracownik, e.Idjezyk })
                    .HasName("programistajezykpk");

                entity.ToTable("programistajezyk");

                entity.Property(e => e.Idpracownik).HasColumnName("idpracownik");

                entity.Property(e => e.Idjezyk).HasColumnName("idjezyk");

                entity.Property(e => e.Staz).HasColumnName("staz");

                entity.HasOne(d => d.IdjezykNavigation)
                    .WithMany(p => p.Programistajezyk)
                    .HasForeignKey(d => d.Idjezyk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("programistajezykjezykprogrfk");

                entity.HasOne(d => d.IdpracownikNavigation)
                    .WithMany(p => p.Programistajezyk)
                    .HasForeignKey(d => d.Idpracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("programistajezykprogramistafk");
            });

            modelBuilder.Entity<Projekt>(entity =>
            {
                entity.HasKey(e => e.Idprojekt)
                    .HasName("projektpk");

                entity.ToTable("projekt");

                entity.Property(e => e.Idprojekt)
                    .HasColumnName("idprojekt")
                    .ValueGeneratedNever();

                entity.Property(e => e.FirmaIdFirma).HasColumnName("Firma_IdFirma");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(50);

                entity.HasOne(d => d.FirmaIdFirmaNavigation)
                    .WithMany(p => p.Projekt)
                    .HasForeignKey(d => d.FirmaIdFirma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("projekt_firma_fk");
            });

            modelBuilder.Entity<Szef>(entity =>
            {
                entity.HasKey(e => e.Idpracownik)
                    .HasName("szefpk");

                entity.ToTable("szef");

                entity.Property(e => e.Idpracownik)
                    .HasColumnName("idpracownik")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.IdpracownikNavigation)
                    .WithOne(p => p.Szef)
                    .HasForeignKey<Szef>(d => d.Idpracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("szefpracownikfk");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Idtag)
                    .HasName("tagpk");

                entity.ToTable("tag");

                entity.Property(e => e.Idtag)
                    .HasColumnName("idtag")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tester>(entity =>
            {
                entity.HasKey(e => e.Idpracownik)
                    .HasName("testerpk");

                entity.ToTable("tester");

                entity.Property(e => e.Idpracownik)
                    .HasColumnName("idpracownik")
                    .ValueGeneratedNever();

                entity.Property(e => e.Testerdoswiadczenie).HasColumnName("testerdoswiadczenie");

                entity.HasOne(d => d.IdpracownikNavigation)
                    .WithOne(p => p.Tester)
                    .HasForeignKey<Tester>(d => d.Idpracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("testerpracownikfk");
            });

            modelBuilder.Entity<Umowa>(entity =>
            {
                entity.HasKey(e => e.Idumowa)
                    .HasName("umowapk");

                entity.ToTable("umowa");

                entity.Property(e => e.Idumowa)
                    .HasColumnName("idumowa")
                    .ValueGeneratedNever();

                entity.Property(e => e.Rodzajumowy)
                    .IsRequired()
                    .HasColumnName("rodzajumowy")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Usluga>(entity =>
            {
                entity.HasKey(e => e.Idusluga)
                    .HasName("uslugapk");

                entity.ToTable("usluga");

                entity.Property(e => e.Idusluga)
                    .HasColumnName("idusluga")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(50);

                entity.Property(e => e.Opis)
                    .HasColumnName("opis")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Zadanie>(entity =>
            {
                entity.HasKey(e => e.Idzadanie)
                    .HasName("zadaniepk");

                entity.ToTable("zadanie");

                entity.Property(e => e.Idzadanie)
                    .HasColumnName("idzadanie")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Zadanieprojekt>(entity =>
            {
                entity.HasKey(e => new { e.Idprojekt, e.Idzadanie, e.Datarozpoczeciazadania })
                    .HasName("zadanieprojektpk");

                entity.ToTable("zadanieprojekt");

                entity.Property(e => e.Idprojekt).HasColumnName("idprojekt");

                entity.Property(e => e.Idzadanie).HasColumnName("idzadanie");

                entity.Property(e => e.Datarozpoczeciazadania)
                    .HasColumnName("datarozpoczeciazadania")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datazakonczeniazadania)
                    .HasColumnName("datazakonczeniazadania")
                    .HasColumnType("datetime");

                entity.Property(e => e.Opis)
                    .HasColumnName("opis")
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdprojektNavigation)
                    .WithMany(p => p.Zadanieprojekt)
                    .HasForeignKey(d => d.Idprojekt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("zadanieprojektprojektfk");

                entity.HasOne(d => d.IdzadanieNavigation)
                    .WithMany(p => p.Zadanieprojekt)
                    .HasForeignKey(d => d.Idzadanie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("zadanieprojektzadaniefk");
            });

            modelBuilder.Entity<Zespol>(entity =>
            {
                entity.HasKey(e => e.Idzespol)
                    .HasName("zespolpk");

                entity.ToTable("zespol");

                entity.Property(e => e.Idzespol)
                    .HasColumnName("idzespol")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Zespolprojekt>(entity =>
            {
                entity.HasKey(e => new { e.Dataprzypisaniazespolu, e.Idprojekt, e.Idzespol })
                    .HasName("zespolprojektpk");

                entity.ToTable("zespolprojekt");

                entity.Property(e => e.Dataprzypisaniazespolu)
                    .HasColumnName("dataprzypisaniazespolu")
                    .HasColumnType("date");

                entity.Property(e => e.Idprojekt).HasColumnName("idprojekt");

                entity.Property(e => e.Idzespol).HasColumnName("idzespol");

                entity.Property(e => e.Dataoddaniaprojektu)
                    .HasColumnName("dataoddaniaprojektu")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdprojektNavigation)
                    .WithMany(p => p.Zespolprojekt)
                    .HasForeignKey(d => d.Idprojekt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("zespolprojektprojektfk");

                entity.HasOne(d => d.IdzespolNavigation)
                    .WithMany(p => p.Zespolprojekt)
                    .HasForeignKey(d => d.Idzespol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("zespolprojektzespolfk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
