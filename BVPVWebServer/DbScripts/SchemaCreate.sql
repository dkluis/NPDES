create table Functions
(
    FunctionID varchar(20) not null,
    constraint Functions_UN
        unique (FunctionID)
)
    engine = InnoDB;

create table Apps
(
    AppID      varchar(15)          not null,
    FunctionID varchar(20)          null,
    ReportApp  tinyint(1) default 1 not null,
    constraint Apps_UN
        unique (AppID),
    constraint Apps_FK_1
        foreign key (FunctionID) references Functions (FunctionID)
)
    engine = InnoDB;

create or replace index Apps_AppID_IDX
    on Apps (AppID);

create table Roles
(
    RoleID    varchar(10) not null,
    RoleLevel int         not null,
    ReadOnly  tinyint(1)  not null,
    constraint Roles_UN
        unique (RoleID)
)
    engine = InnoDB;

create table AppRoles
(
    AppID  varchar(15) not null,
    RoleID varchar(15) not null,
    constraint AppRoles_UN
        unique (AppID, RoleID),
    constraint AppRoles_FK
        foreign key (RoleID) references Roles (RoleID)
            on delete cascade,
    constraint AppRoles_FK_1
        foreign key (AppID) references Apps (AppID)
            on delete cascade
)
    engine = InnoDB;

create table Users
(
    UserID   varchar(30)          not null,
    Password varchar(100)         not null,
    Salt     varchar(100)         null,
    Enabled  tinyint(1) default 1 not null,
    constraint Users_UN
        unique (UserID)
)
    engine = InnoDB;

create table UserAppState
(
    UserID   varchar(10)  not null,
    AppID    varchar(15)  not null,
    Settings varchar(300) null,
    constraint UserAppState_UN
        unique (UserID, AppID),
    constraint UserAppState_FK
        foreign key (UserID) references Users (UserID),
    constraint UserAppState_FK_1
        foreign key (AppID) references Apps (AppID)
)
    engine = InnoDB;

create table UserRoles
(
    UserID varchar(30) not null,
    RoleID varchar(20) not null,
    constraint UserRoles_UN
        unique (UserID, RoleID),
    constraint UserRoles_FK
        foreign key (RoleID) references Roles (RoleID)
            on delete cascade,
    constraint UserRoles_FK_1
        foreign key (UserID) references Users (UserID)
            on update cascade on delete cascade
)
    engine = InnoDB;

create table UserSystemState
(
    UserID    varchar(10)          not null,
    DarkTheme tinyint(1) default 0 not null,
    LastPage  varchar(10)          null,
    constraint UserSystemState_UN
        unique (UserID),
    constraint UserSystemState_FK
        foreign key (UserID) references Users (UserID)
)
    engine = InnoDB;

create or replace view AppsByUserView as
select `ur`.`UserID`    AS `User`,
       `ur`.`RoleID`    AS `Role`,
       `r`.`ReadOnly`   AS `ReadOnly`,
       `r`.`RoleLevel`  AS `Role Level`,
       `ar`.`AppID`     AS `App`,
       `a`.`ReportApp`  AS `Report`,
       `a`.`FunctionID` AS `Function`
from (((`UserRoles` `ur` join `Roles` `r` on (`ur`.`RoleID` = `r`.`RoleID`)) join `AppRoles` `ar` on (`ur`.`RoleID` = `ar`.`RoleID`))
    join `Apps` `a` on (`ar`.`AppID` = `a`.`AppID`))
order by `ur`.`UserID`, `r`.`RoleLevel`, `ar`.`AppID`;

create or replace view UserRolesView as
select `ur`.`UserID`    AS `User`,
       `ur`.`RoleID`    AS `Role`,
       `r`.`ReadOnly`   AS `ReadOnly`,
       `r`.`RoleLevel`  AS `Role Level`,
       `ar`.`AppID`     AS `App`,
       `a`.`ReportApp`  AS `Report`,
       `a`.`FunctionID` AS `Function`
from (((`UserRoles` `ur` join `Roles` `r` on (`ur`.`RoleID` = `r`.`RoleID`)) join `AppRoles` `ar` on (`ur`.`RoleID` = `ar`.`RoleID`))
    join `Apps` `a` on (`ar`.`AppID` = `a`.`AppID`))
order by `ur`.`UserID`, `ar`.`AppID`, `r`.`RoleLevel`;

