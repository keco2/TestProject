using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace TaskMgmt.DAL.Interface
{
    /// <summary>
    /// From DbContext
    /// </summary>
    public interface ITaskMgmtDbContext
    {
        //
        // Summary:
        //     Creates a Database instance for this context that allows for creation/deletion/existence
        //     checks for the underlying database.
        Database Database { get; }
        //
        // Summary:
        //     Provides access to features of the context that deal with change tracking of
        //     entities.
        //
        // Value:
        //     An object used to access features that deal with change tracking.
        DbChangeTracker ChangeTracker { get; }
        //
        // Summary:
        //     Provides access to configuration options for the context.
        //
        // Value:
        //     An object used to access configuration options.
        DbContextConfiguration Configuration { get; }

        //
        // Summary:
        //     Calls the Dispose method.
        void Dispose();
        //
        // Summary:
        //     Gets a System.Data.Entity.Infrastructure.DbEntityEntry object for the given entity
        //     providing access to information about the entity and the ability to perform actions
        //     on the entity.
        //
        // Parameters:
        //   entity:
        //     The entity.
        //
        // Returns:
        //     An entry for the entity.
        DbEntityEntry Entry(object entity);
        //
        // Summary:
        //     Gets a System.Data.Entity.Infrastructure.DbEntityEntry`1 object for the given
        //     entity providing access to information about the entity and the ability to perform
        //     actions on the entity.
        //
        // Parameters:
        //   entity:
        //     The entity.
        //
        // Type parameters:
        //   TEntity:
        //     The type of the entity.
        //
        // Returns:
        //     An entry for the entity.
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        bool Equals(object obj);
        int GetHashCode();
        Type GetType();
        //
        // Summary:
        //     Validates tracked entities and returns a Collection of System.Data.Entity.Validation.DbEntityValidationResult
        //     containing validation results.
        //
        // Returns:
        //     Collection of validation results for invalid entities. The collection is never
        //     null and must not contain null values or results for valid entities.
        //
        // Remarks:
        //     1. This method calls DetectChanges() to determine states of the tracked entities
        //     unless DbContextConfiguration.AutoDetectChangesEnabled is set to false. 2. By
        //     default only Added on Modified entities are validated. The user is able to change
        //     this behavior by overriding ShouldValidateEntity method.
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        //
        // Summary:
        //     Saves all changes made in this context to the underlying database.
        //
        // Returns:
        //     The number of state entries written to the underlying database. This can include
        //     state entries for entities and/or relationships. Relationship state entries are
        //     created for many-to-many relationships and relationships where there is no foreign
        //     key property included in the entity class (often referred to as independent associations).
        //
        // Exceptions:
        //   T:System.Data.Entity.Infrastructure.DbUpdateException:
        //     An error occurred sending updates to the database.
        //
        //   T:System.Data.Entity.Infrastructure.DbUpdateConcurrencyException:
        //     A database command did not affect the expected number of rows. This usually indicates
        //     an optimistic concurrency violation; that is, a row has been changed in the database
        //     since it was queried.
        //
        //   T:System.Data.Entity.Validation.DbEntityValidationException:
        //     The save was aborted because validation of entity property values failed.
        //
        //   T:System.NotSupportedException:
        //     An attempt was made to use unsupported behavior such as executing multiple asynchronous
        //     commands concurrently on the same context instance.
        //
        //   T:System.ObjectDisposedException:
        //     The context or connection have been disposed.
        //
        //   T:System.InvalidOperationException:
        //     Some error occurred attempting to process entities in the context either before
        //     or after sending commands to the database.
        int SaveChanges();
        //
        // Summary:
        //     Asynchronously saves all changes made in this context to the underlying database.
        //
        // Parameters:
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task to
        //     complete.
        //
        // Returns:
        //     A task that represents the asynchronous save operation. The task result contains
        //     the number of state entries written to the underlying database. This can include
        //     state entries for entities and/or relationships. Relationship state entries are
        //     created for many-to-many relationships and relationships where there is no foreign
        //     key property included in the entity class (often referred to as independent associations).
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     Thrown if the context has been disposed.
        //
        // Remarks:
        //     Multiple active operations on the same context instance are not supported. Use
        //     'await' to ensure that any asynchronous operations have completed before calling
        //     another method on this context.
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        //
        // Summary:
        //     Asynchronously saves all changes made in this context to the underlying database.
        //
        // Returns:
        //     A task that represents the asynchronous save operation. The task result contains
        //     the number of state entries written to the underlying database. This can include
        //     state entries for entities and/or relationships. Relationship state entries are
        //     created for many-to-many relationships and relationships where there is no foreign
        //     key property included in the entity class (often referred to as independent associations).
        //
        // Exceptions:
        //   T:System.Data.Entity.Infrastructure.DbUpdateException:
        //     An error occurred sending updates to the database.
        //
        //   T:System.Data.Entity.Infrastructure.DbUpdateConcurrencyException:
        //     A database command did not affect the expected number of rows. This usually indicates
        //     an optimistic concurrency violation; that is, a row has been changed in the database
        //     since it was queried.
        //
        //   T:System.Data.Entity.Validation.DbEntityValidationException:
        //     The save was aborted because validation of entity property values failed.
        //
        //   T:System.NotSupportedException:
        //     An attempt was made to use unsupported behavior such as executing multiple asynchronous
        //     commands concurrently on the same context instance.
        //
        //   T:System.ObjectDisposedException:
        //     The context or connection have been disposed.
        //
        //   T:System.InvalidOperationException:
        //     Some error occurred attempting to process entities in the context either before
        //     or after sending commands to the database.
        //
        // Remarks:
        //     Multiple active operations on the same context instance are not supported. Use
        //     'await' to ensure that any asynchronous operations have completed before calling
        //     another method on this context.
        Task<int> SaveChangesAsync();
        //
        // Summary:
        //     Returns a non-generic System.Data.Entity.DbSet instance for access to entities
        //     of the given type in the context and the underlying store.
        //
        // Parameters:
        //   entityType:
        //     The type of entity for which a set should be returned.
        //
        // Returns:
        //     A set for the given entity type.
        //
        // Remarks:
        //     Note that Entity Framework requires that this method return the same instance
        //     each time that it is called for a given context instance and entity type. Also,
        //     the generic System.Data.Entity.DbSet`1 returned by the System.Data.Entity.DbContext.Set(System.Type)
        //     method must wrap the same underlying query and set of entities. These invariants
        //     must be maintained if this method is overridden for anything other than creating
        //     test doubles for unit testing. See the System.Data.Entity.DbSet class for more
        //     details.
        DbSet Set(Type entityType);
        //
        // Summary:
        //     Returns a System.Data.Entity.DbSet`1 instance for access to entities of the given
        //     type in the context and the underlying store.
        //
        // Type parameters:
        //   TEntity:
        //     The type entity for which a set should be returned.
        //
        // Returns:
        //     A set for the given entity type.
        //
        // Remarks:
        //     Note that Entity Framework requires that this method return the same instance
        //     each time that it is called for a given context instance and entity type. Also,
        //     the non-generic System.Data.Entity.DbSet returned by the System.Data.Entity.DbContext.Set(System.Type)
        //     method must wrap the same underlying query and set of entities. These invariants
        //     must be maintained if this method is overridden for anything other than creating
        //     test doubles for unit testing. See the System.Data.Entity.DbSet`1 class for more
        //     details.
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
    }
}