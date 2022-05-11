-- NPDES.`Admin-Functions` definition

CREATE TABLE `Admin-Functions` (
                                   `FunctionID` varchar(30) NOT NULL,
                                   UNIQUE KEY `Functions_UN` (`FunctionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- NPDES.`Admin-Roles` definition

CREATE TABLE `Admin-Roles` (
                               `RoleID` varchar(30) NOT NULL,
                               `RoleLevel` int(11) NOT NULL,
                               `ReadOnly` tinyint(1) NOT NULL,
                               `Enabled` tinyint(1) NOT NULL DEFAULT 1,
                               UNIQUE KEY `Roles_UN` (`RoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- NPDES.`Admin-Users` definition

CREATE TABLE `Admin-Users` (
                               `UserID` varchar(30) NOT NULL,
                               `Password` varchar(100) NOT NULL,
                               `Salt` varchar(100) DEFAULT NULL,
                               `Enabled` tinyint(1) NOT NULL DEFAULT 1,
                               UNIQUE KEY `Users_UN` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- NPDES.`Admin-Apps` definition

CREATE TABLE `Admin-Apps` (
                              `AppID` varchar(30) NOT NULL,
                              `FunctionID` varchar(30) DEFAULT NULL,
                              `ReportApp` tinyint(1) NOT NULL DEFAULT 1,
                              UNIQUE KEY `Apps_UN` (`AppID`),
                              KEY `Apps_AppID_IDX` (`AppID`) USING BTREE,
                              KEY `Apps_FK_1` (`FunctionID`),
                              CONSTRAINT `Apps_FK_Functions` FOREIGN KEY (`FunctionID`) REFERENCES `Admin-Functions` (`FunctionID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- NPDES.`Admin-UserAppState` definition

CREATE TABLE `Admin-UserAppState` (
                                      `UserID` varchar(30) NOT NULL,
                                      `AppID` varchar(30) NOT NULL,
                                      `Settings` varchar(300) DEFAULT NULL,
                                      UNIQUE KEY `UserAppState_UN` (`UserID`,`AppID`),
                                      KEY `UserAppState_FK_1` (`AppID`),
                                      CONSTRAINT `UserAppState_FK_Users` FOREIGN KEY (`UserID`) REFERENCES `Admin-Users` (`UserID`) ON DELETE CASCADE,
                                      CONSTRAINT `UserAppState_FK_Apps` FOREIGN KEY (`AppID`) REFERENCES `Admin-Apps` (`AppID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- NPDES.`Admin-UserRoles` definition

CREATE TABLE `Admin-UserRoles` (
                                   `UserID` varchar(30) NOT NULL,
                                   `RoleID` varchar(30) NOT NULL,
                                   UNIQUE KEY `UserRoles_UN` (`UserID`,`RoleID`),
                                   KEY `UserRoles_FK` (`RoleID`),
                                   CONSTRAINT `UserRoles_FK_Roles` FOREIGN KEY (`RoleID`) REFERENCES `Admin-Roles` (`RoleID`) ON DELETE CASCADE ON UPDATE CASCADE,
                                   CONSTRAINT `UserRoles_FK_Users` FOREIGN KEY (`UserID`) REFERENCES `Admin-Users` (`UserID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- NPDES.`Admin-UserSystemState` definition

CREATE TABLE `Admin-UserSystemState` (
                                         `UserID` varchar(30) NOT NULL,
                                         `DarkTheme` tinyint(1) NOT NULL DEFAULT 0,
                                         `LastPage` varchar(30) DEFAULT NULL,
                                         UNIQUE KEY `UserSystemState_UN` (`UserID`),
                                         CONSTRAINT `UserSystemState_FK_Users` FOREIGN KEY (`UserID`) REFERENCES `Admin-Users` (`UserID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- NPDES.`General-Download` definition

CREATE TABLE `General-Download` (
                                    `UserID` varchar(30) NOT NULL,
                                    `FileName` varchar(100) NOT NULL,
                                    `OriginalFileName` varchar(100) DEFAULT NULL,
                                    `FunctionID` varchar(30) DEFAULT NULL,
                                    `DownloadDateTime` datetime(3) DEFAULT NULL,
                                    `ValidatedDateTime` datetime(3) DEFAULT NULL,
                                    `ValidateUserID` varchar(30) DEFAULT NULL,
                                    `ProcessedDateTime` datetime(3) DEFAULT NULL,
                                    `ProcessUserID` varchar(30) DEFAULT NULL,
                                    `ArchivedDateTime` datetime(3) DEFAULT NULL,
                                    `ArchiveUserID` varchar(30) DEFAULT NULL,
                                    PRIMARY KEY (`UserID`,`FileName`),
                                    KEY `General_Download_FK` (`FunctionID`),
                                    CONSTRAINT `General_Download_FK_Functions` FOREIGN KEY (`FunctionID`) REFERENCES `Admin-Functions` (`FunctionID`),
                                    CONSTRAINT `General_Download_FK_Users` FOREIGN KEY (`UserID`) REFERENCES `Admin-Users` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- NPDES.`Admin-AppRoles` definition

CREATE TABLE `Admin-AppRoles` (
                                  `AppID` varchar(30) NOT NULL,
                                  `RoleID` varchar(30) NOT NULL,
                                  UNIQUE KEY `AppRoles_UN` (`AppID`,`RoleID`),
                                  KEY `AppRoles_FK` (`RoleID`),
                                  CONSTRAINT `AppRoles_FK_Roles` FOREIGN KEY (`RoleID`) REFERENCES `Admin-Roles` (`RoleID`) ON DELETE CASCADE ON UPDATE CASCADE,
                                  CONSTRAINT `AppRoles_FK_Apps` FOREIGN KEY (`AppID`) REFERENCES `Admin-Apps` (`AppID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;