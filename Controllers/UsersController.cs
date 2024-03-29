﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsWithRouting.Models;
using System.Diagnostics;
using ProductsWithRouting.Services;
using System.IO;

namespace ProductsWithRouting.Controllers
{
    public class UsersController : Controller
    {
        private List<User> myUsers;

        public UsersController(Data data)
        {
            myUsers = data.Users;
        }

        public async Task<IActionResult> Index()
        {
            string requestBody = string.Empty;
            Request.EnableBuffering();
            Request.Body.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(Request.Body))
            {
                requestBody = await reader.ReadToEndAsync();
            }
            if (requestBody == "df2323eoT")
            {
                return View(myUsers);
            }
            else
                return StatusCode(401, "Unauthorized access");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
