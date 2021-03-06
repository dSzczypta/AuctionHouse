CREATE TABLE AH_Auctions(
Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
ObjectId UNIQUEIDENTIFIER NOT NULL,
FOREIGN KEY (ObjectId) REFERENCES AH_ObjectToSell(Id),
UserName VARCHAR(100) NOT NULL,
Price DECIMAL NOT NULL,
PaymentMethod UNIQUEIDENTIFIER ,
FOREIGN KEY (PaymentMethod) REFERENCES AH_PaymentMethod(Id),
ShipmentType UNIQUEIDENTIFIER ,
FOREIGN KEY (ShipmentType) REFERENCES AH_ShipmentType(Id),
IsConfirmed bit
)

CREATE TABLE AH_ShipmentType(
Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
Name VARCHAR(100) NOT NULL,
Price DECIMAL NOT NULL,
Time VARCHAR(100) NOT NULL
)

CREATE TABLE AH_PaymentMethod(
Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
Name VARCHAR(100) NOT NULL
)