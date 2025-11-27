using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Penitenciaria.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Celdas",
                columns: table => new
                {
                    CeldaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCelda = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    OcupacionActual = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Celdas", x => x.CeldaID);
                });

            migrationBuilder.CreateTable(
                name: "Crimenes",
                columns: table => new
                {
                    CrimenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCrimen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PenaMinimaAnios = table.Column<int>(type: "int", nullable: true),
                    PenaMaximaAnios = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crimenes", x => x.CrimenID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reos",
                columns: table => new
                {
                    ReoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentenciaTotalAnios = table.Column<int>(type: "int", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CeldaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reos", x => x.ReoID);
                    table.ForeignKey(
                        name: "FK_Reos_Celdas_CeldaID",
                        column: x => x.CeldaID,
                        principalTable: "Celdas",
                        principalColumn: "CeldaID");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContrasenaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenConfirmacionEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenRefresco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiracionTokenRefresco = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TokenReinicioContrasena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiracionTokenReinicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReoCrimenes",
                columns: table => new
                {
                    ReoCrimenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReoID = table.Column<int>(type: "int", nullable: false),
                    CrimenID = table.Column<int>(type: "int", nullable: false),
                    FechaComision = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReoCrimenes", x => x.ReoCrimenID);
                    table.ForeignKey(
                        name: "FK_ReoCrimenes_Crimenes_CrimenID",
                        column: x => x.CrimenID,
                        principalTable: "Crimenes",
                        principalColumn: "CrimenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReoCrimenes_Reos_ReoID",
                        column: x => x.ReoID,
                        principalTable: "Reos",
                        principalColumn: "ReoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Empleado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Celdas_NumeroCelda",
                table: "Celdas",
                column: "NumeroCelda",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Crimenes_NombreCrimen",
                table: "Crimenes",
                column: "NombreCrimen",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReoCrimenes_CrimenID",
                table: "ReoCrimenes",
                column: "CrimenID");

            migrationBuilder.CreateIndex(
                name: "IX_ReoCrimenes_ReoID_CrimenID",
                table: "ReoCrimenes",
                columns: new[] { "ReoID", "CrimenID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reos_CeldaID",
                table: "Reos",
                column: "CeldaID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReoCrimenes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Crimenes");

            migrationBuilder.DropTable(
                name: "Reos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Celdas");
        }
    }
}
