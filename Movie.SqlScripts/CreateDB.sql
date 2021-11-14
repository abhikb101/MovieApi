DROP DATABASE IF EXISTS MovieBD
GO
CREATE DATABASE MovieDB
GO
USE MovieDB


DROP TABLE IF EXISTS Gender_Lookup
CREATE TABLE Gender_Lookup
(
	GenderID	INT PRIMARY KEY,
	Gender		NVARCHAR(100) NOT NULL,	
)


DROP TABLE IF EXISTS Role_Lookup
CREATE TABLE Role_Lookup
(
	RoleID		INT PRIMARY KEY,
	[Role]		NVARCHAR(100) NOT NULL,	
)

DROP TABLE IF EXISTS Person
CREATE TABLE Person
(
	PersonID	UNIQUEIDENTIFIER PRIMARY KEY,
	IsActive	BIT,
	[Name]		NVARCHAR(50) NOT NULL,
	Bio			NVARCHAR(MAX),
	DateOfBirth	DATE NOT NULL,
	Gender		INT NOT NULL,
)

DROP TABLE IF EXISTS Producer
CREATE TABLE Producer
(
	ProducerID	UNIQUEIDENTIFIER PRIMARY KEY,
	IsActive	BIT,
	PersonID	UNIQUEIDENTIFIER NOT NULL UNIQUE,
	Company		NVARCHAR(500) NOT NULL,	
	CONSTRAINT  PFK_P_P 
		FOREIGN KEY (PersonId)
		REFERENCES Person(PersonId)
)


DROP TABLE IF EXISTS Actor
CREATE TABLE Actor
(
	ActorID		UNIQUEIDENTIFIER PRIMARY KEY,
	IsActive	BIT,
	PersonID	UNIQUEIDENTIFIER NOT NULL UNIQUE,
	CONSTRAINT  PFK_A_P 
		FOREIGN KEY (PersonId)
		REFERENCES Person(PersonId)
)


DROP TABLE IF EXISTS Movie
CREATE TABLE Movie
(
	MovieID		UNIQUEIDENTIFIER PRIMARY KEY,
	[Name]		NVARCHAR(500) NOT NULL,
	Plot		NVARCHAR(MAX) NOT NULL,
	ReleaseDate	DATE NOT NULL,
)


DROP TABLE IF EXISTS CastAndCrew
CREATE TABLE CastAndCrew
(
	CrewID		UNIQUEIDENTIFIER PRIMARY KEY,
	MovieID		UNIQUEIDENTIFIER NOT NULL,
	PersonID	UNIQUEIDENTIFIER NOT NULL, 
	Role		INT NOT NULL,
	CONSTRAINT  PFK_C_P 
		FOREIGN KEY (PersonID)
		REFERENCES Person(PersonId),
	CONSTRAINT  PFK_C_M 
		FOREIGN KEY (MovieID)
		REFERENCES Movie(MovieID)
)


INSERT INTO Gender_Lookup VALUES(0, 'Male')
INSERT INTO Gender_Lookup VALUES(1, 'Female')
INSERT INTO Gender_Lookup VALUES(3, 'Others')

INSERT INTO Role_Lookup VALUES(0, 'Actor')
INSERT INTO Role_Lookup VALUES(1, 'Producer')
