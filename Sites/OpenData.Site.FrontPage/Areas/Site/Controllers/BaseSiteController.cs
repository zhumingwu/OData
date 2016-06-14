﻿using OpenData.Site.Core;
using OpenData.Site.FrontPage.Controllers;
using OpenData.Data.Core;

namespace OpenData.Site.FrontPage.Areas.Sites.Controllers
{
    public class BaseSiteController : BaseController
    {
        public IDatabase db
        {
            get
            {
                return this.SiteManager.GetSiteDataBase();
            }
        }
        public string CurrentSite
        {
            get
            {
                if (this.Session["CurrentSite"] == null)
                {
                    this.Redirect("/User");
                    return string.Empty;
                }
                return this.Session["CurrentSite"].ToString();
            }
            set
            {
                this.Session["CurrentSite"] = value;
            }
        }

        ISiteService siteService;
        public ISiteService SiteService
        {
            get
            {
                if (siteService == null)
                {
                    siteService = ApplicationEngine.Current.Resolve<ISiteService>();
                }
                return siteService;
            }
        }

    }
}