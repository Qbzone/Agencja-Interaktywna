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
                    .HasName("Company_pk");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Graphician>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("Graphician_pk");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.Specialization)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithOne(p => p.Graphician)
                    .HasForeignKey<Graphician>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_Graphician");
            });

            modelBuilder.Entity<ProgrammingLanguage>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                    .HasName("ProgrammingLanguage_pk");

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .HasName("Client_pk");

                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.Property(e => e.Priority)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ClientIdNavigation)
                    .WithOne(p => p.Client)
                    .HasForeignKey<Client>(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Client_Person");
            });

            modelBuilder.Entity<ClientCompany>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.CompanyId })
                    .HasName("ClientCompany_pk");

                entity.HasOne(d => d.CompanyIdNavigation)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClientCompany_Company");

                entity.HasOne(d => d.ClientIdNavigation)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClientCompany_Client");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("Person_pk");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PrivatePhoneNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.BusinessPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.PackageId)
                    .HasName("Package_pk");

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
                    .HasName("PackageService_pk");

                entity.HasOne(d => d.PackageIdNavigation)
                    .WithMany(p => p.PackageService)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PackageService_Package");

                entity.HasOne(d => d.ServiceIdNavigation)
                    .WithMany(p => p.PackageService)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PackageService_Service");
            });

            modelBuilder.Entity<Positioner>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("Positioner_pk");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.FullFunction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithOne(p => p.Positioner)
                    .HasForeignKey<Positioner>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_Positioner");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("Employee_pk");

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
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_Person");
            });

            modelBuilder.Entity<EmployeeClient>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.EmployeeId, e.MeetingStart })
                    .HasName("EmployeeClient_pk");

                entity.Property(e => e.MeetingStart).HasColumnType("datetime");

                entity.Property(e => e.MeetingEnd).HasColumnType("datetime");

                entity.Property(e => e.MeetingLocation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ClientIdNavigation)
                    .WithMany(p => p.EmployeeClient)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeeClient_Client");

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithMany(p => p.EmployeeClient)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeeClient_Employee");
            });

            modelBuilder.Entity<EmployeeContract>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.ContractId, e.ContractStart })
                    .HasName("EmployeeContract_pk");

                entity.Property(e => e.ContractStart).HasColumnType("datetime");

                entity.Property(e => e.ContractEnd).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithMany(p => p.EmployeeContract)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeeContract_Employee");

                entity.HasOne(d => d.ContractIdNavigation)
                    .WithMany(p => p.EmployeeContract)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeeContract_Contract");
            });

            modelBuilder.Entity<EmployeeTeam>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.TeamId, e.AssignStart })
                    .HasName("EmployeeTeam_pk");

                entity.Property(e => e.AssignStart).HasColumnType("datetime");

                entity.Property(e => e.AssignEnd).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithMany(p => p.EmployeeTeam)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeeTeam_Employee");

                entity.HasOne(d => d.TeamIdNavigation)
                    .WithMany(p => p.EmployeeTeam)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeeTeam_Team");
            });

            modelBuilder.Entity<Programmer>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("Programmer_pk");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.AdvancementLevel)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.EmployeeIdkNavigation)
                    .WithOne(p => p.Programmer)
                    .HasForeignKey<Programmer>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_Programmer");
            });

            modelBuilder.Entity<ProgrammerLanguage>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.LanguageId })
                    .HasName("ProgrammerLanguage_pk");

                entity.HasOne(d => d.LanguageIdNavigation)
                    .WithMany(p => p.ProgrammerLanguage)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProgrammerProgrammingLanguage_ProgrammingLanguage");

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithMany(p => p.ProgrammerLanguage)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProgrammerProgrammingLanguage_Programmer");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProjectId)
                    .HasName("Project_pk");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectLogo)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.CompanyIdNavigation)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Project_Company");
            });

            modelBuilder.Entity<ProjectPackage>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.ProjectId, e.DealStart })
                    .HasName("ProjectPackage_pk");

                entity.Property(e => e.DealStart).HasColumnType("datetime");

                entity.Property(e => e.DealEnd).HasColumnType("datetime");

                entity.HasOne(d => d.PackageIdNavigation)
                    .WithMany(p => p.ProjectPackage)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProjectPackage_Package");

                entity.HasOne(d => d.ProjectIdNavigation)
                    .WithMany(p => p.ProjectPackage)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProjectPackage_Project");
            });

            modelBuilder.Entity<Boss>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("Boss_pk");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithOne(p => p.Boss)
                    .HasForeignKey<Boss>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_Boss");
            });

            modelBuilder.Entity<Tester>(entity =>
            {
                entity.HasKey(e => e.EmploueeId)
                    .HasName("Tester_pk");

                entity.Property(e => e.EmploueeId).ValueGeneratedNever();

                entity.HasOne(d => d.EmployeeIdNavigation)
                    .WithOne(p => p.Tester)
                    .HasForeignKey<Tester>(d => d.EmploueeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_Tester");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.ContractId)
                    .HasName("Contract_pk");

                entity.Property(e => e.ContractType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("Service_pk");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Classification)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ServiceProject>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.ServiceId, e.AssignStart })
                    .HasName("ServiceProject_pk");

                entity.Property(e => e.AssignStart).HasColumnType("datetime");

                entity.Property(e => e.AssignEnd).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.ProjectIdNavigation)
                    .WithMany(p => p.ServiceProject)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ServiceProject_Project");

                entity.HasOne(d => d.ServiceIdNavigation)
                    .WithMany(p => p.ServiceProject)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ServiceProject_Service");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamId)
                    .HasName("Team_pk");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TeamProject>(entity =>
            {
                entity.HasKey(e => new { e.TeamId, e.ProjectId, e.AssignStart })
                    .HasName("TeamProject_pk");

                entity.Property(e => e.AssignStart).HasColumnType("datetime");

                entity.Property(e => e.AssignEnd).HasColumnType("datetime");

                entity.HasOne(d => d.ProjectIdNavigation)
                    .WithMany(p => p.TeamProject)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TeamProject_Project");

                entity.HasOne(d => d.TeamIdNavigation)
                    .WithMany(p => p.TeamProject)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TeamProject_Team");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}