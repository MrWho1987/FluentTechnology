using FluentTechnology.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FluentTechnology.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GrantCategories> GrantCategories { get; set; }

    public virtual DbSet<OrganizationTypes> OrganizationTypes { get; set; }

    public virtual DbSet<PersonalizedContentPreferences> PersonalizedContentPreferences { get; set; }

    public virtual DbSet<PreferredCommunicationMethods> PreferredCommunicationMethods { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FluentTechDB;Username=postgres;Password=03Abou!@y!@09;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<GrantCategories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("grant_categories_pkey");

            entity.ToTable("grant_categories", tb => tb.HasComment("Stores grant categories of interest (e.g., Education, Health)."));

            entity.HasIndex(e => e.CategoryName, "grant_categories_category_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<OrganizationTypes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organization_types_pkey");

            entity.ToTable("organization_types", tb => tb.HasComment("Stores types of organizations (e.g., non-profit, educational institution)."));

            entity.HasIndex(e => e.TypeName, "organization_types_type_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<PersonalizedContentPreferences>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("personalized_content_preferences_pkey");

            entity.ToTable("personalized_content_preferences", tb => tb.HasComment("Stores personalized content preferences (e.g., News and Updates, Success Stories)."));

            entity.HasIndex(e => e.PreferenceName, "personalized_content_preferences_preference_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.PreferenceName)
                .HasMaxLength(255)
                .HasColumnName("preference_name");
        });

        modelBuilder.Entity<PreferredCommunicationMethods>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("preferred_communication_methods_pkey");

            entity.ToTable("preferred_communication_methods", tb => tb.HasComment("Stores preferred communication methods (e.g., email, SMS)."));

            entity.HasIndex(e => e.Method, "preferred_communication_methods_method_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Method)
                .HasMaxLength(255)
                .HasColumnName("method");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.EmailAddress).HasName("users_pkey");

            entity.ToTable("users", tb => tb.HasComment("Stores user information including their preferred communication method and organization type."));

            entity.HasIndex(e => e.OrganizationTypeId, "idx_users_organization_type_id");

            entity.HasIndex(e => e.PreferredCommunicationMethodId, "idx_users_preferred_communication_method_id");

            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .HasColumnName("email_address");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.OrganizationTypeId).HasColumnName("organization_type_id");
            entity.Property(e => e.PreferredCommunicationMethodId).HasColumnName("preferred_communication_method_id");

            entity.HasOne(d => d.OrganizationType).WithMany(p => p.Users)
                .HasForeignKey(d => d.OrganizationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_organization_type_id_fkey");

            entity.HasOne(d => d.PreferredCommunicationMethod).WithMany(p => p.Users)
                .HasForeignKey(d => d.PreferredCommunicationMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_preferred_communication_method_id_fkey");

            entity.HasMany(d => d.GrantCategory).WithMany(p => p.EmailAddress)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGrantCategories",
                    r => r.HasOne<GrantCategories>().WithMany()
                        .HasForeignKey("GrantCategoryId")
                        .HasConstraintName("user_grant_categories_grant_category_id_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("EmailAddress")
                        .HasConstraintName("user_grant_categories_email_address_fkey"),
                    j =>
                    {
                        j.HasKey("EmailAddress", "GrantCategoryId").HasName("user_grant_categories_pkey");
                        j.ToTable("user_grant_categories", tb => tb.HasComment("Links users to their grant categories of interest, supporting multiple interests per user."));
                        j.HasIndex(new[] { "EmailAddress" }, "idx_user_grant_categories_email_address");
                        j.HasIndex(new[] { "GrantCategoryId" }, "idx_user_grant_categories_grant_category_id");
                        j.IndexerProperty<string>("EmailAddress")
                            .HasMaxLength(255)
                            .HasColumnName("email_address");
                        j.IndexerProperty<Guid>("GrantCategoryId").HasColumnName("grant_category_id");
                    });

            entity.HasMany(d => d.Preference).WithMany(p => p.EmailAddress)
                .UsingEntity<Dictionary<string, object>>(
                    "UserPersonalizedContentPreferences",
                    r => r.HasOne<PersonalizedContentPreferences>().WithMany()
                        .HasForeignKey("PreferenceId")
                        .HasConstraintName("user_personalized_content_preferences_preference_id_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("EmailAddress")
                        .HasConstraintName("user_personalized_content_preferences_email_address_fkey"),
                    j =>
                    {
                        j.HasKey("EmailAddress", "PreferenceId").HasName("user_personalized_content_preferences_pkey");
                        j.ToTable("user_personalized_content_preferences", tb => tb.HasComment("Links users to their personalized content preferences, allowing multiple preferences per user."));
                        j.HasIndex(new[] { "EmailAddress" }, "idx_user_personalized_content_preferences_email_address");
                        j.HasIndex(new[] { "PreferenceId" }, "idx_user_personalized_content_preferences_preference_id");
                        j.IndexerProperty<string>("EmailAddress")
                            .HasMaxLength(255)
                            .HasColumnName("email_address");
                        j.IndexerProperty<Guid>("PreferenceId").HasColumnName("preference_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
