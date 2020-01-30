# App info api

```
 [System.Web.Http.HttpGet]
        [System.Web.Http.Route("app/info", Name = "AppInfo")]
        public IActionResult Info()
        {
            var asmName = typeof(UserController).Assembly.GetName();
            var siteUrls = new Dictionary<string, string>();

            var logOutUrl = this.Url.Link("Default", new { Controller = "Logout", Action = "Index" });
            var securitySettingsUrl = this.Url.Link("Default", new { Controller = "UserPreferences", Action = "Index" });
            
            siteUrls.Add("logOutUrl", logOutUrl);
            siteUrls.Add("securitySettingsUrl", securitySettingsUrl);

            var userInfo = new AppInfoResponse
            {
                AppName = asmName.Name,
                Version = asmName.Version.ToString(),
                SiteUrls = siteUrls
            };

            return ActionResultFactory.GetActionResult(this.Request).WithModel(userInfo);
        }
        
    
