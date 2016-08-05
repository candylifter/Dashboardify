using System.Collections.Generic;
using System.Linq;

namespace Dashboardify.Repositories
{
    public class ItemsRepository
    {
        private IList<Item> _results;

        public ItemsRepository()
        {
            _results = new List<Item>();

            _results.Add(new Item
            {
                Id = 1,
                Url = "http://www.autogidas.lt/automobiliai/?f_1=&f_model_14=&f_215=&f_216=&f_41=&f_42=&f_3=&f_2=&f_376=",
                Xpath = "/html/body/div/div[8]/div/div[2]/a[1]/div"
            });
        }

        public IList<Item> GetList()
        {
            return _results.ToList();
        }

        public void Update(Item item)
        {
            // UPDATE ITEM BY ID
        }
    }
}