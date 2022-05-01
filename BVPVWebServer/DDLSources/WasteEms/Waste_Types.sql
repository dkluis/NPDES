CREATE TABLE `WasteEms`.`Waste_Types` (
                               `HAZ/NON` VARCHAR(7) NOT NULL,
                               CONSTRAINT SYS_PK_10962 PRIMARY KEY (`HAZ/NON`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10962_10963 ON `WasteEms`.`Waste_Types` (`HAZ/NON`);