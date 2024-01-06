namespace Relations.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategory();
        Category GetByIdCategory(int id);
        void DeleteCategory(Category category);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
    }
}
