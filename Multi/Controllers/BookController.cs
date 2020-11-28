using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MultiChain.Interface;
using MultiChain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        private IMultiChain _multiChain;
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger, IMultiChain multiChain)
        {
            _logger = logger;
            _multiChain = multiChain;
        }

        [HttpGet("{codeLang}")]
        public List<Book> GetAll(string codeLang)
        {
            var result = _multiChain.GetAll(codeLang).Result.ToList();

            return result;
        }

        [HttpGet]
        public Book Get123(string codeLang)
        {
            var result = _multiChain.Get(codeLang).Result;

            return result;
        }
    }
}
