CREATE DATABASE Emelyanenko_AdManagement;

GO

USE Emelyanenko_AdManagement;

CREATE TABLE Users (ID INT PRIMARY KEY IDENTITY(0, 1), User_Login VARCHAR(250) UNIQUE NOT NULL, User_Password VARCHAR(250) NOT NULL);
CREATE TABLE Cities (ID INT PRIMARY KEY IDENTITY(0, 1), Name VARCHAR(250) NOT NULL);
CREATE TABLE Categories (ID INT PRIMARY KEY IDENTITY(0, 1), Name VARCHAR(250) NOT NULL);
CREATE TABLE Ad_Types (ID INT PRIMARY KEY IDENTITY(0, 1), Name VARCHAR(250) NOT NULL);
CREATE TABLE Ad_Statuses (ID INT PRIMARY KEY IDENTITY(0, 1), Name VARCHAR(250) NOT NULL);
CREATE TABLE Adverts (ID INT PRIMARY KEY IDENTITY(0, 1), UserID INT REFERENCES Users NOT NULL, Ad_Title VARCHAR(250) NOT NULL, Ad_Description VARCHAR(500) NOT NULL, Ad_Post_Date DATE NOT NULL, CityID INT REFERENCES Cities NOT NULL, CategoryID INT REFERENCES Categories NOT NULL, Ad_TypeID INT REFERENCES Ad_Types NOT NULL, Ad_StatusID INT REFERENCES Ad_Statuses NOT NULL, Price INT NOT NULL, PhotoPath VARCHAR(500));


--CREATE TABLE #TempTable (
--User_Login VARCHAR(250),
--User_Password VARCHAR(250),
--Ad_Title VARCHAR(250),
--Ad_Description VARCHAR(500),
--Ad_Post_Date DATE,
--City VARCHAR(250),
--Category VARCHAR(250),
--Ad_Type VARCHAR(250),
--Ad_Status VARCHAR(250),
--Price INT);

--BULK INSERT #TempTable
--FROM 'C:\Users\227071\Desktop\Ads_Data.csv'
--WITH (
--FIELDTERMINATOR = ';',
--ROWTERMINATOR = '\N',
--FIRSTROW = 2,
--CODEPAGE = 65001);

INSERT INTO Users (User_Login, User_Password)
SELECT DISTINCT User_Login, User_Password FROM Ads_Data;

INSERT INTO Cities (Name)
SELECT DISTINCT City FROM Ads_Data;

INSERT INTO Categories (Name)
SELECT DISTINCT Category FROM Ads_Data;

INSERT INTO Ad_Types (Name)
SELECT DISTINCT Ad_Type FROM Ads_Data;

INSERT INTO Ad_Statuses (Name)
SELECT DISTINCT Ad_Status FROM Ads_Data;

INSERT INTO Adverts (UserID, Ad_Title, Ad_Description, Ad_Post_Date, CityID, CategoryID, Ad_TypeID, Ad_StatusID, Price)
SELECT
(SELECT TOP 1 ID FROM Users WHERE Users.User_Login = Ads_Data.User_Login),
Ad_Title,
Ad_Description,
Ad_Post_Date,
(SELECT TOP 1 ID FROM Cities WHERE Cities.Name = Ads_Data.City),
(SELECT TOP 1 ID FROM Categories WHERE Categories.Name = Ads_Data.Category),
(SELECT TOP 1 ID FROM Ad_Types WHERE Ad_Types.Name = Ads_Data.Ad_Type),
(SELECT TOP 1 ID FROM Ad_Statuses WHERE Ad_Statuses.Name = Ads_Data.Ad_Status),
Price
FROM Ads_Data;