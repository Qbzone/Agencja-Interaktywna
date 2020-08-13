using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AgencjaInteraktywna.Models
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
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

                entity.Property(e => e.Idfirma).ValueGeneratedNever();
            });

            modelBuilder.Entity<Firmatag>(entity =>
            {
                entity.HasKey(e => new { e.Idfirma, e.Idtag })
                    .HasName("firmatagpk");

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

                entity.Property(e => e.Idpracownik).ValueGeneratedNever();

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

                entity.Property(e => e.Idjezyk).ValueGeneratedNever();
            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasKey(e => e.Idklient)
                    .HasName("klientpk");

                entity.Property(e => e.Idklient).ValueGeneratedNever();

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

                entity.Property(e => e.Idosoba).ValueGeneratedNever();

                entity.Property(e => e.NumerTelefonuPrywatny)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NumerTelefonuSłużbowego)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Pakiet>(entity =>
            {
                entity.HasKey(e => e.Idpakiet)
                    .HasName("pakietpk");

                entity.Property(e => e.Idpakiet).ValueGeneratedNever();
            });

            modelBuilder.Entity<Pakietusluga>(entity =>
            {
                entity.HasKey(e => new { e.Idpakiet, e.Idusluga })
                    .HasName("pakietuslugapk");

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

                entity.Property(e => e.Idpracownik).ValueGeneratedNever();

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

                entity.Property(e => e.Idpracownik).ValueGeneratedNever();

                entity.Property(e => e.Pesel)
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

                entity.Property(e => e.Idpracownik).ValueGeneratedNever();

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

                entity.Property(e => e.Idprojekt).ValueGeneratedNever();

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

                entity.Property(e => e.Idpracownik).ValueGeneratedNever();

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

                entity.Property(e => e.Idtag).ValueGeneratedNever();
            });

            modelBuilder.Entity<Tester>(entity =>
            {
                entity.HasKey(e => e.Idpracownik)
                    .HasName("testerpk");

                entity.Property(e => e.Idpracownik).ValueGeneratedNever();

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

                entity.Property(e => e.Idumowa).ValueGeneratedNever();
            });

            modelBuilder.Entity<Usluga>(entity =>
            {
                entity.HasKey(e => e.Idusluga)
                    .HasName("uslugapk");

                entity.Property(e => e.Idusluga).ValueGeneratedNever();
            });

            modelBuilder.Entity<Zadanie>(entity =>
            {
                entity.HasKey(e => e.Idzadanie)
                    .HasName("zadaniepk");

                entity.Property(e => e.Idzadanie).ValueGeneratedNever();
            });

            modelBuilder.Entity<Zadanieprojekt>(entity =>
            {
                entity.HasKey(e => new { e.Idprojekt, e.Idzadanie, e.Datarozpoczeciazadania })
                    .HasName("zadanieprojektpk");

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

                entity.Property(e => e.Idzespol).ValueGeneratedNever();
            });

            modelBuilder.Entity<Zespolprojekt>(entity =>
            {
                entity.HasKey(e => new { e.Dataprzypisaniazespolu, e.Idprojekt, e.Idzespol })
                    .HasName("zespolprojektpk");

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
