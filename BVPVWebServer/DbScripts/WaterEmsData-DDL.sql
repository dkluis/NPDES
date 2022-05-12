CREATE TABLE `WaterEmsData`.`ComplianceStatus` (
                                    OBJID VARCHAR(25),
                                    MESSAGE VARCHAR(50),
                                    PARAM VARCHAR(50),
                                    SAMPDATE DATETIME,
                                    LIMTYPE VARCHAR(50),
                                    QUAL VARCHAR(8),
                                    `RESULT` NUMERIC(65,7),
                                    LIMVAL NUMERIC(65,2),
                                    UNIT VARCHAR(10)
);
CREATE INDEX COMPLIANCESTATUS_OBJID ON `ComplianceStatus` (OBJID,MESSAGE,PARAM,SAMPDATE,LIMTYPE);

CREATE TABLE `WaterEmsData`.`Conversion Errors` (
                                     `Object Type` VARCHAR(255),
                                     `Object Name` VARCHAR(255),
                                     `Error Description` VARCHAR(21300)
);

CREATE TABLE `WaterEmsData`.`DMRBadUnits` (
                               OBJID VARCHAR(25),
                               PARAM VARCHAR(50),
                               SAMPDATE DATETIME,
                               HLALABID VARCHAR(25),
                               `RESULT` NUMERIC(65,7),
                               UNIT VARCHAR(10),
                               PARAMUNITS VARCHAR(15)
);
CREATE INDEX DMRBADUNITS_HLALABID ON `DMRBadUnits` (HLALABID);
CREATE INDEX DMRBADUNITS_OBJID ON `DMRBadUnits` (OBJID,PARAM,SAMPDATE);

CREATE TABLE `WaterEmsData`.`DMRData` (
                           OBJID VARCHAR(25),
                           PARAM VARCHAR(50),
                           SAMPDATE DATETIME,
                           QUAL VARCHAR(8) DEFAULT 'D',
                           `RESULT` NUMERIC(65,7),
                           UNIT VARCHAR(10),
                           EXCUR BOOLEAN DEFAULT false,
                           FLOWRATE NUMERIC(65,7),
                           MASSQUAL VARCHAR(8),
                           MASSLOAD NUMERIC(65,7),
                           MASSUNIT VARCHAR(255),
                           MASSEXCUR BOOLEAN DEFAULT false,
                           `StartDate` DATETIME,
                           `EndDate` DATETIME,
                           `Sign` VARCHAR(255)
);

CREATE TABLE `WaterEmsData`.`DMRData_MonthAverage` (
                                        OBJID VARCHAR(25),
                                        PARAM VARCHAR(50),
                                        QUAL VARCHAR(8),
                                        AVERESULT NUMERIC(65,7),
                                        UNIT VARCHAR(10),
                                        EXCUR BOOLEAN DEFAULT false,
                                        MASSQUAL VARCHAR(8),
                                        AVEMASSLOAD NUMERIC(65,7),
                                        MASSUNIT VARCHAR(255),
                                        MASSEXCUR BOOLEAN DEFAULT false,
                                        `MinConcQual` VARCHAR(8),
                                        `MinConc` NUMERIC(65,7),
                                        `MaxConcQual` VARCHAR(8),
                                        `MaxConc` NUMERIC(65,7),
                                        `MaxMassQual` VARCHAR(8),
                                        `MaxMass` NUMERIC(65,7),
                                        TOTEXCUR SMALLINT DEFAULT 0
);
CREATE INDEX DMRDATA_MONTHAVERAGE_OBJID ON `DMRData_MonthAverage` (OBJID,PARAM);

CREATE TABLE `WaterEmsData`.`DMRDataLTD` (
                              OBJID VARCHAR(25),
                              PARAM VARCHAR(50),
                              SAMPDATE DATETIME,
                              QUAL VARCHAR(8) DEFAULT 'D',
                              `RESULT` NUMERIC(65,7),
                              UNIT VARCHAR(10),
                              EXCUR BOOLEAN DEFAULT false,
                              FLOWRATE NUMERIC(65,7),
                              MASSQUAL VARCHAR(8),
                              MASSLOAD NUMERIC(65,7),
                              MASSUNIT VARCHAR(255),
                              MASSEXCUR BOOLEAN DEFAULT false,
                              `StartDate` DATETIME,
                              `EndDate` DATETIME
);
CREATE INDEX DMRDATALTD_OBJID ON `DMRDataLTD` (OBJID,PARAM,SAMPDATE);

CREATE TABLE `WaterEmsData`.`DMRExport` (
                             `Permnum` VARCHAR(15),
                             `DMRPage` VARCHAR(15),
                             `StartDate` DATETIME,
                             `EndDate` DATETIME,
                             `PageNum` SMALLINT,
                             `PageTot` SMALLINT,
                             `OrderNum` SMALLINT,
                             `Param` VARCHAR(50),
                             `AveMassQual` VARCHAR(8),
                             `AveMassLoad` NUMERIC(65,7),
                             `MaxMassQual` VARCHAR(8),
                             `MaxMass` NUMERIC(65,7),
                             `MassUnit` VARCHAR(25),
                             `MinConcQual` VARCHAR(8),
                             `MinConc` NUMERIC(65,7),
                             `AveConcQual` VARCHAR(8),
                             `AveConcResult` NUMERIC(65,7),
                             `MaxConcQual` VARCHAR(8),
                             `MaxConc` NUMERIC(65,7),
                             `ConcUnit` VARCHAR(10),
                             `TotalExcur` SMALLINT DEFAULT 0
);

CREATE TABLE `WaterEmsData`.`DMRFormat` (
                             `Permnum` VARCHAR(15),
                             `DMRPage` VARCHAR(15),
                             `PageNum` SMALLINT,
                             `PageTot` SMALLINT,
                             `OrderNum` SMALLINT,
                             OBJID VARCHAR(25),
                             `Param` VARCHAR(50),
                             `SampDate` DATETIME,
                             `ConcQual` VARCHAR(8),
                             `ConcResult` NUMERIC(65,7),
                             `ConcUnit` VARCHAR(10),
                             `ConcExcur` BOOLEAN,
                             `FlowRate` NUMERIC(65,7),
                             `MassQual` VARCHAR(8),
                             `MassLoad` NUMERIC(65,7),
                             `MassUnit` VARCHAR(25),
                             `MassExcur` BOOLEAN,
                             `AveConcQual` VARCHAR(8),
                             `AveConcResult` NUMERIC(65,7),
                             `AveConcExcur` BOOLEAN,
                             `AveMassQual` VARCHAR(8),
                             `AveMassLoad` NUMERIC(65,7),
                             `AveMassExcur` BOOLEAN,
                             `MinConcQual` VARCHAR(8),
                             `MinConc` NUMERIC(65,7),
                             `MaxConcQual` VARCHAR(8),
                             `MaxConc` NUMERIC(65,7),
                             `MaxMassQual` VARCHAR(8),
                             `MaxMass` NUMERIC(65,7),
                             `TotalExcur` SMALLINT DEFAULT 0,
                             `StartDate` DATETIME,
                             `EndDate` DATETIME
);
CREATE INDEX DMRFORMAT_DMRPAGES ON `DMRFormat` (`DMRPage`,`PageNum`,`OrderNum`);
CREATE INDEX DMRFORMAT_OBJID ON `DMRFormat` (OBJID);
CREATE INDEX DMRFORMAT_PERMNUM ON `DMRFormat` (`Permnum`);

CREATE TABLE `WaterEmsData`.`DMRMissFlows` (
                                OBJID VARCHAR(25),
                                SAMPDATE DATETIME
);
CREATE INDEX DMRMISSFLOWS_OBJID ON `DMRMissFlows` (OBJID);

CREATE TABLE `WaterEmsData`.`Entry_Lab_Param_Temp` (
                                        HLALABID VARCHAR(25) NOT NULL,
                                        PARAM VARCHAR(50) NOT NULL,
                                        FIELDNUM SMALLINT DEFAULT 1,
                                        FORMORDER SMALLINT,
                                        RPTORDER SMALLINT,
                                        `RESULT` DOUBLE,
                                        PARAMUNITS VARCHAR(10),
                                        QUAL VARCHAR(8) DEFAULT 'D',
                                        `METHOD` VARCHAR(15),
                                        ANALDATE DATETIME,
                                        ANALYST VARCHAR(15),
                                        DATAUSE VARCHAR(10),
                                        ENTRYNOTE VARCHAR(120),
                                        CONSTRAINT SYS_PK_10758 PRIMARY KEY (HLALABID,PARAM)
);
CREATE INDEX ENTRY_LAB_PARAM_TEMP_FIELDNUM ON `Entry_Lab_Param_Temp` (FIELDNUM);
CREATE INDEX ENTRY_LAB_PARAM_TEMP_FORMORDER ON `Entry_Lab_Param_Temp` (FORMORDER);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10758_10759 ON `Entry_Lab_Param_Temp` (HLALABID,PARAM);

CREATE TABLE `WaterEmsData`.`Entry_Lab_SampInfo_Temp` (
                                           HLALABID VARCHAR(25) NOT NULL,
                                           OBJID VARCHAR(25),
                                           PERMNUM VARCHAR(15),
                                           SAMPTYPE VARCHAR(18),
                                           SAMPBY VARCHAR(15),
                                           COLLDATE DATETIME,
                                           COLLTIME DATETIME,
                                           SAMPDATE DATETIME,
                                           LABNAME VARCHAR(25),
                                           COMMENT VARCHAR(140),
                                           CONSTRAINT SYS_PK_10763 PRIMARY KEY (HLALABID)
);
CREATE INDEX ENTRY_LAB_SAMPINFO_TEMP_OBJID ON `Entry_Lab_SampInfo_Temp` (OBJID);
CREATE INDEX ENTRY_LAB_SAMPINFO_TEMP_PERMNUM ON `Entry_Lab_SampInfo_Temp` (PERMNUM);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10763_10764 ON `Entry_Lab_SampInfo_Temp` (HLALABID);

CREATE TABLE `WaterEmsData`.`Entry_Lab_Temp` (
                                  SAMPLEIDPREFIX VARCHAR(16) NOT NULL,
                                  FIELDNUM SMALLINT DEFAULT 1 NOT NULL,
                                  FORMORDER SMALLINT,
                                  RPTORDER SMALLINT,
                                  HLALABID VARCHAR(25),
                                  OBJID VARCHAR(25),
                                  PERMNUM VARCHAR(15),
                                  SAMPTYPE VARCHAR(18),
                                  SAMPBY VARCHAR(15),
                                  COLLDATE DATETIME,
                                  COLLTIME DATETIME,
                                  SAMPDATE DATETIME,
                                  LABNAME VARCHAR(25),
                                  COMMENT VARCHAR(140),
                                  PARAM VARCHAR(50),
                                  `RESULT` NUMERIC(65,7),
                                  PARAMUNITS VARCHAR(10),
                                  QUAL VARCHAR(8) DEFAULT 'D',
                                  `METHOD` VARCHAR(15),
                                  ANALDATE DATETIME,
                                  ANALYST VARCHAR(15),
                                  DATAUSE VARCHAR(10),
                                  ENTRYNOTE VARCHAR(120),
                                  CONSTRAINT SYS_PK_10769 PRIMARY KEY (SAMPLEIDPREFIX,FIELDNUM)
);
CREATE INDEX ENTRY_LAB_TEMP_HLALABID ON `Entry_Lab_Temp` (HLALABID);
CREATE INDEX ENTRY_LAB_TEMP_OBJID ON `Entry_Lab_Temp` (OBJID);
CREATE INDEX ENTRY_LAB_TEMP_PERMNUM ON `Entry_Lab_Temp` (PERMNUM);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10769_10770 ON `Entry_Lab_Temp` (SAMPLEIDPREFIX,FIELDNUM);

CREATE TABLE `WaterEmsData`.`Exceptions_Monthly_Temp` (
                                           OBJID VARCHAR(25),
                                           PARAM VARCHAR(50),
                                           TOTSAMPLE INTEGER DEFAULT 0,
                                           TOTLIMIT INTEGER DEFAULT 0,
                                           TOTEXCUR INTEGER DEFAULT 0
);
CREATE INDEX EXCEPTIONS_MONTHLY_TEMP_OBJID ON `Exceptions_Monthly_Temp` (OBJID,PARAM);

CREATE TABLE `WaterEmsData`.`Import_FLOW` (
                               OBJID VARCHAR(25),
                               SAMPDATE DATETIME,
                               IMPDATE VARCHAR(20),
                               FLOWGPM NUMERIC(65,7),
                               FLOWMGD NUMERIC(65,7),
                               ENTER VARCHAR(20)
);
CREATE INDEX IMPORT_FLOW_OBJID ON `Import_FLOW` (OBJID);

CREATE TABLE `WaterEmsData`.`Import_OBJID_Check` (
                                      ORDERNUM VARCHAR(13),
                                      SAMPLEID VARCHAR(7),
                                      OBJID VARCHAR(25)
);
CREATE INDEX IMPORT_OBJID_CHECK_OBJID ON `Import_OBJID_Check` (OBJID);
CREATE INDEX IMPORT_OBJID_CHECK_ORDERNUM ON `Import_OBJID_Check` (ORDERNUM);
CREATE INDEX IMPORT_OBJID_CHECK_SAMPLEID ON `Import_OBJID_Check` (SAMPLEID);

CREATE TABLE `WaterEmsData`.`Import_Param` (
                                HLALABID VARCHAR(25),
                                FIELDNUM SMALLINT DEFAULT 0,
                                PARAM VARCHAR(50),
                                LABRESULT VARCHAR(10),
                                LABUNIT VARCHAR(10),
                                LABQUAL VARCHAR(8) DEFAULT 'D',
                                `RESULT` NUMERIC(65,7) DEFAULT 0,
                                UNIT VARCHAR(10),
                                QUAL VARCHAR(8) DEFAULT 'D',
                                `METHOD` VARCHAR(15),
                                ANALDATE DATETIME,
                                ANALYST VARCHAR(25),
                                DATAUSE VARCHAR(10)
);
CREATE INDEX IMPORT_PARAM_FIELDNUM ON `Import_Param` (FIELDNUM);
CREATE INDEX IMPORT_PARAM_LABID ON `Import_Param` (HLALABID,PARAM);

CREATE TABLE `WaterEmsData`.`Import_Param_Check` (
                                      ORDERNUM VARCHAR(13),
                                      SAMPLEID VARCHAR(7),
                                      PARAM VARCHAR(50)
);
CREATE INDEX IMPORT_PARAM_CHECK_ORDERNUM ON `Import_Param_Check` (ORDERNUM);
CREATE INDEX IMPORT_PARAM_CHECK_SAMPLEID ON `Import_Param_Check` (SAMPLEID);

CREATE TABLE `WaterEmsData`.`Import_SampInfo` (
                                   HLALABID VARCHAR(25),
                                   OBJID VARCHAR(25),
                                   PERMNUM VARCHAR(15),
                                   ORDERNUM VARCHAR(13),
                                   SAMPLEID VARCHAR(7),
                                   SAMPTYPE VARCHAR(18),
                                   SAMPBY VARCHAR(15),
                                   COLLDATE DATETIME,
                                   COLLTIME DATETIME,
                                   SAMPDATE DATETIME,
                                   LABNAME VARCHAR(25),
                                   RECDATE DATETIME,
                                   COMMENT VARCHAR(50),
                                   `SOURCE` VARCHAR(150),
                                   ENTERDATE DATETIME DEFAULT CURRENT_TIMESTAMP
);
CREATE INDEX IMPORT_SAMPINFO_HLALABID ON `Import_SampInfo` (HLALABID);
CREATE INDEX IMPORT_SAMPINFO_OBJID ON `Import_SampInfo` (OBJID);
CREATE INDEX IMPORT_SAMPINFO_ORDERNUM ON `Import_SampInfo` (ORDERNUM);
CREATE INDEX IMPORT_SAMPINFO_PERMNUM ON `Import_SampInfo` (PERMNUM);
CREATE INDEX IMPORT_SAMPINFO_SAMPLEID ON `Import_SampInfo` (SAMPLEID);

CREATE TABLE `WaterEmsData`.`Import_Temp` (
                               HLALABID VARCHAR(25),
                               OBJID VARCHAR(25),
                               PERMNUM VARCHAR(15),
                               ORDERNUM VARCHAR(13),
                               SAMPLEID VARCHAR(7),
                               SAMPTYPE VARCHAR(18),
                               SAMPBY VARCHAR(15),
                               COLLDATE DATETIME,
                               COLLTIME DATETIME,
                               SAMPDATE DATETIME,
                               LABNAME VARCHAR(25),
                               RECDATE DATETIME,
                               `COMMENT` VARCHAR(50),
                               `SOURCE` VARCHAR(150),
                               ENTERDATE DATETIME DEFAULT CURRENT_TIMESTAMP,
                               FIELDNUM SMALLINT DEFAULT 1,
                               PARAM VARCHAR(50),
                               LABRESULT VARCHAR(10),
                               LABUNIT VARCHAR(10),
                               LABQUAL VARCHAR(8) DEFAULT 'D',
                               `RESULT` NUMERIC(65,7) DEFAULT 0,
                               UNIT VARCHAR(10),
                               QUAL VARCHAR(8) DEFAULT 'D',
                               `METHOD` VARCHAR(15),
                               ANALDATE DATETIME,
                               ANALYST VARCHAR(25),
                               DATAUSE VARCHAR(10) DEFAULT 'NPDES ONLY'
);
CREATE INDEX IMPORT_TEMP_FIELDNUM ON `Import_Temp` (FIELDNUM);
CREATE INDEX IMPORT_TEMP_HLALABID ON `Import_Temp` (HLALABID);
CREATE INDEX IMPORT_TEMP_OBJID ON `Import_Temp` (OBJID);
CREATE INDEX IMPORT_TEMP_ORDERNUM ON `Import_Temp` (ORDERNUM,SAMPLEID,PARAM);
CREATE INDEX IMPORT_TEMP_PERMNUM ON `Import_Temp` (PERMNUM);

CREATE TABLE `WaterEmsData`.`Import_Unit_Check` (
                                     ORDERNUM VARCHAR(13),
                                     SAMPLEID VARCHAR(7),
                                     PARAM VARCHAR(50),
                                     LABUNIT VARCHAR(10)
);
CREATE INDEX IMPORT_UNIT_CHECK_ORDERNUM ON `Import_Unit_Check` (ORDERNUM);
CREATE INDEX IMPORT_UNIT_CHECK_SAMPLEID ON `Import_Unit_Check` (SAMPLEID);

CREATE TABLE `WaterEmsData`.`NPDES$_ImportErrors` (
                                       `Error` VARCHAR(255),
                                       `Field` VARCHAR(255),
                                       `Row` INTEGER
);

CREATE TABLE `WaterEmsData`.`Paste Errors` (
                                `Field0` TEXT(21300),
                                `Field1` TEXT(21300),
                                `Field2` TEXT(21300),
                                `Field3` TEXT(21300),
                                `Field4` TEXT(21300),
                                `Field5` TEXT(21300),
                                `Field6` TEXT(21300),
                                `Field7` TEXT(21300),
                                `Field8` TEXT(21300),
                                `Field9` TEXT(21300),
                                `Field10` TEXT(21300),
                                `Field11` TEXT(21300),
                                `Field12` TEXT(21300)
);

CREATE TABLE `WaterEmsData`.`SARA_Release_Estimates` (
                                          `YEAR` VARCHAR(255),
                                          OBJID VARCHAR(25),
                                          PARAM VARCHAR(50),
                                          SAMPDATE DATETIME,
                                          QUAL VARCHAR(8),
                                          `RESULT` NUMERIC(65,7),
                                          UNIT VARCHAR(10),
                                          FLOWRATE NUMERIC(65,7),
                                          DAYMASSLOAD VARCHAR(255),
                                          DAYMASSUNIT VARCHAR(255),
                                          `IntervalDays` SMALLINT,
                                          INTMASSLOAD VARCHAR(255),
                                          INTMASSUNIT VARCHAR(255)
);
CREATE INDEX SARA_RELEASE_ESTIMATES_OBJID ON `SARA_Release_Estimates` (OBJID);

CREATE TABLE `WaterEmsData`.`ZZTempTest` (
                              HLALABID VARCHAR(25),
                              PARAM VARCHAR(50),
                              `RESULT` NUMERIC(65,7),
                              LABRESULT VARCHAR(10),
                              ANALDATE DATETIME
);