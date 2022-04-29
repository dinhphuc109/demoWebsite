
using Model1.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1.Dao
{
    public class MapsDao
    {
        OnlineDbOrder db = null;
        public MapsDao()
        {
            db = new OnlineDbOrder();
        }
        public Map GetMaps()
        {
            return db.Maps.SingleOrDefault(x => x.Status == true);
        }
        public List<Map> ListAll()
        {
            return db.Maps.Where(x => x.Status == true).ToList();
        }
        public IEnumerable<Map> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Map> model = db.Maps;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public Map ViewDetail(long id)
        {
            return db.Maps.Find(id);
        }
        public IEnumerable<Map> ListAll(long searchString, int page, int pageSize)
        {
            IQueryable<Map> model = db.Maps;

            model = model.Where(x => x.ID == searchString);

            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public List<Map> ListByGroupId(long groupId)
        {
            return db.Maps.Where(x => x.ID == groupId && x.Status == true).OrderBy(x => x.ID).ToList();
        }

        public List<Map> ListMap(int top)
        {
            return db.Maps.Where(x => x.Status == true).OrderByDescending(x => x.ID).Take(top).ToList();
        }

        public long Create(Map map)
        {
            
            db.Maps.Add(map);
            db.SaveChanges();
            return map.ID;
        }
    }
}
