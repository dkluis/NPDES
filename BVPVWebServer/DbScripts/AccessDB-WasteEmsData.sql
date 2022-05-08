CREATE TABLE `WasteEmsData`.`Biennual_Haz_Previous_Year` (
	`ProfileNumber` VARCHAR(18),
	`SumOfQuantity` DOUBLE
);

CREATE TABLE `WasteEmsData`.`Biennual_Haz_Profiles` (
	`ProfileNumber` VARCHAR(18) NOT NULL,
	`CurTotal` DOUBLE DEFAULT 0,
	`PrevTotal` DOUBLE DEFAULT 0,
	CONSTRAINT SYS_PK_10935 PRIMARY KEY (`ProfileNumber`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_Biennual_Haz_Profiles ON `Biennual_Haz_Profiles` (`ProfileNumber`);

CREATE TABLE `WasteEmsData`.BIENPRT (
	`Profile Number` VARCHAR(18),
	`Common Waste Name` VARCHAR(50)
);

CREATE TABLE `WasteEmsData`.BIENPRT1 (
	`ProfileNumber` VARCHAR(18),
	`WasteDescription` VARCHAR(255),
	`SourceCode` VARCHAR(4),
	`TRIBox` BOOLEAN,
	`SICCode` VARCHAR(5),
	`Comments` VARCHAR(255)
);
CREATE INDEX BIENPRT1_SICCODE ON BIENPRT1 (`SICCode`);
CREATE INDEX BIENPRT1_SOURCECODE ON BIENPRT1 (`SourceCode`);

CREATE TABLE `WasteEmsData`.`BIENPRT1 (2000 RW Report)` (
	`ProfileNumber` VARCHAR(18),
	`WasteDescription` VARCHAR(255),
	`SourceCode` VARCHAR(4),
	`TRIBox` BOOLEAN,
	`SICCode` VARCHAR(4),
	`Comments` VARCHAR(255)
);
CREATE INDEX `BIENPRT1 (2000 RW REPORT)_SICCODE` ON `BIENPRT1 (2000 RW Report)` (`SICCode`);
CREATE INDEX `BIENPRT1 (2000 RW REPORT)_SOURCECODE` ON `BIENPRT1 (2000 RW Report)` (`SourceCode`);

CREATE TABLE `WasteEmsData`.`BIENPRT1 (2002 RW Report)` (
	`ProfileNumber` VARCHAR(18),
	`WasteDescription` VARCHAR(255),
	`SourceCode` VARCHAR(4),
	`TRIBox` BOOLEAN,
	`SICCode` VARCHAR(5),
	`Comments` VARCHAR(255)
);
CREATE INDEX `BIENPRT1 (2002 RW REPORT)_SICCODE` ON `BIENPRT1 (2002 RW Report)` (`SICCode`);
CREATE INDEX `BIENPRT1 (2002 RW REPORT)_SOURCECODE` ON `BIENPRT1 (2002 RW Report)` (`SourceCode`);

CREATE TABLE `WasteEmsData`.`Bienprt1_backup` (
	`ProfileNumber` VARCHAR(18),
	`WasteDescription` VARCHAR(255),
	`SourceCode` VARCHAR(4),
	`TRIBox` BOOLEAN,
	`SICCode` VARCHAR(4),
	`Comments` VARCHAR(255)
);
CREATE INDEX BIENPRT1_BACKUP_SICCODE ON `Bienprt1_backup` (`SICCode`);
CREATE INDEX BIENPRT1_BACKUP_SOURCECODE ON `Bienprt1_backup` (`SourceCode`);

CREATE TABLE `WasteEmsData`.BIENPRT4 (
	`ProfileNumber` VARCHAR(18),
	`BlockNo` SMALLINT,
	`TSDFacility` VARCHAR(40),
	`SystemDescription` VARCHAR(42),
	`Unit/SystemCode` VARCHAR(4),
	`PhysicalState` VARCHAR(3),
	`TSDCode` VARCHAR(13),
	`TSDAddress` VARCHAR(75),
	`CurYr` DOUBLE,
	`Captive` VARCHAR(1)
);
CREATE INDEX BIENPRT4_TSDCODE ON BIENPRT4 (`TSDCode`);
CREATE INDEX `BIENPRT4_UNIT/SYSTEMCODE` ON BIENPRT4 (`Unit/SystemCode`);

CREATE TABLE `WasteEmsData`.`BIENPRT4 (2000 RW Report)` (
	`ProfileNumber` VARCHAR(18),
	`BlockNo` SMALLINT,
	`TSDFacility` VARCHAR(40),
	`SystemDescription` VARCHAR(42),
	`Unit/SystemCode` VARCHAR(4),
	`PhysicalState` VARCHAR(3),
	`TSDCode` VARCHAR(13),
	`TSDAddress` VARCHAR(75),
	`CurYr` DOUBLE,
	`Captive` VARCHAR(1)
);
CREATE INDEX `BIENPRT4 (2000 RW REPORT)_TSDCODE` ON `BIENPRT4 (2000 RW Report)` (`TSDCode`);
CREATE INDEX `BIENPRT4 (2000 RW REPORT)_UNIT/SYSTEMCODE` ON `BIENPRT4 (2000 RW Report)` (`Unit/SystemCode`);

CREATE TABLE `WasteEmsData`.`BIENPRT4 (2002 RW Report)` (
	`ProfileNumber` VARCHAR(18),
	`BlockNo` SMALLINT,
	`TSDFacility` VARCHAR(40),
	`SystemDescription` VARCHAR(42),
	`Unit/SystemCode` VARCHAR(4),
	`PhysicalState` VARCHAR(3),
	`TSDCode` VARCHAR(13),
	`TSDAddress` VARCHAR(75),
	`CurYr` DOUBLE,
	`Captive` VARCHAR(1)
);
CREATE INDEX `BIENPRT4 (2002 RW REPORT)_TSDCODE` ON `BIENPRT4 (2002 RW Report)` (`TSDCode`);
CREATE INDEX `BIENPRT4 (2002 RW REPORT)_UNIT/SYSTEMCODE` ON `BIENPRT4 (2002 RW Report)` (`Unit/SystemCode`);

CREATE TABLE `WasteEmsData`.`bienprt4_backup` (
	`ProfileNumber` VARCHAR(18),
	`BlockNo` SMALLINT,
	`TSDFacility` VARCHAR(40),
	`SystemDescription` VARCHAR(42),
	`Unit/SystemCode` VARCHAR(4),
	`PhysicalState` VARCHAR(3),
	`TSDCode` VARCHAR(13),
	`TSDAddress` VARCHAR(75),
	`CurYr` DOUBLE,
	`Captive` VARCHAR(1)
);
CREATE INDEX BIENPRT4_BACKUP_TSDCODE ON `bienprt4_backup` (`TSDCode`);
CREATE INDEX `BIENPRT4_BACKUP_UNIT/SYSTEMCODE` ON `bienprt4_backup` (`Unit/SystemCode`);

CREATE TABLE `WasteEmsData`.BIENPRT5 (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`OnsiteProcessing1` VARCHAR(2),
	`OnsiteProcessing2` VARCHAR(2),
	`SystemQuantity1` DOUBLE,
	`PhysicalState1` VARCHAR(2),
	`UnitType1` VARCHAR(2),
	`SystemQuantity2` DOUBLE,
	`PhysicalState2` VARCHAR(2),
	`UnitType2` VARCHAR(2)
);

CREATE TABLE `WasteEmsData`.`BIENPRT5 (2000 RW Report)` (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`OnsiteProcessing1` VARCHAR(2),
	`OnsiteProcessing2` VARCHAR(2),
	`SystemQuantity1` DOUBLE,
	`PhysicalState1` VARCHAR(2),
	`UnitType1` VARCHAR(2),
	`SystemQuantity2` DOUBLE,
	`PhysicalState2` VARCHAR(2),
	`UnitType2` VARCHAR(2)
);

CREATE TABLE `WasteEmsData`.`BIENPRT5 (2002 RW Report)` (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`OnsiteProcessing1` VARCHAR(2),
	`OnsiteProcessing2` VARCHAR(2),
	`SystemQuantity1` DOUBLE,
	`PhysicalState1` VARCHAR(2),
	`UnitType1` VARCHAR(2),
	`SystemQuantity2` DOUBLE,
	`PhysicalState2` VARCHAR(2),
	`UnitType2` VARCHAR(2)
);

CREATE TABLE `WasteEmsData`.BIENPRT6 (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`ActivityCode1` VARCHAR(2),
	`ActivityCode2` VARCHAR(2),
	`ActivityCode3` VARCHAR(2),
	`ToxicityReductYes` VARCHAR(1),
	`ToxicityReductNo` VARCHAR(1),
	`ToxicityText` VARCHAR(255),
	`PrevYr` DOUBLE DEFAULT 0,
	`CurYr` DOUBLE,
	`Activity Index-CurYr` VARCHAR(10),
	`Adjusted-PrevYr` DOUBLE,
	`Adjusted-CurYr` DOUBLE,
	`CurYrReduction` DOUBLE DEFAULT 0
);

CREATE TABLE `WasteEmsData`.`BIENPRT6 (2000 RW Report)` (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`ActivityCode1` VARCHAR(2),
	`ActivityCode2` VARCHAR(2),
	`ActivityCode3` VARCHAR(2),
	`ToxicityReductYes` VARCHAR(1),
	`ToxicityReductNo` VARCHAR(1),
	`ToxicityText` VARCHAR(255),
	`PrevYr` DOUBLE DEFAULT 0,
	`CurYr` DOUBLE,
	`Activity Index-CurYr` VARCHAR(10),
	`Adjusted-PrevYr` DOUBLE,
	`Adjusted-CurYr` DOUBLE,
	`CurYrReduction` DOUBLE DEFAULT 0
);

CREATE TABLE `WasteEmsData`.`BIENPRT6 (2002 RW Report)` (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`ActivityCode1` VARCHAR(2),
	`ActivityCode2` VARCHAR(2),
	`ActivityCode3` VARCHAR(2),
	`ToxicityReductYes` VARCHAR(1),
	`ToxicityReductNo` VARCHAR(1),
	`ToxicityText` VARCHAR(255),
	`PrevYr` DOUBLE DEFAULT 0,
	`CurYr` DOUBLE,
	`Activity Index-CurYr` VARCHAR(10),
	`Adjusted-PrevYr` DOUBLE,
	`Adjusted-CurYr` DOUBLE,
	`CurYrReduction` DOUBLE DEFAULT 0
);

CREATE TABLE `WasteEmsData`.`Bienprt6_backup` (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`ActivityCode1` VARCHAR(2),
	`ActivityCode2` VARCHAR(2),
	`ActivityCode3` VARCHAR(2),
	`ToxicityReductYes` VARCHAR(1),
	`ToxicityReductNo` VARCHAR(1),
	`ToxicityText` VARCHAR(255),
	`PrevYr` DOUBLE DEFAULT 0,
	`CurYr` DOUBLE,
	`Activity Index-CurYr` VARCHAR(10),
	`Adjusted-PrevYr` DOUBLE,
	`Adjusted-CurYr` DOUBLE,
	`CurYrReduction` DOUBLE DEFAULT 0
);

CREATE TABLE `WasteEmsData`.BIENPT4A (
	`Profile Number` VARCHAR(18),
	`TSD Facility` VARCHAR(40),
	`Annual total` DOUBLE
);

CREATE TABLE `WasteEmsData`.BIENPT4B (
	`ProfileNumber` VARCHAR(18),
	`TSDFacility` VARCHAR(40),
	`Unit/SystemCode` VARCHAR(4),
	`SystemDescription` VARCHAR(42),
	`PhysicalState` VARCHAR(3),
	`TSDCode` VARCHAR(13),
	`TSDAddress` VARCHAR(75),
	`CurYr` DOUBLE,
	`Captive` VARCHAR(1)
);
CREATE INDEX BIENPT4B_PROFILE ON BIENPT4B (`ProfileNumber`,`TSDFacility`);
CREATE INDEX BIENPT4B_TSDCODE ON BIENPT4B (`TSDCode`);
CREATE INDEX `BIENPT4B_UNIT/SYSTEMCODE` ON BIENPT4B (`Unit/SystemCode`);

CREATE TABLE `WasteEmsData`.BIENPT5B (
	`Profile Number` VARCHAR(18),
	`Common Waste Name` VARCHAR(50),
	`System Quantity1` DOUBLE
);

CREATE TABLE `WasteEmsData`.BIENPT6A (
	`ProfileNumber` VARCHAR(18),
	`CurYr` DOUBLE,
	`PrevYr` DOUBLE DEFAULT 0
);

CREATE TABLE `WasteEmsData`.BIENPT6B (
	`ProfileNumber` VARCHAR(18),
	`PrevYr` DOUBLE
);

CREATE TABLE `WasteEmsData`.`Conversion Errors` (
	`Object Type` VARCHAR(255),
	`Object Name` VARCHAR(255),
	`Error Description` VARCHAR(21300)
);

CREATE TABLE `WasteEmsData`.`Current Year` (
	`CurYear` SMALLINT,
	`BegMon` SMALLINT,
	`CurMon` SMALLINT,
	`StartDate` TIMESTAMP,
	`EndDate` TIMESTAMP,
	`PreStartDate` TIMESTAMP,
	`PreEndDate` TIMESTAMP
);

CREATE TABLE `WasteEmsData`.`Form 26R1` (
	`ShippedOffSite` TIMESTAMP,
	`Manifest/DocumentNo` VARCHAR(13),
	`HAZ/NON` VARCHAR(3),
	`SourceDept` VARCHAR(20),
	`QuantityShipped` DOUBLE,
	`Units` VARCHAR(3),
	`ProfileNumber` VARCHAR(18),
	OFFSITE VARCHAR(40),
	PH VARCHAR(7),
	`PhysicalState` VARCHAR(1),
	`Color` VARCHAR(15),
	`Layers` VARCHAR(5),
	`Odor` VARCHAR(15)
);

CREATE TABLE `WasteEmsData`.`Form26R_Backup1` (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`WasteDescription` VARCHAR(255),
	`SourceCode` VARCHAR(4),
	PH VARCHAR(7),
	`PhysicalState` VARCHAR(1),
	`Color` VARCHAR(15),
	`Layers` VARCHAR(5),
	`LayerDescription` VARCHAR(120),
	`GenPropDate` TIMESTAMP,
	`ChemAnalDate` TIMESTAMP,
	`ProcDescDate` TIMESTAMP,
	`BenefUse` BOOLEAN,
	`ResponsibleOfficial` VARCHAR(40),
	`ResponsibleTitle` VARCHAR(40),
	YR VARCHAR(4)
);
CREATE INDEX FORM26R_BACKUP1_SOURCECODE ON `Form26R_Backup1` (`SourceCode`);

CREATE TABLE `WasteEmsData`.`Form26R_Backup2` (
	`ProfileNumber` VARCHAR(18),
	`BlockNo` SMALLINT,
	OFFSITE VARCHAR(40),
	ADDRESS VARCHAR(40),
	ZIP VARCHAR(10),
	MUNICIPALITY VARCHAR(30),
	COUNTY VARCHAR(15),
	`Permit_Num` VARCHAR(13),
	OFFNAME VARCHAR(40),
	OFFTITLE VARCHAR(20),
	OFFPHONE VARCHAR(20),
	`CurYr_Tons` DOUBLE
);
CREATE INDEX FORM26R_BACKUP2_BLCKNUM ON `Form26R_Backup2` (`BlockNo`);
CREATE INDEX FORM26R_BACKUP2_PERMIT_NUM ON `Form26R_Backup2` (`Permit_Num`);

CREATE TABLE `WasteEmsData`.`Form26RPrt1` (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`WasteDescription` VARCHAR(255),
	`SourceCode` VARCHAR(4),
	PH VARCHAR(7),
	`PhysicalState` VARCHAR(1),
	`Color` VARCHAR(15),
	`Layers` VARCHAR(5),
	`LayerDescription` VARCHAR(120),
	`GenPropDate` TIMESTAMP,
	`ChemAnalDate` TIMESTAMP,
	`ProcDescDate` TIMESTAMP,
	`BenefUse` BOOLEAN,
	`ResponsibleOfficial` VARCHAR(40),
	`ResponsibleTitle` VARCHAR(40),
	YR VARCHAR(4),
	`OneTime` BOOLEAN,
	`TimeFrame` VARCHAR(20),
	`2a` BOOLEAN,
	`2b` BOOLEAN,
	`2c` BOOLEAN,
	`2d` BOOLEAN,
	`2e` BOOLEAN,
	`2eNA` BOOLEAN,
	`3a` BOOLEAN,
	`3b` BOOLEAN,
	`3c` BOOLEAN,
	`3cNA` BOOLEAN,
	`Odor` VARCHAR(15)
);
CREATE INDEX FORM26RPRT1_SOURCECODE ON `Form26RPrt1` (`SourceCode`);

CREATE TABLE `WasteEmsData`.`Form26RPrt2` (
	`ProfileNumber` VARCHAR(18),
	`BlockNo` SMALLINT,
	OFFSITE VARCHAR(40),
	ADDRESS VARCHAR(40),
	`State` VARCHAR(2),
	ZIP VARCHAR(10),
	MUNICIPALITY VARCHAR(30),
	COUNTY VARCHAR(15),
	`Permit_Num` VARCHAR(13),
	OFFNAME VARCHAR(40),
	OFFTITLE VARCHAR(20),
	OFFPHONE VARCHAR(20),
	`CurYr_Tons` DOUBLE
);
CREATE INDEX FORM26RPRT2_BLCKNUM ON `Form26RPrt2` (`BlockNo`);
CREATE INDEX FORM26RPRT2_PERMIT_NUM ON `Form26RPrt2` (`Permit_Num`);

CREATE TABLE `WasteEmsData`.`Form26RPrt2B` (
	`ProfileNumber` VARCHAR(18),
	OFFSITE VARCHAR(40),
	ADDRESS VARCHAR(40),
	`State` VARCHAR(2),
	ZIP VARCHAR(10),
	MUNICIPALITY VARCHAR(30),
	COUNTY VARCHAR(15),
	`Permit_Num` VARCHAR(13),
	OFFNAME VARCHAR(40),
	OFFTITLE VARCHAR(20),
	OFFPHONE VARCHAR(20),
	`CurYr_Tons` DOUBLE
);
CREATE INDEX FORM26RPRT2B_PERMIT_NUM ON `Form26RPrt2B` (`Permit_Num`);
CREATE INDEX FORM26RPRT2B_PROFILE ON `Form26RPrt2B` (`ProfileNumber`,OFFSITE);

CREATE TABLE `WasteEmsData`.`Paste Errors` (
	`ShippedOffSite` TIMESTAMP,
	`Manifest/DocumnetNo` VARCHAR(255),
	`lineItem` DOUBLE,
	`ProfileNumber` VARCHAR(255),
	`HAZ/NON` VARCHAR(255),
	`Routine` BOOLEAN,
	`Transporter` VARCHAR(255),
	`StateTranspID` VARCHAR(255),
	`CostCenter` VARCHAR(255),
	`SourceDept` VARCHAR(255),
	`QuantityShipped` DOUBLE,
	`Units` VARCHAR(255),
	`Quanitity` DOUBLE,
	`NumberOfContainers` DOUBLE,
	`ContainerType` VARCHAR(255),
	`HazardCharacteristics` VARCHAR(255),
	`EPA/StateWasteCode` VARCHAR(255),
	`ProperShippingName` VARCHAR(255),
	`Destination` VARCHAR(255),
	`DestCode` VARCHAR(255),
	`SystemDescription` VARCHAR(255),
	`MethdDisposal` VARCHAR(255),
	`UltimateDisposal` VARCHAR(255),
	OFFSITE VARCHAR(255)
);

CREATE TABLE PROFILE_INFORMATION2 (
	`ProfileNumber` VARCHAR(18) NOT NULL,
	`CommonWasteName` VARCHAR(55),
	`WasteDescription` VARCHAR(255),
	`HAZ/NON` VARCHAR(3),
	`Routine` BOOLEAN DEFAULT true,
	`TRIBox` BOOLEAN DEFAULT false,
	`SICCode` VARCHAR(4),
	`CostCenter` VARCHAR(7),
	`SourceDept` VARCHAR(20),
	`SourceActivity` VARCHAR(40),
	`SourceCode` VARCHAR(4),
	`SourceDescription` VARCHAR(56),
	`HazardCharacteristics` VARCHAR(60),
	`EPA/StateWasteCode` VARCHAR(60),
	`ProperShippingName` VARCHAR(100),
	`HazardClass` VARCHAR(12),
	`UN/NANumber` VARCHAR(10),
	`PackingGroup` VARCHAR(5),
	`Destination` VARCHAR(33),
	`DestCode` VARCHAR(65),
	`UnitSystemCode` VARCHAR(4),
	`SystemDescription` VARCHAR(42),
	OFFSITE VARCHAR(40),
	`MethdDisposal` VARCHAR(60),
	`UltimateDisposal` VARCHAR(40),
	`DisposalCode` VARCHAR(11),
	PH VARCHAR(7),
	`TOCPer` VARCHAR(5),
	`BTU's/lb` VARCHAR(12),
	`Below5,000BTU/LB` VARCHAR(3),
	`Color` VARCHAR(15),
	`OrganicHalogenPer` VARCHAR(5),
	`Odor` VARCHAR(12),
	`FlashPoint` VARCHAR(7),
	`BoilingPt>95F` VARCHAR(3),
	`PhysicalState` VARCHAR(1),
	`Below10PerWater` BOOLEAN DEFAULT false,
	`WaterPer` VARCHAR(6),
	`PhysStateDescription` VARCHAR(40),
	`FormCode` VARCHAR(4),
	`FormCodeDescription` VARCHAR(60),
	`Layers` VARCHAR(5),
	`SpecificGravity` DOUBLE DEFAULT 1,
	`Miscellaneous` VARCHAR(100),
	CONSTRAINT SYS_PK_10940 PRIMARY KEY (`ProfileNumber`)
);
CREATE INDEX PROFILE_INFORMATION2_DESTCODE ON PROFILE_INFORMATION2 (`DestCode`);
CREATE INDEX PROFILE_INFORMATION2_DISPOSALCODE ON PROFILE_INFORMATION2 (`DisposalCode`);
CREATE INDEX `PROFILE_INFORMATION2_EPA/STATEWASTECODE` ON PROFILE_INFORMATION2 (`EPA/StateWasteCode`);
CREATE INDEX PROFILE_INFORMATION2_FORMCODE ON PROFILE_INFORMATION2 (`FormCode`);
CREATE INDEX PROFILE_INFORMATION2_SICCODE ON PROFILE_INFORMATION2 (`SICCode`);
CREATE INDEX PROFILE_INFORMATION2_SOURCECODE ON PROFILE_INFORMATION2 (`SourceCode`);
CREATE INDEX PROFILE_INFORMATION2_UNITSYSTEMCODE ON PROFILE_INFORMATION2 (`UnitSystemCode`);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10940_10941 ON PROFILE_INFORMATION2 (`ProfileNumber`);

CREATE TABLE `WasteEmsData`.`Profiles_To_Print` (
	`GrpName` VARCHAR(25) NOT NULL,
	`ProfileNumber` VARCHAR(18) NOT NULL,
	CONSTRAINT SYS_PK_10946 PRIMARY KEY (`GrpName`,`ProfileNumber`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10946_10947 ON `Profiles_To_Print` (`GrpName`,`ProfileNumber`);

CREATE TABLE `WasteEmsData`.`RecycleSumRptTable` (
	`Month` INTEGER DEFAULT 0,
	`Year` INTEGER DEFAULT 0,
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`HAZ/NON` VARCHAR(3),
	`Routine` BOOLEAN,
	`CostCenter` VARCHAR(7),
	`SourceDept` VARCHAR(20),
	`Qty` DOUBLE,
	`Units` VARCHAR(3)
);

CREATE TABLE `WasteEmsData`.`RecycleSumRptTableXtab` (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`HAZ/NON` VARCHAR(20),
	`Routine` VARCHAR(50),
	`CostCenter` VARCHAR(7),
	`SourceDept` VARCHAR(20),
	`Year1` DOUBLE DEFAULT 0,
	`Year2` DOUBLE DEFAULT 0,
	`1` DOUBLE DEFAULT 0,
	`2` DOUBLE DEFAULT 0,
	`3` DOUBLE DEFAULT 0,
	`4` DOUBLE DEFAULT 0,
	`5` DOUBLE DEFAULT 0,
	`6` DOUBLE DEFAULT 0,
	`7` DOUBLE DEFAULT 0,
	`8` DOUBLE DEFAULT 0,
	`9` DOUBLE DEFAULT 0,
	`10` DOUBLE DEFAULT 0,
	`11` DOUBLE DEFAULT 0,
	`12` DOUBLE DEFAULT 0,
	`13` DOUBLE DEFAULT 0
);
CREATE INDEX RECYCLESUMRPTTABLEXTAB_PROFILE ON `RecycleSumRptTableXtab` (`ProfileNumber`,`CommonWasteName`,`HAZ/NON`,`Routine`,`CostCenter`,`SourceDept`);

CREATE TABLE `WasteEmsData`.`RELEASE` (
	`ShippedOffSite` TIMESTAMP,
	`CostCenter` VARCHAR(7),
	`SourceDept` VARCHAR(15),
	`ProfileNumber` VARCHAR(18),
	`NumberofContainers` DOUBLE,
	`Disposal Cost per Gallon` DOUBLE,
	`DispCost` DOUBLE,
	`TotalCost` DOUBLE,
	`TranCostPerDrum` DOUBLE
);
CREATE INDEX RELEASE_NUMBEROFCONTAINERS ON `RELEASE` (`NumberofContainers`);

CREATE TABLE RELEASE_ENSCO (
	`CostCenter` VARCHAR(7),
	`SourceDept` VARCHAR(15),
	`ProfileNumber` VARCHAR(18),
	`Manifest` VARCHAR(20),
	`NumberofContainers` DOUBLE,
	`DisposalCost` DOUBLE DEFAULT 0,
	`LineItem` VARCHAR(20),
	`ContainerType` VARCHAR(20)
);
CREATE INDEX RELEASE_ENSCO_NUMBEROFCONTAINERS ON RELEASE_ENSCO (`NumberofContainers`);

CREATE TABLE REPORTS (
	`Report Name` VARCHAR(75),
	`Frequency` VARCHAR(100),
	`Content` VARCHAR(200)
);

CREATE TABLE `WasteEmsData`.`SARA_Releases_All` (
	PROFILENUMBER VARCHAR(18),
	CONSTITUENT VARCHAR(45),
	DESTINATION VARCHAR(33),
	DESTCODE VARCHAR(65),
	OFFSITE VARCHAR(40),
	TRI_RELEASED NUMERIC(65,7) DEFAULT 0
);
CREATE INDEX SARA_RELEASES_ALL_DESTCODE ON `SARA_Releases_All` (DESTCODE);

CREATE TABLE `WasteEmsData`.`SARA_Summary_All` (
	CONSTITUENT VARCHAR(45) NOT NULL,
	SECT1 NUMERIC(65,7) DEFAULT 0,
	SECT2 NUMERIC(65,7) DEFAULT 0,
	SECT3 NUMERIC(65,7) DEFAULT 0,
	SECT4 NUMERIC(65,7) DEFAULT 0,
	SECT5 NUMERIC(65,7) DEFAULT 0,
	SECT6 NUMERIC(65,7) DEFAULT 0,
	SECT7 NUMERIC(65,7) DEFAULT 0,
	CONSTRAINT SYS_PK_10951 PRIMARY KEY (CONSTITUENT)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10951_10952 ON `SARA_Summary_All` (CONSTITUENT);

CREATE TABLE `WasteEmsData`.SPILLS (
	`Date` VARCHAR(10),
	`Material` VARCHAR(15),
	`Description of Incident` VARCHAR(200),
	`Quantity Released` VARCHAR(15),
	`Unit of Quantity` VARCHAR(4)
);

CREATE TABLE `WasteEmsData`.`Summary$_ImportErrors` (
	`Error` VARCHAR(255),
	`Field` VARCHAR(255),
	`Row` INTEGER
);

CREATE TABLE `WasteEmsData`.`Waste_Types` (
	`HAZ/NON` VARCHAR(7) NOT NULL,
	CONSTRAINT SYS_PK_10962 PRIMARY KEY (`HAZ/NON`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10962_10963 ON `Waste_Types` (`HAZ/NON`);

CREATE TABLE `WasteEmsData`.`WasteSumRptTable` (
	`Month` INTEGER DEFAULT 0,
	`Year` INTEGER DEFAULT 0,
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`HAZ/NON` VARCHAR(3),
	`Routine` BOOLEAN,
	`CostCenter` VARCHAR(7),
	`SourceDept` VARCHAR(20),
	`Qty` DOUBLE,
	`Units` VARCHAR(3)
);

CREATE TABLE `WasteEmsData`.`WasteSumRptTableXtab` (
	`ProfileNumber` VARCHAR(18),
	`CommonWasteName` VARCHAR(50),
	`HAZ/NON` VARCHAR(20),
	`Routine` VARCHAR(50),
	`CostCenter` VARCHAR(7),
	`SourceDept` VARCHAR(20),
	`Year1` DOUBLE DEFAULT 0,
	`Year2` DOUBLE DEFAULT 0,
	`1` DOUBLE DEFAULT 0,
	`2` DOUBLE DEFAULT 0,
	`3` DOUBLE DEFAULT 0,
	`4` DOUBLE DEFAULT 0,
	`5` DOUBLE DEFAULT 0,
	`6` DOUBLE DEFAULT 0,
	`7` DOUBLE DEFAULT 0,
	`8` DOUBLE DEFAULT 0,
	`9` DOUBLE DEFAULT 0,
	`10` DOUBLE DEFAULT 0,
	`11` DOUBLE DEFAULT 0,
	`12` DOUBLE DEFAULT 0,
	`13` DOUBLE DEFAULT 0
);
CREATE INDEX WASTESUMRPTTABLEXTAB_PROFILE ON `WasteSumRptTableXtab` (`ProfileNumber`,`CommonWasteName`,`HAZ/NON`,`Routine`,`CostCenter`,`SourceDept`);

CREATE TABLE `WasteEmsData`.`WS_SHIP_RPT_HeritageData` (
	`ShippedOffSite` TIMESTAMP,
	`Manifest/DocumnetNo` VARCHAR(20),
	`lineItem` VARCHAR(2),
	`ProfileNumber` VARCHAR(18),
	`Haz/Non` VARCHAR(3),
	`Routine` BOOLEAN DEFAULT false,
	`Transporter` VARCHAR(25),
	`StateTranspID` VARCHAR(255),
	`CostCenter` VARCHAR(7),
	`SourceDept` VARCHAR(255),
	`DisposalCost` INTEGER DEFAULT 0,
	`QuantityShipped` DOUBLE,
	`Units` VARCHAR(255),
	`Quantity` DOUBLE,
	`NumberOfContainers` DOUBLE,
	`ContainerType` VARCHAR(255),
	`HazardCharacteristics` VARCHAR(255),
	`EPA/StateWasteCode` VARCHAR(255),
	`MethodDisposal` VARCHAR(255),
	OFFSITE VARCHAR(255)
);

CREATE TABLE `WasteEmsData`.`ZZ_Month` (
	ID INTEGER NOT NULL,
	`Mo` VARCHAR(12),
	CONSTRAINT SYS_PK_10967 PRIMARY KEY (ID)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10967_10968 ON `ZZ_Month` (ID);
CREATE INDEX ZZ_MONTH_ID ON `ZZ_Month` (ID);

CREATE TABLE `WasteEmsData`.`zz_SummaryApprover` (
	`ShippedOffSite` TIMESTAMP,
	`TicketNo` DOUBLE,
	`ProfileNumber` VARCHAR(50),
	`Manifest/DocumentNo` VARCHAR(50),
	`QuantityShipped` DOUBLE,
	`ManifestReturned` VARCHAR(255),
	`InvoiceDate` TIMESTAMP,
	`InvoicePaid` VARCHAR(255),
	`ManifestComments` VARCHAR(255)
);
CREATE INDEX ZZ_SUMMARYAPPROVER_INVOICEPAID ON `zz_SummaryApprover` (`InvoicePaid`);

CREATE TABLE `WasteEmsData`.`zz_YearList` (
	`Year` INTEGER DEFAULT 0 NOT NULL,
	CONSTRAINT SYS_PK_10972 PRIMARY KEY (`Year`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_10972_10973 ON `zz_YearList` (`Year`);