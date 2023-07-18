using Common.Configuration;
using Common.Enums;
using DAL.Entities;
using DAL.Entities.AuthCode;
using DAL.Entities.CommonConfigs;
using DAL.Entities.FAQ;
using DAL.Entities.Files;
using DAL.Entities.KontragentIdentity;
using DAL.Entities.Order;
using DAL.Entities.Payment;
using DAL.Entities.PersonIdentity;
using DAL.Entities.Return;
using DAL.Entities.RoutePoint;
using DAL.Entities.Transport;
using DAL.Entities.Trip;
using DAL.Entities.TripRoute;
using DAL.Entities.User;
using DAL.Entities.News;
using DAL.Entities.PassengerData;
using DAL.Entities.TripDraft;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DAL.SQL;

public class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }

	public DbSet<PersonIdentity> Persons { get; set; }

	public DbSet<KontragentIdentity> Kontragents { get; set; }
    public DbSet<TripRoutePoint> TripRoutePoints { get; set; }
    public DbSet<S3File> S3Files { get; set; }
    public DbSet<Base64File> Base64Files { get; set; }
    public DbSet<Entities.Files.File> Files { get; set; }
    public DbSet<CommonConfiguration> CommonConfigurations { get; set; }


    

    //!!!!!!!------------------!!!!!!!
#if DEBUG
	public AppDbContext()
	{
		
	}
#endif
	

    //!!!!!!!------------------!!!!!!!
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        optionsBuilder.ConfigureWarnings(warnings =>
        {
            warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored);
        });
        #if DEBUG
        optionsBuilder.UseNpgsql("");
        #endif
    }

	public DbSet<ClientPersonIdentity> Clients { get; set; }

	public DbSet<DriverPersonIdentity> Drivers { get; set; }

	public DbSet<DispatcherPersonIdentity> Dispatchers { get; set; }

	public DbSet<OperatorPersonIdentity> Operators { get; set; }

	public DbSet<CarrierIdentity> Carriers { get; set; }

	public DbSet<InsuranceIdentity> Insurances { get; set; }

	public DbSet<OperatorOrder> OperatorOrders { get; set; }

	public DbSet<Order> Orders { get; set; }

	public DbSet<Payment> Payments { get; set; }

	public DbSet<Return> Returns { get; set; }

	public DbSet<Transport> Transports { get; set; }

	public DbSet<RoutePoint> RoutePoints { get; set; }

	public DbSet<AuthCode> AuthCodes { get; set; }

	public DbSet<TripRoute> Trips { get; set; }

	public DbSet<FAQ> FAQs { get; set; }
    public DbSet<News> News { get; set; }

    public DbSet<FAQCategory> FAQCategories { get; set; }
    public DbSet<PassengerData> PassengersData { get; set; }
    
    public DbSet<TripDraft> TripDrafts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.Migrate();
    }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Configure identity discriminator
		modelBuilder.Entity<PersonIdentity>()
			.HasDiscriminator(e => e.Discriminator)
			.HasValue<ClientPersonIdentity>(PersonIdentityType.Client.ToString())
			.HasValue<DriverPersonIdentity>(PersonIdentityType.Driver.ToString())
			.HasValue<DispatcherPersonIdentity>(PersonIdentityType.Dispatcher.ToString())
			.HasValue<OperatorPersonIdentity>(PersonIdentityType.Operator.ToString());

		modelBuilder.Entity<PersonIdentity>()
			.HasOne(r => r.User)
			.WithMany(r => r.Identities)
			.HasForeignKey(r => r.UserId);

		modelBuilder.Entity<KontragentIdentity>()
			.HasDiscriminator(e => e.Discriminator)
			.HasValue<CarrierIdentity>(KontragentIdentityType.Carrier.ToString())
			.HasValue<InsuranceIdentity>(KontragentIdentityType.Insurance.ToString());

		modelBuilder.Entity<CarrierIdentity>()
			.HasMany(r => r.Transports)
			.WithOne(e => e.Carrier)
			.HasForeignKey(e => e.CarrierId);

		modelBuilder.Entity<CarrierIdentity>()
			.HasMany(r => r.Drivers)
			.WithOne(e => e.Carrier)
			.HasForeignKey(e => e.CarrierId);
		
		modelBuilder.Entity<CarrierIdentity>()
			.HasMany(r => r.Trips)
			.WithOne(e => e.Carrier)
			.HasForeignKey(e => e.CarrierId);
		

		modelBuilder.Entity<Transport>()
			.HasOne(r => r.Carrier)
			.WithMany(r=>r.Transports)
			.HasForeignKey(r => r.CarrierId);

		modelBuilder.Entity<Transport>()
			.HasOne(r => r.Driver)
			.WithMany()
			.HasForeignKey(r => r.DriverId);

		modelBuilder.Entity<DriverPersonIdentity>()
			.HasOne(r => r.Carrier)
			.WithMany(r=>r.Drivers)
			.HasForeignKey(r => r.CarrierId);
		
		modelBuilder.Entity<DriverPersonIdentity>()
			.HasMany(r => r.Trips)
			.WithOne(r=>r.Driver)
			.HasForeignKey(r => r.DriverId);

		modelBuilder.Entity<Order>()
			.HasDiscriminator(e => e.CreationType)
			.HasValue<OperatorOrder>(OrderCreationType.Operator.ToString())
			.HasValue<Order>(OrderCreationType.Web.ToString());
		//
		modelBuilder.Entity<Order>()
			.HasOne(r => r.Client)
			.WithMany(r => r.Orders)
			.HasForeignKey(r => r.ClientId);
		
		modelBuilder.Entity<Order>()
			.HasOne(r => r.Trip)
			.WithMany(r => r.Orders)
			.HasForeignKey(r => r.TripId);
		
		modelBuilder.Entity<Order>()
			.HasOne(r => r.StartTripRoutePoint)
			.WithMany()
			.HasForeignKey(r => r.StartTripRoutePointId);
		
		modelBuilder.Entity<Order>()
			.HasOne(r => r.EndTripRoutePoint)
			.WithMany()
			.HasForeignKey(r => r.EndTripRoutePointId);
		
		

		modelBuilder.Entity<OperatorOrder>()
			.HasOne(r => r.Operator)
			.WithMany()
			.HasForeignKey(r => r.OperatorId);

		modelBuilder.Entity<Payment>()
			.HasOne(r => r.Return)
			.WithOne()
			.HasForeignKey<Payment>(r => r.ReturnId);

		// modelBuilder.Entity<Order>()
		// 	.HasOne(r => r.Carrier)
		// 	.WithMany()
		// 	.HasForeignKey(r => r.CarrierId);
		//
		// modelBuilder.Entity<Order>()
		// 	.HasOne(r => r.Payment)
		// 	.WithOne()
		// 	.HasForeignKey<Order>(r => r.PaymentId);

		modelBuilder.Entity<User>()
			.HasMany(r => r.Identities)
			.WithOne(r => r.User)
			.HasForeignKey(r => r.UserId);

		modelBuilder.Entity<AuthCode>()
			.HasOne(r => r.User)
			.WithMany(r => r.AuthCodes)
			.HasForeignKey(r => r.UserId);

		modelBuilder.Entity<TripRoute>()
			.HasMany(r => r.TripRoutePoints)
			.WithOne(e => e.TripRoute)
			.HasForeignKey(e => e.TripRouteId);

		modelBuilder.Entity<TripRoute>()
			.HasOne(r => r.PopularDestinationPicture)
			.WithMany()
			.HasForeignKey(r => r.PopularDestinationPictureId);

		modelBuilder.Entity<TripRoutePoint>()
			.HasOne(r => r.TripRoute)
			.WithMany(r => r.TripRoutePoints)
			.HasForeignKey(r => r.TripRouteId);

		modelBuilder.Entity<TripRoutePoint>()
			.HasOne(r => r.RoutePoint)
			.WithMany()
			.HasForeignKey(r => r.RoutePointId);

		modelBuilder.Entity<FAQCategory>()
			.HasMany(r => r.FAQs)
			.WithOne(e => e.Category)
			.HasForeignKey(e => e.CategoryId);

		modelBuilder.Entity<FAQ>()
			.HasOne(r => r.Category)
			.WithMany()
			.HasForeignKey(r => r.CategoryId);

		modelBuilder.Entity<Trip>()
			.HasOne(r => r.TripRoute)
			.WithMany()
			.HasForeignKey(r => r.TripRouteId);

		modelBuilder.Entity<Trip>()
			.HasOne(r => r.Carrier)
			.WithMany(r=>r.Trips)
			.HasForeignKey(r => r.CarrierId);

		modelBuilder.Entity<Trip>()
			.HasOne(r => r.Transport)
			.WithMany()
			.HasForeignKey(r => r.TransportId);
		
		modelBuilder.Entity<Trip>()
			.HasOne(r => r.Driver)
			.WithMany(r=>r.Trips)
			.HasForeignKey(r => r.DriverId);
		
		modelBuilder.Entity<Entities.Files.File>()
			.HasDiscriminator(e => e.Discriminator)
			.HasValue<Base64File>(FileType.Base64.ToString())
			.HasValue<S3File>(FileType.S3.ToString());

		modelBuilder.Entity<Entities.Files.File>()
			.HasOne(r => r.Uploader)
			.WithMany()
			.HasForeignKey(r => r.UploaderId);

		modelBuilder.Entity<News>()
			.HasOne(r => r.File)
			.WithMany()
			.HasForeignKey(r => r.FileId);

		// prefil data
		modelBuilder.Entity<RoutePoint>()
			.HasData(new List<RoutePoint>()
			{
				new RoutePoint()
				{
					Address = "Москва, Россия",
					Latitude = "55.755826",
					Longitude = "37.6172999",
					Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
					Title = "Москва",
					CreatedAt = DateTime.Parse("2023-01-01")
				},
				new RoutePoint()
				{
					Address = "Санкт-Петербург, Россия",
					Latitude = "59.9342802",
					Longitude = "30.3350986",
					Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
					Title = "Санкт-Петербург",
					CreatedAt = DateTime.Parse("2023-01-01")
				},
			});

        modelBuilder.Entity<User>()
            .HasMany(r=>r.Identities)
            .WithOne(r=>r.User)
            .HasForeignKey(r=>r.UserId);
        
        modelBuilder.Entity<AuthCode>()
            .HasOne(r=>r.User)
            .WithMany(r=>r.AuthCodes)
            .HasForeignKey(r=>r.UserId);
        
        modelBuilder.Entity<PassengerData>()
	        .HasOne(r => r.User)
	        .WithMany()
	        .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<User>().HasData(new User
        {
	        Id = Constants.DefaultUserId,
	        CreatedAt = DateTime.Parse("2023-01-01"),
	        IsDeleted = false,
	        PasswordHash = "e10adc3949ba59abbe56e057f20f883e",
	        Email = "not@user.com",
	        PhoneNumber = "+0000000000",
	        Roles = "Client",
	        FirstName = "Заказ оператора",
	        LastName = "",
	        Patronymic = "",
	        BirthDate = DateTime.Parse("2023-01-01"),
	        Sex = null
        });

        modelBuilder.Entity<ClientPersonIdentity>().HasData(new ClientPersonIdentity()
        {
	        Id = Constants.DefaultClientId,
	        CreatedAt = DateTime.Parse("2023-01-01"),
	        Discriminator = PersonIdentityType.Client.ToString(),
	        Blacklisted = false,
	        UserId = Constants.DefaultUserId,
	        SendAdvertisements = false
        });

        modelBuilder.Entity<TripDraft>()
	        .HasOne(r => r.Carrier)
	        .WithMany()
	        .HasForeignKey(r => r.CarrierId);
        
        modelBuilder.Entity<TripDraft>()
	        .HasOne(r => r.Transport)
	        .WithMany()
	        .HasForeignKey(r => r.TransportId);
        
        modelBuilder.Entity<TripDraft>()
	        .HasOne(r => r.Driver)
	        .WithMany()
	        .HasForeignKey(r => r.DriverId);
        
        modelBuilder.Entity<TripDraft>()
	        .HasOne(r=>r.TripRoute)
	        .WithMany()
	        .HasForeignKey(r=>r.TripRouteId);

        // Configure primary keys

	}
}