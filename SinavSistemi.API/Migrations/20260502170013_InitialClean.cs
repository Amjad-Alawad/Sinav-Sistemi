using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinavSistemi.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hocalar",
                columns: table => new
                {
                    HocaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hocalar", x => x.HocaId);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    OgrenciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.OgrenciId);
                });

            migrationBuilder.CreateTable(
                name: "Salonlar",
                columns: table => new
                {
                    SalonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapasite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salonlar", x => x.SalonId);
                });

            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    DersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HocaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.DersId);
                    table.ForeignKey(
                        name: "FK_Dersler_Hocalar_HocaId",
                        column: x => x.HocaId,
                        principalTable: "Hocalar",
                        principalColumn: "HocaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrenciDersleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    DersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciDersleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrenciDersleri_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "DersId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgrenciDersleri_Ogrenciler_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrenciler",
                        principalColumn: "OgrenciId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sinavlar",
                columns: table => new
                {
                    SinavId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersId = table.Column<int>(type: "int", nullable: false),
                    SalonId = table.Column<int>(type: "int", nullable: false),
                    SinavTarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    BaslangicSaati = table.Column<TimeOnly>(type: "time", nullable: false),
                    BitisSaati = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinavlar", x => x.SinavId);
                    table.ForeignKey(
                        name: "FK_Sinavlar_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "DersId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sinavlar_Salonlar_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salonlar",
                        principalColumn: "SalonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrenciSinavlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    SinavId = table.Column<int>(type: "int", nullable: false),
                    Notu = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciSinavlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrenciSinavlari_Ogrenciler_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrenciler",
                        principalColumn: "OgrenciId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgrenciSinavlari_Sinavlar_SinavId",
                        column: x => x.SinavId,
                        principalTable: "Sinavlar",
                        principalColumn: "SinavId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dersler_HocaId",
                table: "Dersler",
                column: "HocaId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciDersleri_DersId",
                table: "OgrenciDersleri",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciDersleri_OgrenciId_DersId",
                table: "OgrenciDersleri",
                columns: new[] { "OgrenciId", "DersId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciSinavlari_OgrenciId_SinavId",
                table: "OgrenciSinavlari",
                columns: new[] { "OgrenciId", "SinavId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciSinavlari_SinavId",
                table: "OgrenciSinavlari",
                column: "SinavId");

            migrationBuilder.CreateIndex(
                name: "IX_Sinavlar_DersId",
                table: "Sinavlar",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_Sinavlar_SalonId",
                table: "Sinavlar",
                column: "SalonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgrenciDersleri");

            migrationBuilder.DropTable(
                name: "OgrenciSinavlari");

            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "Sinavlar");

            migrationBuilder.DropTable(
                name: "Dersler");

            migrationBuilder.DropTable(
                name: "Salonlar");

            migrationBuilder.DropTable(
                name: "Hocalar");
        }
    }
}
