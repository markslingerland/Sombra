﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Sombra.DonateService.DAL;

namespace Sombra.DonateService.Migrations
{
    [DbContext(typeof(DonationsContext))]
    [Migration("20180615101846_AddCharitySlogan")]
    partial class AddCharitySlogan
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sombra.DonateService.DAL.Charity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CharityKey");

                    b.Property<string>("CoverImage");

                    b.Property<string>("Name");

                    b.Property<string>("Slogan");

                    b.Property<string>("ThankYou");

                    b.HasKey("Id");

                    b.ToTable("Charities");
                });

            modelBuilder.Entity("Sombra.DonateService.DAL.CharityAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActionEndDateTime");

                    b.Property<Guid>("CharityActionKey");

                    b.Property<Guid>("CharityId");

                    b.Property<string>("CoverImage");

                    b.Property<string>("Name");

                    b.Property<string>("ThankYou");

                    b.HasKey("Id");

                    b.HasIndex("CharityId");

                    b.ToTable("CharityActions");
                });

            modelBuilder.Entity("Sombra.DonateService.DAL.CharityActionDonation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid>("CharityActionId");

                    b.Property<DateTime>("DateTimeStamp");

                    b.Property<int>("DonationType");

                    b.Property<bool>("IsAnonymous");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CharityActionId");

                    b.HasIndex("UserId");

                    b.ToTable("CharityActionDonations");
                });

            modelBuilder.Entity("Sombra.DonateService.DAL.CharityDonation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid>("CharityId");

                    b.Property<DateTime>("DateTimeStamp");

                    b.Property<int>("DonationType");

                    b.Property<bool>("IsAnonymous");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CharityId");

                    b.HasIndex("UserId");

                    b.ToTable("CharityDonations");
                });

            modelBuilder.Entity("Sombra.DonateService.DAL.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProfileImage");

                    b.Property<Guid>("UserKey");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sombra.DonateService.DAL.CharityAction", b =>
                {
                    b.HasOne("Sombra.DonateService.DAL.Charity", "Charity")
                        .WithMany("ChartityActions")
                        .HasForeignKey("CharityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sombra.DonateService.DAL.CharityActionDonation", b =>
                {
                    b.HasOne("Sombra.DonateService.DAL.CharityAction", "CharityAction")
                        .WithMany("ChartityActionDonations")
                        .HasForeignKey("CharityActionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sombra.DonateService.DAL.User", "User")
                        .WithMany("CharityActionDonations")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Sombra.DonateService.DAL.CharityDonation", b =>
                {
                    b.HasOne("Sombra.DonateService.DAL.Charity", "Charity")
                        .WithMany("ChartityDonations")
                        .HasForeignKey("CharityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sombra.DonateService.DAL.User", "User")
                        .WithMany("CharityDonations")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
