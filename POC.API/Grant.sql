IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'IIS APPPOOL\POC Api')
BEGIN
    CREATE LOGIN [IIS APPPOOL\POC Api] 
      FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
      DEFAULT_LANGUAGE=[us_english]
END
GO
CREATE USER [POCApiUser] 
  FOR LOGIN [IIS APPPOOL\POC Api]
GO
EXEC sp_addrolemember 'db_owner', 'POCApiUser'
GO