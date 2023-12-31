﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nullam.Data;

#nullable disable

namespace Nullam.Migrations
{
    [DbContext(typeof(NullamDbContext))]
    [Migration("20230612222053_SetUpDb")]
    partial class SetUpDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventCompany", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventId", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("EventCompany");

                    b.HasData(
                        new
                        {
                            EventId = new Guid("d01267c3-eaaf-4b10-aff6-040004763f80"),
                            CompanyId = new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b")
                        },
                        new
                        {
                            EventId = new Guid("102cae40-d67b-4ab4-91e8-a9478a382cca"),
                            CompanyId = new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8")
                        },
                        new
                        {
                            EventId = new Guid("6a3e00af-001a-42a2-8c94-523b698388b8"),
                            CompanyId = new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b")
                        },
                        new
                        {
                            EventId = new Guid("0b39238d-daf7-421c-9947-8b3ef79852ab"),
                            CompanyId = new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8")
                        },
                        new
                        {
                            EventId = new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"),
                            CompanyId = new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b")
                        },
                        new
                        {
                            EventId = new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"),
                            CompanyId = new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8")
                        });
                });

            modelBuilder.Entity("EventPerson", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("EventPerson");

                    b.HasData(
                        new
                        {
                            EventId = new Guid("d01267c3-eaaf-4b10-aff6-040004763f80"),
                            PersonId = new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7")
                        },
                        new
                        {
                            EventId = new Guid("d01267c3-eaaf-4b10-aff6-040004763f80"),
                            PersonId = new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e")
                        },
                        new
                        {
                            EventId = new Guid("102cae40-d67b-4ab4-91e8-a9478a382cca"),
                            PersonId = new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e")
                        },
                        new
                        {
                            EventId = new Guid("6a3e00af-001a-42a2-8c94-523b698388b8"),
                            PersonId = new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7")
                        },
                        new
                        {
                            EventId = new Guid("0b39238d-daf7-421c-9947-8b3ef79852ab"),
                            PersonId = new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7")
                        },
                        new
                        {
                            EventId = new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"),
                            PersonId = new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7")
                        },
                        new
                        {
                            EventId = new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"),
                            PersonId = new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e")
                        });
                });

            modelBuilder.Entity("Nullam.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParticipantAmount")
                        .HasColumnType("int");

                    b.Property<int>("PaymentMethodTypeId")
                        .HasColumnType("int");

                    b.Property<double>("RegistrationCode")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodTypeId");

                    b.ToTable("Company");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c22d4e06-421f-496c-b0a0-bc49f963853b"),
                            CreatedDate = new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Local),
                            Info = "Qui simul cum eo ex Peloponneso in Siciliam venerat",
                            Name = "BlueBerry OÜ",
                            ParticipantAmount = 0,
                            PaymentMethodTypeId = 1,
                            RegistrationCode = 70334221.0
                        },
                        new
                        {
                            Id = new Guid("e07be49d-5485-4eb4-8e03-7856dd123cd8"),
                            CreatedDate = new DateTime(2023, 6, 11, 0, 0, 0, 0, DateTimeKind.Local),
                            Info = "Sed Dion, fretus non tam",
                            Name = "WhiteBerry AS",
                            ParticipantAmount = 0,
                            PaymentMethodTypeId = 2,
                            RegistrationCode = 12341234.0
                        });
                });

            modelBuilder.Entity("Nullam.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurrenceTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Event");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d01267c3-eaaf-4b10-aff6-040004763f80"),
                            Info = "Minus sudorem excutiebat",
                            Name = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium",
                            OccurrenceTime = new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Place = "Libro plura"
                        },
                        new
                        {
                            Id = new Guid("102cae40-d67b-4ab4-91e8-a9478a382cca"),
                            Info = "Vulgus autem offensa in eum militum",
                            Name = "Zacynthios adulescentes",
                            OccurrenceTime = new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Place = "Hic autem"
                        },
                        new
                        {
                            Id = new Guid("6a3e00af-001a-42a2-8c94-523b698388b8"),
                            Info = " Qui motus non minus sudorem excutiebat, quam si in spatio decurreret.",
                            Name = "Dionis uxorem",
                            OccurrenceTime = new DateTime(2022, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Place = "Syracusanus, nobili genere natus"
                        },
                        new
                        {
                            Id = new Guid("0b39238d-daf7-421c-9947-8b3ef79852ab"),
                            Info = "Itaque cum eum in custodiam dedisset et praefectus custodum quaesisset, quemadmodum servari vellet",
                            Name = "Perdiccam opprimendum",
                            OccurrenceTime = new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Place = "Nam invidia ducum"
                        },
                        new
                        {
                            Id = new Guid("f67d4664-dfa2-4c29-847c-a9ad68144ef2"),
                            Info = "Alexandrum Magnum reges sunt appellati, ex hoc facillime potest iudicari, quod nemo Eumene vivo rex appellatus",
                            Name = "VII annos Philippo",
                            OccurrenceTime = new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Place = "Alexandri liberis regnum servare"
                        });
                });

            modelBuilder.Entity("Nullam.Models.PaymentMethodType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LabelText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethodType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LabelText = "Sularaha",
                            Name = "Cash"
                        },
                        new
                        {
                            Id = 2,
                            LabelText = "Pangaülekanne",
                            Name = "BankTransfer"
                        });
                });

            modelBuilder.Entity("Nullam.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("IdCode")
                        .HasColumnType("float");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentMethodTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodTypeId");

                    b.ToTable("Person");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2b4818ec-606d-4b42-8c01-0f93a7eb66d7"),
                            CreatedDate = new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "Malle",
                            IdCode = 49125238133.0,
                            Info = "Qui simul cum eo ex Peloponneso in Siciliam venerat",
                            LastName = "Code",
                            PaymentMethodTypeId = 1
                        },
                        new
                        {
                            Id = new Guid("0c204104-8182-4e98-b8f6-b3f93a46a67e"),
                            CreatedDate = new DateTime(2023, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "Martin",
                            IdCode = 39025325813.0,
                            Info = "Sed Dion, fretus non tam",
                            LastName = "Crisp",
                            PaymentMethodTypeId = 2
                        });
                });

            modelBuilder.Entity("EventCompany", b =>
                {
                    b.HasOne("Nullam.Models.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nullam.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventPerson", b =>
                {
                    b.HasOne("Nullam.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nullam.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nullam.Models.Company", b =>
                {
                    b.HasOne("Nullam.Models.PaymentMethodType", "PaymentMethodType")
                        .WithMany()
                        .HasForeignKey("PaymentMethodTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethodType");
                });

            modelBuilder.Entity("Nullam.Models.Person", b =>
                {
                    b.HasOne("Nullam.Models.PaymentMethodType", "PaymentMethodType")
                        .WithMany()
                        .HasForeignKey("PaymentMethodTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethodType");
                });
#pragma warning restore 612, 618
        }
    }
}
