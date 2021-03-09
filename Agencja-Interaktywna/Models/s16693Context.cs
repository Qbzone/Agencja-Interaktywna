using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

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

        public virtual DbSet<Firma> Firmas { get; set; }
        public virtual DbSet<FirmaTag> FirmaTags { get; set; }
        public virtual DbSet<Grafik> Grafiks { get; set; }
        public virtual DbSet<JezykProgramowania> JezykProgramowania { get; set; }
        public virtual DbSet<Klient> Klients { get; set; }
        public virtual DbSet<KlientFirma> KlientFirmas { get; set; }
        public virtual DbSet<KlientPakiet> KlientPakiets { get; set; }
        public virtual DbSet<Osoba> Osobas { get; set; }
        public virtual DbSet<Pakiet> Pakiets { get; set; }
        public virtual DbSet<PakietUsluga> PakietUslugas { get; set; }
        public virtual DbSet<Pozycjoner> Pozycjoners { get; set; }
        public virtual DbSet<Pracownik> Pracowniks { get; set; }
        public virtual DbSet<PracownikKlient> PracownikKlients { get; set; }
        public virtual DbSet<PracownikUmowa> PracownikUmowas { get; set; }
        public virtual DbSet<PracownikZespol> PracownikZespols { get; set; }
        public virtual DbSet<ProgramistaJezyk> ProgramistaJezyks { get; set; }
        public virtual DbSet<Programista> Programista { get; set; }
        public virtual DbSet<Projekt> Projekts { get; set; }
        public virtual DbSet<Szef> Szefs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Tester> Testers { get; set; }
        public virtual DbSet<Umowa> Umowas { get; set; }
        public virtual DbSet<Usluga> Uslugas { get; set; }
        public virtual DbSet<Zadanie> Zadanies { get; set; }
        public virtual DbSet<ZadanieProjekt> ZadanieProjekts { get; set; }
        public virtual DbSet<Zespol> Zespols { get; set; }
        public virtual DbSet<ZespolProjekt> ZespolProjekts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s17379;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Firma>(entity =>
            {
                entity.HasKey(e => e.IdFirma)
                    .HasName("Firma_pk");

                entity.ToTable("Firma");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FirmaTag>(entity =>
            {
                entity.HasKey(e => new { e.IdFirma, e.IdTag })
                    .HasName("FirmaTag_pk");

                entity.ToTable("FirmaTag");

                entity.HasOne(d => d.IdFirmaNavigation)
                    .WithMany(p => p.FirmaTags)
                    .HasForeignKey(d => d.IdFirma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FirmaTag_Firma");

                entity.HasOne(d => d.IdTagNavigation)
                    .WithMany(p => p.FirmaTags)
                    .HasForeignKey(d => d.IdTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FirmaTag_Tag");
            });

            modelBuilder.Entity<Grafik>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Grafik_pk");

                entity.ToTable("Grafik");

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

                entity.ToTable("Klient");

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

                entity.ToTable("KlientFirma");

                entity.HasOne(d => d.IdFirmaNavigation)
                    .WithMany(p => p.KlientFirmas)
                    .HasForeignKey(d => d.IdFirma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("KlientFirma_Firma");

                entity.HasOne(d => d.IdKlientNavigation)
                    .WithMany(p => p.KlientFirmas)
                    .HasForeignKey(d => d.IdKlient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("KlientFirma_Klient");
            });

            modelBuilder.Entity<KlientPakiet>(entity =>
            {
                entity.HasKey(e => new { e.IdKlient, e.IdPakiet, e.DataRozpoczeciaWspolpracy })
                    .HasName("KlientPakiet_pk");

                entity.ToTable("KlientPakiet");

                entity.Property(e => e.DataRozpoczeciaWspolpracy).HasColumnType("date");

                entity.Property(e => e.DataZakonczeniaWspolpracy).HasColumnType("date");

                entity.HasOne(d => d.IdKlientNavigation)
                    .WithMany(p => p.KlientPakiets)
                    .HasForeignKey(d => d.IdKlient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("KlientPakiet_Klient");

                entity.HasOne(d => d.IdPakietNavigation)
                    .WithMany(p => p.KlientPakiets)
                    .HasForeignKey(d => d.IdPakiet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("KlientPakiet_Pakiet");
            });

            modelBuilder.Entity<Osoba>(entity =>
            {
                entity.HasKey(e => e.IdOsoba)
                    .HasName("Osoba_pk");

                entity.ToTable("Osoba");

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
                    .IsFixedLength(true);

                entity.Property(e => e.NumerTelefonuSluzbowego)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Rola)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Pakiet>(entity =>
            {
                entity.HasKey(e => e.IdPakiet)
                    .HasName("Pakiet_pk");

                entity.ToTable("Pakiet");

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

                entity.ToTable("PakietUsluga");

                entity.HasOne(d => d.IdPakietNavigation)
                    .WithMany(p => p.PakietUslugas)
                    .HasForeignKey(d => d.IdPakiet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PakietUsluga_Pakiet");

                entity.HasOne(d => d.IdUslugaNavigation)
                    .WithMany(p => p.PakietUslugas)
                    .HasForeignKey(d => d.IdUsluga)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PakietUsluga_Usluga");
            });

            modelBuilder.Entity<Pozycjoner>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Pozycjoner_pk");

                entity.ToTable("Pozycjoner");

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

                entity.ToTable("Pracownik");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.Property(e => e.AdresZamieszkania)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Pracownik)
                    .HasForeignKey<Pracownik>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pracownik_Osoba");
            });

            modelBuilder.Entity<PracownikKlient>(entity =>
            {
                entity.HasKey(e => new { e.IdKlient, e.IdPracownik })
                    .HasName("PracownikKlient_pk");

                entity.ToTable("PracownikKlient");

                entity.Property(e => e.DataRozpoczeciaSpotkania).HasColumnType("date");

                entity.Property(e => e.DataZakonczeniaSpotkania).HasColumnType("date");

                entity.Property(e => e.MiejsceSpotkania)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdKlientNavigation)
                    .WithMany(p => p.PracownikKlients)
                    .HasForeignKey(d => d.IdKlient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikKlient_Klient");

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithMany(p => p.PracownikKlients)
                    .HasForeignKey(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikKlient_Pracownik");
            });

            modelBuilder.Entity<PracownikUmowa>(entity =>
            {
                entity.HasKey(e => new { e.IdPracownik, e.IdUmowa, e.DataPodpisaniaUmowy })
                    .HasName("PracownikUmowa_pk");

                entity.ToTable("PracownikUmowa");

                entity.Property(e => e.DataPodpisaniaUmowy).HasColumnType("date");

                entity.Property(e => e.DataZakonczeniaUmowy).HasColumnType("date");

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithMany(p => p.PracownikUmowas)
                    .HasForeignKey(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikUmowa_Pracownik");

                entity.HasOne(d => d.IdUmowaNavigation)
                    .WithMany(p => p.PracownikUmowas)
                    .HasForeignKey(d => d.IdUmowa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikUmowa_Umowa");
            });

            modelBuilder.Entity<PracownikZespol>(entity =>
            {
                entity.HasKey(e => new { e.IdPracownik, e.IdZespol, e.DataPrzypisaniaPracownika })
                    .HasName("PracownikZespol_pk");

                entity.ToTable("PracownikZespol");

                entity.Property(e => e.DataPrzypisaniaPracownika).HasColumnType("date");

                entity.Property(e => e.DataWypisaniaPracownika).HasColumnType("date");

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithMany(p => p.PracownikZespols)
                    .HasForeignKey(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_22_Pracownik");

                entity.HasOne(d => d.IdZespolNavigation)
                    .WithMany(p => p.PracownikZespols)
                    .HasForeignKey(d => d.IdZespol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_22_Zespol");
            });

            modelBuilder.Entity<ProgramistaJezyk>(entity =>
            {
                entity.HasKey(e => new { e.IdPracownik, e.IdJezyk })
                    .HasName("ProgramistaJezyk_pk");

                entity.ToTable("ProgramistaJezyk");

                entity.HasOne(d => d.IdJezykNavigation)
                    .WithMany(p => p.ProgramistaJezyks)
                    .HasForeignKey(d => d.IdJezyk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_18_JezykProgramowania");

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithMany(p => p.ProgramistaJezyks)
                    .HasForeignKey(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_18_Programista");
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
                    .WithOne(p => p.Programistum)
                    .HasForeignKey<Programista>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_17_Pracownik");
            });

            modelBuilder.Entity<Projekt>(entity =>
            {
                entity.HasKey(e => e.IdProjekt)
                    .HasName("Projekt_pk");

                entity.ToTable("Projekt");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdFirmaNavigation)
                    .WithMany(p => p.Projekts)
                    .HasForeignKey(d => d.IdFirma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Projekt_Firma");
            });

            modelBuilder.Entity<Szef>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Szef_pk");

                entity.ToTable("Szef");

                entity.Property(e => e.IdPracownik).ValueGeneratedNever();

                entity.HasOne(d => d.IdPracownikNavigation)
                    .WithOne(p => p.Szef)
                    .HasForeignKey<Szef>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pracownik_Szef");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.IdTag)
                    .HasName("Tag_pk");

                entity.ToTable("Tag");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tester>(entity =>
            {
                entity.HasKey(e => e.IdPracownik)
                    .HasName("Tester_pk");

                entity.ToTable("Tester");

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

                entity.ToTable("Umowa");

                entity.Property(e => e.RodzajUmowy)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Usluga>(entity =>
            {
                entity.HasKey(e => e.IdUsluga)
                    .HasName("Usluga_pk");

                entity.ToTable("Usluga");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Zadanie>(entity =>
            {
                entity.HasKey(e => e.IdZadanie)
                    .HasName("Zadanie_pk");

                entity.ToTable("Zadanie");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ZadanieProjekt>(entity =>
            {
                entity.HasKey(e => new { e.IdProjekt, e.DZadanie, e.DataPrzypisaniaZadania })
                    .HasName("ZadanieProjekt_pk");

                entity.ToTable("ZadanieProjekt");

                entity.Property(e => e.DZadanie).HasColumnName("dZadanie");

                entity.Property(e => e.DataPrzypisaniaZadania).HasColumnType("date");

                entity.Property(e => e.DataZakonczeniaZadania).HasColumnType("date");

                entity.Property(e => e.Opis).IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.DZadanieNavigation)
                    .WithMany(p => p.ZadanieProjekts)
                    .HasForeignKey(d => d.DZadanie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_26_Zadanie");

                entity.HasOne(d => d.IdProjektNavigation)
                    .WithMany(p => p.ZadanieProjekts)
                    .HasForeignKey(d => d.IdProjekt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_26_Projekt");
            });

            modelBuilder.Entity<Zespol>(entity =>
            {
                entity.HasKey(e => e.IdZespol)
                    .HasName("Zespol_pk");

                entity.ToTable("Zespol");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ZespolProjekt>(entity =>
            {
                entity.HasKey(e => new { e.IdZespol, e.IdProjekt, e.DataPrzypisaniaZespolu })
                    .HasName("ZespolProjekt_pk");

                entity.ToTable("ZespolProjekt");

                entity.Property(e => e.DataPrzypisaniaZespolu).HasColumnType("date");

                entity.Property(e => e.DataWypisaniaZespolu).HasColumnType("date");

                entity.HasOne(d => d.IdProjektNavigation)
                    .WithMany(p => p.ZespolProjekts)
                    .HasForeignKey(d => d.IdProjekt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_24_Projekt");

                entity.HasOne(d => d.IdZespolNavigation)
                    .WithMany(p => p.ZespolProjekts)
                    .HasForeignKey(d => d.IdZespol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_24_Zespol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        internal Task<Osoba> GetUserAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
