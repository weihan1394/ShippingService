-- Create tables
CREATE TABLE IF NOT EXISTS postal
(
    id                    SERIAL        PRIMARY KEY,
    type                  TEXT          NOT NULL,
    trackable             TEXT          NOT NULL,
    service_level_days    TEXT          NOT NULL,
    country               TEXT          NOT NULL,
    country_code          TEXT          NOT NULL,
    item_weight_kg        INTEGER       NOT NULL,
    singpost_item_rate    NUMERIC       NOT NULL,
    singpost_rate_per_kg  NUMERIC       NOT NULL
);
