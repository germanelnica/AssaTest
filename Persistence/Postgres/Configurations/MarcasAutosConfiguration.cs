using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Postgres.Configurations
{
    public class MarcasAutosConfiguration : IEntityTypeConfiguration<MarcasAutosEntity>
    {
        public void Configure(EntityTypeBuilder<MarcasAutosEntity> builder)
        {
            builder.ToTable("MarcasAutos");
            builder.HasKey(x => x.Id);
            builder.Property(p=> p.Nombre).HasMaxLength(100).IsRequired();
            builder.Property(p=> p.Descripcion).HasMaxLength(255);
            builder.Property(p=> p.Pais).HasMaxLength(150);

            builder.HasData(
                new MarcasAutosEntity { Id = 1, Nombre = "Toyota", Pais = "Japón", Descripcion = "Marca japonesa fundada en 1937" },
                new MarcasAutosEntity { Id = 2, Nombre = "Ford", Pais = "Estados Unidos", Descripcion = "Marca estadounidense fundada en 1903" },
                new MarcasAutosEntity { Id = 3, Nombre = "Volkswagen", Pais = "Alemania", Descripcion = "Marca alemana fundada en 1937" }
            );
        }
    }
}
