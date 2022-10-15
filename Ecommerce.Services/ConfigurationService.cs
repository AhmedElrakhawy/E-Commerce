using Ecommerce.Database;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class ConfigurationService
    {
        public static ConfigurationService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ConfigurationService();
                return instance;
            }
        }
        private static ConfigurationService instance { set; get; }

        private ConfigurationService()
        {

        }
        public Config GetConfig(string Key)
        {
            using(var Context = new CBContext())
            {
                return Context.Configurations.Find(Key);
            }
        }

        public int PageSize()
        {
            using (var context = new CBContext())
            {
                var pageSizeConfig = context.Configurations.Find("PageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 5;
            }
        }

        public int ShopPageSize()
        {
            using (var context = new CBContext())
            {
                var pageSizeConfig = context.Configurations.Find("ShopPageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }
        }
    }
}
