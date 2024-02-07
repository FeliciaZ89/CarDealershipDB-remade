using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using Microsoft.EntityFrameworkCore;

public class CategoryRepositoryTests
{
    private DataContext _context;
    private CategoryRepository _repo;

    public CategoryRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
         .Options;
        _context = new DataContext(options);
        _repo = new CategoryRepository(_context);
    }

    [Fact]
    public void Create_Should_Create_CategoryEntity_To_Database_Then_Return_CategoryEntity_WithId1()
    {
        // Arrange

        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };

        // Act
        var result = _repo.Create(categoryEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Create_Should_Not_SaveRecord_To_Database_Return_Null()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity();

        // Act
        var result = _repo.Create(categoryEntity);

        // Assert
        Assert.Null(result);
    
    }


    [Fact]
    public void Get_Retrieves_AllRecords_From_Database_Returns_IEnumerableofTypeCategoryEntity()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);

        // Act
        var result = _repo.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CategoryEntity>>(result);
    }

    [Fact]
    public void Get_ShouldRetrieve_OneCategoryByCategoryName_Return_One_CategoryName()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };
        _repo.Create(categoryEntity);


        // Act
        var result = _repo.Get(x => x.CategoryName == categoryEntity.CategoryName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryEntity.CategoryName, result.CategoryName);
    }

    [Fact]
    public void Get_ShouldNotFind_OneCategoryByCategoryNAme_Return_Null()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };

        // Act
        var result = _repo.Get(x => x.CategoryName == categoryEntity.CategoryName);

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void Update_ShouldUpdateExistingCategory_ReturnUpdatedCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };
        categoryEntity=_repo.Create(categoryEntity);


        // Act
        categoryEntity.CategoryName = "Testing";
        var result = _repo.Update(x => x.Id == categoryEntity.Id, categoryEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryEntity.Id, result.Id);
        Assert.Equal("Testing" ,result.CategoryName);


    }
    

    [Fact]
    public void Delete_ShouldRemoveOneCategory_Return_True()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };
        _repo.Create(categoryEntity);

        // Act
        var result=_repo.Delete(x => x.CategoryName == categoryEntity.CategoryName);

        // Assert

        Assert.True(result);
    }
    [Fact]
    public void Delete_ShouldNotFindCategoryAndRemoveIt_Return_False()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };

        // Act
        var result = _repo.Delete(x => x.CategoryName == categoryEntity.CategoryName);

        // Assert

        Assert.False(result);
    }




}
