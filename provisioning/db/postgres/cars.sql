CREATE DATABASE cars ENCODING 'UTF8';

CREATE [IF NOT EXISTS] TABLE Cars (
    Id AUTOINCREMENT PRIMARY KEY,
	Plate VARCHAR(50) NOT NULL,
	Model VARCHAR (50) NULL,
	OwnerId Integer NULL,
);

CREATE[IF NOT EXISTS] TABLE Owners (
    Id AUTOINCREMENT PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
 CONSTRAINT [PK_Owners] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

INSERT INTO Cars (Id, Plate, Model, OwnerId)
VALUES
		(1, 'JHV 770', 'Mercedes-Benz GLE Coupe', 1),
		(2, 'TAD-3173', 'Datsun GO+', 1),
		(3, '43-L348', 'Maruti Suzuki Swift', 2),
		(4, 'XPB-2935', 'Land Rover Discovery Sport', 3),
		(5, '805-UXC', 'Nissan GT-R', NULL);