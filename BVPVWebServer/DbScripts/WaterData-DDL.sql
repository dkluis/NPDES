CREATE TABLE ARCODMR (
                         PERMNUM VARCHAR(15) NOT NULL,
                         DMRPAGE VARCHAR(15) NOT NULL,
                         PAGENUM SMALLINT DEFAULT 1 NOT NULL,
                         OBJID VARCHAR(25),
                         PAGETOT SMALLINT DEFAULT 0,
                         CONSTRAINT SYS_PK_10608 PRIMARY KEY (PERMNUM,DMRPAGE,PAGENUM)
);
CREATE INDEX ARCODMR_OBJID ON ARCODMR (OBJID);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_ARCODMR ON ARCODMR (PERMNUM,DMRPAGE,PAGENUM);

CREATE TABLE `ARCOLimType` (
                               `LimType` VARCHAR(20) NOT NULL,
                               `LimDesc` VARCHAR(200),
                               CONSTRAINT SYS_PK_10656 PRIMARY KEY (`LimType`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_ARCOLimTYpe ON `ARCOLimType` (`LimType`);

CREATE TABLE `ARCOObjectInfo` (
                                  OBJID VARCHAR(25) NOT NULL,
                                  OBJDESC VARCHAR(120),
                                  OBJLAT VARCHAR(12),
                                  OBJLONG VARCHAR(12),
                                  OBJTYPE VARCHAR(50),
                                  COMMENT VARCHAR(100),
                                  `SOURCE` VARCHAR(40),
                                  ENTERDATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                                  CONSTRAINT SYS_PK_10661 PRIMARY KEY (OBJID)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_ARCOObjectInfo ON `ARCOObjectInfo` (OBJID);


CREATE TABLE `ARCOParamList` (
                                 PARAM VARCHAR(50) NOT NULL,
                                 PARAMDESC VARCHAR(150),
                                 PARAMUNITS VARCHAR(10),
                                 MASSLIMIT BOOLEAN DEFAULT true,
                                 DMRREPORTNUM VARCHAR(255),
                                 `Field1` VARCHAR(255),
                                 CONSTRAINT SYS_PK_10666 PRIMARY KEY (PARAM)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_ARCOParamList ON `ARCOParamList` (PARAM);

CREATE TABLE `ARCOPermit` (
                              `PermNum` VARCHAR(15) NOT NULL,
                              `PermDesc` VARCHAR(240),
                              `IssueDate` TIMESTAMP,
                              `ExpirDate` TIMESTAMP,
                              `Comments` VARCHAR(240),
                              `Source` VARCHAR(40),
                              `EnterDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                              CONSTRAINT SYS_PK_10672 PRIMARY KEY (`PermNum`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_ARCOPermit ON `ARCOPermit` (`PermNum`);

CREATE TABLE `ARCOUnits` (
                             `UnitType` VARCHAR(10) NOT NULL,
                             `UnitDesc` VARCHAR(80),
                             CONSTRAINT SYS_PK_10682 PRIMARY KEY (`UnitType`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_ARCOPermits ON `ARCOUnits` (`UnitType`);

CREATE TABLE `Conversion Errors` (
                                     `Object Type` VARCHAR(255),
                                     `Object Name` VARCHAR(255),
                                     `Error Description` VARCHAR(21300)
);

CREATE TABLE `Current Year` (
                                `CurYear` SMALLINT,
                                `CurMon` SMALLINT,
                                `StartDate` TIMESTAMP,
                                `EndDate` TIMESTAMP,
                                `PermNum` VARCHAR(25)
);
CREATE INDEX `CURRENT YEAR_PERMNUM` ON `Current Year` (`PermNum`);

CREATE TABLE `LABENTRY_Effluent and Process Water Report` (
                                                              SAMPLEIDPREFIX VARCHAR(16) NOT NULL,
                                                              FIELDNUM SMALLINT NOT NULL,
                                                              FORMORDER SMALLINT,
                                                              RPTORDER SMALLINT,
                                                              OBJID VARCHAR(25),
                                                              SAMPTYPE VARCHAR(18),
                                                              PARAM VARCHAR(50),
                                                              PARAMUNITS VARCHAR(10),
                                                              `METHOD` VARCHAR(15),
                                                              DATAUSE VARCHAR(10) DEFAULT 'ALL',
                                                              ENTRYNOTE VARCHAR(120),
                                                              CONSTRAINT SYS_PK_10707 PRIMARY KEY (SAMPLEIDPREFIX,FIELDNUM)
);
CREATE INDEX `LABENTRY_EFFLUENT AND PROCESS WATER REPORT_OBJID` ON `LABENTRY_Effluent and Process Water Report` (OBJID);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_LabENTRY_Effluent ON `LABENTRY_Effluent and Process Water Report` (SAMPLEIDPREFIX,FIELDNUM);

CREATE TABLE `Paste Errors` (
                                F1 VARCHAR(255),
                                F2 TIMESTAMP,
                                F3 DOUBLE,
                                F4 VARCHAR(255)
);

CREATE TABLE ARCODMRPARAM (
                              PERMNUM VARCHAR(15) NOT NULL,
                              DMRPAGE VARCHAR(15) NOT NULL,
                              PAGENUM SMALLINT DEFAULT 1 NOT NULL,
                              ORDERNUM SMALLINT NOT NULL,
                              PARAM VARCHAR(50),
                              FREQ VARCHAR(15),
                              SAMPTYPE VARCHAR(15),
                              CONSTRAINT SYS_PK_10616 PRIMARY KEY (PERMNUM,DMRPAGE,PAGENUM,ORDERNUM),
                              CONSTRAINT `ARCODMRPARAM_Constraint_ArcoParamList` FOREIGN KEY (PARAM) REFERENCES `ARCOParamList`(PARAM) ON UPDATE CASCADE
);
CREATE INDEX `SYS_IDX_ARCODMRPARAM_Constraint_ArcoParamList` ON ARCODMRPARAM (PARAM);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_ARCODMRPARAM ON ARCODMRPARAM (PERMNUM,DMRPAGE,PAGENUM,ORDERNUM);

CREATE TABLE `ARCOFlow` (
                            OBJID VARCHAR(25) NOT NULL,
                            SAMPDATE TIMESTAMP NOT NULL,
                            FLOWRATE NUMERIC(65,7),
                            FLOWGPM NUMERIC(65,7),
                            WEIR1 NUMERIC(65,7),
                            WEIR2 NUMERIC(65,7),
                            WEIR3 NUMERIC(65,7),
                            ENTER VARCHAR(20),
                            CONSTRAINT SYS_PK_10630 PRIMARY KEY (OBJID,SAMPDATE),
                            CONSTRAINT `ARCOFLOW_Constraint_ArcoObjectInfo` FOREIGN KEY (OBJID) REFERENCES `ARCOObjectInfo`(OBJID) ON UPDATE CASCADE
);
CREATE INDEX `SYS_IDX_ARCOFLOW_Constraint_ArcoObjectInfo` ON `ARCOFlow` (OBJID);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_ARCOFLOW ON `ARCOFlow` (OBJID,SAMPDATE);

CREATE TABLE `ARCOLimits` (
                              PERMNUM VARCHAR(15) NOT NULL,
                              PARAM VARCHAR(50) NOT NULL,
                              ACTIVE BOOLEAN DEFAULT true NOT NULL,
                              OBJID VARCHAR(25) NOT NULL,
                              LIMTYPE VARCHAR(20) NOT NULL,
                              MESSAGE VARCHAR(20) NOT NULL,
                              LIMVAL NUMERIC(65,7) DEFAULT 0,
                              CONSTRAINT SYS_PK_10645 PRIMARY KEY (PERMNUM,PARAM,ACTIVE,OBJID,LIMTYPE,MESSAGE),
                              CONSTRAINT `ARCOLIMITS_ARCOObjectInfo` FOREIGN KEY (OBJID) REFERENCES `ARCOObjectInfo`(OBJID) ON UPDATE CASCADE,
                              CONSTRAINT `ARCOLIMITS_ARCOParamList` FOREIGN KEY (PARAM) REFERENCES `ARCOParamList`(PARAM) ON UPDATE CASCADE,
                              CONSTRAINT `ARCOLIMITS_ARCOPermit` FOREIGN KEY (PERMNUM) REFERENCES `ARCOPermit`(`PermNum`) ON UPDATE CASCADE
);
CREATE INDEX `SYS_IDX_ARCOLIMITS_ARCOObjectInfo` ON `ARCOLimits` (OBJID);
CREATE INDEX `SYS_IDX_ARCOLIMITS_ARCOParamList` ON `ARCOLimits` (PARAM);
CREATE INDEX `SYS_IDX_ARCOLIMITS_ARCOPermit` ON `ARCOLimits` (PERMNUM);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_ARCOLimits ON `ARCOLimits` (PERMNUM,PARAM,ACTIVE,OBJID,LIMTYPE,MESSAGE);

CREATE TABLE `ARCOSampInfo` (
                                HLALABID VARCHAR(25) NOT NULL,
                                OBJID VARCHAR(25),
                                PERMNUM VARCHAR(15),
                                ORDERNUM VARCHAR(13),
                                SAMPLEID VARCHAR(7),
                                SAMPTYPE VARCHAR(18),
                                SAMPBY VARCHAR(15),
                                COLLDATE TIMESTAMP,
                                COLLTIME TIMESTAMP,
                                SAMPDATE TIMESTAMP,
                                LABNAME VARCHAR(25),
                                RECDATE TIMESTAMP,
                                COMMENT VARCHAR(140),
                                ENTERDATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                                `SOURCE` VARCHAR(150),
                                CONSTRAINT SYS_PK_10677 PRIMARY KEY (HLALABID),
                                CONSTRAINT `ARCOSAMPINFO_ARCOObjectInfo` FOREIGN KEY (OBJID) REFERENCES `ARCOObjectInfo`(OBJID) ON UPDATE CASCADE,
                                CONSTRAINT `ARCOSAMPINFO_ARCOPermit` FOREIGN KEY (PERMNUM) REFERENCES `ARCOPermit`(`PermNum`) ON UPDATE CASCADE
);
CREATE INDEX ARCOSAMPINFO_ORDERNUM ON `ARCOSampInfo` (ORDERNUM);
CREATE INDEX ARCOSAMPINFO_SAMPLEID ON `ARCOSampInfo` (SAMPLEID);
CREATE INDEX `SYS_IDX_ARCOSAMPINFO_ARCOObjectInfo` ON `ARCOSampInfo` (OBJID);
CREATE INDEX `SYS_IDX_ARCOSAMPINFO_ARCOPermit` ON `ARCOSampInfo` (PERMNUM);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_ARCOSampleInfo ON `ARCOSampInfo` (HLALABID);

CREATE TABLE `Exceptions_Monthly` (
                                      OBJID VARCHAR(25) NOT NULL,
                                      PARAM VARCHAR(50) NOT NULL,
                                      CURMON SMALLINT DEFAULT 0 NOT NULL,
                                      CURYEAR SMALLINT DEFAULT 0 NOT NULL,
                                      TOTSAMPLE INTEGER DEFAULT 0,
                                      TOTLIMIT INTEGER DEFAULT 0,
                                      TOTEXCUR INTEGER DEFAULT 0,
                                      CONSTRAINT SYS_PK_10690 PRIMARY KEY (OBJID,PARAM,CURMON,CURYEAR),
                                      CONSTRAINT `EXCEPTIONS_MONTHLY_ARCOParamList` FOREIGN KEY (PARAM) REFERENCES `ARCOParamList`(PARAM) ON UPDATE CASCADE,
                                      CONSTRAINT `EXCEPTIONS_MONTHLY_ARCOObjectInfo` FOREIGN KEY (OBJID) REFERENCES `ARCOObjectInfo`(OBJID) ON UPDATE CASCADE
);
CREATE INDEX `SYS_IDX_EXCEPTIONS_MONTHLY_ARCOParamList` ON `Exceptions_Monthly` (PARAM);
CREATE INDEX `SYS_IDX_EXCEPTIONS_MONTHLY_ARCOObjectInfo` ON `Exceptions_Monthly` (OBJID);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_Exceptions_Monthly ON `Exceptions_Monthly` (OBJID,PARAM,CURMON,CURYEAR);

CREATE TABLE `ARCOParam` (
                             HLALABID VARCHAR(25) NOT NULL,
                             PARAM VARCHAR(50),
                             FIELDNUM SMALLINT DEFAULT 1,
                             LABRESULT VARCHAR(10),
                             LABUNIT VARCHAR(10),
                             LABQUAL VARCHAR(8),
                             `RESULT` NUMERIC(65,7) DEFAULT 0,
                             UNIT VARCHAR(10),
                             QUAL VARCHAR(8) DEFAULT 'D',
                             `METHOD` VARCHAR(15),
                             ANALDATE TIMESTAMP,
                             ANALYST VARCHAR(15),
                             DATAUSE VARCHAR(10) DEFAULT 'NPDES',
                             CONSTRAINT `ARCOPARAM_ARCOSampInfo` FOREIGN KEY (HLALABID) REFERENCES `ARCOSampInfo`(HLALABID) ON DELETE CASCADE ON UPDATE CASCADE,
                             CONSTRAINT `ARCOPARAM_ARCOParamList` FOREIGN KEY (PARAM) REFERENCES `ARCOParamList`(PARAM) ON UPDATE CASCADE
);
CREATE INDEX ARCOPARAM_FIELDNUM ON `ARCOParam` (FIELDNUM);
CREATE INDEX `SYS_IDX_ARCOPARAM_ARCOSampInfo_10789` ON `ARCOParam` (HLALABID);
CREATE INDEX `SYS_IDX_ARCOPARAM_ARCOParamList_10797` ON `ARCOParam` (PARAM);