using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIClientTest.Models;

namespace WebAPIClientTest.Server.Controllers
{
    [Route("api/[controller]")]
    public class DataController
    {
        public IEnumerable<string> Post([FromBody]User user)
        {
            yield return $"Welcome {user.UserName}!";
            yield return $"Your ID is {user.UserId}.";
            yield return $"And your password contains {user.UserPassword.Length} characters.";
        }
    }
}