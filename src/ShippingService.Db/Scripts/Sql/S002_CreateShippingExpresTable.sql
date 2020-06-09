-- Create tables
CREATE TABLE IF NOT EXISTS express
(
    type          TEXT          NOT NULL,
    trackable     TEXT          NOT NULL,
    serviceLevel  TEXT          NOT NULL,
    country       TEXT          NOT NULL,
    countryCode   TEXT          NOT NULL,
    rateFlag      INTEGER       NOT NULL,
    weight        NUMERIC       NOT NULL,
    dhlExpress    NUMERIC       NOT NULL,
    sfEconomy     NUMERIC       NOT NULL,
    zone          INTEGER       NOT NULL
);
