CREATE TABLE IF NOT EXISTS CarTypes (
    Id      SERIAL PRIMARY KEY,
    Type    VARCHAR(50) NOT NULL
);

INSERT INTO CarTypes (Type)
VALUES
       ('Hatchback'),
       ('Kombi'),
       ('Sedan');

