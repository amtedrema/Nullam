using Microsoft.EntityFrameworkCore;
using Nullam.Models;

namespace Nullam.Data
{
    /// <summary>
    /// Represents the database context for Nullam
    /// </summary>
    public class NullamDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet for the Event entity
        /// </summary>
        public virtual DbSet<Event> Event { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Company entity
        /// </summary>
        public virtual DbSet<Company> Company { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Person entity
        /// </summary>
        public virtual DbSet<Person> Person { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the PaymentMethodType entity
        /// </summary>
        public virtual DbSet<PaymentMethodType> PaymentMethodType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullamDbContext"/> class. Needed in Moq testing
        /// </summary>
        public NullamDbContext() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullamDbContext"/> class with the specified options
        /// </summary>
        /// <param name="options">The options for configuring the context</param>
        public NullamDbContext(DbContextOptions<NullamDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasIndex(u => u.IdCode)
                .IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(u => u.RegistrationCode)
                .IsUnique();

            modelBuilder.Entity<Company>()
                .HasOne(v => v.PaymentMethodType)
                .WithMany()
                .HasForeignKey(v => v.PaymentMethodTypeId);

            modelBuilder.Entity<Person>()
                .HasOne(v => v.PaymentMethodType)
                .WithMany()
                .HasForeignKey(v => v.PaymentMethodTypeId);

            int paymentMethodTypeId1;
            int paymentMethodTypeId2;

            modelBuilder.Entity<PaymentMethodType>().HasData(
                new PaymentMethodType { Id = paymentMethodTypeId1 = 1, Name = "Cash", LabelText = "Sularaha" },
                new PaymentMethodType { Id = paymentMethodTypeId2 = 2, Name = "BankTransfer", LabelText = "Pangaülekanne" }
            );

            Guid companyId1;
            Guid companyId2;

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = companyId1 = Guid.NewGuid(),
                    RegistrationCode = 70334221,
                    Name = "BlueBerry OÜ",
                    Info = "Qui simul cum eo ex Peloponneso in Siciliam venerat",
                    PaymentMethodTypeId = paymentMethodTypeId1,
                    CreatedDate = DateTime.Today.AddDays(-1),
                    ParticipantAmount = 5
                },
                new Company
                {
                    Id = companyId2 = Guid.NewGuid(),
                    RegistrationCode = 12341234,
                    Name = "WhiteBerry AS",
                    Info = "Sed Dion, fretus non tam",
                    PaymentMethodTypeId = paymentMethodTypeId2,
                    CreatedDate = DateTime.Today.AddDays(-3),
                    ParticipantAmount = 15
                });

            Guid personId1;
            Guid personId2;

            modelBuilder.Entity<Person>().HasData(
               new Person
               {
                   Id = personId1 = Guid.NewGuid(),
                   IdCode = 49125238133,
                   FirstName = "Malle",
                   LastName = "Code",
                   Info = "Qui simul cum eo ex Peloponneso in Siciliam venerat",
                   PaymentMethodTypeId = paymentMethodTypeId1,
                   CreatedDate = DateTime.Today.AddDays(-2)
               },
               new Person
               {
                   Id = personId2 = Guid.NewGuid(),
                   IdCode = 39025325813,
                   FirstName = "Martin",
                   LastName = "Crisp",
                   Info = "Sed Dion, fretus non tam",
                   PaymentMethodTypeId = paymentMethodTypeId2,
                   CreatedDate = DateTime.Today.AddDays(-4)
               });


            Guid eventId1;
            Guid eventId2;
            Guid eventId3;
            Guid eventId4;
            Guid eventId5;

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = eventId1 = Guid.NewGuid(),
                    Name = "Sed ut perspiciatis unde ",
                    OccurrenceTime = new DateTime(2024, 2, 16),
                    Place = "Libro plura",
                    Info = "Minus sudorem excutiebat"
                },
                new Event
                {
                    Id = eventId2 = Guid.NewGuid(),
                    Name = "Zacynthios adulescentes",
                    OccurrenceTime = new DateTime(2025, 6, 12),
                    Place = "Hic autem",
                    Info = "Vulgus autem offensa in eum militum"
                },
                new Event
                {
                    Id = eventId3 = Guid.NewGuid(),
                    Name = "Dionis uxorem",
                    OccurrenceTime = new DateTime(2022, 8, 27),
                    Place = "Syracusanus, nobili genere natus",
                    Info = " Qui motus non minus sudorem excutiebat, quam si in spatio decurreret."
                },
                new Event
                {
                    Id = eventId4 = Guid.NewGuid(),
                    Name = "Perdiccam opprimendum",
                    OccurrenceTime = new DateTime(2021, 9, 3),
                    Place = "Nam invidia ducum",
                    Info = "Itaque cum eum in custodiam dedisset et praefectus custodum quaesisset, quemadmodum servari vellet"
                },
                new Event
                {
                    Id = eventId5 = Guid.NewGuid(),
                    Name = "VII annos Philippo",
                    OccurrenceTime = new DateTime(2024, 2, 14),
                    Place = "Alexandri liberis regnum servare",
                    Info = "Alexandrum Magnum reges sunt appellati, ex hoc facillime potest iudicari, quod nemo Eumene vivo rex appellatus"
                });

            modelBuilder.Entity<Company>()
                .HasMany(p => p.Events)
                .WithMany(t => t.Companies)
                .UsingEntity<Dictionary<string, object>>(
                    "EventCompany",
                    r => r.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                    l => l.HasOne<Company>().WithMany().HasForeignKey("CompanyId"),
                    je =>
                    {
                        je.HasKey("EventId", "CompanyId");
                        je.HasData(
                            new { EventId = eventId1, CompanyId = companyId1 },
                            new { EventId = eventId2, CompanyId = companyId2 },
                            new { EventId = eventId3, CompanyId = companyId1 },
                            new { EventId = eventId4, CompanyId = companyId2 },
                            new { EventId = eventId5, CompanyId = companyId1 },
                            new { EventId = eventId5, CompanyId = companyId2 });
                    });

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Events)
                .WithMany(t => t.Persons)
                .UsingEntity<Dictionary<string, object>>(
                    "EventPerson",
                    r => r.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                    l => l.HasOne<Person>().WithMany().HasForeignKey("PersonId"),
                    je =>
                    {
                        je.HasKey("EventId", "PersonId");
                        je.HasData(
                            new { EventId = eventId1, PersonId = personId1 },
                            new { EventId = eventId1, PersonId = personId2 },
                            new { EventId = eventId2, PersonId = personId2 },
                            new { EventId = eventId3, PersonId = personId1 },
                            new { EventId = eventId4, PersonId = personId1 },
                            new { EventId = eventId5, PersonId = personId1 },
                            new { EventId = eventId5, PersonId = personId2 });
                    });
        }
    }
}
