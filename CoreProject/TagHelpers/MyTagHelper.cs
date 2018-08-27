using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.TagHelpers
{
    // To make this tag specially for a certain element 
    // Don't write this attribute if you want to make it public
    [HtmlTargetElement("span",Attributes ="asp-AhmedSalem")]

    public class MyTagHelper:TagHelper
    {
        public string text { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //// add this text to  the span text
            //output.Content.Append("Ahmed Mohamed Salem");
            // In the Razor Page => <span asp-AhmedSalem>Hello </span>

            // to make this param ariable
             output.Content.Append(text);
            // In the Razor Page => <span asp-AhmedSalem text="Mohamed">Hello </span>
            // Usefull for making Rating or Stars
            output.Attributes.Add("style", "color:blue");
           
        }
    }
}
