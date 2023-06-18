using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nullam.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8"), new Guid("0b39238d-daf7-421c-9947-8b3ef79852ab") });

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8"), new Guid("102cae40-d67b-4ab4-91e8-a9478a382cca") });

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b"), new Guid("6a3e00af-001a-42a2-8c94-523b698388b8") });

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b"), new Guid("d01267c3-eaaf-4b10-aff6-040004763f80") });

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b"), new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2") });

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8"), new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("0b39238d-daf7-421c-9947-8b3ef79852ab"), new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("102cae40-d67b-4ab4-91e8-a9478a382cca"), new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("6a3e00af-001a-42a2-8c94-523b698388b8"), new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("d01267c3-eaaf-4b10-aff6-040004763f80"), new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("d01267c3-eaaf-4b10-aff6-040004763f80"), new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"), new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"), new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7") });

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("0b39238d-daf7-421c-9947-8b3ef79852ab"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("102cae40-d67b-4ab4-91e8-a9478a382cca"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("6a3e00af-001a-42a2-8c94-523b698388b8"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("d01267c3-eaaf-4b10-aff6-040004763f80"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"));

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e"));

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7"));

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Person",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "IdCode",
                table: "Person",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "CreatedDate", "Info", "Name", "ParticipantAmount", "PaymentMethodTypeId", "RegistrationCode" },
                values: new object[,]
                {
                    { new Guid("69bc0ae3-667b-4e09-82e9-46fe44883fbf"), new DateTime(2023, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Qui simul cum eo ex Peloponneso in Siciliam venerat", "BlueBerry OÜ", 5, 1, 70334221.0 },
                    { new Guid("c9c4dfea-07ae-4794-9895-a40373427e3a"), new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Local), "Sed Dion, fretus non tam", "WhiteBerry AS", 15, 2, 12341234.0 }
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "Info", "Name", "OccurrenceTime", "Place" },
                values: new object[,]
                {
                    { new Guid("5eff17f2-f1b4-45fa-a8f7-b435be71a302"), "Itaque cum eum in custodiam dedisset et praefectus custodum quaesisset, quemadmodum servari vellet", "Perdiccam opprimendum", new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nam invidia ducum" },
                    { new Guid("aa57c261-7765-41d0-898f-1d6621fb26f2"), "Vulgus autem offensa in eum militum", "Zacynthios adulescentes", new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hic autem" },
                    { new Guid("ab2d252f-6c74-421b-bfdd-f323b4912157"), " Qui motus non minus sudorem excutiebat, quam si in spatio decurreret.", "Dionis uxorem", new DateTime(2022, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Syracusanus, nobili genere natus" },
                    { new Guid("d979c31b-617f-4fea-ac66-11d2ba628faf"), "Alexandrum Magnum reges sunt appellati, ex hoc facillime potest iudicari, quod nemo Eumene vivo rex appellatus", "VII annos Philippo", new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexandri liberis regnum servare" },
                    { new Guid("e426f972-f70d-480e-9876-913dc03393e0"), "Minus sudorem excutiebat", "Sed ut perspiciatis unde ", new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Libro plura" }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IdCode", "Info", "LastName", "PaymentMethodTypeId" },
                values: new object[,]
                {
                    { new Guid("885d8f89-f52d-43b7-b3a3-bed2b5da951c"), new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), "Malle", 49125238133L, "Qui simul cum eo ex Peloponneso in Siciliam venerat", "Code", 1 },
                    { new Guid("db46fbef-fd61-4027-93d0-a2b46af2c96d"), new DateTime(2023, 6, 14, 0, 0, 0, 0, DateTimeKind.Local), "Martin", 39025325813L, "Sed Dion, fretus non tam", "Crisp", 2 }
                });

            migrationBuilder.InsertData(
                table: "EventCompany",
                columns: new[] { "CompanyId", "EventId" },
                values: new object[,]
                {
                    { new Guid("c9c4dfea-07ae-4794-9895-a40373427e3a"), new Guid("5eff17f2-f1b4-45fa-a8f7-b435be71a302") },
                    { new Guid("c9c4dfea-07ae-4794-9895-a40373427e3a"), new Guid("aa57c261-7765-41d0-898f-1d6621fb26f2") },
                    { new Guid("69bc0ae3-667b-4e09-82e9-46fe44883fbf"), new Guid("ab2d252f-6c74-421b-bfdd-f323b4912157") },
                    { new Guid("69bc0ae3-667b-4e09-82e9-46fe44883fbf"), new Guid("d979c31b-617f-4fea-ac66-11d2ba628faf") },
                    { new Guid("c9c4dfea-07ae-4794-9895-a40373427e3a"), new Guid("d979c31b-617f-4fea-ac66-11d2ba628faf") },
                    { new Guid("69bc0ae3-667b-4e09-82e9-46fe44883fbf"), new Guid("e426f972-f70d-480e-9876-913dc03393e0") }
                });

            migrationBuilder.InsertData(
                table: "EventPerson",
                columns: new[] { "EventId", "PersonId" },
                values: new object[,]
                {
                    { new Guid("5eff17f2-f1b4-45fa-a8f7-b435be71a302"), new Guid("885d8f89-f52d-43b7-b3a3-bed2b5da951c") },
                    { new Guid("aa57c261-7765-41d0-898f-1d6621fb26f2"), new Guid("db46fbef-fd61-4027-93d0-a2b46af2c96d") },
                    { new Guid("ab2d252f-6c74-421b-bfdd-f323b4912157"), new Guid("885d8f89-f52d-43b7-b3a3-bed2b5da951c") },
                    { new Guid("d979c31b-617f-4fea-ac66-11d2ba628faf"), new Guid("885d8f89-f52d-43b7-b3a3-bed2b5da951c") },
                    { new Guid("d979c31b-617f-4fea-ac66-11d2ba628faf"), new Guid("db46fbef-fd61-4027-93d0-a2b46af2c96d") },
                    { new Guid("e426f972-f70d-480e-9876-913dc03393e0"), new Guid("885d8f89-f52d-43b7-b3a3-bed2b5da951c") },
                    { new Guid("e426f972-f70d-480e-9876-913dc03393e0"), new Guid("db46fbef-fd61-4027-93d0-a2b46af2c96d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_IdCode",
                table: "Person",
                column: "IdCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_RegistrationCode",
                table: "Company",
                column: "RegistrationCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_IdCode",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Company_RegistrationCode",
                table: "Company");

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("c9c4dfea-07ae-4794-9895-a40373427e3a"), new Guid("5eff17f2-f1b4-45fa-a8f7-b435be71a302") });

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("c9c4dfea-07ae-4794-9895-a40373427e3a"), new Guid("aa57c261-7765-41d0-898f-1d6621fb26f2") });

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("69bc0ae3-667b-4e09-82e9-46fe44883fbf"), new Guid("ab2d252f-6c74-421b-bfdd-f323b4912157") });

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("69bc0ae3-667b-4e09-82e9-46fe44883fbf"), new Guid("d979c31b-617f-4fea-ac66-11d2ba628faf") });

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("c9c4dfea-07ae-4794-9895-a40373427e3a"), new Guid("d979c31b-617f-4fea-ac66-11d2ba628faf") });

            migrationBuilder.DeleteData(
                table: "EventCompany",
                keyColumns: new[] { "CompanyId", "EventId" },
                keyValues: new object[] { new Guid("69bc0ae3-667b-4e09-82e9-46fe44883fbf"), new Guid("e426f972-f70d-480e-9876-913dc03393e0") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("5eff17f2-f1b4-45fa-a8f7-b435be71a302"), new Guid("885d8f89-f52d-43b7-b3a3-bed2b5da951c") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("aa57c261-7765-41d0-898f-1d6621fb26f2"), new Guid("db46fbef-fd61-4027-93d0-a2b46af2c96d") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("ab2d252f-6c74-421b-bfdd-f323b4912157"), new Guid("885d8f89-f52d-43b7-b3a3-bed2b5da951c") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("d979c31b-617f-4fea-ac66-11d2ba628faf"), new Guid("885d8f89-f52d-43b7-b3a3-bed2b5da951c") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("d979c31b-617f-4fea-ac66-11d2ba628faf"), new Guid("db46fbef-fd61-4027-93d0-a2b46af2c96d") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("e426f972-f70d-480e-9876-913dc03393e0"), new Guid("885d8f89-f52d-43b7-b3a3-bed2b5da951c") });

            migrationBuilder.DeleteData(
                table: "EventPerson",
                keyColumns: new[] { "EventId", "PersonId" },
                keyValues: new object[] { new Guid("e426f972-f70d-480e-9876-913dc03393e0"), new Guid("db46fbef-fd61-4027-93d0-a2b46af2c96d") });

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("69bc0ae3-667b-4e09-82e9-46fe44883fbf"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("c9c4dfea-07ae-4794-9895-a40373427e3a"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("5eff17f2-f1b4-45fa-a8f7-b435be71a302"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("aa57c261-7765-41d0-898f-1d6621fb26f2"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("ab2d252f-6c74-421b-bfdd-f323b4912157"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("d979c31b-617f-4fea-ac66-11d2ba628faf"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("e426f972-f70d-480e-9876-913dc03393e0"));

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: new Guid("885d8f89-f52d-43b7-b3a3-bed2b5da951c"));

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: new Guid("db46fbef-fd61-4027-93d0-a2b46af2c96d"));

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "IdCode",
                table: "Person",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "CreatedDate", "Info", "Name", "ParticipantAmount", "PaymentMethodTypeId", "RegistrationCode" },
                values: new object[,]
                {
                    { new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b"), new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Local), "Qui simul cum eo ex Peloponneso in Siciliam venerat", "BlueBerry OÜ", 0, 1, 70334221.0 },
                    { new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8"), new DateTime(2023, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), "Sed Dion, fretus non tam", "WhiteBerry AS", 0, 2, 12341234.0 }
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
        }
    }
}
