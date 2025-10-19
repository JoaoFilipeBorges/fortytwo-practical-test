using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortytwo.PracticalTest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class insertAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
         INSERT INTO Users 
             (FullName, UserName, Email, ProfilePicUrl, PhoneNumber, CreatedOn, UpdatedOn, CreatedBy, UpdatedBy)
         VALUES
             (
                 'Admin User',                   -- FullName
                 'admin',                         -- UserName
                 'admin@example.com',             -- Email
                 '',                              -- ProfilePicUrl
                 '',                              -- PhoneNumber
                 CURRENT_TIMESTAMP,               -- CreatedOn
                 CURRENT_TIMESTAMP,               -- UpdatedOn
                 0,                               -- CreatedBy
                 0                                -- UpdatedBy
             );
     ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
