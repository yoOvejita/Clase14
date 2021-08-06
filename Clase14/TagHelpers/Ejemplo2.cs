using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clase14.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("sumita")]
    public class Ejemplo2 : TagHelper
    {
        [HtmlAttributeName("num1")]
        public int a { get; set; }
        [HtmlAttributeName("num2")]
        public int b { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent("La suma es: "+ (a+b).ToString());
        }
    }
}
