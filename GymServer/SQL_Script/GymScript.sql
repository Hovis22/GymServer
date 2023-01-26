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
		Sex VARCHAR(50) NOT NULL,
		RoleId INT NOT NULL,
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


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Schedule' and xtype='U')
BEGIN
    CREATE TABLE Schedule (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        Name VARCHAR(100)  NOT NULL,
		DateOfTrain Date NOT NULL,
		CoachId INT NOT NULL,
        CONSTRAINT [FK_Schedule_CoachId_Personal] FOREIGN KEY ([CoachId]) REFERENCES [dbo].[Personal] ([Id]) ON DELETE CASCADE
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
		Sex VARCHAR(50) NOT NULL,
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






