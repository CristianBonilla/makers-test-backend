namespace Makers.Dev.Contracts.Enums;

public enum DbConnectionTypes : ushort
{
  OpenConnection = 1,
  EnsureCreated = 2,
  EnsureDeleted = 3,
  Migration = 4
}
