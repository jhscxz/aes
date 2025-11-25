using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aes.Migrations
{
    /// <inheritdoc />
    public partial class removeeditmodelsandrepos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElektraKupacEdit");

            migrationBuilder.DropTable(
                name: "OdsEdit");

            migrationBuilder.DropTable(
                name: "RacunElektraEdit");

            migrationBuilder.DropTable(
                name: "RacunElektraIzvrsenjeUslugeEdit");

            migrationBuilder.DropTable(
                name: "RacunElektraRateEdit");

            migrationBuilder.DropTable(
                name: "RacunHoldingEdit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElektraKupacEdit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElektraKupacId = table.Column<int>(type: "int", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditingByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElektraKupacEdit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElektraKupacEdit_ElektraKupac_ElektraKupacId",
                        column: x => x.ElektraKupacId,
                        principalTable: "ElektraKupac",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OdsEdit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdsId = table.Column<int>(type: "int", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditingByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdsEdit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdsEdit_Ods_OdsId",
                        column: x => x.OdsId,
                        principalTable: "Ods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RacunElektraEdit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RacunElektraId = table.Column<int>(type: "int", nullable: false),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditingByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacunElektraEdit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RacunElektraEdit_RacunElektra_RacunElektraId",
                        column: x => x.RacunElektraId,
                        principalTable: "RacunElektra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RacunElektraIzvrsenjeUslugeEdit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RacunElektraIzvrsenjeUslugeId = table.Column<int>(type: "int", nullable: false),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditingByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacunElektraIzvrsenjeUslugeEdit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RacunElektraIzvrsenjeUslugeEdit_RacunElektraIzvrsenjeUsluge_RacunElektraIzvrsenjeUslugeId",
                        column: x => x.RacunElektraIzvrsenjeUslugeId,
                        principalTable: "RacunElektraIzvrsenjeUsluge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RacunElektraRateEdit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RacunElektraRateId = table.Column<int>(type: "int", nullable: false),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditingByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacunElektraRateEdit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RacunElektraRateEdit_RacunElektraRate_RacunElektraRateId",
                        column: x => x.RacunElektraRateId,
                        principalTable: "RacunElektraRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RacunHoldingEdit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RacunHoldingId = table.Column<int>(type: "int", nullable: false),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditingByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacunHoldingEdit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RacunHoldingEdit_RacunHolding_RacunHoldingId",
                        column: x => x.RacunHoldingId,
                        principalTable: "RacunHolding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElektraKupacEdit_ElektraKupacId",
                table: "ElektraKupacEdit",
                column: "ElektraKupacId");

            migrationBuilder.CreateIndex(
                name: "IX_OdsEdit_OdsId",
                table: "OdsEdit",
                column: "OdsId");

            migrationBuilder.CreateIndex(
                name: "IX_RacunElektraEdit_RacunElektraId",
                table: "RacunElektraEdit",
                column: "RacunElektraId");

            migrationBuilder.CreateIndex(
                name: "IX_RacunElektraIzvrsenjeUslugeEdit_RacunElektraIzvrsenjeUslugeId",
                table: "RacunElektraIzvrsenjeUslugeEdit",
                column: "RacunElektraIzvrsenjeUslugeId");

            migrationBuilder.CreateIndex(
                name: "IX_RacunElektraRateEdit_RacunElektraRateId",
                table: "RacunElektraRateEdit",
                column: "RacunElektraRateId");

            migrationBuilder.CreateIndex(
                name: "IX_RacunHoldingEdit_RacunHoldingId",
                table: "RacunHoldingEdit",
                column: "RacunHoldingId");
        }
    }
}
