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




        [HttpGet("{codeL}")]
        public List<Book> GetAll(string codeL)
        {
            


            var resuult = _multiChain.GetAll(codeL).Result.ToList();

            return resuult;

        }


        [HttpGet]
        public Book Get123(string temp)
        {

            
            var resuult = _multiChain.Get(temp).Result;

           
            return resuult;


        }


    }



    

   
  






}
