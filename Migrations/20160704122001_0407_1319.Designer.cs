using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ThesisApplication.Data;

namespace ThesisApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160704122001_0407_1319")]
    partial class _0407_1319
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ThesisApplication.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ThesisApplication.Models.fileUpload", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("caseName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.Property<string>("inputFilename");

                    b.Property<string>("status");

                    b.Property<string>("unitModel")
                        .IsRequired();

                    b.Property<DateTime>("uploadDate");

                    b.Property<string>("userName");

                    b.HasKey("ID");

                    b.ToTable("fileUpload");
                });

            modelBuilder.Entity("ThesisApplication.Models.meshingStage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("caseName")
                        .HasAnnotation("MaxLength", 60);

                    b.Property<float>("expRatio");

                    b.Property<float>("finLayerThickness");

                    b.Property<string>("inputFilename");

                    b.Property<DateTime>("meshedDate");

                    b.Property<float>("minThickness");

                    b.Property<int>("numLayers");

                    b.Property<float>("refRegDist1");

                    b.Property<float>("refRegDist2");

                    b.Property<float>("refRegDist3");

                    b.Property<int>("refRegLvl1");

                    b.Property<int>("refRegLvl2");

                    b.Property<int>("refRegLvl3");

                    b.Property<int>("refSurfLvlMax");

                    b.Property<int>("refSurfLvlMin");

                    b.Property<string>("status");

                    b.Property<string>("unitModel");

                    b.Property<DateTime>("uploadDate");

                    b.Property<string>("userName");

                    b.HasKey("ID");

                    b.ToTable("meshingStage");
                });

            modelBuilder.Entity("ThesisApplication.Models.userCase", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("caseName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.Property<string>("inputFilename");

                    b.Property<string>("meshStatus");

                    b.Property<float>("mesh_expRatio");

                    b.Property<float>("mesh_finLayerThickness");

                    b.Property<float>("mesh_minThickness");

                    b.Property<int>("mesh_numLayers");

                    b.Property<float>("mesh_refRegDist1");

                    b.Property<float>("mesh_refRegDist2");

                    b.Property<float>("mesh_refRegDist3");

                    b.Property<int>("mesh_refRegLvl1");

                    b.Property<int>("mesh_refRegLvl2");

                    b.Property<int>("mesh_refRegLvl3");

                    b.Property<int>("mesh_refSurfLvlMax");

                    b.Property<int>("mesh_refSurfLvlMin");

                    b.Property<DateTime>("meshedDate");

                    b.Property<string>("status");

                    b.Property<string>("unitModel")
                        .IsRequired();

                    b.Property<DateTime>("uploadDate");

                    b.Property<string>("userName");

                    b.HasKey("ID");

                    b.ToTable("userCase");
                });

            modelBuilder.Entity("ThesisApplication.Models.userCaseParam", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Genre");

                    b.Property<decimal>("Price");

                    b.Property<string>("caseDirectory");

                    b.Property<string>("caseName");

                    b.Property<int>("metricConversion");

                    b.Property<string>("modelName");

                    b.Property<DateTime>("uploadDate");

                    b.Property<string>("userName");

                    b.HasKey("ID");

                    b.ToTable("userCaseParam");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ThesisApplication.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ThesisApplication.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ThesisApplication.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
