using System;
using System.Collections.Generic;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class soporteContext : DbContext
{
    public soporteContext()
    {
    }

    public soporteContext(DbContextOptions<soporteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConfiguracionGeneral> ConfiguracionGeneral { get; set; }

    public virtual DbSet<ConfiguracionOdt> ConfiguracionOdt { get; set; }

    public virtual DbSet<DetalleOdt> DetalleOdt { get; set; }

    public virtual DbSet<Empresa> Empresa { get; set; }

    public virtual DbSet<Maquinaria> Maquinaria { get; set; }

    public virtual DbSet<OrdenTrabajo> OrdenTrabajo { get; set; }

    public virtual DbSet<Sucursal> Sucursal { get; set; }

    public virtual DbSet<TareasMaquinaria> TareasMaquinaria { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfiguracionGeneral>(entity =>
        {
            entity.HasKey(e => e.CogeCodigo);

            entity.ToTable("ConfiguracionGeneral");

            entity.Property(e => e.CogeCodigo).HasColumnName("coge_codigo");
            entity.Property(e => e.CogeFecha)
                .HasColumnType("datetime")
                .HasColumnName("coge_fecha");
            entity.Property(e => e.CogeKey)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("coge_key");
            entity.Property(e => e.CogeObservacion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("coge_observacion");
            entity.Property(e => e.CogeService)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("coge_service");
            entity.Property(e => e.CogeValue)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("coge_value");
            entity.Property(e => e.EmprCodigo).HasColumnName("empr_codigo");

            entity.HasOne(d => d.EmprCodigoNavigation).WithMany(p => p.ConfiguracionGenerals)
                .HasForeignKey(d => d.EmprCodigo)
                .HasConstraintName("FK_ConfiguracionGeneral_Empresa");
        });

        modelBuilder.Entity<ConfiguracionOdt>(entity =>
        {
            entity.HasKey(e => e.CodtCodigo);

            entity.ToTable("ConfiguracionODT");

            entity.Property(e => e.CodtCodigo).HasColumnName("codt_codigo");
            entity.Property(e => e.CodtEstado).HasColumnName("codt_estado");
            entity.Property(e => e.CodtFecha)
                .HasColumnType("datetime")
                .HasColumnName("codt_fecha");
            entity.Property(e => e.CodtId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("codt_id");
            entity.Property(e => e.CodtObservacion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("codt_observacion");
            entity.Property(e => e.CodtVersion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("codt_version");
        });

        modelBuilder.Entity<DetalleOdt>(entity =>
        {
            entity.HasKey(e => e.DodtCodigo);

            entity.ToTable("DetalleODT");

            entity.Property(e => e.DodtCodigo).HasColumnName("dodt_codigo");
            entity.Property(e => e.DodtFecha)
                .HasColumnType("datetime")
                .HasColumnName("dodt_fecha");
            entity.Property(e => e.DodtObservacion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("dodt_observacion");
            entity.Property(e => e.DodtResultado).HasColumnName("dodt_resultado");
            entity.Property(e => e.OrtrCodigo).HasColumnName("ortr_codigo");
            entity.Property(e => e.TamaCodigo).HasColumnName("tama_codigo");

            entity.HasOne(d => d.OrtrCodigoNavigation).WithMany(p => p.DetalleOdts)
                .HasForeignKey(d => d.OrtrCodigo)
                .HasConstraintName("FK_DetalleODT_OrdenTrabajo");

            entity.HasOne(d => d.TamaCodigoNavigation).WithMany(p => p.DetalleOdts)
                .HasForeignKey(d => d.TamaCodigo)
                .HasConstraintName("FK_DetalleODT_TareasMaquinaria");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.EmprCodigo);

            entity.ToTable("Empresa");

            entity.Property(e => e.EmprCodigo).HasColumnName("empr_codigo");
            entity.Property(e => e.EmprLogo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("empr_logo");
            entity.Property(e => e.EmprNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("empr_nombre");
            entity.Property(e => e.EmprRuc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("empr_ruc");
        });

        modelBuilder.Entity<Maquinaria>(entity =>
        {
            entity.HasKey(e => e.MaquCodigo);

            entity.Property(e => e.MaquCodigo).HasColumnName("maqu_codigo");
            entity.Property(e => e.MaquCodigoFk).HasColumnName("maqu_codigo_fk");
            entity.Property(e => e.MaquDescripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("maqu_descripcion");
            entity.Property(e => e.MaquEstado).HasColumnName("maqu_estado");
            entity.Property(e => e.MaquFechaAct)
                .HasColumnType("datetime")
                .HasColumnName("maqu_fecha_act");
            entity.Property(e => e.MaquFechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("maqu_fecha_creacion");
            entity.Property(e => e.MaquNombre)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("maqu_nombre");
            entity.Property(e => e.MaquTipo).HasColumnName("maqu_tipo");
            entity.Property(e => e.SucuCodigo).HasColumnName("sucu_codigo");

            entity.HasOne(d => d.MaquCodigoFkNavigation).WithMany(p => p.InverseMaquCodigoFkNavigation)
                .HasForeignKey(d => d.MaquCodigoFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Maquinaria_Maquinaria");

            entity.HasOne(d => d.SucuCodigoNavigation).WithMany(p => p.Maquinaria)
                .HasForeignKey(d => d.SucuCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Maquinaria_Sucursal");
        });

        modelBuilder.Entity<OrdenTrabajo>(entity =>
        {
            entity.HasKey(e => e.OrtrCodigo);

            entity.ToTable("OrdenTrabajo");

            entity.Property(e => e.OrtrCodigo).HasColumnName("ortr_codigo");
            entity.Property(e => e.CodtCodigo).HasColumnName("codt_codigo");
            entity.Property(e => e.MaquCodigo).HasColumnName("maqu_codigo");
            entity.Property(e => e.OrtrFechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("ortr_fecha_creacion");
            entity.Property(e => e.OrtrFechaEjecucionFin)
                .HasColumnType("datetime")
                .HasColumnName("ortr_fecha_ejecucion_fin");
            entity.Property(e => e.OrtrFechaEjecucionInicio)
                .HasColumnType("datetime")
                .HasColumnName("ortr_fecha_ejecucion_inicio");
            entity.Property(e => e.OrtrFechaEmision)
                .HasColumnType("datetime")
                .HasColumnName("ortr_fecha_emision");
            entity.Property(e => e.OrtrFechaPrevistaFin)
                .HasColumnType("datetime")
                .HasColumnName("ortr_fecha_prevista_fin");
            entity.Property(e => e.OrtrFechaPrevistaInicio)
                .HasColumnType("datetime")
                .HasColumnName("ortr_fecha_prevista_inicio");
            entity.Property(e => e.OrtrNúmero).HasColumnName("ortr_número");
            entity.Property(e => e.OrtrObservacion)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("ortr_observacion");
            entity.Property(e => e.OrtrSemana).HasColumnName("ortr_semana");
            entity.Property(e => e.OrtrTipo).HasColumnName("ortr_tipo");
            entity.Property(e => e.UsuaResponsable).HasColumnName("usua_responsable");
            entity.Property(e => e.UsuaRevisa).HasColumnName("usua_revisa");

            entity.HasOne(d => d.CodtCodigoNavigation).WithMany(p => p.OrdenTrabajos)
                .HasForeignKey(d => d.CodtCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenTrabajo_ConfiguracionODT");

            entity.HasOne(d => d.MaquCodigoNavigation).WithMany(p => p.OrdenTrabajos)
                .HasForeignKey(d => d.MaquCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenTrabajo_Maquinaria");

            entity.HasOne(d => d.UsuaResponsableNavigation).WithMany(p => p.OrdenTrabajoUsuaResponsableNavigations)
                .HasForeignKey(d => d.UsuaResponsable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenTrabajo_Usuario");

            entity.HasOne(d => d.UsuaRevisaNavigation).WithMany(p => p.OrdenTrabajoUsuaRevisaNavigations)
                .HasForeignKey(d => d.UsuaRevisa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenTrabajo_Usuario1");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.SucuCodigo);

            entity.ToTable("Sucursal");

            entity.Property(e => e.SucuCodigo).HasColumnName("sucu_codigo");
            entity.Property(e => e.EmprCodigo).HasColumnName("empr_codigo");
            entity.Property(e => e.SucuCiudad)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sucu_ciudad");
            entity.Property(e => e.SucuDireccion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sucu_direccion");
            entity.Property(e => e.SucuEmail)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sucu_email");
            entity.Property(e => e.SucuEstado).HasColumnName("sucu_estado");
            entity.Property(e => e.SucuFechaAct)
                .HasColumnType("datetime")
                .HasColumnName("sucu_fecha_act");
            entity.Property(e => e.SucuFechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("sucu_fecha_creacion");
            entity.Property(e => e.SucuNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sucu_nombre");
            entity.Property(e => e.SucuResponsable)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sucu_responsable");
            entity.Property(e => e.SucuTelefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sucu_telefono");

            entity.HasOne(d => d.EmprCodigoNavigation).WithMany(p => p.Sucursals)
                .HasForeignKey(d => d.EmprCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sucursal_Empresa");
        });

        modelBuilder.Entity<TareasMaquinaria>(entity =>
        {
            entity.HasKey(e => e.TamaCodigo);

            entity.Property(e => e.TamaCodigo).HasColumnName("tama_codigo");
            entity.Property(e => e.MaquCodigo).HasColumnName("maqu_codigo");
            entity.Property(e => e.TamaDescripcion)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("tama_descripcion");
            entity.Property(e => e.TamaEstado).HasColumnName("tama_estado");
            entity.Property(e => e.TamaFechaAct)
                .HasColumnType("datetime")
                .HasColumnName("tama_fecha_act");
            entity.Property(e => e.TamaFechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("tama_fecha_creacion");
            entity.Property(e => e.TamaNombre)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("tama_nombre");

            entity.HasOne(d => d.MaquCodigoNavigation).WithMany(p => p.TareasMaquinaria)
                .HasForeignKey(d => d.MaquCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TareasMaquinaria_Maquinaria");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuaCodigo);

            entity.ToTable("Usuario");

            entity.Property(e => e.UsuaCodigo).HasColumnName("usua_codigo");
            entity.Property(e => e.SucuCodigo).HasColumnName("sucu_codigo");
            entity.Property(e => e.UsuaCedula)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("usua_cedula");
            entity.Property(e => e.UsuaEmail)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("usua_email");
            entity.Property(e => e.UsuaEstado).HasColumnName("usua_estado");
            entity.Property(e => e.UsuaFechaAct)
                .HasColumnType("datetime")
                .HasColumnName("usua_fecha_act");
            entity.Property(e => e.UsuaFechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("usua_fecha_creacion");
            entity.Property(e => e.UsuaFoto)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("usua_foto");
            entity.Property(e => e.UsuaNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("usua_nombre");
            entity.Property(e => e.UsuaPassword)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("usua_password");
            entity.Property(e => e.UsuaPerfil).HasColumnName("usua_perfil");
            entity.Property(e => e.UsuaTelefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usua_telefono");
            entity.Property(e => e.UsuaTipo).HasColumnName("usua_tipo");

            entity.HasOne(d => d.SucuCodigoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.SucuCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Sucursal");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
