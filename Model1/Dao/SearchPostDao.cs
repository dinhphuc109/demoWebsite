using Model1.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1.Dao
{
    public class SearchPostDao
    {
        OnlineDbOrder db = null;
        public static string USER_SESSION = "USER_SESSION";
        public SearchPostDao()
        {
            db = new OnlineDbOrder();
        }
        public IEnumerable<SearchPost> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<SearchPost> model = db.SearchPosts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDay).ToPagedList(page, pageSize);
        }

        public IEnumerable<SearchPost> ListAllPaging(int page, int pageSize)
        {
            IQueryable<SearchPost> model = db.SearchPosts;
            return model.OrderByDescending(x => x.CreatedDay).ToPagedList(page, pageSize);
        }

        public long Create(SearchPost content)
        {
            DateTime dt = DateTime.Now;
            String.Format("{0:dd/MM/yyyy}", dt);           
            content.CreatedDay = dt;   
            db.SearchPosts.Add(content);
            db.SaveChanges();
            return content.ID;
        }

        public SearchPost ViewDetail(string Keyword)
        {
            return db.SearchPosts.Find(Keyword);
        }
    }
}
