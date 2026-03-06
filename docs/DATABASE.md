# Database: backup, restore, and migrations

This document describes backup and restore procedures for the MerchTrade SQL Server database and how to roll back EF Core migrations when needed.

## Backup

### Full backup (SQL Server)

```sql
BACKUP DATABASE [merchTradeDb]
TO DISK = 'C:\Backups\merchTradeDb_full.bak'
WITH INIT, COMPRESSION, STATS = 10;
```

### Scheduled backups

- Use SQL Server Maintenance Plans or a job (e.g. daily full backup, optional differential/log backups).
- Retain full backups according to your policy (e.g. 7–30 days); keep at least one backup off the server.

### Connection and database name

- Default database name: `merchTradeDb` (configurable via `ConnectionStrings:DefaultConnection` in appsettings).
- Use a dedicated backup user with `db_backupoperator` if not using Windows auth.

---

## Restore

### Restore from full backup

```sql
USE master;
ALTER DATABASE [merchTradeDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE [merchTradeDb]
FROM DISK = 'C:\Backups\merchTradeDb_full.bak'
WITH REPLACE, RECOVERY;
ALTER DATABASE [merchTradeDb] SET MULTI_USER;
```

- Ensure no application is connected (stop MT.Api and any tools using the DB).
- `REPLACE` overwrites the existing database; use a different name if you want to restore to a new DB.

---

## EF Core migrations

### Applying migrations

- **At development:** run `dotnet ef database update` from the `MT.Infrastructure` directory (or solution root with `--project MT.Infrastructure --startup-project MT.Api`).
- **At deploy:** run the same command as a separate step before starting the API, or use startup migration with care when running multiple instances (prefer a single migration runner job).

### Rollback (downgrade) a migration

1. List migrations to get the target migration name:
   ```bash
   dotnet ef migrations list --project MT.Infrastructure --startup-project MT.Api
   ```
2. Roll back to the previous migration (replace `PreviousMigrationName` with the name you want to keep):
   ```bash
   dotnet ef database update PreviousMigrationName --project MT.Infrastructure --startup-project MT.Api
   ```
3. Remove the migration class from the codebase (optional, only if you want to delete the migration entirely):
   - Delete the migration folder under `MT.Infrastructure/Migrations/` that corresponds to the rolled-back migration.
   - Then run `dotnet ef migrations remove --project MT.Infrastructure --startup-project MT.Api` to sync the snapshot.

### Rollback and recovery together

- After a bad deploy: restore the database from backup (see Restore above), then fix the code and migrations.
- If you only need to undo the last migration: use `dotnet ef database update <PreviousMigration>` as above; no backup restore needed unless data was already changed by the new migration.

---

## Health check

The API exposes a `/health` endpoint that includes a check for the database. Use it to verify connectivity after restore or migration:

```bash
curl -s http://localhost:5000/health
```
