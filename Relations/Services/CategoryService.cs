
namespace Relations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _ct;
        List<Category> CategoryList;
        public CategoryService(DataContext _ct)
        {
            this._ct = _ct;
            CategoryList = new List<Category>();
        }
        public void AddCategory(Category category)
        {
            _ct.Categories.Add(category);
            _ct.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _ct.Categories.Remove(category);
            _ct.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return _ct.Categories.OrderByDescending(p => p.Id).ToList();
        }

        public Category GetByIdCategory(int id)
        {
            var category = _ct.Categories.Find(id);
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _ct.Categories.Update(category);
            _ct.SaveChanges();
        }
    }
}
