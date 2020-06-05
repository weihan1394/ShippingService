-- Create tables
CREATE TABLE IF NOT EXISTS Cars
(
    Id SERIAL PRIMARY KEY,
	  Plate VARCHAR(50) NOT NULL,
	  Model VARCHAR (50) NULL
);

-- Insert data
INSERT INTO Cars (Id, Plate, Model)
VALUES
		(1, 'JHV 770', 'Mercedes-Benz GLE Coupe'),
		(2, 'TAD-3173', 'Datsun GO+'),
		(3, '43-L348', 'Maruti Suzuki Swift'),
		(4, 'XPB-2935', 'Land Rover Discovery Sport'),
		(5, '805-UXC', 'Nissan GT-R');
