#pragma checksum "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\Account\ConfirmEmail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "00a315b9080859888fcee14a1d6f022be4618ffd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_ConfirmEmail), @"mvc.1.0.view", @"/Views/Account/ConfirmEmail.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/ConfirmEmail.cshtml", typeof(AspNetCore.Views_Account_ConfirmEmail))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\_ViewImports.cshtml"
using shopapp.Domain;

#line default
#line hidden
#line 2 "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\_ViewImports.cshtml"
using shopapp.Common.Dto;

#line default
#line hidden
#line 3 "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\_ViewImports.cshtml"
using shopapp.Common.Extensions;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00a315b9080859888fcee14a1d6f022be4618ffd", @"/Views/Account/ConfirmEmail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea7bdb524bcb5b663f3381de7cba6e3d65657ee6", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_ConfirmEmail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\Account\ConfirmEmail.cshtml"
  
    ViewData["Title"] = "ConfirmEmail";

#line default
#line hidden
            BeginContext(50, 71, true);
            WriteLiteral("\r\n<h1>Confirm Email</h1>\r\n<hr />\r\n\r\n<div class=\"alert alert-warning\">\r\n");
            EndContext();
#line 10 "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\Account\ConfirmEmail.cshtml"
     if (TempData["message"] != null)
    {
        

#line default
#line hidden
            BeginContext(176, 19, false);
#line 12 "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\Account\ConfirmEmail.cshtml"
   Write(TempData["message"]);

#line default
#line hidden
            EndContext();
#line 12 "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\Account\ConfirmEmail.cshtml"
                            
    }

#line default
#line hidden
            BeginContext(204, 10, true);
            WriteLiteral("</div>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
