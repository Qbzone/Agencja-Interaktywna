using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Interactive_Agency.Models
{
    public partial class InteractiveAgencyContext : DbContext
    {
        public InteractiveAgencyContext() { }

        public InteractiveAgencyContext(DbContextOptions<InteractiveAgencyContext> options) : base(options) { }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Graphician> Graphician { get; set; }
        public virtual DbSet<ProgrammingLanguage> ProgrammingLanguage { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientCompany> ClientCompany { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PackageService> PackageService { get; set; }
        public virtual DbSet<Positioner> Positioner { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeClient> EmployeeClient { get; set; }
        public virtual DbSet<EmployeeContract> EmployeeContract { get; set; }
        public virtual DbSet<EmployeeTeam> EmployeeTeam { get; set; }
        public virtual DbSet<Programmer> Programmer { get; set; }
        public virtual DbSet<ProgrammerLanguage> ProgrammerLanguage { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectPackage> ProjectPackage { get; set; }
        public virtual DbSet<Boss> Boss { get; set; }
        public virtual DbSet<Tester> Tester { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceProject> ServiceProject { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamProject> TeamProject { get; set; }

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
                entity.HasKey(e => e.CompanyId)
                    .HasName("Firma_pk");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Graphician>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("Grafik_pk");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.Specialization)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithOne(p => p.Graphician)
                    .HasForeignKey<Graphician>(d => d.EmployeeId)
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
                entity.HasKey(e => e.ClientId)
                    .HasName("Klient_pk");

                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.Property(e => e.Priority)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ClientIdNavigation)
                    .WithOne(p => p.Klient)
                    .HasForeignKey<Client>(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Klient_Osoba");
            });

            modelBuilder.Entity<ClientCompany>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.CompanyId })
                    .HasName("KlientFirma_pk");

                entity.HasOne(d => d.CompanyIdNavigation)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("KlientFirma_Firma");

                entity.HasOne(d => d.ClientIdNavigation)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.ClientId)
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
                entity.HasKey(e => e.PackageId)
                    .HasName("Pakiet_pk");

                entity.Property(e => e.PackageName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FeeType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PackageService>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.ServiceId })
                    .HasName("PakietUsluga_pk");

                entity.HasOne(d => d.PackageIdNavigation)
                    .WithMany(p => p.PackageService)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PakietUsluga_Pakiet");

                entity.HasOne(d => d.ServiceIdNavigation)
                    .WithMany(p => p.PakietUsluga)
                    .HasForeignKey(d => d.ServiceId)
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
                    .WithOne(p => p.Positioner)
                    .HasForeignKey<Positioner>(d => d.IdPracownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_15_Pracownik");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("Pracownik_pk");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.HomeAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PeselNumber)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithOne(p => p.Pracownik)
                    .HasForeignKey<Employee>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pracownik_Osoba");
            });

            modelBuilder.Entity<EmployeeClient>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.EmployeeId, e.MeetingStart })
                    .HasName("PracownikKlient_pk");

                entity.Property(e => e.MeetingStart).HasColumnType("datetime");

                entity.Property(e => e.MeetingEnd).HasColumnType("datetime");

                entity.Property(e => e.MeetingLocation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ClientIdNavigation)
                    .WithMany(p => p.EmployeeClient)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikKlient_Klient");

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithMany(p => p.EmployeeClient)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikKlient_Pracownik");
            });

            modelBuilder.Entity<EmployeeContract>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.ContractId, e.ContractStart })
                    .HasName("PracownikUmowa_pk");

                entity.Property(e => e.ContractStart).HasColumnType("datetime");

                entity.Property(e => e.ContractEnd).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithMany(p => p.EmployeeContract)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikUmowa_Pracownik");

                entity.HasOne(d => d.ContractIdNavigation)
                    .WithMany(p => p.EmployeeContract)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownikUmowa_Umowa");
            });

            modelBuilder.Entity<EmployeeTeam>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.TeamId, e.AssignStart })
                    .HasName("PracownikZespol_pk");

                entity.Property(e => e.AssignStart).HasColumnType("datetime");

                entity.Property(e => e.AssignEnd).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithMany(p => p.EmployeeTeam)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_22_Pracownik");

                entity.HasOne(d => d.TeamIdNavigation)
                    .WithMany(p => p.PracownikZespol)
                    .HasForeignKey(d => d.TeamId)
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
                    .WithOne(p => p.Programmer)
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
                    .WithMany(p => p.Project)
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
                    .WithMany(p => p.ProjectPackage)
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
                entity.HasKey(e => e.EmployeeId)
                    .HasName("Szef_pk");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithOne(p => p.Boss)
                    .HasForeignKey<Boss>(d => d.EmployeeId)
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
                entity.HasKey(e => e.ContractId)
                    .HasName("Umowa_pk");

                entity.Property(e => e.ContractType)
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