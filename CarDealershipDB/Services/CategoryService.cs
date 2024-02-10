using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using System.Diagnostics;

public class CategoryService(CategoryRepository categoryRepository)


{
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public CategoryEntity CreateCategory(string categoryName)
    {
            try
            {
            var existingCategory = _categoryRepository.Get(x => x.CategoryName == categoryName);

            if (existingCategory != null)
            {
                return existingCategory;
            }

            var newCategory = new CategoryEntity { CategoryName = categoryName };
            _categoryRepository.Create(newCategory);

            return newCategory;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!;
        
      }
    public CategoryEntity GetCategoryById(int id)
    {
        try
        {
            var categoryEntity = _categoryRepository.Get(x => x.Id == id);
            return categoryEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
            return null!;
        
    }

    public IEnumerable<CategoryEntity> GetCategories()
    {
        try
        {
            var categories = _categoryRepository.GetAll();
            return categories;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
            return null!;
        
    }

    public CategoryEntity UpdateCategory(CategoryEntity categoryEntity)
    {
        try
        {
            var updatedCategoryEntity = _categoryRepository.Update(x => x.Id == categoryEntity.Id, categoryEntity);
            return updatedCategoryEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
            return null!;
        
    }

    public bool DeleteCategory(int id)
    {
        try
        {
           
            var existingCategory = _categoryRepository.Get(x => x.Id == id);

         
            if (existingCategory == null)
            {
                return false;
            }

            _categoryRepository.Delete(x => x.Id == id);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}



