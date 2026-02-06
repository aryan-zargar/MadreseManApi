using MadreseManModels.Auth;
using MadreseManModels.BaseInfo;
using MadreseManModels.complications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MadreseManCore
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<AcademicYear> academic_years  { get; set; }

        public DbSet<Grade> grades { get; set; }

        public DbSet<Class> classes { get; set; }

        public DbSet<Subject> subjects { get; set; }

        public DbSet<Student> students { get; set; }

        public DbSet<StudentAbsence> student_absences { get; set; }

        public DbSet<Attachment> attachments { get; set; }

        public DbSet<ReportCard> report_cards { get; set; }

        public DbSet<ReportCardEntry> report_card_entries { get; set; }

        public DbSet<TuitionPayments> tuition_payments { get; set; }

        public DbSet<GradeSubjectRelation> grade_subject_relations { get; set; }

        public DbSet<Budget> budgets { get; set; }

        public DbSet<BudgetTransaction> budget_transactions { get; set; }
        public DbSet<Todo> todos { get; set; }



    }
}
