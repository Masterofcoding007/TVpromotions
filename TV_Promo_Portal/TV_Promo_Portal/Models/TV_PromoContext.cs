using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TV_Promo_Portal.Models
{
    public partial class TV_PromoContext : DbContext
    {
        public TV_PromoContext()
        {
        }

        public TV_PromoContext(DbContextOptions<TV_PromoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<PromoStatus> PromoStatuses { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<TvPromo> TvPromos { get; set; }
        public virtual DbSet<Usermaster> Usermasters { get; set; }
        public virtual DbSet<Usertype> Usertypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ctsdotnet846;Initial Catalog=TV_Promo;Persist Security Info=True;User ID=sa;password = pass@word1;");
            }
        }

        internal Task GetTvPromoByPromoCode(int promoID)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityId).HasColumnName("City_Id");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("City_Name");

                entity.Property(e => e.StateId).HasColumnName("State_Id");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cities_States");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Country_Name");

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Login");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Usertype)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PromoStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("Promo_Status");

                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("Status_ID");

                entity.Property(e => e.StatusType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Status_Type");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.StateId).HasColumnName("State_Id");

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("State_Name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_States_Countries");
            });

            modelBuilder.Entity<TvPromo>(entity =>
            {
                entity.HasKey(e => e.PromoId);

                entity.ToTable("TvPromo");

                entity.Property(e => e.PromoId).HasColumnName("Promo_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.EventDate).HasColumnType("date");

                entity.Property(e => e.EventName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EventTime).HasColumnType("datetime");

                entity.Property(e => e.PromoDate).HasColumnType("datetime");

                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.VenueName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.TvPromos)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_TvPromo_Promo_Status");
            });

            modelBuilder.Entity<Usermaster>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Usermaster");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UsertypeId).HasColumnName("UsertypeID");

                entity.HasOne(d => d.Usertype)
                    .WithMany(p => p.Usermasters)
                    .HasForeignKey(d => d.UsertypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usermaster_Usertype");
            });

            modelBuilder.Entity<Usertype>(entity =>
            {
                entity.ToTable("Usertype");

                entity.Property(e => e.UserTypeId).ValueGeneratedNever();

                entity.Property(e => e.UserType1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UserType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
