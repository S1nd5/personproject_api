CREATE TABLE Users (
    Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    UserName varchar(100) NOT NULL,
	Password varchar(255) NOT NULL,
    FirstName varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
    Occupation varchar(50) NOT NULL,
	Salary decimal(7,2) NOT NULL,
	Country varchar(50) NOT NULL,
	City varchar(50) NULL
);

CREATE TABLE Projects (
    ProjectId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    ProjectName varchar(100) NOT NULL,
);

CREATE TABLE ProjectMembers (
    ProjectId int NOT NULL FOREIGN KEY REFERENCES Projects(ProjectId),
	UserId int NOT NULL FOREIGN KEY REFERENCES Users(Id),
    RoleName varchar(100) NOT NULL,

	CONSTRAINT PK_ProjectMember Primary Key (ProjectId, UserId)
);