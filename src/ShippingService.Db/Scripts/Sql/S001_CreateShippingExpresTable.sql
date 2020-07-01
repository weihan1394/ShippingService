-- Create tables
CREATE TABLE IF NOT EXISTS express
(
    id            SERIAL        PRIMARY KEY,
    type          TEXT          NOT NULL,
    trackable     TEXT          NOT NULL,
    service_level TEXT          NOT NULL,
    country       TEXT          NOT NULL,
    country_code  TEXT          NOT NULL,
    rate_flag     INTEGER       NOT NULL,
    weight        NUMERIC       NOT NULL,
    dhl_express   NUMERIC       NOT NULL,
    sf_economy    NUMERIC       NOT NULL,
    ninja_van     NUMERIC       NOT NULL,
    zone          INTEGER       NOT NULL
);
