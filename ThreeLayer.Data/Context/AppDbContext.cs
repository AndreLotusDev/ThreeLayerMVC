using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using ThreeLayer.Business.Models;

namespace ThreeLayer.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<BrazilianPerson> BrazilianPeople { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entity>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            var types = modelBuilder.Model.GetEntityTypes().ToList();

            ToSnake(types);
            SetDefaultDateTimes(types);
            SetDecimalPrecision(types);
            SetToUTCDefault(types);
            SetStringDefaultValue(types);
            ConfigureVersion(types);

            ConfigureRestrictDelete(modelBuilder);

            GlobalDeleteFilter(modelBuilder);

            modelBuilder.HasDefaultSchema("default");
            modelBuilder.UseCollation("ci_ai_collation");
        }

        private static void GlobalDeleteFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(entityType.ClrType) && entityType.ClrType != typeof(Entity))
                {
                    var method = typeof(ModelBuilder).GetMethod(nameof(ModelBuilder.Entity), Type.EmptyTypes);
                    var genericMethod = method.MakeGenericMethod(entityType.ClrType);
                    dynamic entityBuilder = genericMethod.Invoke(modelBuilder, null);

                    entityBuilder.HasQueryFilter(GetIsNotDeletedRestriction(entityType.ClrType));
                }
            }
        }

        private static void ConfigureVersion(List<IMutableEntityType> types)
        {
            foreach (var entityType in types)
            {
                var property = entityType.FindProperty("RowVersion");
                if (property != null && property.ClrType == typeof(byte[]))
                {
                    property.ValueGenerated = ValueGenerated.OnAddOrUpdate;
                    property.IsConcurrencyToken = true;
                    property.SetColumnType("xid");
                }
            }
        }

        private static void ConfigureRestrictDelete(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetStringDefaultValue(List<IMutableEntityType> types)
        {
            foreach (var property in types.SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
        }

        private static void SetDecimalPrecision(List<IMutableEntityType> types)
        {
            foreach (var property in types.SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?))))
            {
                property.SetColumnType("decimal(18, 2)");
            }
        }

        private static void SetToUTCDefault(List<IMutableEntityType> types)
        {
            foreach (var property in types.SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))))
            {
                property.SetColumnType("timestamptz");

                // Optionally, use a ValueConverter to enforce UTC
                var converter = new ValueConverter<DateTime, DateTime>(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                property.SetValueConverter(converter);
            }
        }

        private static void SetDefaultDateTimes(List<IMutableEntityType> types)
        {
            foreach (var entityType in types)
            {
                var properties = entityType.GetProperties()
                .Where(p => (p.Name == "CreatedAt" || p.Name == "LastModifiedAt") && p.ClrType == typeof(DateTime));

                foreach (var property in properties)
                {
                    property.SetDefaultValueSql("CURRENT_TIMESTAMP");
                }
            }
        }

        private static void ToSnake(List<IMutableEntityType> types)
        {
            foreach (var entityType in types)
            {
                if (typeof(Entity).IsAssignableFrom(entityType.ClrType) && entityType.ClrType != typeof(Entity))
                {
                    var tableName = entityType.GetTableName();
                    var pluralizedTableName = tableName.Pluralize(); 
                    var snakeCaseTableName = ToSnakeCase(pluralizedTableName);

                    entityType.SetTableName(snakeCaseTableName);

                    foreach (var property in entityType.GetProperties())
                    {
                        var snakeCaseColumnName = ToSnakeCase(property.Name);
                        property.SetColumnName(snakeCaseColumnName);
                    }

                    foreach (var key in entityType.GetKeys())
                    {
                        key.SetName(ToSnakeCase(key.GetName()));
                    }

                    foreach (var foreignKey in entityType.GetForeignKeys())
                    {
                        foreignKey.SetConstraintName(ToSnakeCase(foreignKey.GetConstraintName()));
                    }

                    foreach (var index in entityType.GetIndexes())
                    {
                        index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()));
                    }
                }
            }
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var startUnderscores = Regex.Match(input, @"^_+");
            var snakeCase = Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();

            return startUnderscores + snakeCase;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entriesModified = ChangeTracker.Entries<Entity>().ToList();
            foreach (var entry in entriesModified)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedByUserId = ""; //To be implemented
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedAt = DateTime.Now;
                    entry.Entity.LastModifiedByUserId = ""; //To be implemented
                }

                if(entry.State == EntityState.Deleted)
                {

                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        private void SoftDeleteEntity(EntityEntry entry)
        {
            if (entry.Entity is Entity softDeleteEntity)
            {
                entry.State = EntityState.Modified;
                softDeleteEntity.Deleted = true;

                foreach (var navigationEntry in entry.Navigations)
                {
                    if (navigationEntry.Metadata is INavigation navigationMetadata)
                    {
                        if (navigationMetadata.IsCollection)
                        {
                            var relatedEntities = navigationEntry.CurrentValue as IEnumerable<object>;
                            if (relatedEntities != null)
                            {
                                foreach (var relatedEntity in relatedEntities)
                                {
                                    if (relatedEntity is Entity)
                                    {
                                        var relatedEntry = Entry(relatedEntity);
                                        if (relatedEntry.State != EntityState.Deleted)
                                        {
                                            SoftDeleteEntity(relatedEntry);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var relatedEntity = navigationEntry.CurrentValue;
                            if (relatedEntity is Entity)
                            {
                                var relatedEntry = Entry(relatedEntity);
                                if (relatedEntry.State != EntityState.Deleted)
                                {
                                    SoftDeleteEntity(relatedEntry);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static LambdaExpression GetIsNotDeletedRestriction(Type type)
        {
            var param = Expression.Parameter(type, "p");
            var prop = Expression.Property(param, "Deleted");
            var condition = Expression.Equal(prop, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, param);
            return lambda;
        }
    }
}
