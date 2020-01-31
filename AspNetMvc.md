# App info api

```
 [System.Web.Http.HttpGet]
        [System.Web.Http.Route("app/info", Name = "AppInfo")]
        public IActionResult Info()
        {
            var asmName = typeof(UserController).Assembly.GetName();
            var siteUrls = new Dictionary<string, string>();

            var logOutUrl = this.Url.Route("Default", new { Controller = "Logout", Action = "Index" }); 
            //.Link generates absoule url. In some scenario .Link was generating url with http instead of https
            var securitySettingsUrl = this.Url.Route("Default", new { Controller = "UserPreferences", Action = "Index" });
            
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
        
    
