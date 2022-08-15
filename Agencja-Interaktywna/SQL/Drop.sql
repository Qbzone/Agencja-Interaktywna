-- foreign keys
ALTER TABLE ClientCompany DROP CONSTRAINT ClientCompany_Company;

ALTER TABLE ClientCompany DROP CONSTRAINT ClientCompany_Client;

ALTER TABLE Client DROP CONSTRAINT Client_Person;

ALTER TABLE PackageService DROP CONSTRAINT PackageService_Package;

ALTER TABLE PackageService DROP CONSTRAINT PackageService_Service;

ALTER TABLE EmployeeClient DROP CONSTRAINT EmployeeClient_Client;

ALTER TABLE EmployeeClient DROP CONSTRAINT EmployeeClient_Employee;

ALTER TABLE EmployeeContract DROP CONSTRAINT EmployeeContract_Employee;

ALTER TABLE EmployeeContract DROP CONSTRAINT EmployeeContract_Contract;

ALTER TABLE Employee DROP CONSTRAINT Employee_Person;

ALTER TABLE Boss DROP CONSTRAINT Employee_Boss;

ALTER TABLE ProjectPackage DROP CONSTRAINT ProjectPackage_Package;

ALTER TABLE ProjectPackage DROP CONSTRAINT ProjectPackage_Project;

ALTER TABLE Project DROP CONSTRAINT Project_Company;

ALTER TABLE Tester DROP CONSTRAINT Employee_Tester;

ALTER TABLE Positioner DROP CONSTRAINT Employee_Positioner;

ALTER TABLE Graphician DROP CONSTRAINT Employee_Graphician;

ALTER TABLE Programmer DROP CONSTRAINT Employee_Programmer;

ALTER TABLE ProgrammerLanguage DROP CONSTRAINT ProgrammerProgrammingLanguage_ProgrammingLanguage;

ALTER TABLE ProgrammerLanguage DROP CONSTRAINT ProgrammerProgrammingLanguage_Programmer;

ALTER TABLE EmployeeTeam DROP CONSTRAINT EmployeeTeam_Team;

ALTER TABLE EmployeeTeam DROP CONSTRAINT EmployeeTeam_Employee;

ALTER TABLE TeamProject DROP CONSTRAINT TeamProject_Team;

ALTER TABLE TeamProject DROP CONSTRAINT TeamProject_Project;

ALTER TABLE ServiceProject DROP CONSTRAINT ServiceProject_Project;

ALTER TABLE ServiceProject DROP CONSTRAINT ServiceProject_Service;

-- tables
DROP TABLE ClientCompany;

DROP TABLE PackageService;

DROP TABLE EmployeeClient;

DROP TABLE EmployeeContract;

DROP TABLE EmployeeTeam;

DROP TABLE ProgrammerLanguage;

DROP TABLE ProjectPackage;

DROP TABLE ServiceProject;

DROP TABLE TeamProject;

DROP TABLE ProgrammingLanguage;

DROP TABLE Company;

DROP TABLE Package;

DROP TABLE Project;

DROP TABLE Contract;

DROP TABLE Service;

DROP TABLE Team;

DROP TABLE Graphician;

DROP TABLE Positioner;

DROP TABLE Programmer;

DROP TABLE Tester;

DROP TABLE Boss;

DROP TABLE Client;

DROP TABLE Employee;

DROP TABLE Person;

-- End of file.