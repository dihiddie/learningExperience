#pragma checksum "D:\work\LearningExperience\LearningExperience.Web\Pages\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3d8092dea222cfb22ce666a50fa5d703846bed08"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Web.Helper.Pages.Pages_Error), @"mvc.1.0.razor-page", @"/Pages/Error.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Error.cshtml", typeof(Web.Helper.Pages.Pages_Error), null)]
namespace Web.Helper.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\work\LearningExperience\LearningExperience.Web\Pages\_ViewImports.cshtml"
using Web.Helper;

#line default
#line hidden
#line 2 "D:\work\LearningExperience\LearningExperience.Web\Pages\_ViewImports.cshtml"
using LearningExperience.Core.Interfaces;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3d8092dea222cfb22ce666a50fa5d703846bed08", @"/Pages/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00c699e81bc90dbe391180a2f053ec707a171254", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Error : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "D:\work\LearningExperience\LearningExperience.Web\Pages\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
            BeginContext(67, 120, true);
            WriteLiteral("\r\n<h2 class=\"text-danger\">Error.</h2>\r\n<h2 class=\"text-danger\">An error occurred while processing your request.</h2>\r\n\r\n");
            EndContext();
#line 10 "D:\work\LearningExperience\LearningExperience.Web\Pages\Error.cshtml"
 if (Model.ShowRequestId)
{

#line default
#line hidden
            BeginContext(217, 52, true);
            WriteLiteral("    <p>\r\n        <strong>Request ID:</strong> <code>");
            EndContext();
            BeginContext(270, 15, false);
#line 13 "D:\work\LearningExperience\LearningExperience.Web\Pages\Error.cshtml"
                                      Write(Model.RequestId);

#line default
#line hidden
            EndContext();
            BeginContext(285, 19, true);
            WriteLiteral("</code>\r\n    </p>\r\n");
            EndContext();
#line 15 "D:\work\LearningExperience\LearningExperience.Web\Pages\Error.cshtml"
}

#line default
#line hidden
            BeginContext(307, 7, true);
            WriteLiteral("\r\n<p>\r\n");
            EndContext();
#line 18 "D:\work\LearningExperience\LearningExperience.Web\Pages\Error.cshtml"
      
        if (Model.CurrentException != null)
        {

#line default
#line hidden
            BeginContext(378, 38, true);
            WriteLiteral("            <span class=\"text-danger\">");
            EndContext();
            BeginContext(417, 30, false);
#line 21 "D:\work\LearningExperience\LearningExperience.Web\Pages\Error.cshtml"
                                 Write(Model.CurrentException.Message);

#line default
#line hidden
            EndContext();
            BeginContext(447, 9, true);
            WriteLiteral("</span>\r\n");
            EndContext();
#line 22 "D:\work\LearningExperience\LearningExperience.Web\Pages\Error.cshtml"
        }
        else
        {

#line default
#line hidden
            BeginContext(492, 46, true);
            WriteLiteral("            <span>Exception not found</span>\r\n");
            EndContext();
#line 26 "D:\work\LearningExperience\LearningExperience.Web\Pages\Error.cshtml"
        }
    

#line default
#line hidden
            BeginContext(556, 6, true);
            WriteLiteral("</p>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ErrorModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ErrorModel>)PageContext?.ViewData;
        public ErrorModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
