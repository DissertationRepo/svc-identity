using IdentityService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(x => x.FirstName)
                    .HasColumnName("first_name");

                entity.Property(x => x.LastName)
                    .HasColumnName("last_name");

                entity.Property(x => x.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.HasIndex(x => x.Email)
                    .IsUnique();

                entity.Property(x => x.PasswordHash)
                    .HasColumnName("password_hash")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(x => x.Role)
                    .HasColumnName("role")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(x => x.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();

                entity.HasMany(x => x.RefreshTokens)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("refresh_tokens");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .HasColumnName("id");

                entity.Property(x => x.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                entity.Property(x => x.TokenHash)
                    .HasColumnName("token_hash")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(x => x.ExpiresAt)
                    .HasColumnName("expires_at")
                    .IsRequired();

                entity.Property(x => x.Revoked)
                    .HasColumnName("revoked")
                    .IsRequired();

                entity.Property(x => x.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();

                entity.Property(x => x.RevokedAt)
                    .HasColumnName("revoked_at");

                entity.HasIndex(x => x.TokenHash)
                    .IsUnique();
            });
        }
    }
}
