using Microsoft.EntityFrameworkCore.Migrations;

namespace PROG1442___Project_1.Data
{
    public static class ExtraMigration
    {
        public static void Steps(MigrationBuilder migrationBuilder)
        {
            //Player table triggers for concurrency
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetPlayerTimestampOnUpdate
                    AFTER UPDATE ON Players
                    BEGIN
                            UPDATE Players
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END;
                ");
            migrationBuilder.Sql(
                @"

                    CREATE TRIGGER SetPlayerTimestampOnInsert
                    AFTER INSERT ON Players
                    BEGIN
                            UPDATE Players
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END;
                ");
        }
    }
}
