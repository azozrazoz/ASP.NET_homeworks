﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Util
{
    public class ImageResult : ActionResult
    {
        public string path;

        public ImageResult(string path)
        {
            this.path = path;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response
                .Write("<div style=\"width: 100%; text-align: center; \">" + 
                "<img style=\"max-width: 600px;\" src=\"" + path + "\" /></div>"); 
        }
    }
}