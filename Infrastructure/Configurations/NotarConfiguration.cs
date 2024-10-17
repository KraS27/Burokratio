using Core.Entities;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal class NotarConfiguration : IEntityTypeConfiguration<Notar>
    {
        public void Configure(EntityTypeBuilder<Notar> builder)
        {
            builder.ToTable("notars");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                .HasColumnName("id");

            builder.Property(n => n.Name)
                .HasColumnName("name")
                .HasMaxLength(Notar.MAX_NAME_LENGTH);

            builder.ComplexProperty(n => n.Address, b =>
            {
                b.IsRequired();

                b.Property(a => a.Division)
                .HasColumnName("division")
                .HasMaxLength(Address.DIVISION_MAX_LENGTH);

                b.Property(a => a.Country)
                .HasColumnName("country")
                .HasMaxLength(Address.COUNTRY_MAX_LENGTH);

                b.Property(a => a.City)
                .HasColumnName("city")
                .HasMaxLength(Address.CITY_MAX_LENGTH);

                b.Property(a => a.Street)
                .HasColumnName("street")
                .HasMaxLength(Address.STREET_MAX_LENGTH);

                b.Property(a => a.PostalCode)
                .HasColumnName("postal_code")
                .HasMaxLength(Address.POSTALCODE_MAX_LENGTH);
            });

            builder.ComplexProperty(n => n.Coordinates, b =>
            {
                b.IsRequired();

                b.Property(c => c.Latitude)
                .HasColumnName("latitude");

                b.Property(c => c.Longitude)
                .HasColumnName("longitude");
            });

            builder.ComplexProperty(n => n.Email, b =>
            {
                b.IsRequired();

                b.Property(e => e.Value)
                .HasColumnName("email")
                .HasMaxLength(Email.MAX_LENGTH);
            });

            builder.ComplexProperty(n => n.PhoneNumber, b =>
            {
                b.IsRequired();

                b.Property(p => p.Number)
                .HasColumnName("phone_number")
                .HasMaxLength(PhoneNumber.MAX_LENGTH);
            });

            builder.Property(n => n.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder.Property(n => n.UpdatedAt)
                .IsRequired()
                .HasColumnName("updated_at");
        }
    }
}
