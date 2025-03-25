using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Prueba3.Models;

public partial class TumaeContext : DbContext
{
    public TumaeContext()
    {
    }

    public TumaeContext(DbContextOptions<TumaeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<CategoriasAudit> CategoriasAudits { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<DetalleVentasAudit> DetalleVentasAudits { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<InventarioAudit> InventarioAudits { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<MesasAudit> MesasAudits { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosAudit> ProductosAudits { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<VentasAudit> VentasAudits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SEBASTIANGERMOS\\SQLEXPRESS; DataBase=tumae;Integrated Security=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.Idcarrito).HasName("PK__Carrito__C8EB8D92175AB54F");

            entity.ToTable("Carrito");

            entity.Property(e => e.Idcarrito).HasColumnName("IDCarrito");
            entity.Property(e => e.CorreoCliente)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("FK__Carrito__IDProdu__619B8048");
        });

        modelBuilder.Entity<CategoriasAudit>(entity =>
        {
            entity.HasKey(e => e.IdAudit).HasName("PK__Categori__C87E13DD015DBFD6");

            entity.ToTable("Categorias_Audit");

            entity.Property(e => e.Actividad)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("actividad");
            entity.Property(e => e.FhModi)
                .HasColumnType("datetime")
                .HasColumnName("fh_modi");
            entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IddetalleVenta).HasName("PK__DetalleV__DA9AFB79F07BEF70");

            entity.Property(e => e.IddetalleVenta).HasColumnName("IDDetalleVenta");
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Idventa).HasColumnName("IDVenta");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("FK__DetalleVe__IDPro__787EE5A0");

            entity.HasOne(d => d.IdventaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.Idventa)
                .HasConstraintName("FK__DetalleVe__IDVen__797309D9");
        });

        modelBuilder.Entity<DetalleVentasAudit>(entity =>
        {
            entity.HasKey(e => e.IdAudit).HasName("PK__DetalleV__C87E13DD96E42548");

            entity.ToTable("DetalleVentas_Audit");

            entity.Property(e => e.Actividad)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("actividad");
            entity.Property(e => e.FhModi)
                .HasColumnType("datetime")
                .HasColumnName("fh_modi");
            entity.Property(e => e.IddetalleVenta).HasColumnName("IDDetalleVenta");
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Idventa).HasColumnName("IDVenta");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Usuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__50E7BAF198F90918");

            entity.ToTable("Factura");

            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.Idventa).HasColumnName("IDVenta");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("total");
            entity.Property(e => e.TotalPago)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("total_pago");

            entity.HasOne(d => d.IdventaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.Idventa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__IDVenta__7E37BEF6");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__Inventar__1927B20CAA1103AC");

            entity.ToTable("Inventario");

            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaVencimiento).HasColumnType("date");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombre_producto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.PuntoReorden).HasColumnName("punto_reorden");
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("unidad_medida");
        });

        modelBuilder.Entity<InventarioAudit>(entity =>
        {
            entity.HasKey(e => e.IdAudit).HasName("PK__Inventar__C87E13DD5E96E166");

            entity.ToTable("Inventario_Audit");

            entity.Property(e => e.Actividad)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("actividad");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaVencimiento).HasColumnType("date");
            entity.Property(e => e.FhModi)
                .HasColumnType("datetime")
                .HasColumnName("fh_modi");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombre_producto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.PuntoReorden).HasColumnName("punto_reorden");
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("unidad_medida");
            entity.Property(e => e.Usuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.IdMesa).HasName("PK__Mesas__4D7E81B1A6A2526F");

            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
        });

        modelBuilder.Entity<MesasAudit>(entity =>
        {
            entity.HasKey(e => e.IdAudit).HasName("PK__Mesas_Au__C87E13DDDF09C89F");

            entity.ToTable("Mesas_Audit");

            entity.Property(e => e.Actividad)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("actividad");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.FhModi)
                .HasColumnType("datetime")
                .HasColumnName("fh_modi");
            entity.Property(e => e.Usuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PK__Producto__ABDAF2B4FA6DC215");

            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idcategoria)
                .HasConstraintName("FK__Productos__IDCat__49C3F6B7");
        });

        modelBuilder.Entity<ProductosAudit>(entity =>
        {
            entity.HasKey(e => e.IdAudit).HasName("PK__Producto__C87E13DDEF46FCB5");

            entity.ToTable("Productos_Audit");

            entity.Property(e => e.Actividad)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("actividad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FhModi)
                .HasColumnType("datetime")
                .HasColumnName("fh_modi");
            entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Usuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Idventa).HasName("PK__Ventas__27134B8278AE614E");

            entity.Property(e => e.Idventa).HasColumnName("IDVenta");
            entity.Property(e => e.CorreoCliente)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('Activo')")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdTransaccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Referente)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdMesa)
                .HasConstraintName("FK__Ventas__IdMesa__75A278F5");
        });

        modelBuilder.Entity<VentasAudit>(entity =>
        {
            entity.HasKey(e => e.IdAudit).HasName("PK__Ventas_A__C87E13DDE10EE2B6");

            entity.ToTable("Ventas_Audit");

            entity.Property(e => e.Actividad)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("actividad");
            entity.Property(e => e.CorreoCliente)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaVenta).HasColumnType("datetime");
            entity.Property(e => e.FhModi)
                .HasColumnType("datetime")
                .HasColumnName("fh_modi");
            entity.Property(e => e.Idventa).HasColumnName("IDVenta");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
