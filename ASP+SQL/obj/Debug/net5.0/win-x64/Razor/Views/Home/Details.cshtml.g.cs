#pragma checksum "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "153fc7b3a888914d34ff9489f9136eece67f9c23"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Details), @"mvc.1.0.view", @"/Views/Home/Details.cshtml")]
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
#nullable restore
#line 1 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\_ViewImports.cshtml"
using ReWeight;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
using ReWeight.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"153fc7b3a888914d34ff9489f9136eece67f9c23", @"/Views/Home/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5a36b865ac59be96a609e42a416bf995fde844ee", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ReWeight.Models.ControlPoint>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "InterReWeight", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
  
    ViewBag.Title = Model.Name;

#line default
#line hidden
#nullable disable
            WriteLiteral("<h2>Контрольная точка ");
#nullable restore
#line 6 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
                 Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n<div>\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>Регион</dt>\r\n        <dd>");
#nullable restore
#line 10 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
       Write(Model.Region.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n\r\n        <dt>Ip Emoiv</dt>\r\n        <dd>");
#nullable restore
#line 13 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
       Write(Model.Region.IpEmoiv);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n\r\n        <dt>Id контрольной точки</dt>\r\n        <dd>");
#nullable restore
#line 16 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
       Write(Model.ControlPointId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n\r\n        <dt>Ip Vpn</dt>\r\n        <dd>");
#nullable restore
#line 19 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
       Write(Model.IpVpn);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n\r\n        <dt>Локальный Ip</dt>\r\n        <dd>");
#nullable restore
#line 22 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
       Write(Model.Iplokal);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n\r\n        <dt>IPMI</dt>\r\n        <dd>");
#nullable restore
#line 25 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
       Write(Model.IPMI);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n\r\n\r\n        <dd><a");
            BeginWriteAttribute("href", " href=\"", 589, "\"", 614, 1);
#nullable restore
#line 28 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
WriteAttributeValue("", 596, Model.Links.Nomad, 596, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" target=\"_blank\">Nomad</a></dd>\r\n\r\n        <dd> <a");
            BeginWriteAttribute("href", " href=\"", 665, "\"", 702, 1);
#nullable restore
#line 30 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
WriteAttributeValue("", 672, Model.Links.ConsulDataCapture, 672, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" target=\"_blank\">Consul DataCapture</a></dd>\r\n\r\n        <dd> <a");
            BeginWriteAttribute("href", " href=\"", 766, "\"", 790, 1);
#nullable restore
#line 32 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
WriteAttributeValue("", 773, Model.Links.SGDK, 773, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" target=\"_blank\">Consul SGDK</a></dd>\r\n\r\n        <dd><a");
            BeginWriteAttribute("href", " href=\"", 846, "\"", 878, 1);
#nullable restore
#line 34 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
WriteAttributeValue("", 853, Model.Links.Сoefficients, 853, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" target=\"_blank\">функциональные поправки</a></dd>\r\n\r\n\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "153fc7b3a888914d34ff9489f9136eece67f9c237566", async() => {
                WriteLiteral("Восстановить вес");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 37 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
                                                              WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n        <dt>Дополнительная информация</dt>\r\n        <dd>");
#nullable restore
#line 40 "C:\Users\admin\source\repos\CommonLegacy\ReWeight\Views\Home\Details.cshtml"
       Write(Model.Information);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n    </dl>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ReWeight.Models.ControlPoint> Html { get; private set; }
    }
}
#pragma warning restore 1591