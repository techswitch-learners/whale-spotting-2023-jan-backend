# Whale Spotting Backend

## Setup

First, run `dotnet build`.

Create a new role in your Postgres installation with login and create database permissions. (For example, `whale-spotting` with password `whale-spotting`.)

Then, set the two environment variables `DATABASE_URL` and `USE_SSL`. These follow the format:

```powershell
$env:DATABASE_URL = "postgres://whale-spotting:whale-spotting@localhost:5432/whale-spotting"
$env:USE_SSL = "false"
```

(Note: the three `whale-spotting`s in the `DATABASE_URL` are the username, password, and database name, in that order.)

Then, run the migrations to create the database with

```powershell
dotnet ef database update
```

If this succeeds, you are successfully connected to the database!

You can then run the app with `dotnet run`.
