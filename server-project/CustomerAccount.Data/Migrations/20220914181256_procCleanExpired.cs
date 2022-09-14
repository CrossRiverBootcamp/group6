using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerAccount.Data.Migrations
{
    public partial class procCleanExpired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"create or alter procedure cleanExpiredCodeRows AS DELETE [dbo].[EmailVerifications] WHERE DATEDIFF(minute,[dbo].[EmailVerifications].[ExpirationTime],getdate())>0";
            migrationBuilder.Sql(createProcSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProcSql = @"drop procedure cleanExpiredCodeRows";
            migrationBuilder.Sql(dropProcSql);
        }
    }
}
