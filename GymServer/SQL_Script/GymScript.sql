   IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Gym')
  BEGIN
    CREATE DATABASE [Gym]


    END
	  GO
       USE [Gym]
    GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Roles' and xtype='U')
BEGIN
    CREATE TABLE Roles (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        Name VARCHAR(100)  NOT NULL
    )
END




IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Personal' and xtype='U')
BEGIN
    CREATE TABLE Personal (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        Name VARCHAR(100)  NOT NULL,
		LastName  VARCHAR(100) NOT NULL,
		BirthDay Date NOT NULL,
		Phone VARCHAR(100) NOT NULL,
		Email VARCHAR(100) NOT NULL,
		Gender VARCHAR(50) NOT NULL,
		RoleId INT NOT NULL,
        IMGPath VARCHAR(50),
		Password VARCHAR(50) NOT NULL
        CONSTRAINT [FK_Personal_RoleId_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE
    )
END




IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Specialization' and xtype='U')
BEGIN
    CREATE TABLE Specialization (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        Name VARCHAR(100)  NOT NULL,
		CoachId INT NOT NULL,
        CONSTRAINT [FK_Specialization_CoachId_Personal] FOREIGN KEY ([CoachId]) REFERENCES [dbo].[Personal] ([Id]) ON DELETE CASCADE
    )
END



IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TypeOfTrain' and xtype='U')
BEGIN
    CREATE TABLE TypeOfTrain (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        Name VARCHAR(100)  NOT NULL,
    )
END






IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Schedule' and xtype='U')
BEGIN
    CREATE TABLE Schedule (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        Name VARCHAR(100)  NOT NULL,
		DateOfTrain DateTime NOT NULL,
        TimeOfTrain int NOT NULL,
		CoachId INT NOT NULL,
        TypeId INT NOT NULL,
        MaxPeople INT NOT NULL,
        CountPeople INT NOT NULL,
        CONSTRAINT [FK_Schedule_CoachId_Personal] FOREIGN KEY ([CoachId]) REFERENCES [dbo].[Personal] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Schedule_TypeId_Type] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[TypeOfTrain] ([Id]) ON DELETE CASCADE
    )
END


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Clients' and xtype='U')
BEGIN
    CREATE TABLE Clients (
         Id INT PRIMARY KEY IDENTITY (1, 1),
        Name VARCHAR(100)  NOT NULL,
		LastName  VARCHAR(100) NOT NULL,
		BirthDay Date NOT NULL,
		Phone VARCHAR(100) NOT NULL,
		Email VARCHAR(100) NOT NULL,
		Gender VARCHAR(50) NOT NULL,
		Password VARCHAR(50) NOT NULL

    )
END



IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PeopleOnWorkouts' and xtype='U')
BEGIN
    CREATE TABLE PeopleOnWorkouts (
         Id INT PRIMARY KEY IDENTITY (1, 1),
        ClientId INT NOT NULL,
		ScheduleId INT NOT NULL
		 CONSTRAINT [FK_PeopleOnWorkouts_ClientId_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE CASCADE,
		 CONSTRAINT [FK_PeopleOnWorkouts_ScheduleId_Schedule] FOREIGN KEY ([ScheduleId]) REFERENCES [dbo].[Schedule] ([Id]) ON DELETE CASCADE
    )
END






INSERT INTO Roles (Name) VALUES('Coach');
INSERT INTO Roles (Name) VALUES('Admin');


INSERT INTO TypeOfTrain (Name) VALUES('Solo');
INSERT INTO TypeOfTrain (Name) VALUES('Group');




INSERT INTO Personal (Name, LastName,BirthDay,Phone,Email,Gender,RoleId,IMGPath,Password) VALUES('admin','admin',convert(datetime,'18-06-12 10:34:09 PM',5),'9622367','admin@i.ua','Man','2',NULL,'123');

