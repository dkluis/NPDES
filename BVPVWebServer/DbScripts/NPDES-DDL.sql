CREATE TABLE `NPDES`.`Admin-Functions` (
                                   `FunctionID` varchar(30) NOT NULL,
                                   UNIQUE KEY `Functions_UN` (`FunctionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `NPDES`.`Admin-Roles` (
                               `RoleID` varchar(30) NOT NULL,
                               `RoleLevel` int(11) NOT NULL,
                               `ReadOnly` tinyint(1) NOT NULL,
                               `Enabled` tinyint(1) NOT NULL DEFAULT 1,
                               UNIQUE KEY `Roles_UN` (`RoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `NPDES`.`Admin-Users` (
                               `UserID` varchar(30) NOT NULL,
                               `Password` varchar(100) NOT NULL,
                               `Salt` varchar(100) DEFAULT NULL,
                               `Enabled` tinyint(1) NOT NULL DEFAULT 1,
                               UNIQUE KEY `Users_UN` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `NPDES`.`Admin-Apps` (
                              `AppID` varchar(30) NOT NULL,
                              `FunctionID` varchar(30) DEFAULT NULL,
                              `ReportApp` tinyint(1) NOT NULL DEFAULT 1,
                              UNIQUE KEY `Apps_UN` (`AppID`),
                              KEY `Apps_AppID_IDX` (`AppID`) USING BTREE,
                              KEY `Apps_FK_1` (`FunctionID`),
                              CONSTRAINT `Apps_FK_1` FOREIGN KEY (`FunctionID`) REFERENCES `Admin-Functions` (`FunctionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `NPDES`.`Admin-UserAppState` (
                                      `UserID` varchar(30) NOT NULL,
                                      `AppID` varchar(30) NOT NULL,
                                      `Settings` varchar(300) DEFAULT NULL,
                                      UNIQUE KEY `UserAppState_UN` (`UserID`,`AppID`),
                                      KEY `UserAppState_FK_1` (`AppID`),
                                      CONSTRAINT `UserAppState_FK` FOREIGN KEY (`UserID`) REFERENCES `Admin-Users` (`UserID`),
                                      CONSTRAINT `UserAppState_FK_1` FOREIGN KEY (`AppID`) REFERENCES `Admin-Apps` (`AppID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `NPDES`.`Admin-UserRoles` (
                                   `UserID` varchar(30) NOT NULL,
                                   `RoleID` varchar(30) NOT NULL,
                                   UNIQUE KEY `UserRoles_UN` (`UserID`,`RoleID`),
                                   KEY `UserRoles_FK` (`RoleID`),
                                   CONSTRAINT `UserRoles_FK` FOREIGN KEY (`RoleID`) REFERENCES `Admin-Roles` (`RoleID`) ON DELETE CASCADE,
                                   CONSTRAINT `UserRoles_FK_1` FOREIGN KEY (`UserID`) REFERENCES `Admin-Users` (`UserID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `NPDES`.`Admin-UserSystemState` (
                                         `UserID` varchar(30) NOT NULL,
                                         `DarkTheme` tinyint(1) NOT NULL DEFAULT 0,
                                         `LastPage` varchar(30) DEFAULT NULL,
                                         UNIQUE KEY `UserSystemState_UN` (`UserID`),
                                         CONSTRAINT `UserSystemState_FK` FOREIGN KEY (`UserID`) REFERENCES `Admin-Users` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `NPDES`.`General-Download` (
                                    `UserID` varchar(30) NOT NULL,
                                    `FileName` varchar(100) NOT NULL,
                                    `OriginalFileName` varchar(100) DEFAULT NULL,
                                    `FunctionID` varchar(30) DEFAULT NULL,
                                    `DownloadDateTime` datetime(3) DEFAULT NULL,
                                    `ValidatedDateTime` datetime(3) DEFAULT NULL,
                                    `ValidateUserID` varchar(30) DEFAULT NULL,
                                    `ProcessedDateTime` datetime(3) DEFAULT NULL,
                                    `ProcessUserID` varchar(30) DEFAULT NULL,
                                    `ArchiveDataTime` datetime(3) DEFAULT NULL,
                                    `ArchiveUserID` varchar(30) DEFAULT NULL,
                                    PRIMARY KEY (`UserID`,`FileName`),
                                    KEY `General_Download_FK` (`FunctionID`),
                                    CONSTRAINT `General_Download_FK` FOREIGN KEY (`FunctionID`) REFERENCES `Admin-Functions` (`FunctionID`),
                                    CONSTRAINT `General_Download_FK_1` FOREIGN KEY (`UserID`) REFERENCES `Admin-Users` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `NPDES`.`Admin-AppRoles` (
                                  `AppID` varchar(30) NOT NULL,
                                  `RoleID` varchar(30) NOT NULL,
                                  UNIQUE KEY `AppRoles_UN` (`AppID`,`RoleID`),
                                  KEY `AppRoles_FK` (`RoleID`),
                                  CONSTRAINT `AppRoles_FK` FOREIGN KEY (`RoleID`) REFERENCES `Admin-Roles` (`RoleID`) ON DELETE CASCADE,
                                  CONSTRAINT `AppRoles_FK_1` FOREIGN KEY (`AppID`) REFERENCES `Admin-Apps` (`AppID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE OR REPLACE
    ALGORITHM = UNDEFINED VIEW `NPDES`.`Admin-AppsByUserView` AS
select
    `ur`.`UserID` AS `User`,
    `ur`.`RoleID` AS `Role`,
    `r`.`ReadOnly` AS `ReadOnly`,
    `r`.`RoleLevel` AS `Role Level`,
    `r`.`Enabled` AS `Role Enabled`,
    `ar`.`AppID` AS `App`,
    `a`.`ReportApp` AS `Report`,
    `a`.`FunctionID` AS `Function`
from
    (((`NPDES`.`Admin-UserRoles` `ur`
        join `NPDES`.`Admin-Roles` `r` on
        (`ur`.`RoleID` = `r`.`RoleID`))
        join `NPDES`.`Admin-AppRoles` `ar` on
        (`ur`.`RoleID` = `ar`.`RoleID`))
        join `NPDES`.`Admin-Apps` `a` on
        (`ar`.`AppID` = `a`.`AppID`))
order by
    `ur`.`UserID`,
    `r`.`RoleLevel`,
    `ar`.`AppID`;

CREATE OR REPLACE
    ALGORITHM = UNDEFINED VIEW `NPDES`.`Admin-AppsWithoutRoles` AS
select
    `a`.`AppID` AS `App`,
    `a`.`ReportApp` AS `Report`,
    `a`.`FunctionID` AS `Function`,
    `ar`.`RoleID` AS `Role`
from
    (`NPDES`.`Admin-Apps` `a`
        left join `NPDES`.`Admin-AppRoles` `ar` on
        (`ar`.`AppID` = `a`.`AppID`
            and `ar`.`RoleID` <> 'SuperAdmin'))
where
    `ar`.`RoleID` is null;