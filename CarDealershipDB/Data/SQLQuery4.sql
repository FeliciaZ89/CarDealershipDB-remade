




CREATE TABLE ServicePrice(
    Id int PRIMARY KEY IDENTITY NOT NULL,
    Cost money NOT NULL
);

CREATE TABLE TireServices(
    Id int PRIMARY KEY IDENTITY NOT NULL,
    ServiceName nvarchar(50) NOT NULL,
    CostId int FOREIGN KEY REFERENCES ServicePrice(Id) NOT NULL
);

CREATE TABLE TireInventory(
    Id int PRIMARY KEY IDENTITY NOT NULL,
    Quantity int NULL
);

CREATE TABLE Prices(
    Id int PRIMARY KEY identity NOT NULL,
    Price money NOT NULL
);

CREATE TABLE Tires (
    Id int PRIMARY KEY IDENTITY,
    Brand nvarchar(50) NOT NULL,
    Size varchar(20) NOT NULL,
    [Type] varchar(25) NOT NULL,
    Seasonality varchar(15) NOT NULL,
    PriceId int FOREIGN KEY REFERENCES Prices(Id) NOT NULL,
    TireInventoryId int FOREIGN KEY REFERENCES TireInventory(Id) NOT NULL
);
