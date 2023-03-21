using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WhaleSpotting.Migrations
{
    public partial class AddSightingAndSpeciesModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WhaleSpecies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TailType = table.Column<int>(type: "integer", nullable: false),
                    TeethType = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Colour = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Diet = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhaleSpecies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhaleSightings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfSighting = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LocationLatitude = table.Column<float>(type: "real", nullable: false),
                    LocationLongitude = table.Column<float>(type: "real", nullable: false),
                    PhotoImageURL = table.Column<string>(type: "text", nullable: false),
                    NumberOfWhales = table.Column<int>(type: "integer", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    WhaleSpeciesId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhaleSightings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WhaleSightings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WhaleSightings_WhaleSpecies_WhaleSpeciesId",
                        column: x => x.WhaleSpeciesId,
                        principalTable: "WhaleSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WhaleSightings_UserId",
                table: "WhaleSightings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WhaleSightings_WhaleSpeciesId",
                table: "WhaleSightings",
                column: "WhaleSpeciesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WhaleSightings");

            migrationBuilder.DropTable(
                name: "WhaleSpecies");
        }
    }
}
