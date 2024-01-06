using Microsoft.EntityFrameworkCore;
using Relations.Settings;

namespace Relations.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext dataContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductService(DataContext dataContext, IWebHostEnvironment webHostEnvironment) 
        { 
            this.dataContext = dataContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task Add(Product product, IFormFile file)
        {
            string wwwRooPath = webHostEnvironment.WebRootPath;

            if(file != null)
            {
                string fileName = Guid.NewGuid().ToString(); //ชื่่อไฟล์
                string extension = Path.GetExtension(file.FileName); //นามสกุลไฟล์ png,Jpg
                var folders = Path.Combine(wwwRooPath, Paths.Images); //ต่อไฟล์โดยใส่ \ ให้ด้วย\

                var externalFile = Path.Combine(folders,fileName+extension); //ตัวไฟล์จริงๆ
                var fileInDatabase = fileName + extension; //เฉพาะชื่อ

                if(!Directory.Exists(folders)) Directory.CreateDirectory(folders);

                using (var fileStreams = new FileStream(externalFile, FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }

                product.ImageUrl = fileInDatabase;
            }

            await dataContext.Products.AddAsync(product);
            await dataContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await dataContext.Products.ToListAsync();

            products.ForEach(product =>
            {
                product.ImageUrl = !string.IsNullOrEmpty(product.ImageUrl) ? Path.Combine(Paths.Images,product.ImageUrl) : "NoImage.jpeg";
            });

            return products;
        }

        public async Task<Product> Find(int id)
        {
            var product = await dataContext.Products.FindAsync(id);

            if (product.ImageUrl != null) 
            { 
                product.ImageUrl = Path.Combine("\\", Paths.Images, product.ImageUrl); 
            }

            return product;
        }

        public async Task Delete(Product product)
        {
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var fileDelete = webHostEnvironment.WebRootPath + product.ImageUrl;

                if (File.Exists(fileDelete)) File.Delete(fileDelete);

            }

            dataContext.Products.Remove(product);
            await dataContext.SaveChangesAsync();
        }

        public async Task Update(Product product,IFormFile file)
        {
            string wwwRooPath = webHostEnvironment.WebRootPath;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file.FileName);
                var folders = Path.Combine(wwwRooPath, Paths.Images);

                var externalFile = Path.Combine(folders, fileName + extension);
                var fileInDatabase = fileName + extension;

                if (!Directory.Exists(folders)) Directory.CreateDirectory(folders);

                //สร้างไฟล์ใหม่
                using (var fileStreams = new FileStream(externalFile, FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }

                //ลบไฟล์เดิม
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var fileDelete = webHostEnvironment.WebRootPath+product.ImageUrl;

                    if (File.Exists(fileDelete)) File.Delete(fileDelete);

                }

                product.ImageUrl = fileInDatabase;
            }

            dataContext.Products.Update(product);
            await dataContext.SaveChangesAsync();
        }
    }
}
