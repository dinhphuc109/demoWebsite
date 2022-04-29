
using Common;
using Model1.EF;
using Model1.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Model1.Dao
{
    public class ProductDao
    {
        OnlineDbOrder db = null;
        public ProductDao()
        {
            db = new OnlineDbOrder();
        }
        public Product GetById(long id)
        {
            return db.Products.Find(id);
        }
        public long Insert(Product entity)
        {
            var product = db.Products.Find(entity.ID);
            db.Products.Add(entity);
            entity.MetaTile = entity.Name.RemoveDiacritics().Replace(" ", "-");
            entity.CreateDate = DateTime.Now;
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.MetaTile = entity.Name.RemoveDiacritics().Replace(" ", "-");
                product.Code = entity.Code;
                product.Description = entity.Description;
                product.Detail = entity.Detail;
                product.Image = entity.Image;
                
                product.MoreImage = entity.MoreImage;
                product.ModifiedBy = entity.ModifiedBy;
                product.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Logging

                return false;
            }

        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<Product> ListAll(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            
                model = model.Where(x => x.CreateBy==searchString);
            
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }



        public bool changeStatus(long id)
        {
            var product = db.Products.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public void UpdateImages(long id,string images)
        {
            var product = db.Products.Find(id);
            product.MoreImage = images;
            db.SaveChanges();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        public List<string> ListuserName(string keyword)
        {
            return db.Products.Where(x => x.CreateBy.Contains(keyword)).Select(x => x.Name).ToList();
        }

        /// <summary>
        /// lấy ra danh sách sản phẩm theo thể loại
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1,int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model = (from a in db.Products
                        join b in db.ProductCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == categoryID
                        select new 
                        {
                            CateMetaTitle = b.MetaTitle,
                            CateName = b.Name,
                            CreateBy=a.CreateBy,
                            Description=a.Description,
                            CreatedDate = a.CreateDate,
                            ID = a.ID,
                            Images = a.Image,
                            Name = a.Name,
                            MetaTitle = a.MetaTile
                        }).AsEnumerable().Select(x => new ProductViewModel()
                        {
                            CateMetaTitle = x.MetaTitle,
                            CreateBy=x.CreateBy,
                            CateName = x.Name,
                            CreatedDate = x.CreatedDate,
                            ID = x.ID,
                            Images = x.Images,
                            Name = x.Name,
                            MetaTitle = x.MetaTitle                            
                        });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return model.ToList();
        }

        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword)).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreateBy = a.CreateBy,
                             Description = a.Description,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTile
                         }).AsEnumerable().Select(x=>new ProductViewModel() {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreateBy = x.CreateBy,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model.ToList();
        }

        public List<Product> Listrelatedproducts(long productid)
        {
            var product = db.Products.Find(productid);
            return db.Products.Where(x => x.ID != productid && x.CategoryID == product.CategoryID).ToList();
        }

        public long Create(Product product)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(product.MetaTile))
            {
                product.MetaTile = StringHelper.ToUnsignString(product.Name);
            }
            product.CreateDate = DateTime.Now;
            product.Viewcount = 0;
            db.Products.Add(product);
            db.SaveChanges();

            

            return product.ID;
        }
    }
}
