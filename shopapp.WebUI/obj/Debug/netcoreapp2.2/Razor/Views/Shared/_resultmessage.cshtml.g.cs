#pragma checksum "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\Shared\_resultmessage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f75a651a0d424f5e2ce91646dce429d5e377cfd4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__resultmessage), @"mvc.1.0.view", @"/Views/Shared/_resultmessage.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_resultmessage.cshtml", typeof(AspNetCore.Views_Shared__resultmessage))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f75a651a0d424f5e2ce91646dce429d5e377cfd4", @"/Views/Shared/_resultmessage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea7bdb524bcb5b663f3381de7cba6e3d65657ee6", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__resultmessage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ResultMessage>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(22, 60, true);
            WriteLiteral("<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n        <div");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 82, "\"", 112, 3);
            WriteAttributeValue("", 90, "alert", 90, 5, true);
            WriteAttributeValue(" ", 95, "alert-", 96, 7, true);
#line 4 "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\Shared\_resultmessage.cshtml"
WriteAttributeValue("", 102, Model.Css, 102, 10, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(113, 39, true);
            WriteLiteral(">\r\n            <h4 class=\"alert-title\">");
            EndContext();
            BeginContext(153, 11, false);
#line 5 "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\Shared\_resultmessage.cshtml"
                               Write(Model.Title);

#line default
#line hidden
            EndContext();
            BeginContext(164, 22, true);
            WriteLiteral("</h4>\r\n            <p>");
            EndContext();
            BeginContext(187, 13, false);
#line 6 "C:\Users\Oğuzcan\source\repos\ShopApp.v3\shopapp.WebUI\Views\Shared\_resultmessage.cshtml"
          Write(Model.Message);

#line default
#line hidden
            EndContext();
            BeginContext(200, 42, true);
            WriteLiteral("</p>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ResultMessage> Html { get; private set; }
    }
}
#pragma warning restore 1591