﻿// <auto-generated />
using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace KEC.Curation.Data.Migrations
{
    [DbContext(typeof(CurationDataContext))]
    [Migration("20180408164535_boolApproveandRejected")]
    partial class boolApproveandRejected
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KEC.Curation.Data.Models.ChiefCuratorAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AssignmetDateUtc");

                    b.Property<string>("ChiefCuratorGuid");

                    b.Property<string>("PrincipalCuratorGuid");

                    b.Property<int?>("PublicationId");

                    b.Property<bool>("Submitted");

                    b.HasKey("Id");

                    b.ToTable("ChiefCuratorAssignments");
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.ChiefCuratorComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChiefCuratorGuid");

                    b.Property<string>("Notes");

                    b.Property<int>("PublicationId");

                    b.Property<int>("Recomendation");

                    b.Property<bool>("Submitted");

                    b.HasKey("Id");

                    b.ToTable("ChiefCuratorComments");
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.CuratorAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignedBy");

                    b.Property<string>("Assignee");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("Notes");

                    b.Property<int>("PublicationId");

                    b.Property<int>("PublicationSectionId");

                    b.Property<bool>("Status");

                    b.Property<bool>("Submitted");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.ToTable("CuratorAssignments");
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.PrincipalCuratorComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Notes");

                    b.Property<string>("PrincipalCuratorGuid");

                    b.Property<int>("PublicationId");

                    b.Property<bool>("Submitted");

                    b.HasKey("Id");

                    b.ToTable("PrincipalCuratorComments");
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approved");

                    b.Property<string>("AuthorName");

                    b.Property<string>("CertificateNumber");

                    b.Property<string>("CertificateUrl");

                    b.Property<int?>("ChiefCuratorAssignmentId");

                    b.Property<DateTime>("CompletionDate");

                    b.Property<DateTime>("CreatedTimeUtc");

                    b.Property<string>("Description");

                    b.Property<bool>("FullyAssigned");

                    b.Property<string>("ISBNNumber");

                    b.Property<string>("KICDNumber");

                    b.Property<int>("LevelId");

                    b.Property<string>("MimeType");

                    b.Property<DateTime>("ModifiedTimeUtc");

                    b.Property<string>("Owner");

                    b.Property<decimal>("Price");

                    b.Property<string>("PublisherName");

                    b.Property<bool>("Rejected");

                    b.Property<int>("SubjectId");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ChiefCuratorAssignmentId")
                        .IsUnique()
                        .HasFilter("[ChiefCuratorAssignmentId] IS NOT NULL");

                    b.HasIndex("LevelId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.PublicationSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ChiefCuratorAssignmentId");

                    b.Property<int?>("ChiefCuratorAssignmentId1");

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<int?>("CuratorAssignmentId");

                    b.Property<string>("Owner");

                    b.Property<int>("PublicationId");

                    b.Property<string>("SectionDescription");

                    b.HasKey("Id");

                    b.HasIndex("ChiefCuratorAssignmentId")
                        .IsUnique()
                        .HasFilter("[ChiefCuratorAssignmentId] IS NOT NULL");

                    b.HasIndex("ChiefCuratorAssignmentId1")
                        .IsUnique()
                        .HasFilter("[ChiefCuratorAssignmentId1] IS NOT NULL");

                    b.HasIndex("CuratorAssignmentId");

                    b.HasIndex("PublicationId");

                    b.ToTable("PublicationSections");
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.PublicationStageLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ActionTaken");

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<string>("Notes");

                    b.Property<string>("Owner");

                    b.Property<int>("PublicationId");

                    b.Property<int>("Stage");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.ToTable("PublicationStageLogs");
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<string>("Name");

                    b.Property<int>("SubjectTypeId");

                    b.Property<DateTime>("UpdatedAtUtc");

                    b.HasKey("Id");

                    b.HasIndex("SubjectTypeId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.SubjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAtUtc");

                    b.HasKey("Id");

                    b.ToTable("SubjectTypes");
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.CuratorAssignment", b =>
                {
                    b.HasOne("KEC.Curation.Data.Models.Publication", "Publication")
                        .WithMany()
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.Publication", b =>
                {
                    b.HasOne("KEC.Curation.Data.Models.ChiefCuratorAssignment", "ChiefCuratorAssignment")
                        .WithOne("Publication")
                        .HasForeignKey("KEC.Curation.Data.Models.Publication", "ChiefCuratorAssignmentId");

                    b.HasOne("KEC.Curation.Data.Models.Level", "Level")
                        .WithMany("Publications")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KEC.Curation.Data.Models.Subject", "Subject")
                        .WithMany("Publications")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.PublicationSection", b =>
                {
                    b.HasOne("KEC.Curation.Data.Models.CuratorAssignment", "ChiefCuratorAssignment")
                        .WithOne("PublicationSection")
                        .HasForeignKey("KEC.Curation.Data.Models.PublicationSection", "ChiefCuratorAssignmentId");

                    b.HasOne("KEC.Curation.Data.Models.ChiefCuratorAssignment")
                        .WithOne("PublicationSection")
                        .HasForeignKey("KEC.Curation.Data.Models.PublicationSection", "ChiefCuratorAssignmentId1");

                    b.HasOne("KEC.Curation.Data.Models.CuratorAssignment", "CuratorAssignment")
                        .WithMany()
                        .HasForeignKey("CuratorAssignmentId");

                    b.HasOne("KEC.Curation.Data.Models.Publication", "Publication")
                        .WithMany()
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.PublicationStageLog", b =>
                {
                    b.HasOne("KEC.Curation.Data.Models.Publication", "Publication")
                        .WithMany("PublicationStageLogs")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KEC.Curation.Data.Models.Subject", b =>
                {
                    b.HasOne("KEC.Curation.Data.Models.SubjectType", "SubjectType")
                        .WithMany("Subjects")
                        .HasForeignKey("SubjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
