-- Create database
CREATE DATABASE cars ENCODING 'UTF8';

-- Create tables
CREATE TABLE IF NOT EXISTS Cars
(
    Id SERIAL PRIMARY KEY,
	Plate VARCHAR(50) NOT NULL,
	Model VARCHAR (50) NULL,
	OwnerId Integer NULL
);

CREATE TABLE IF NOT EXISTS Owners
(
    Id        SERIAL PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName  VARCHAR(50) NOT NULL
);

-- Insert data
INSERT INTO Cars (Id, Plate, Model, OwnerId)
VALUES
		(1, 'JHV 770', 'Mercedes-Benz GLE Coupe', 1),
		(2, 'TAD-3173', 'Datsun GO+', 1),
		(3, '43-L348', 'Maruti Suzuki Swift', 2),
		(4, 'XPB-2935', 'Land Rover Discovery Sport', 3),
		(5, '805-UXC', 'Nissan GT-R', NULL);

INSERT INTO Owners (Id, FirstName, LastName)
VALUES
		(1, 'Peter', 'Diaz'),
		(2, 'Leon', 'Leonard'),
		(3, 'Shirley', 'Baker'),
		(4, 'Nancy', 'Davis');

-- Alter table with foreign key
ALTER TABLE Cars
    ADD CONSTRAINT fk_cars_owners FOREIGN KEY (OwnerId) REFERENCES  Owners(Id);

-- Create user
CREATE USER markono WITH ENCRYPTED PASSWORD 'Markono123!';
GRANT ALL PRIVILEGES ON DATABASE cars TO markono;

CREATE TABLE IF NOT EXISTS cars.CarTypes
(
    Id      SERIAL PRIMARY KEY,
    Type    VARCHAR(50) NOT NULL
);

INSERT INTO CarTypes (Type)
VALUES
       ('Hatchback'),
       ('Kombi'),
       ('Sedan');