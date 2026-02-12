-----------------------------------------------------
-- Create Database
-----------------------------------------------------
IF DB_ID('TestDB') IS NULL
BEGIN
    CREATE DATABASE TestDB;
END;
GO

USE TestDB;
GO

-----------------------------------------------------
-- PLAYERS TABLE
-----------------------------------------------------
IF OBJECT_ID('dbo.Players', 'U') IS NULL
BEGIN
    CREATE TABLE Players
    (
        PlayerId INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL UNIQUE,
        MatchesPlayed INT NOT NULL DEFAULT 0,
        MatchesWon INT NOT NULL DEFAULT 0
    );
END;
GO

-----------------------------------------------------
-- MATCHES TABLE
-----------------------------------------------------
IF OBJECT_ID('dbo.Matches', 'U') IS NULL
BEGIN
    CREATE TABLE Matches
    (
        MatchId INT IDENTITY(1,1) PRIMARY KEY,
        StartedAt DATETIME NOT NULL,
        EndedAt DATETIME NOT NULL,
        WinnerPlayerId INT NOT NULL,
        FOREIGN KEY (WinnerPlayerId)
            REFERENCES Players(PlayerId)
    );
END;
GO

-----------------------------------------------------
-- MATCHPLAYERS TABLE
-----------------------------------------------------
IF OBJECT_ID('dbo.MatchPlayers', 'U') IS NULL
BEGIN
    CREATE TABLE MatchPlayers
    (
        MatchPlayerId INT IDENTITY(1,1) PRIMARY KEY,
        MatchId INT NOT NULL,
        PlayerId INT NOT NULL,
        FinalHealth INT NOT NULL,
        FOREIGN KEY (MatchId)
            REFERENCES Matches(MatchId),
        FOREIGN KEY (PlayerId)
            REFERENCES Players(PlayerId),
        CONSTRAINT UQ_Match_Player UNIQUE (MatchId, PlayerId)
    );
END;
GO

-----------------------------------------------------
-- InsertMatch PROCEDURE
-----------------------------------------------------
IF OBJECT_ID('dbo.InsertMatch', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE dbo.InsertMatch
        @StartedAt DATETIME,
        @EndedAt DATETIME,
        @WinnerName NVARCHAR(100)
    AS
    BEGIN
        SET NOCOUNT ON;

        DECLARE @WinnerPlayerId INT;

        SELECT @WinnerPlayerId = PlayerId
        FROM Players
        WHERE Name = @WinnerName;

        IF @WinnerPlayerId IS NULL
        BEGIN
            INSERT INTO Players (Name, MatchesPlayed, MatchesWon)
            VALUES (@WinnerName, 0, 0);

            SET @WinnerPlayerId = SCOPE_IDENTITY();
        END

        INSERT INTO Matches (StartedAt, EndedAt, WinnerPlayerId)
        VALUES (@StartedAt, @EndedAt, @WinnerPlayerId);

        DECLARE @MatchId INT = SCOPE_IDENTITY();

        UPDATE Players
        SET MatchesWon = MatchesWon + 1
        WHERE PlayerId = @WinnerPlayerId;

        SELECT @MatchId;
    END
    ');
END;
GO

-----------------------------------------------------
-- InsertMatchPlayer PROCEDURE
-----------------------------------------------------
IF OBJECT_ID('dbo.InsertMatchPlayer', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE dbo.InsertMatchPlayer
        @MatchId INT,
        @PlayerName NVARCHAR(100),
        @FinalHealth INT
    AS
    BEGIN
        DECLARE @PlayerId INT;

        SELECT @PlayerId = PlayerId
        FROM Players
        WHERE Name = @PlayerName;

        IF @PlayerId IS NULL
        BEGIN
            INSERT INTO Players (Name, MatchesPlayed, MatchesWon)
            VALUES (@PlayerName, 0, 0);

            SET @PlayerId = SCOPE_IDENTITY();
        END

        INSERT INTO MatchPlayers (MatchId, PlayerId, FinalHealth)
        VALUES (@MatchId, @PlayerId, @FinalHealth);

        UPDATE Players
        SET MatchesPlayed = MatchesPlayed + 1
        WHERE PlayerId = @PlayerId;
    END
    ');
END;
GO
