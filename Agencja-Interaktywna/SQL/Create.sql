-- tables
-- Table: Company
CREATE TABLE Company (
    CompanyId int  NOT NULL IDENTITY,
    CompanyName nvarchar(50)  NOT NULL,
    CONSTRAINT Company_pk PRIMARY KEY  (CompanyId)
);

-- Table: Graphician
CREATE TABLE Graphician (
    EmployeeId int  NOT NULL,
    Specialization nvarchar(50)  NOT NULL,
    CONSTRAINT Graphician_pk PRIMARY KEY  (EmployeeId)
);

-- Table: ProgrammingLanguage
CREATE TABLE ProgrammingLanguage (
    LanguageId int  NOT NULL IDENTITY,
    LanguageName nvarchar(50)  NOT NULL,
    CONSTRAINT ProgrammingLanguage_pk PRIMARY KEY  (LanguageId)
);

-- Table: Client
CREATE TABLE Client (
    ClientId int  NOT NULL,
    Priority nvarchar(50)  NOT NULL,
    CONSTRAINT Client_pk PRIMARY KEY  (ClientId)
);

-- Table: ClientCompany
CREATE TABLE ClientCompany (
    ClientId int  NOT NULL,
    CompanyId int  NOT NULL,
    CONSTRAINT ClientCompany_pk PRIMARY KEY  (ClientId,CompanyId)
);

-- Table: Person
CREATE TABLE Person (
    PersonId int  NOT NULL IDENTITY,
    FirstName nvarchar(25)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    PrivatePhoneNumber char(9)  NULL,
    BusinessPhoneNumber char(9)  NOT NULL,
    EmailAddress nvarchar(50)  NOT NULL,
    Password nvarchar(max)  NOT NULL,
    IsEmailVerified bit  NOT NULL,
    ActivationCode uniqueidentifier  NOT NULL,
    Role nvarchar(25)  NOT NULL,
    CONSTRAINT Person_pk PRIMARY KEY  (PersonId)
);

-- Table: Package
CREATE TABLE Package (
    PackageId int  NOT NULL IDENTITY,
    PackageName nvarchar(50)  NOT NULL,
    Fee int  NOT NULL,
    FeeType nvarchar(50)  NOT NULL,
    CONSTRAINT Package_pk PRIMARY KEY  (PackageId)
);

-- Table: PackageService
CREATE TABLE PackageService (
    PackageId int  NOT NULL,
    ServiceId int  NOT NULL,
    CONSTRAINT PackageService_pk PRIMARY KEY  (PackageId,ServiceId)
);

-- Table: Positioner
CREATE TABLE Positioner (
    EmployeeId int  NOT NULL,
    FullFunction nvarchar(50)  NOT NULL,
    CONSTRAINT Positioner_pk PRIMARY KEY  (EmployeeId)
);

-- Table: Employee
CREATE TABLE Employee (
    EmployeeId int  NOT NULL,
    HomeAddress nvarchar(100)  NOT NULL,
    Salary int  NOT NULL,
    Bonus int  NOT NULL,
    PeselNumber char(11)  NOT NULL,
    Seniority int  NOT NULL,
    CONSTRAINT Employee_pk PRIMARY KEY  (EmployeeId)
);

-- Table: EmployeeClient
CREATE TABLE EmployeeClient (
    EmployeeId int  NOT NULL,
    ClientId int  NOT NULL,
    MeetingStart datetime  NOT NULL,
    MeetingEnd datetime  NOT NULL,
    MeetingLocation nvarchar(50)  NOT NULL,
    CONSTRAINT EmployeeClient_pk PRIMARY KEY  (ClientId,EmployeeId,MeetingStart)
);

-- Table: EmployeeContract
CREATE TABLE EmployeeContract (
    EmployeeId int  NOT NULL,
    ContractId int  NOT NULL,
    ContractStart datetime  NOT NULL,
    ContractEnd datetime NOT NULL,
    CONSTRAINT EmployeeContract_pk PRIMARY KEY  (EmployeeId,ContractId,ContractStart)
);

-- Table: EmployeeTeam
CREATE TABLE EmployeeTeam (
    EmployeeId int  NOT NULL,
    TeamId int  NOT NULL,
    AssignStart datetime  NOT NULL,
    AssignEnd datetime  NULL,
    CONSTRAINT EmployeeTeam_pk PRIMARY KEY  (EmployeeId,TeamId,AssignStart)
);

-- Table: Programmer
CREATE TABLE Programmer (
    EmployeeId int  NOT NULL,
    AdvancementLevel nvarchar(50)  NOT NULL,
    CONSTRAINT Programmer_pk PRIMARY KEY  (EmployeeId)
);

-- Table: ProgrammerLanguage
CREATE TABLE ProgrammerLanguage (
    EmployeeId int  NOT NULL,
    LanguageId int  NOT NULL,
    KnowledgeLevel int  NOT NULL,
    CONSTRAINT ProgrammeLanguage_pk PRIMARY KEY  (EmployeeId,LanguageId)
);

-- Table: Project
CREATE TABLE Project (
    ProjectId int  NOT NULL IDENTITY,
    ProjectName nvarchar(50)  NOT NULL,
    ProjectLogo nvarchar(MAX)  NOT NULL,
    CompanyId int  NOT NULL,
    CONSTRAINT Project_pk PRIMARY KEY  (ProjectId)
);

-- Table: ProjectPackage
CREATE TABLE ProjectPackage (
    ProjectId int  NOT NULL,
    PackageId int  NOT NULL,
    DealStart datetime  NOT NULL,
    DealEnd datetime  NULL,
    CONSTRAINT ProjectPackage_pk PRIMARY KEY  (PackageId,ProjectId,DealStart)
);

-- Table: Boss
CREATE TABLE Boss (
    EmployeeId int  NOT NULL,
    CONSTRAINT Boss_pk PRIMARY KEY  (EmployeeId)
);

-- Table: Tester
CREATE TABLE Tester (
    EmployeeId int  NOT NULL,
    TestingExperience int  NOT NULL,
    CONSTRAINT Tester_pk PRIMARY KEY  (EmployeeId)
);

-- Table: Contract
CREATE TABLE Contract (
    ContractId int  NOT NULL IDENTITY,
    ContractType nvarchar(50)  NOT NULL,
    CONSTRAINT Contract_pk PRIMARY KEY  (ContractId)
);

-- Table: Service
CREATE TABLE Service (
    ServiceId int  NOT NULL IDENTITY,
    ServiceName nvarchar(50)  NOT NULL,
    Classification nvarchar(50)  NOT NULL,
    CONSTRAINT Service_pk PRIMARY KEY  (ServiceId)
);

-- Table: ServiceProject
CREATE TABLE ServiceProject (
    ProjectId int  NOT NULL,
    ServiceId int  NOT NULL,
    AssignStart datetime  NOT NULL,
    AssignEnd datetime  NULL,
    Status nvarchar(30)  NOT NULL,
    Description nvarchar(max)  NOT NULL,
    CONSTRAINT ServiceProject_pk PRIMARY KEY  (ProjectId,AssignStart,ServiceId)
);

-- Table: Team
CREATE TABLE Team (
    TeamId int  NOT NULL IDENTITY,
    TeamName nvarchar(50)  NOT NULL,
    CONSTRAINT Team_pk PRIMARY KEY  (TeamId)
);

-- Table: TeamProject
CREATE TABLE TeamProject (
    TeamId int  NOT NULL,
    ProjectId int  NOT NULL,
    AssignStart datetime  NOT NULL,
    AssignEnd datetime  NULL,
    CONSTRAINT TeamProject_pk PRIMARY KEY  (TeamId,ProjectId,AssignStart)
);

-- foreign keys
-- Reference: ClientCompany_Company (table: ClientCompany)
ALTER TABLE ClientCompany ADD CONSTRAINT ClientCompany_Company
    FOREIGN KEY (CompanyId)
    REFERENCES Company (CompanyId);

-- Reference: ClientCompany_Client (table: ClientCompany)
ALTER TABLE ClientCompany ADD CONSTRAINT ClientCompany_Client
    FOREIGN KEY (ClientId)
    REFERENCES Client (ClientId);

-- Reference: Client_Person (table: Client)
ALTER TABLE Client ADD CONSTRAINT Client_Person
    FOREIGN KEY (ClientId)
    REFERENCES Person (PersonId);

-- Reference: PackageService_Package (table: PackageService)
ALTER TABLE PackageService ADD CONSTRAINT PackageService_Package
    FOREIGN KEY (PackageID)
    REFERENCES Package (PackageId);

-- Reference: PackageService_Service (table: PackageService)
ALTER TABLE PackageService ADD CONSTRAINT PackageService_Service
    FOREIGN KEY (ServiceId)
    REFERENCES Service (ServiceId);

-- Reference: EmployeeClient_Client (table: EmployeeClient)
ALTER TABLE EmployeeClient ADD CONSTRAINT EmployeeClient_Client
    FOREIGN KEY (ClientId)
    REFERENCES Client (ClientId);

-- Reference: EmployeeClient_Employee (table: EmployeeClient)
ALTER TABLE EmployeeClient ADD CONSTRAINT EmployeeClient_Employee
    FOREIGN KEY (EmployeeId)
    REFERENCES Employee (EmployeeId);

-- Reference: EmployeeContract_Employee (table: EmployeeContract)
ALTER TABLE EmployeeContract ADD CONSTRAINT EmployeeContract_Employee
    FOREIGN KEY (EmployeeId)
    REFERENCES Employee (EmployeeId);

-- Reference: EmployeeContract_Contract (table: EmployeeContract)
ALTER TABLE EmployeeContract ADD CONSTRAINT EmployeeContract_Contract
    FOREIGN KEY (ContractId)
    REFERENCES Contract (ContractId);

-- Reference: Employee_Person (table: Employee)
ALTER TABLE Employee ADD CONSTRAINT Employee_Person
    FOREIGN KEY (EmployeeId)
    REFERENCES Person (PersonId);

-- Reference: Employee_Boss (table: Boss)
ALTER TABLE Boss ADD CONSTRAINT Employee_Boss
    FOREIGN KEY (EmployeeId)
    REFERENCES Employee (EmployeeId);

-- Reference: ProjectPackage_Package (table: ProjectPackage)
ALTER TABLE ProjectPackage ADD CONSTRAINT ProjectPackage_Package
    FOREIGN KEY (PackageId)
    REFERENCES Package (PackageId);

-- Reference: ProjectPackage_Project (table: ProjectPackage)
ALTER TABLE ProjectPackage ADD CONSTRAINT ProjectPackage_Project
    FOREIGN KEY (ProjectId)
    REFERENCES Project (ProjectId);

-- Reference: Project_Company (table: Project)
ALTER TABLE Project ADD CONSTRAINT Project_Company
    FOREIGN KEY (CompanyId)
    REFERENCES Company (CompanyId);

-- Reference: Employee_Tester (table: Tester)
ALTER TABLE Tester ADD CONSTRAINT Employee_Tester
    FOREIGN KEY (EmployeeId)
    REFERENCES Employee (EmployeeId);

-- Reference: Employee_Positioner (table: Positioner)
ALTER TABLE Positioner ADD CONSTRAINT Employee_Positioner
    FOREIGN KEY (EmployeeId)
    REFERENCES Employee (EmployeeId);

-- Reference: Employee_Graphician (table: Graphician)
ALTER TABLE Graphician ADD CONSTRAINT Employee_Graphician
    FOREIGN KEY (EmployeeId)
    REFERENCES Employee (EmployeeId);

-- Reference: Employee_Programmer (table: Programmer)
ALTER TABLE Programmer ADD CONSTRAINT Employee_Programmer
    FOREIGN KEY (EmployeeId)
    REFERENCES Employee (EmployeeId);

-- Reference: ProgrammerProgrammingLanguage_ProgrammingLanguage (table: ProgrammerLanguage)
ALTER TABLE ProgrammerLanguage ADD CONSTRAINT ProgrammerProgrammingLanguage_ProgrammingLanguage
    FOREIGN KEY (LanguageId)
    REFERENCES ProgrammingLanguage (LanguageId);

-- Reference: ProgrammerProgrammingLanguage_Programmer (table: ProgrammerLanguage)
ALTER TABLE ProgrammerLanguage ADD CONSTRAINT ProgrammerProgrammingLanguage_Programmer
    FOREIGN KEY (EmployeeId)
    REFERENCES Programmer (EmployeeId);

-- Reference: EmployeeTeam_Employee (table: EmployeeTeam)
ALTER TABLE EmployeeTeam ADD CONSTRAINT EmployeeTeam_Employee
    FOREIGN KEY (EmployeeId)
    REFERENCES Employee (EmployeeId);

-- Reference: EmployeeTeam_Team (table: EmployeeTeam)
ALTER TABLE EmployeeTeam ADD CONSTRAINT EmployeeTeam_Team
    FOREIGN KEY (TeamId)
    REFERENCES Team (TeamId);

-- Reference: TeamProject_Project (table: TeamProject)
ALTER TABLE TeamProject ADD CONSTRAINT TeamProject_Project
    FOREIGN KEY (ProjectId)
    REFERENCES Project (ProjectId);

-- Reference: TeamProject_Team (table: TeamProject)
ALTER TABLE TeamProject ADD CONSTRAINT TeamProject_Team
    FOREIGN KEY (TeamId)
    REFERENCES Team (TeamId);

-- Reference: ServiceProject_Project (table: ServiceProject)
ALTER TABLE ServiceProject ADD CONSTRAINT ServiceProject_Project
    FOREIGN KEY (ProjectId)
    REFERENCES Project (ProjectId);

-- Reference: ServiceProject_Service (table: ServiceProject)
ALTER TABLE ServiceProject ADD CONSTRAINT ServiceProject_Service
    FOREIGN KEY (ServiceId)
    REFERENCES Service (ServiceId);

-- End of file.