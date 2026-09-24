using GastricCancerDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastricCancerDetection.Infrastructure.Persistence;

public class GastricCancerDbContext : DbContext
{
    public GastricCancerDbContext(
        DbContextOptions<GastricCancerDbContext> options)
        : base(options)
    {
    }

    // =========================
    // Security
    // =========================

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    // =========================
    // Clinical / Subjects
    // =========================

    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Sample> Samples => Set<Sample>();
    public DbSet<GenomeFile> GenomeFiles => Set<GenomeFile>();

    // =========================
    // Reference / Gene
    // =========================

    public DbSet<Gene> Genes => Set<Gene>();
    public DbSet<VariantCatalog> VariantCatalog => Set<VariantCatalog>();
    public DbSet<GenePanel> GenePanels => Set<GenePanel>();

    // =========================
    // Analysis
    // =========================

    public DbSet<AnalysisRun> AnalysisRuns => Set<AnalysisRun>();
    public DbSet<GeneComparisonResult> GeneComparisonResults => Set<GeneComparisonResult>();
    public DbSet<SubjectRiskResult> SubjectRiskResults => Set<SubjectRiskResult>();
    public DbSet<GeneratedReport> GeneratedReports => Set<GeneratedReport>();
    public DbSet<RiskModelVersion> RiskModelVersions => Set<RiskModelVersion>();

    // =========================
    // Settings
    // =========================

    public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();

    // =========================
    // Phase 1 - Candidate Engine
    // =========================

    public DbSet<AnalysisCandidate> Candidates => Set<AnalysisCandidate>();
    public DbSet<CandidateEvidence> CandidateEvidence => Set<CandidateEvidence>();
    public DbSet<LiteratureEvidence> LiteratureEvidence => Set<LiteratureEvidence>();
    public DbSet<WhitespaceScore> WhitespaceScores => Set<WhitespaceScore>();
    public DbSet<ValidationGapScore> ValidationGapScores => Set<ValidationGapScore>();
    public DbSet<EarlyStageScore> EarlyStageScores => Set<EarlyStageScore>();
    public DbSet<CancerSpecificityScore> CancerSpecificityScores => Set<CancerSpecificityScore>();
    public DbSet<CfDNADetectabilityScore> CfDNADetectabilityScores => Set<CfDNADetectabilityScore>();

    // =========================
    // Phase 1 - Genomic
    // =========================

    public DbSet<NonCodingVariant> NonCodingVariants => Set<NonCodingVariant>();
    public DbSet<CopyNumberAlteration> CopyNumberAlterations => Set<CopyNumberAlteration>();

    // =========================
    // Phase 1 - Reference
    // =========================

    public DbSet<RegulatoryElement> RegulatoryElements => Set<RegulatoryElement>();
    public DbSet<RepeatElement> RepeatElements => Set<RepeatElement>();
    public DbSet<PMDRegion> PMDRegions => Set<PMDRegion>();
    public DbSet<MitoRegion> MitoRegions => Set<MitoRegion>();

    // =========================
    // Phase 1 - Mito / Methylation
    // =========================

    public DbSet<MitoVariant> MitoVariants => Set<MitoVariant>();
    public DbSet<MethylationCall> MethylationCalls => Set<MethylationCall>();
    public DbSet<MethylationRegion> MethylationRegions => Set<MethylationRegion>();

    // =========================
    // Phase 1 - Integrated
    // =========================

    public DbSet<IntegratedPanel> IntegratedPanels => Set<IntegratedPanel>();
    public DbSet<ModelVersion> ModelVersions => Set<ModelVersion>();


    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // ============================================================
        // PRIMARY KEYS
        // ============================================================

        b.Entity<Role>()
            .HasKey(x => x.RoleId);

        b.Entity<User>()
            .HasKey(x => x.UserId);

        b.Entity<Subject>()
            .HasKey(x => x.SubjectId);

        b.Entity<Sample>()
            .HasKey(x => x.SampleId);

        b.Entity<GenomeFile>()
            .HasKey(x => x.FileId);

        b.Entity<Gene>()
            .HasKey(x => x.GeneId);

        b.Entity<VariantCatalog>()
            .HasKey(x => x.VariantCatalogId);

        b.Entity<GenePanel>()
            .HasKey(x => x.PanelId);

        b.Entity<AnalysisRun>()
            .HasKey(x => x.RunId);

        b.Entity<GeneComparisonResult>()
            .HasKey(x => x.ResultId);

        b.Entity<SubjectRiskResult>()
            .HasKey(x => x.ResultId);

        b.Entity<GeneratedReport>()
            .HasKey(x => x.ReportId);

        b.Entity<RiskModelVersion>()
            .HasKey(x => x.ModelVersionId);

        b.Entity<SystemSetting>()
            .HasKey(x => x.SettingKey);

        b.Entity<AuditLog>()
            .HasKey(x => x.AuditId);

        b.Entity<AnalysisCandidate>()
            .HasKey(x => x.CandidateId);

        b.Entity<CandidateEvidence>()
            .HasKey(x => x.EvidenceId);
        // ============================================================
        // SUBJECT
        // ============================================================

        b.Entity<Subject>()
            .Property(x => x.ValidFrom)
            .ValueGeneratedOnAddOrUpdate();

        b.Entity<Subject>()
            .Property(x => x.ValidTo)
            .ValueGeneratedOnAddOrUpdate();

        b.Entity<Subject>()
            .Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        b.Entity<Subject>()
            .Property(x => x.ConsentObtained)
            .HasDefaultValue(false);


        // ============================================================
        // ANALYSIS RUN
        // ============================================================

        b.Entity<AnalysisRun>()
            .Property(x => x.ValidFrom)
            .ValueGeneratedOnAddOrUpdate();

        b.Entity<AnalysisRun>()
            .Property(x => x.ValidTo)
            .ValueGeneratedOnAddOrUpdate();


        // ============================================================
        // UNIQUE INDEXES
        // ============================================================

        b.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        b.Entity<Role>()
            .HasIndex(x => x.RoleName)
            .IsUnique();

        b.Entity<Subject>()
            .HasIndex(x => x.SubjectCode)
            .IsUnique();

        b.Entity<Sample>()
            .HasIndex(x => x.SampleCode)
            .IsUnique();

        b.Entity<Gene>()
            .HasIndex(x => new
            {
                x.GeneSymbol,
                x.BuildId
            })
            .IsUnique();

        b.Entity<GenePanel>()
            .HasIndex(x => new
            {
                x.PanelName,
                x.PanelVersion
            })
            .IsUnique();

        b.Entity<RiskModelVersion>()
            .HasIndex(x => x.VersionName)
            .IsUnique();

        b.Entity<ModelVersion>()
            .HasIndex(x => x.VersionName)
            .IsUnique();

        b.Entity<AnalysisCandidate>()
            .HasIndex(x => new
            {
                x.AnalysisRunId,
                x.CandidateKey
            })
            .IsUnique();


        // ============================================================
        // NON-CODING VARIANTS
        // ============================================================

        b.Entity<NonCodingVariant>()
            .HasIndex(x => new
            {
                x.AnalysisRunId,
                x.Chromosome,
                x.Position,
                x.RefAllele,
                x.AltAllele
            });


        // ============================================================
        // COPY NUMBER ALTERATIONS
        // ============================================================

        b.Entity<CopyNumberAlteration>()
            .HasIndex(x => new
            {
                x.AnalysisRunId,
                x.Chromosome,
                x.Start,
                x.End
            });


        // ============================================================
        // RELATIONSHIPS
        // ============================================================

        b.Entity<User>()
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);


        // ============================================================
        // GENOME FILE
        // ============================================================

        b.Entity<GenomeFile>()
            .Property(x => x.IsEncryptedAtRest)
            .HasDefaultValue(true);

        b.Entity<GenomeFile>()
            .Property(x => x.FileFormat)
            .HasDefaultValue("VCF");


        // ============================================================
        // RISK MODEL
        // ============================================================

        b.Entity<RiskModelVersion>()
            .Property(x => x.StatisticalMethod)
            .HasDefaultValue("Fisher_Exact_Test");


        // ============================================================
        // REPORT
        // ============================================================

        b.Entity<GeneratedReport>()
            .Property(x => x.ReportFormat)
            .HasMaxLength(10);


        // ============================================================
        // VARIANT CATALOG
        // ============================================================

        b.Entity<VariantCatalog>()
            .Property(x => x.RefAllele)
            .HasMaxLength(500);

        b.Entity<VariantCatalog>()
            .Property(x => x.AltAllele)
            .HasMaxLength(500);
    }
}