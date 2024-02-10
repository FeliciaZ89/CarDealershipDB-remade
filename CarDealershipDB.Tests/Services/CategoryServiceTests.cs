using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.Tests.Services;

public class CategoryServiceTests
{
    private readonly DataContext _context =
        new (new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public void CreateCategory_Should_CreateNewCategory_ReturnCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        string categoryName = "Test Category";

        // Act
        var result= categoryService.CreateCategory(categoryName);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryName, result.CategoryName);


    }

 
    [Fact]
    public void CreateCategory_ShouldNotCreateNewCategoryIfCategoryAlreadyExists_ReturnExistingCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        string categoryName = "Test Category";

        var existingCategory = categoryService.CreateCategory(categoryName);

        // Act
        var result = categoryService.CreateCategory(categoryName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingCategory.Id, result.Id);
        Assert.Equal(existingCategory.CategoryName, result.CategoryName);
    }

    [Fact]
    public void GetCategories_Should_ReturnAllRecords_From_Database_Returns_IEnumerableofTypeCategoryEntity()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);

        // Act
        var result = categoryService.GetCategories();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CategoryEntity>>(result);
    }


    [Fact]
    public void GetCategoryById_Should_RetrieveOnCategory_When_CategoryExists_Return_OneCategoryName()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var newCategory = categoryService.CreateCategory("Test Category");

        // Act
        var result = categoryService.GetCategoryById(newCategory.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newCategory.Id, result.Id);
    }

    [Fact]
    public void GetCategoryById_ShouldNotRetrieveCategory_When_CategoryDoesNotExist_ReturnNull()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);

        // Act
        var result = categoryService.GetCategoryById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateCategory_Should_UpdateCategory_Return_UpdatedCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var newCategory = categoryService.CreateCategory("Test Category");
        newCategory.CategoryName = "Updated Category";

        // Act
        var result = categoryService.UpdateCategory(newCategory);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Category", result.CategoryName);
    }

    [Fact]
    public void DeleteCategory_Should_FindCategoryandDeleteIt_ReturnTrue()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var newCategory = categoryService.CreateCategory("Test Category");

        // Act
        var result = categoryService.DeleteCategory(newCategory.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteCategory_ShouldNotFindCategoryDeleteIt_ReturnFalse()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);

        // Act
        var result = categoryService.DeleteCategory(999); 

        // Assert
        Assert.False(result);
    }
}




        
         
  

