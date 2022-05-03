CREATE TABLE `25RData` (
                           ID INTEGER NOT NULL,
                           `ProfileID` VARCHAR(18),
                           B2 TEXT(21300),
                           B3 TEXT(21300),
                           C1 TEXT(21300),
                           C2 TEXT(21300),
                           C3 TEXT(21300),
                           CS1 TEXT(21300),
                           CS2 TEXT(21300),
                           CS3 TEXT(21300),
                           D1 TEXT(21300),
                           D2 TEXT(21300),
                           D3 TEXT(21300),
                           D4 TEXT(21300),
                           DS1 TEXT(21300),
                           DS2 TEXT(21300),
                           `RCLeader` VARCHAR(50),
                           CONSTRAINT SYS_PK_10852 PRIMARY KEY (ID)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_25RData ON `25RData` (ID);
CREATE INDEX SYS_IDX_25RDATA_ID ON `25RData` (ID);
CREATE INDEX SYS_IDX_25RDATA_PROFILEID ON `25RData` (`ProfileID`);

CREATE TABLE `BV_WasteSumRptTable` (
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

CREATE TABLE `BV_WasteSumRptTableXTab` (
                                           `ProfileNumber` VARCHAR(18),
                                           `CommonWasteName` VARCHAR(50),
                                           `HAZ/NON` VARCHAR(20),
                                           `Routine` BOOLEAN,
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
CREATE INDEX BV_WASTESUMRPTTABLEXTAB_PROFILE ON `BV_WasteSumRptTableXTab` (`ProfileNumber`,`CommonWasteName`,`HAZ/NON`,`Routine`,`CostCenter`,`SourceDept`);

CREATE TABLE CONSTITUENT (
                             `Constituent` VARCHAR(45) NOT NULL,
                             CASNUM VARCHAR(9),
                             TRICHEM BOOLEAN DEFAULT false NOT NULL,
                             TRIRPT BOOLEAN DEFAULT false NOT NULL,
                             DEMIN NUMERIC(65,7),
                             COMMENTS VARCHAR(80),
                             CONSTRAINT SYS_PK_10857 PRIMARY KEY (`Constituent`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_CONSTITUENT ON CONSTITUENT (`Constituent`);

CREATE TABLE CONTAINER_TYPE_CODES (
                                      `ContainerType` VARCHAR(22) NOT NULL,
                                      `Abrv` VARCHAR(4),
                                      CONSTRAINT SYS_PK_10865 PRIMARY KEY (`ContainerType`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_CONTAINER_TYPE_CODES ON CONTAINER_TYPE_CODES (`ContainerType`);

CREATE TABLE `Conversion Errors` (
                                     `Object Type` VARCHAR(255),
                                     `Object Name` VARCHAR(255),
                                     `Error Description` TEXT(21300)
);

CREATE TABLE COST_CENTERS (
                              `CostCenter` VARCHAR(7) NOT NULL,
                              `SourceDept` VARCHAR(20) NOT NULL,
                              `GrpName` VARCHAR(25),
                              CONSTRAINT SYS_PK_10870 PRIMARY KEY (`CostCenter`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_COST_CENTERS ON COST_CENTERS (`CostCenter`);

CREATE TABLE DISPOSAL_CODE (
                               `DisposalCode` VARCHAR(11) NOT NULL,
                               `MethodTreat/Disposal` VARCHAR(60),
                               `PriceLowPerDrum` DOUBLE,
                               `PriceHighPerDrum` DOUBLE,
                               `PriceLowPerLb` DOUBLE,
                               CONSTRAINT SYS_PK_10882 PRIMARY KEY (`DisposalCode`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_DISPOSAL_CODE ON DISPOSAL_CODE (`DisposalCode`);

CREATE TABLE `DrumLocation` (
                                ID INTEGER NOT NULL,
                                `Location` VARCHAR(50),
                                CONSTRAINT SYS_PK_10892 PRIMARY KEY (ID)
);
CREATE INDEX DRUMLOCATION_ID ON `DrumLocation` (ID);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_DRUMLOCATION ON `DrumLocation` (ID);

CREATE TABLE FORM_CODE (
                           `FormCode` VARCHAR(4) NOT NULL,
                           `FormCodeDescription` VARCHAR(60) NOT NULL,
                           CONSTRAINT SYS_PK_10897 PRIMARY KEY (`FormCode`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_FORM_CODE ON FORM_CODE (`FormCode`);

CREATE TABLE `FreightType` (
                               `FrtID` INTEGER NOT NULL,
                               `FtrType` VARCHAR(50),
                               CONSTRAINT SYS_PK_10903 PRIMARY KEY (`FrtID`)
);
CREATE INDEX FREIGHTTYPE_FRTID ON `FreightType` (`FrtID`);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_FREIGHTTYPE ON `FreightType` (`FrtID`);

CREATE TABLE `FrtUOM` (
                          `Frt_UOM_ID` INTEGER NOT NULL,
                          `Frt_Desc` VARCHAR(50),
                          CONSTRAINT SYS_PK_10908 PRIMARY KEY (`Frt_UOM_ID`)
);
CREATE INDEX FRTUOM_FRT_UOM_ID ON `FrtUOM` (`Frt_UOM_ID`);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_FRTUOM ON `FrtUOM` (`Frt_UOM_ID`);

CREATE TABLE METHODS (
                         `MethdTreat/Disposal` VARCHAR(60) NOT NULL,
                         CONSTRAINT SYS_PK_10912 PRIMARY KEY (`MethdTreat/Disposal`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_METHODS ON METHODS (`MethdTreat/Disposal`);

CREATE TABLE `Off_Site_Location_Information` (
                                                 OFFSITE VARCHAR(40) NOT NULL,
                                                 EPAID VARCHAR(13),
                                                 ADDRESS VARCHAR(40),
                                                 CITY VARCHAR(15),
                                                 STATE VARCHAR(2),
                                                 ZIP VARCHAR(10),
                                                 MUNICIPALITY VARCHAR(30),
                                                 COUNTY VARCHAR(15),
                                                 OFFNAME VARCHAR(40),
                                                 OFFTITLE VARCHAR(20),
                                                 OFFPHONE VARCHAR(20),
                                                 PRNT BOOLEAN,
                                                 CONSTRAINT SYS_PK_10917 PRIMARY KEY (OFFSITE)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_OFF_SITE_LOC_INFO ON `Off_Site_Location_Information` (OFFSITE);


CREATE TABLE `Opr_WasteSumRptTable` (
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


CREATE TABLE `Opr_WasteSumRptTableXtab` (
                                            `ProfileNumber` VARCHAR(18),
                                            `CommonWasteName` VARCHAR(50),
                                            `HAZ/NON` VARCHAR(20),
                                            `Routine` BOOLEAN,
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
CREATE INDEX OPR_WASTESUMRPTTABLEXTAB_PROFILE ON `Opr_WasteSumRptTableXtab` (`ProfileNumber`,`CommonWasteName`,`HAZ/NON`,`Routine`,`CostCenter`,`SourceDept`);

CREATE TABLE `Pick_Destination_Treatment_Codes` (
                                                    DESTINATION VARCHAR(33) NOT NULL,
                                                    DESTCODE VARCHAR(65) NOT NULL,
                                                    `OnOffSite` VARCHAR(8),
                                                    RELSECT VARCHAR(7),
                                                    SUMMARY VARCHAR(3),
                                                    CONSTRAINT SYS_PK_10923 PRIMARY KEY (DESTINATION,DESTCODE)
);
CREATE INDEX PICK_DESTINATION_TREATMENT_CODES_DESTINATION ON `Pick_Destination_Treatment_Codes` (DESTINATION,DESTCODE);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_PICK_DEST_LOC_TREAT_CODE ON `Pick_Destination_Treatment_Codes` (DESTINATION,DESTCODE);

CREATE TABLE `Pick_State_Names` (
                                    STATE VARCHAR(2),
                                    NAME VARCHAR(20)
);

CREATE TABLE `Pick_TRI_Chemicals` (
                                      CAS_NUM VARCHAR(9),
                                      CHEM_NAME VARCHAR(70),
                                      LISTED_YR VARCHAR(2),
                                      DELIST_YR VARCHAR(2)
);

CREATE TABLE `Pick_Waste_Type_Code` (
                                        `TYPE` VARCHAR(1) NOT NULL,
                                        `DESC` VARCHAR(60),
                                        CONSTRAINT SYS_PK_10928 PRIMARY KEY (`TYPE`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_PICK_WST_TYPE ON `Pick_Waste_Type_Code` (`TYPE`);

CREATE TABLE `ProfileCharacterization` (
                                           ID INTEGER NOT NULL,
                                           `Characterized` VARCHAR(50),
                                           CONSTRAINT SYS_PK_10948 PRIMARY KEY (ID)
);
CREATE INDEX PROFILECHARACTERIZATION_ID ON `ProfileCharacterization` (ID);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_PROFCHAR ON `ProfileCharacterization` (ID);

CREATE TABLE `ShipContainerType` (
                                     `ContainerID` INTEGER NOT NULL,
                                     `ContainerType` VARCHAR(50),
                                     CONSTRAINT SYS_PK_10953 PRIMARY KEY (`ContainerID`)
);
CREATE INDEX SHIPCONTAINERTYPE_ID ON `ShipContainerType` (`ContainerID`);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_SHIPCONTTYPE ON `ShipContainerType` (`ContainerID`);

CREATE TABLE SOURCE_CODE (
                             `SourceCode` VARCHAR(4) NOT NULL,
                             `HAZ/NON` VARCHAR(3),
                             `SourceDescription` VARCHAR(56) NOT NULL,
                             CONSTRAINT SYS_PK_10958 PRIMARY KEY (`SourceCode`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_SOURCE_CODE ON SOURCE_CODE (`SourceCode`);


CREATE TABLE SYSTEM_DESCRIPTION (
                                    `UnitSystemCode` VARCHAR(4) NOT NULL,
                                    `HAZ/NON` VARCHAR(3),
                                    `SystemDescription` VARCHAR(42) NOT NULL,
                                    CONSTRAINT SYS_PK_10964 PRIMARY KEY (`UnitSystemCode`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_SYS_DESCRIPTION ON SYSTEM_DESCRIPTION (`UnitSystemCode`);

CREATE TABLE `Tech_WasteSumRptTable` (
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

CREATE TABLE `Tech_WasteSumRptTableXtab` (
                                             `ProfileNumber` VARCHAR(18),
                                             `CommonWasteName` VARCHAR(50),
                                             `HAZ/NON` VARCHAR(20),
                                             `Routine` BOOLEAN,
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
CREATE INDEX TECH_WASTESUMRPTTABLEXTAB_PROFILE ON `Tech_WasteSumRptTableXtab` (`ProfileNumber`,`CommonWasteName`,`HAZ/NON`,`Routine`,`CostCenter`,`SourceDept`);


CREATE TABLE `TimeFrame` (
    `TmeFrame` VARCHAR(20)
);

CREATE TABLE TRANPORTER (
                            `Transporter` VARCHAR(25) NOT NULL,
                            `StateTranspID` VARCHAR(6) NOT NULL,
                            CONSTRAINT SYS_PK_10970 PRIMARY KEY (`Transporter`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_TRANSPORTER ON TRANPORTER (`Transporter`);

CREATE TABLE ULTIMATE_DISPOSAL (
                                   `UltimateDisposal` VARCHAR(40) NOT NULL,
                                   `State` VARCHAR(2),
                                   CONSTRAINT SYS_PK_10986 PRIMARY KEY (`UltimateDisposal`)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_ULTIMATE_DISPOSAL ON ULTIMATE_DISPOSAL (`UltimateDisposal`);


CREATE TABLE UNIT_TYPES (
                            UNITS VARCHAR(3) NOT NULL,
                            CONSTRAINT SYS_PK_10991 PRIMARY KEY (UNITS)
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_UNIT_TYPES ON UNIT_TYPES (UNITS);

CREATE TABLE `WasteSumRptTable` (
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

CREATE TABLE PROFILE_INFORMATION (
                                     `ProfileNumber` VARCHAR(18) NOT NULL,
                                     `CommonWasteName` VARCHAR(50),
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
                                     `ProperShippingName` VARCHAR(255),
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
                                     `DisposalCost` DOUBLE,
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
                                     `LayerDescription` VARCHAR(120),
                                     `SpecificGravity` DOUBLE DEFAULT 1,
                                     `Miscellaneous` VARCHAR(100),
                                     `GenProp` BOOLEAN DEFAULT false,
                                     `GenPropDate` TIMESTAMP,
                                     `ChemAnal` BOOLEAN DEFAULT false,
                                     `ChemAnalDate` TIMESTAMP,
                                     `ProcDesc` BOOLEAN DEFAULT false,
                                     `ProcDescDate` TIMESTAMP,
                                     `SourReduct` BOOLEAN DEFAULT false,
                                     `SourReductDate` TIMESTAMP,
                                     `BenefUse` BOOLEAN DEFAULT false,
                                     `Active` BOOLEAN DEFAULT true,
                                     `25RApply` BOOLEAN,
                                     `26RApply` BOOLEAN,
                                     `OneTime` BOOLEAN,
                                     `TimeFrame` VARCHAR(50),
                                     `UltimateState` VARCHAR(2),
                                     `25RBiennielReport` BOOLEAN,
                                     `ProfileBook` BOOLEAN DEFAULT true,
                                     `FrtUnitCost` DOUBLE DEFAULT 0,
                                     `ContainerType` INTEGER DEFAULT 0,
                                     `FreightUOM` INTEGER DEFAULT 0,
                                     `Manifest` VARCHAR(100),
                                     `ProfileExpirationDate` TIMESTAMP,
                                     `ProfileApprovalDate` TIMESTAMP,
                                     VPN VARCHAR(50),
                                     ERG VARCHAR(50),
                                     `Characterized` VARCHAR(50),
                                     `AnalyticalDate` TIMESTAMP,
                                     `AnalyticalTesting` BOOLEAN DEFAULT 0,
                                     `Expediate` BOOLEAN DEFAULT false,
                                     CONSTRAINT SYS_PK_10942 PRIMARY KEY (`ProfileNumber`),
                                     CONSTRAINT PROFILE_INFORMATION_REFERENCE10 FOREIGN KEY (`SourceCode`) REFERENCES SOURCE_CODE(`SourceCode`) ON UPDATE CASCADE,
                                     CONSTRAINT PROFILE_INFORMATION_REFERENCE12 FOREIGN KEY (`MethdDisposal`) REFERENCES METHODS(`MethdTreat/Disposal`) ON UPDATE CASCADE,
                                     CONSTRAINT PROFILE_INFORMATION_REFERENCE22 FOREIGN KEY (`PhysicalState`) REFERENCES `Pick_Waste_Type_Code`(`TYPE`) ON UPDATE CASCADE,
                                     CONSTRAINT PROFILE_INFORMATION_REFERENCE23 FOREIGN KEY (OFFSITE) REFERENCES `Off_Site_Location_Information`(OFFSITE) ON UPDATE CASCADE,
                                     CONSTRAINT PROFILE_INFORMATION_REFERENCE24 FOREIGN KEY (`UltimateDisposal`) REFERENCES ULTIMATE_DISPOSAL(`UltimateDisposal`) ON UPDATE CASCADE,
                                     CONSTRAINT PROFILE_INFORMATION_REFERENCE26 FOREIGN KEY (`Destination`,`DestCode`) REFERENCES `Pick_Destination_Treatment_Codes`(DESTINATION,DESTCODE) ON UPDATE CASCADE,
                                     CONSTRAINT PROFILE_INFORMATION_REFERENCE6 FOREIGN KEY (`CostCenter`) REFERENCES COST_CENTERS(`CostCenter`) ON UPDATE CASCADE,
                                     CONSTRAINT PROFILE_INFORMATION_REFERENCE7 FOREIGN KEY (`UnitSystemCode`) REFERENCES SYSTEM_DESCRIPTION(`UnitSystemCode`) ON UPDATE CASCADE,
                                     CONSTRAINT PROFILE_INFORMATION_REFERENCE9 FOREIGN KEY (`FormCode`) REFERENCES FORM_CODE(`FormCode`) ON UPDATE CASCADE
);
CREATE INDEX SYS_IDX_PROFILE_INFORMATION_REFERENCE10_11070 ON PROFILE_INFORMATION (`SourceCode`);
CREATE INDEX SYS_IDX_PROFILE_INFORMATION_REFERENCE12_11077 ON PROFILE_INFORMATION (`MethdDisposal`);
CREATE INDEX SYS_IDX_PROFILE_INFORMATION_REFERENCE22_11085 ON PROFILE_INFORMATION (`PhysicalState`);
CREATE INDEX SYS_IDX_PROFILE_INFORMATION_REFERENCE23_11093 ON PROFILE_INFORMATION (OFFSITE);
CREATE INDEX SYS_IDX_PROFILE_INFORMATION_REFERENCE24_11101 ON PROFILE_INFORMATION (`UltimateDisposal`);
CREATE INDEX SYS_IDX_PROFILE_INFORMATION_REFERENCE26_11111 ON PROFILE_INFORMATION (`Destination`,`DestCode`);
CREATE INDEX SYS_IDX_PROFILE_INFORMATION_REFERENCE6_11119 ON PROFILE_INFORMATION (`CostCenter`);
CREATE INDEX SYS_IDX_PROFILE_INFORMATION_REFERENCE7_11127 ON PROFILE_INFORMATION (`UnitSystemCode`);
CREATE INDEX SYS_IDX_PROFILE_INFORMATION_REFERENCE9_11135 ON PROFILE_INFORMATION (`FormCode`);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_PROFILE_INFO ON PROFILE_INFORMATION (`ProfileNumber`);


CREATE TABLE `TrashSite` (
                             `ContainerSite` VARCHAR(22) NOT NULL,
                             `CostCenter` VARCHAR(7) NOT NULL,
                             `SourceDept` VARCHAR(20) NOT NULL,
                             `ContainerType` VARCHAR(22) NOT NULL,
                             `PerRCRAEmpty` NUMERIC(65,7) DEFAULT 0 NOT NULL,
                             CONSTRAINT SYS_PK_10976 PRIMARY KEY (`ContainerSite`),
                             CONSTRAINT TRASHSITE_REFERENCE17 FOREIGN KEY (`ContainerType`) REFERENCES CONTAINER_TYPE_CODES(`ContainerType`) ON UPDATE CASCADE,
                             CONSTRAINT TRASHSITE_REFERENCE18 FOREIGN KEY (`CostCenter`) REFERENCES COST_CENTERS(`CostCenter`) ON UPDATE CASCADE
);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_TrashSite ON `TrashSite` (`ContainerSite`);
CREATE INDEX SYS_IDX_TRASHSITE_REFERENCE17_11143 ON `TrashSite` (`ContainerType`);
CREATE INDEX SYS_IDX_TRASHSITE_REFERENCE18_11155 ON `TrashSite` (`CostCenter`);

CREATE TABLE WASTE_SHIPMENTS (
                                 `ShippedOffSite` TIMESTAMP NOT NULL,
                                 `LineItem` VARCHAR(2) NOT NULL,
                                 `ProfileNumber` VARCHAR(18) NOT NULL,
                                 `HAZ/NON` VARCHAR(3) NOT NULL,
                                 `Routine` BOOLEAN DEFAULT true,
                                 `Transporter` VARCHAR(25) NOT NULL,
                                 `StateTranspID` VARCHAR(6) NOT NULL,
                                 `CostCenter` VARCHAR(7) NOT NULL,
                                 `SourceDept` VARCHAR(20) NOT NULL,
                                 `DisposalCost` DOUBLE,
                                 `QuantityShipped` DOUBLE,
                                 `Units` VARCHAR(3),
                                 `Quantity` DOUBLE NOT NULL,
                                 `NumberofContainers` NUMERIC(65,7) DEFAULT 1 NOT NULL,
                                 `ContainerType` VARCHAR(22),
                                 `HazardCharacteristics` VARCHAR(60),
                                 `EPA/StateWasteCode` VARCHAR(60),
                                 `ProperShippingName` VARCHAR(100),
                                 `Destination` VARCHAR(33) NOT NULL,
                                 `DestCode` VARCHAR(65) NOT NULL,
                                 `Unit/SystemCode` VARCHAR(4) NOT NULL,
                                 `SystemDescription` VARCHAR(42) NOT NULL,
                                 OFFSITE VARCHAR(40) NOT NULL,
                                 `MethdDisposal` VARCHAR(60) NOT NULL,
                                 `UltimateDisposal` VARCHAR(40) NOT NULL,
                                 `Below10PerWater` BOOLEAN DEFAULT false,
                                 `WaterPer` VARCHAR(6),
                                 `Comments` VARCHAR(150),
                                 `PerRCRAEmpty` NUMERIC(65,7) DEFAULT 0,
                                 `ContainerSite` VARCHAR(22),
                                 `TotConatiners` DOUBLE DEFAULT 0,
                                 FTL BOOLEAN DEFAULT false,
                                 `Cost` DOUBLE DEFAULT 0,
                                 `TicketNo` VARCHAR(50),
                                 `ManifestReturned` BOOLEAN DEFAULT false,
                                 `ManifestReturnedDate` TIMESTAMP,
                                 `InvoicePaid` BOOLEAN DEFAULT false,
                                 `InvoiceDate` TIMESTAMP,
                                 `ManifestComments` VARCHAR(255),
                                 `Manifest/DocumentNo` VARCHAR(20) NOT NULL,
                                 CONSTRAINT SYS_PK_10998 PRIMARY KEY (`ShippedOffSite`,`Manifest/DocumentNo`,`LineItem`,`ProfileNumber`),
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE FOREIGN KEY (`ProfileNumber`) REFERENCES PROFILE_INFORMATION(`ProfileNumber`) ON UPDATE CASCADE,
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE1 FOREIGN KEY (`Transporter`) REFERENCES TRANPORTER(`Transporter`) ON UPDATE CASCADE,
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE13 FOREIGN KEY (`MethdDisposal`) REFERENCES METHODS(`MethdTreat/Disposal`) ON UPDATE CASCADE,
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE14 FOREIGN KEY (`UltimateDisposal`) REFERENCES ULTIMATE_DISPOSAL(`UltimateDisposal`) ON UPDATE CASCADE,
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE15 FOREIGN KEY (`Destination`,`DestCode`) REFERENCES `Pick_Destination_Treatment_Codes`(DESTINATION,DESTCODE) ON UPDATE CASCADE,
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE16 FOREIGN KEY (`ContainerType`) REFERENCES CONTAINER_TYPE_CODES(`ContainerType`) ON UPDATE CASCADE,
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE19 FOREIGN KEY (`ContainerSite`) REFERENCES `TrashSite`(`ContainerSite`) ON UPDATE CASCADE,
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE2 FOREIGN KEY (`CostCenter`) REFERENCES COST_CENTERS(`CostCenter`) ON UPDATE CASCADE,
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE20 FOREIGN KEY (`Units`) REFERENCES UNIT_TYPES(UNITS) ON UPDATE CASCADE,
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE4 FOREIGN KEY (`Unit/SystemCode`) REFERENCES SYSTEM_DESCRIPTION(`UnitSystemCode`) ON UPDATE CASCADE,
                                 CONSTRAINT WASTE_SHIPMENTS_REFERENCE5 FOREIGN KEY (OFFSITE) REFERENCES `Off_Site_Location_Information`(OFFSITE) ON UPDATE CASCADE
);
CREATE UNIQUE INDEX SYS_IDX_SYS_WASTE_SHIPMENTS ON WASTE_SHIPMENTS (`ShippedOffSite`,`Manifest/DocumentNo`,`LineItem`,`ProfileNumber`);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE13_11220 ON WASTE_SHIPMENTS (`MethdDisposal`);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE14_11247 ON WASTE_SHIPMENTS (`UltimateDisposal`);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE15_11276 ON WASTE_SHIPMENTS (`Destination`,`DestCode`);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE16_11303 ON WASTE_SHIPMENTS (`ContainerType`);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE19_11330 ON WASTE_SHIPMENTS (`ContainerSite`);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE1_11194 ON WASTE_SHIPMENTS (`Transporter`);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE20_11384 ON WASTE_SHIPMENTS (`Units`);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE2_11357 ON WASTE_SHIPMENTS (`CostCenter`);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE4_11410 ON WASTE_SHIPMENTS (`Unit/SystemCode`);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE5_11437 ON WASTE_SHIPMENTS (OFFSITE);
CREATE INDEX SYS_IDX_WASTE_SHIPMENTS_REFERENCE_11167 ON WASTE_SHIPMENTS (`ProfileNumber`);


CREATE TABLE DRUM_TRACKING (
                               `DrumNumber` VARCHAR(10) NOT NULL,
                               `ProfileNumber` VARCHAR(18),
                               `HAZ/NON` VARCHAR(3),
                               `ContactPerson` VARCHAR(30),
                               `SourceProcess` VARCHAR(20),
                               `SourceActivity` VARCHAR(40),
                               `AccumStartDate` TIMESTAMP,
                               `ShippedOffSite` TIMESTAMP,
                               `Comments` VARCHAR(60),
                               `CostCenter` VARCHAR(7),
                               `SourceDept` VARCHAR(20),
                               `DrumType` VARCHAR(5),
                               `verified` BOOLEAN,
                               `Location` VARCHAR(50),
                               `AccumulationArea` VARCHAR(25),
                               CONSTRAINT SYS_PK_10887 PRIMARY KEY (`DrumNumber`),
                               CONSTRAINT DRUM_TRACKING_REFERENCE21 FOREIGN KEY (`ProfileNumber`) REFERENCES PROFILE_INFORMATION(`ProfileNumber`) ON UPDATE CASCADE,
                               CONSTRAINT DRUM_TRACKING_REFERENCE25 FOREIGN KEY (`CostCenter`) REFERENCES COST_CENTERS(`CostCenter`) ON UPDATE CASCADE
);
CREATE INDEX SYS_IDX_DRUM_TRACKING_REFERENCE21_11036 ON DRUM_TRACKING (`ProfileNumber`);
CREATE INDEX SYS_IDX_DRUM_TRACKING_REFERENCE25_11043 ON DRUM_TRACKING (`CostCenter`);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_DRUM_TRACKING ON DRUM_TRACKING (`DrumNumber`);

CREATE TABLE PROFILE_COMPOSITION (
                                     `ProfileNumber` VARCHAR(18) NOT NULL,
                                     `Component` VARCHAR(2) NOT NULL,
                                     `Constituent` VARCHAR(45) NOT NULL,
                                     `PercentComposition` VARCHAR(15),
                                     `LowValue` DOUBLE,
                                     `WorkingPer` DOUBLE DEFAULT 0 NOT NULL,
                                     `HighValue` DOUBLE,
                                     `ValueUnits` VARCHAR(3) DEFAULT '%' NOT NULL,
                                     CONSTRAINT SYS_PK_10934 PRIMARY KEY (`ProfileNumber`,`Component`),
                                     CONSTRAINT PROFILE_COMPOSITION_REFERENCE3 FOREIGN KEY (`Constituent`) REFERENCES CONSTITUENT(`Constituent`) ON UPDATE CASCADE,
                                     CONSTRAINT PROFILE_COMPOSITION_REFERENCE8 FOREIGN KEY (`ProfileNumber`) REFERENCES PROFILE_INFORMATION(`ProfileNumber`) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE INDEX SYS_IDX_PROFILE_COMPOSITION_REFERENCE3_11050 ON PROFILE_COMPOSITION (`Constituent`);
CREATE INDEX SYS_IDX_PROFILE_COMPOSITION_REFERENCE8_11060 ON PROFILE_COMPOSITION (`ProfileNumber`);
CREATE UNIQUE INDEX SYS_IDX_SYS_PK_PROFILE_COMP ON PROFILE_COMPOSITION (`ProfileNumber`,`Component`);