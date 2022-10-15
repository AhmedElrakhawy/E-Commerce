using Ecommerce.Database;
using Ecommerce.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class CategoriesService
    {
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null)
                    instance = new CategoriesService();
                return instance;
            }
        }
        private static CategoriesService instance { set; get; }

        private CategoriesService()
        {

        }
        public List<Category> GetAllCategories()
        {
            using (var Context = new CBContext())
            {
                return Context.Categories.Include(x => x.Products).ToList();
            }
        }
        public int GetCategoriesCount(string Search)
        {
            using (var Context = new CBContext())
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return Context.Categories
                        .Where(X => X.Name != null && X.Name.ToUpper()
                        .Contains(Search.ToUpper()))
                        .Count();
                }
                else
                {
                    return Context.Categories.Count();
                }
            }
        }
        public List<Category> GetCategories(string Search, int PageNo)
        {
            int PageSize = 3;
            using (var Context = new CBContext())
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return Context.Categories.OrderBy(x => x.ID)
                        .Skip((PageNo - 1) * PageSize)
                        .Take(PageSize)
                        .Include(x => x.Products)
                        .Where(X => X.Name != null && X.Name.ToUpper()
                        .Contains(Search.ToUpper()))
                        .ToList();
                }
                else
                {
                    return Context.Categories
                        .OrderBy(x=> x.ID)
                        .Skip((PageNo - 1) * PageSize)
                        .Take(PageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var Context = new CBContext())
            {
                return Context.Categories.Where(X => X.IsFeatured && X.ImageUrl != null).ToList();
            }
        }
        public Category GetCategory(int ID)
        {
            using (var Context = new CBContext())
            {
                return Context.Categories.Find(ID);
            }
        }
        public void Save(Category category)
        {
            using (var Context = new CBContext())
            {
                Context.Categories.Add(category);
                Context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using (var Context = new CBContext())
            {
                Context.Entry(category).State = EntityState.Modified;
                Context.SaveChanges();
            }

        }
        public void DeleteCategory(int ID)
        {
            using (var Context = new CBContext())
            {
                var category = GetCategory(ID);
                Context.Entry(category).State = EntityState.Deleted;
                //Context.Categories.Remove(category);
                Context.SaveChanges();
            }
        }
    }
}
