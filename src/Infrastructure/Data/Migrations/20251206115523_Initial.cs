using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finisher.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "user");

        migrationBuilder.CreateTable(
            name: "ApplicationRole",
            schema: "user",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationRole", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ApplicationUser",
            schema: "user",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationUser", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ApplicationRoleClaim",
            schema: "user",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<int>(type: "int", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationRoleClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_ApplicationRoleClaim_ApplicationRole_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "user",
                    principalTable: "ApplicationRole",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ApplicationUserClaim",
            schema: "user",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationUserClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_ApplicationUserClaim_ApplicationUser_UserId",
                    column: x => x.UserId,
                    principalSchema: "user",
                    principalTable: "ApplicationUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ApplicationUserLogin",
            schema: "user",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    name: "FK_ApplicationUserLogin_ApplicationUser_UserId",
                    column: x => x.UserId,
                    principalSchema: "user",
                    principalTable: "ApplicationUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ApplicationUserRole",
            schema: "user",
            columns: table => new
            {
                UserId = table.Column<int>(type: "int", nullable: false),
                RoleId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationUserRole", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_ApplicationUserRole_ApplicationRole_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "user",
                    principalTable: "ApplicationRole",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ApplicationUserRole_ApplicationUser_UserId",
                    column: x => x.UserId,
                    principalSchema: "user",
                    principalTable: "ApplicationUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ApplicationUserToken",
            schema: "user",
            columns: table => new
            {
                UserId = table.Column<int>(type: "int", nullable: false),
                LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_ApplicationUserToken_ApplicationUser_UserId",
                    column: x => x.UserId,
                    principalSchema: "user",
                    principalTable: "ApplicationUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            schema: "user",
            table: "ApplicationRole",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_ApplicationRoleClaim_RoleId",
            schema: "user",
            table: "ApplicationRoleClaim",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            schema: "user",
            table: "ApplicationUser",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "ix_userApplicationUseruserNationalCode",
            schema: "user",
            table: "ApplicationUser",
            column: "NationalCode",
            unique: true,
            filter: "[NationalCode] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "ix_userApplicationUseruserPhoneNumber",
            schema: "user",
            table: "ApplicationUser",
            column: "PhoneNumber",
            unique: true,
            filter: "[PhoneNumber] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            schema: "user",
            table: "ApplicationUser",
            column: "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_ApplicationUserClaim_UserId",
            schema: "user",
            table: "ApplicationUserClaim",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ApplicationUserLogin_UserId",
            schema: "user",
            table: "ApplicationUserLogin",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ApplicationUserRole_RoleId",
            schema: "user",
            table: "ApplicationUserRole",
            column: "RoleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ApplicationRoleClaim",
            schema: "user");

        migrationBuilder.DropTable(
            name: "ApplicationUserClaim",
            schema: "user");

        migrationBuilder.DropTable(
            name: "ApplicationUserLogin",
            schema: "user");

        migrationBuilder.DropTable(
            name: "ApplicationUserRole",
            schema: "user");

        migrationBuilder.DropTable(
            name: "ApplicationUserToken",
            schema: "user");

        migrationBuilder.DropTable(
            name: "ApplicationRole",
            schema: "user");

        migrationBuilder.DropTable(
            name: "ApplicationUser",
            schema: "user");
    }
}
