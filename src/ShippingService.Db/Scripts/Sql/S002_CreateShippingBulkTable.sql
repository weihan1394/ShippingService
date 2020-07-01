-- Create tables
CREATE TABLE IF NOT EXISTS bulk
(
    id                    SERIAL        PRIMARY KEY,
    type                  TEXT          NOT NULL,
    trackable             TEXT          NOT NULL,
    service_level         TEXT          NOT NULL,
    country               TEXT          NOT NULL,
    country_code          TEXT          NOT NULL,
    item_weight_kg        INTEGER       NOT NULL,
    total_weight_kg       NUMERIC       NOT NULL,
    ascendia_item_rate    NUMERIC       NOT NULL,
    ascendia_rate_per_kg  NUMERIC       NOT NULL,
    singpost_item_rate    NUMERIC       NOT NULL,
    singpost_rate_per_kg  NUMERIC       NOT NULL,
    dai_item_rate         NUMERIC       NOT NULL,
    dai_rate_per_kg       NUMERIC       NOT NULL
);
