CREATE TABLE AH_ObjectToSell (
Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
Name VARCHAR(100) NOT NULL, 
Description VARCHAR(MAX) NOT NULL,
Price DECIMAL NOT NULL,
AddedBy VARCHAR(100) NOT NULL,
Sold BIT NOT NULL,
DateAdded DATETIME NOT NULL
)