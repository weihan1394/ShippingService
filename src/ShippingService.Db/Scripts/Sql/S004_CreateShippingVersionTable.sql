-- Create version table
CREATE TABLE IF NOT EXISTS shipping_version
(
    id            SERIAL        PRIMARY KEY,
    upload_date   TEXT          NOT NULL,
    shipping_mode TEXT          NOT NULL,
    valid_start   TEXT          NOT NULL,
    valid_end     TEXT          NOT NULL,
    uploaded_by   TEXT          NOT NULL
);

-- Alter table to create relationship with version table
ALTER TABLE express
ADD COLUMN express_shipping_version     INTEGER   NOT NULL;

ALTER TABLE express
ADD CONSTRAINT express_shipping_version_fk  FOREIGN KEY(express_shipping_version)
REFERENCES shipping_version(id);

ALTER TABLE bulk
ADD COLUMN bulk_shipping_version     INTEGER   NOT NULL;

ALTER TABLE bulk
ADD CONSTRAINT bulk_shipping_version_fk  FOREIGN KEY(bulk_shipping_version)
REFERENCES shipping_version(id);

ALTER TABLE postal
ADD COLUMN postal_shipping_version     INTEGER   NOT NULL;

ALTER TABLE postal
ADD CONSTRAINT postal_shipping_version_fk  FOREIGN KEY(postal_shipping_version)
REFERENCES shipping_version(id);
