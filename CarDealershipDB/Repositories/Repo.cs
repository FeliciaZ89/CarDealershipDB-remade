﻿using CarDealershipDB.Context;
using System.Diagnostics;
using System.Linq.Expressions;

public class Repo<TEntity>(DataContext context) where TEntity : class
{
    protected readonly DataContext _context = context;

    //Create
    /// <summary>
    /// Creates a new entity into the database.
    /// </summary>
    /// <param name="entity">The entity to be added to the database</param>
    /// <returns>The created entity with its database generated fields populated, else null if an exception occured.</returns>
    public virtual TEntity Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
            return null!;
        
    }

    //Read
    /// <summary>
    /// Retrieves all entities of type TEntity from the database.
    /// </summary>
    /// <returns>An IQueryable of all TEntity objects, or null if an exception occurred.</returns>
    public IQueryable<TEntity> GetAll()
    {
        try
        {
            return _context.Set<TEntity>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
            return null!;
        
    }
    /// <summary>
    /// Retrieves a single entity of type TEntity..
    /// </summary>
    /// <param name="expression">A LINQ expression used to filter the entities.</param>
    /// <returns>The first TEntity object that suits the expression, else null.</returns>
    public virtual TEntity Get(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = _context.Set<TEntity>()
                                 .FirstOrDefault(expression);
            return entity!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
            return null!;
        
    }

    //Update

    /// <summary>
    /// Updates an entity of type TEntity in the database that suits the provided expression.
    /// </summary>
    /// <param name="expression">A LINQ expression used to filter the entities.</param>
    /// <param name="entity">The updated TEntity object.</param>
    /// <returns>The updated TEntity object, or else null</returns>
    public TEntity Update(Expression<Func<TEntity, bool>> expression, TEntity entity)
    {
        try
        {
            var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(expression);
            _context.Entry(entityToUpdate!).CurrentValues.SetValues(entity);
            _context.SaveChanges();

            return entityToUpdate!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
            return null!;
        
    }

    //Delete

    /// <summary>
    /// Deletes an entity of type TEntity from the database that suits the provided expression.
    /// </summary>
    /// <param name="expression">A LINQ expression used to filter the entities.</param>
    public bool Delete(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(expression);
            _context.Remove(entity!);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
