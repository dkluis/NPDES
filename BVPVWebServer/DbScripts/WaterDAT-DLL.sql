CREATE TABLE ARCODMR (
                         PERMNUM VARCHAR(15) NOT NULL,
                         DMRPAGE VARCHAR(15) NOT NULL,
                         PAGENUM SMALLINT DEFAULT 1 NOT NULL,
                         OBJID VARCHAR(25),
                         PAGETOT SMALLINT DEFAULT 0,
                         CONSTRAINT SYS_PK_10608 PRIMARY KEY (PERMNUM,DMRPAGE,PAGENUM)
);
CREATE INDEX ARCODMR_OBJID ON ARCODMR (OBJID);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10608_10609 ON ARCODMR (PERMNUM,DMRPAGE,PAGENUM);

CREATE TABLE `ARCOParamList` (
                                 PARAM VARCHAR(50) NOT NULL,
                                 PARAMDESC VARCHAR(150),
                                 PARAMUNITS VARCHAR(10),
                                 MASSLIMIT BOOLEAN DEFAULT true,
                                 DMRREPORTNUM VARCHAR(255),
                                 `Field1` VARCHAR(255),
                                 CONSTRAINT SYS_PK_10666 PRIMARY KEY (PARAM)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10666_10667 ON `ARCOParamList` (PARAM);

CREATE TABLE ARCODMRPARAM (
                              PERMNUM VARCHAR(15) NOT NULL,
                              DMRPAGE VARCHAR(15) NOT NULL,
                              PAGENUM SMALLINT DEFAULT 1 NOT NULL,
                              ORDERNUM SMALLINT NOT NULL,
                              PARAM VARCHAR(50),
                              FREQ VARCHAR(15),
                              SAMPTYPE VARCHAR(15),
                              CONSTRAINT SYS_PK_10616 PRIMARY KEY (PERMNUM,DMRPAGE,PAGENUM,ORDERNUM),
                              CONSTRAINT `ARCODMRPARAM_{D547B3AA-5E24-48CA-86F5-71A438D02F25}` FOREIGN KEY (PARAM) REFERENCES `ARCOParamList`(PARAM) ON UPDATE CASCADE
);
CREATE INDEX `SYS_IDX_ARCODMRPARAM_{D547B3AA-5E24-48CA-86F5-71A438D02F25}_10736` ON ARCODMRPARAM (PARAM);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10616_10617 ON ARCODMRPARAM (PERMNUM,DMRPAGE,PAGENUM,ORDERNUM);

CREATE TABLE `ARCOObjectInfo` (
                                  OBJID VARCHAR(25) NOT NULL,
                                  OBJDESC VARCHAR(120),
                                  OBJLAT VARCHAR(12),
                                  OBJLONG VARCHAR(12),
                                  OBJTYPE VARCHAR(50),
                                  COMMENT VARCHAR(100),
                                  `SOURCE` VARCHAR(40),
                                  ENTERDATE DATETIME(3) DEFAULT CURRENT_TIMESTAMP,
                                  CONSTRAINT SYS_PK_10661 PRIMARY KEY (OBJID)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10661_10662 ON `ARCOObjectInfo` (OBJID);

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
                            CONSTRAINT `ARCOFLOW_{2A8CE72C-34E8-4EBC-A761-5679950CEAFB}` FOREIGN KEY (OBJID) REFERENCES `ARCOObjectInfo`(OBJID) ON UPDATE CASCADE
);
CREATE INDEX `SYS_IDX_ARCOFLOW_{2A8CE72C-34E8-4EBC-A761-5679950CEAFB}_10742` ON `ARCOFlow` (OBJID);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10630_10631 ON `ARCOFlow` (OBJID,SAMPDATE);

CREATE TABLE `ARCOPermit` (
                              `PermNum` VARCHAR(15) NOT NULL,
                              `PermDesc` VARCHAR(240),
                              `IssueDate` TIMESTAMP,
                              `ExpirDate` TIMESTAMP,
                              `Comments` VARCHAR(240),
                              `Source` VARCHAR(40),
                              `EnterDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
                              CONSTRAINT SYS_PK_10672 PRIMARY KEY (`PermNum`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10672_10673 ON `ARCOPermit` (`PermNum`);

CREATE TABLE `ARCOLimits` (
                              PERMNUM VARCHAR(15) NOT NULL,
                              PARAM VARCHAR(50) NOT NULL,
                              ACTIVE BOOLEAN DEFAULT true NOT NULL,
                              OBJID VARCHAR(25) NOT NULL,
                              LIMTYPE VARCHAR(20) NOT NULL,
                              MESSAGE VARCHAR(20) NOT NULL,
                              LIMVAL NUMERIC(65,7) DEFAULT 0,
                              CONSTRAINT SYS_PK_10645 PRIMARY KEY (PERMNUM,PARAM,ACTIVE,OBJID,LIMTYPE,MESSAGE),
                              CONSTRAINT `ARCOLIMITS_{36D2F3DE-A1F3-41C6-A85E-C8B7328DCF25}` FOREIGN KEY (OBJID) REFERENCES `ARCOObjectInfo`(OBJID) ON UPDATE CASCADE,
                              CONSTRAINT `ARCOLIMITS_{9CF6D6F8-5CBC-4853-B35B-68D9BC9FBBAA}` FOREIGN KEY (PARAM) REFERENCES `ARCOParamList`(PARAM) ON UPDATE CASCADE,
                              CONSTRAINT `ARCOLIMITS_{CC1BE19A-46FB-4094-BB72-3CE3D31FD85B}` FOREIGN KEY (PERMNUM) REFERENCES `ARCOPermit`(`PermNum`) ON UPDATE CASCADE
);
CREATE INDEX `SYS_IDX_ARCOLIMITS_{36D2F3DE-A1F3-41C6-A85E-C8B7328DCF25}_10753` ON `ARCOLimits` (OBJID);
CREATE INDEX `SYS_IDX_ARCOLIMITS_{9CF6D6F8-5CBC-4853-B35B-68D9BC9FBBAA}_10765` ON `ARCOLimits` (PARAM);
CREATE INDEX `SYS_IDX_ARCOLIMITS_{CC1BE19A-46FB-4094-BB72-3CE3D31FD85B}_10777` ON `ARCOLimits` (PERMNUM);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10645_10646 ON `ARCOLimits` (PERMNUM,PARAM,ACTIVE,OBJID,LIMTYPE,MESSAGE);