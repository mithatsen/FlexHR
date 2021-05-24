using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    class deneme
    {


        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyBranch> CompanyBranch { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<EmailHistory> EmailHistory { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<GeneralSubType> GeneralSubType { get; set; }
        public virtual DbSet<GeneralType> GeneralType { get; set; }
        public virtual DbSet<PublicHoliday> PublicHoliday { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffCareer> StaffCareer { get; set; }
        public virtual DbSet<StaffDebit> StaffDebit { get; set; }
        public virtual DbSet<StaffFile> StaffFile { get; set; }
        public virtual DbSet<StaffGeneralSubType> StaffGeneralSubType { get; set; }
        public virtual DbSet<StaffLeave> StaffLeave { get; set; }
        public virtual DbSet<StaffOtherInfo> StaffOtherInfo { get; set; }
        public virtual DbSet<StaffPayment> StaffPayment { get; set; }
        public virtual DbSet<StaffPersonelInfo> StaffPersonelInfo { get; set; }
        public virtual DbSet<StaffSalary> StaffSalary { get; set; }
        public virtual DbSet<StaffShift> StaffShift { get; set; }
        public virtual DbSet<Town> Town { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-2NBD61T\\SQLEXPRESS;Database=FlexHR;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Country");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CompanyBranch>(entity =>
            {
                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyBranch)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyBranch_Company");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<EmailHistory>(entity =>
            {
                entity.Property(e => e.AttachmentFileFullPath).HasMaxLength(1000);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.EmailCc)
                    .HasColumnName("EmailCC")
                    .HasMaxLength(300);

                entity.Property(e => e.EmailFrom)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EmailSubject)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.EmailTo)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<GeneralSubType>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.GeneralType)
                    .WithMany(p => p.GeneralSubType)
                    .HasForeignKey(d => d.GeneralTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralSubType_GeneralType");
            });

            modelBuilder.Entity<GeneralType>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PublicHoliday>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.HolidayDuration).HasColumnType("numeric(3, 1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.EmailJob).HasMaxLength(50);

                entity.Property(e => e.EmailPersonal).HasMaxLength(50);

                entity.Property(e => e.JobFinishDate).HasColumnType("datetime");

                entity.Property(e => e.JobJoinDate).HasColumnType("datetime");

                entity.Property(e => e.NameSurname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneJob).HasMaxLength(15);

                entity.Property(e => e.PhonePersonal).HasMaxLength(15);
            });

            modelBuilder.Entity<StaffCareer>(entity =>
            {
                entity.Property(e => e.JobFinishDate).HasColumnType("datetime");

                entity.Property(e => e.JobStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.CompanyBranch)
                    .WithMany(p => p.StaffCareer)
                    .HasForeignKey(d => d.CompanyBranchId)
                    .HasConstraintName("FK_StaffCareer_CompanyBranch");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffCareer)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffCareer_Staff");
            });

            modelBuilder.Entity<StaffDebit>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffDebit)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffDebit_Staff");
            });

            modelBuilder.Entity<StaffFile>(entity =>
            {
                entity.ToTable("Staff_File");

                entity.Property(e => e.FileFullPath)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffFile)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_File_Staff");
            });

            modelBuilder.Entity<StaffGeneralSubType>(entity =>
            {
                entity.HasKey(e => e.GeneralSubTypeStaffId);

                entity.ToTable("Staff_GeneralSubType");

                entity.HasOne(d => d.GeneralSubType)
                    .WithMany(p => p.StaffGeneralSubType)
                    .HasForeignKey(d => d.GeneralSubTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_GeneralSubType_GeneralSubType");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffGeneralSubType)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_GeneralSubType_Staff");
            });

            modelBuilder.Entity<StaffLeave>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.LeaveEndDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffLeave)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffLeave_Staff");
            });

            modelBuilder.Entity<StaffOtherInfo>(entity =>
            {
                entity.Property(e => e.AccountNo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Adress).HasMaxLength(2000);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.CallPersonNameSurname).HasMaxLength(50);

                entity.Property(e => e.CallPersonPhoneNumber).HasMaxLength(15);

                entity.Property(e => e.CallPersonProximityDegree).HasMaxLength(50);

                entity.Property(e => e.HomePhoneNumber).HasMaxLength(15);

                entity.Property(e => e.Iban)
                    .HasColumnName("IBAN")
                    .HasMaxLength(30);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffOtherInfo)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffOtherInfo_Staff");

                entity.HasOne(d => d.Town)
                    .WithMany(p => p.StaffOtherInfo)
                    .HasForeignKey(d => d.TownId)
                    .HasConstraintName("FK_StaffOtherInfo_Town");
            });

            modelBuilder.Entity<StaffPayment>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffPayment)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffPayment_Staff");
            });

            modelBuilder.Entity<StaffPersonelInfo>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.IdNumber).HasMaxLength(20);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffPersonelInfo)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffPersonelInfo_Staff");
            });

            modelBuilder.Entity<StaffSalary>(entity =>
            {
                entity.Property(e => e.ConstantBonus).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.FoodPayment).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.IsAgi).HasColumnName("IsAGI");

                entity.Property(e => e.PrivateHealthCare).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PrivatePension).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RoadPayment).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Salary).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffSalary)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffSalary_Staff");
            });

            modelBuilder.Entity<StaffShift>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffShift)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffShift_Staff");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Town)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Town_City");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.NameSurname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("User_Role");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

