using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nullam.Migrations
{
    /// <inheritdoc />
    public partial class SetUpDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurrenceTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LabelText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticipantAmount = table.Column<int>(type: "int", nullable: false),
                    RegistrationCode = table.Column<double>(type: "float", nullable: false),
                    PaymentMethodTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_PaymentMethodType_PaymentMethodTypeId",
                        column: x => x.PaymentMethodTypeId,
                        principalTable: "PaymentMethodType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCode = table.Column<double>(type: "float", nullable: false),
                    PaymentMethodTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_PaymentMethodType_PaymentMethodTypeId",
                        column: x => x.PaymentMethodTypeId,
                        principalTable: "PaymentMethodType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventCompany",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCompany", x => new { x.EventId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_EventCompany_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCompany_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventPerson",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPerson", x => new { x.EventId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_EventPerson_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventPerson_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "Info", "Name", "OccurrenceTime", "Place" },
                values: new object[,]
                {
                    { new Guid("0b39238d-daf7-421c-9947-8b3ef79852ab"), "Itaque cum eum in custodiam dedisset et praefectus custodum quaesisset, quemadmodum servari vellet", "Perdiccam opprimendum", new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nam invidia ducum" },
                    { new Guid("102cae40-d67b-4ab4-91e8-a9478a382cca"), "Vulgus autem offensa in eum militum", "Zacynthios adulescentes", new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hic autem" },
                    { new Guid("6a3e00af-001a-42a2-8c94-523b698388b8"), " Qui motus non minus sudorem excutiebat, quam si in spatio decurreret.", "Dionis uxorem", new DateTime(2022, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Syracusanus, nobili genere natus" },
                    { new Guid("d01267c3-eaaf-4b10-aff6-040004763f80"), "Minus sudorem excutiebat", "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium", new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Libro plura" },
                    { new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"), "Alexandrum Magnum reges sunt appellati, ex hoc facillime potest iudicari, quod nemo Eumene vivo rex appellatus", "VII annos Philippo", new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexandri liberis regnum servare" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethodType",
                columns: new[] { "Id", "LabelText", "Name" },
                values: new object[,]
                {
                    { 1, "Sularaha", "Cash" },
                    { 2, "Pangaülekanne", "BankTransfer" }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "CreatedDate", "Info", "Name", "ParticipantAmount", "PaymentMethodTypeId", "RegistrationCode" },
                values: new object[,]
                {
                    { new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b"), new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Local), "Qui simul cum eo ex Peloponneso in Siciliam venerat", "BlueBerry OÜ", 0, 1, 70334221.0 },
                    { new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8"), new DateTime(2023, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), "Sed Dion, fretus non tam", "WhiteBerry AS", 0, 2, 12341234.0 }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IdCode", "Info", "LastName", "PaymentMethodTypeId" },
                values: new object[,]
                {
                    { new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e"), new DateTime(2023, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "Martin", 39025325813.0, "Sed Dion, fretus non tam", "Crisp", 2 },
                    { new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7"), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Local), "Malle", 49125238133.0, "Qui simul cum eo ex Peloponneso in Siciliam venerat", "Code", 1 }
                });

            migrationBuilder.InsertData(
                table: "EventCompany",
                columns: new[] { "CompanyId", "EventId" },
                values: new object[,]
                {
                    { new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8"), new Guid("0b39238d-daf7-421c-9947-8b3ef79852ab") },
                    { new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8"), new Guid("102cae40-d67b-4ab4-91e8-a9478a382cca") },
                    { new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b"), new Guid("6a3e00af-001a-42a2-8c94-523b698388b8") },
                    { new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b"), new Guid("d01267c3-eaaf-4b10-aff6-040004763f80") },
                    { new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b"), new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2") },
                    { new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8"), new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2") }
                });

            migrationBuilder.InsertData(
                table: "EventPerson",
                columns: new[] { "EventId", "PersonId" },
                values: new object[,]
                {
                    { new Guid("0b39238d-daf7-421c-9947-8b3ef79852ab"), new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7") },
                    { new Guid("102cae40-d67b-4ab4-91e8-a9478a382cca"), new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e") },
                    { new Guid("6a3e00af-001a-42a2-8c94-523b698388b8"), new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7") },
                    { new Guid("d01267c3-eaaf-4b10-aff6-040004763f80"), new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e") },
                    { new Guid("d01267c3-eaaf-4b10-aff6-040004763f80"), new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7") },
                    { new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"), new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e") },
                    { new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"), new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_PaymentMethodTypeId",
                table: "Company",
                column: "PaymentMethodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCompany_CompanyId",
                table: "EventCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPerson_PersonId",
                table: "EventPerson",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PaymentMethodTypeId",
                table: "Person",
                column: "PaymentMethodTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventCompany");

            migrationBuilder.DropTable(
                name: "EventPerson");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "PaymentMethodType");
        }
    }
}
