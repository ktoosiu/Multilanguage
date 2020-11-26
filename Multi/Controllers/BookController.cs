using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Multi.Model;
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
       

        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }




        [HttpGet("{codeL}")]
        public IEnumerable<Autor> GetAll(string codeL)
        {
            var temp = new[]
            {
                new Autor
                {
                    Id=1,
                    FirstName="Kamil",
                    LastName="Pajak",
                    Books=new[]
                    {
                        new Book
                        {
                            Title="test",
                            ShortDescription="polski opisz",
                            LanguageCode="pl-PL"
                        },
                         new Book
                        {
                            Title="test1",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                          new Book
                        {
                            Title="test2",
                            ShortDescription="angielski opis",
                            LanguageCode="en-GB"
                        }
                    }

                },
                  new Autor
                {
                    Id=2,
                    FirstName="bartosz",
                    LastName="jarosz",
                    Books=new[]
                    {
                        new Book
                        {
                            Title="test",
                            ShortDescription="portugalski opisz",
                            LanguageCode="pt-BR"
                        },
                         new Book
                        {
                            Title="test1",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                          new Book
                        {
                            Title="test2",
                            ShortDescription="angielski opis",
                            LanguageCode="en-GB"
                        }
                    }

                },
                   new Autor
                {
                    Id=3,
                    FirstName="tt",
                    LastName="tt",
                    Books=new[]
                    {
                        new Book
                        {
                            Title="test",
                            ShortDescription="portugalski opisz",
                            LanguageCode="pt-BR"
                        },
                         new Book
                        {
                            Title="test1",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        }
                        
                    }

                },

            };




            return temp;
          
        }

        //[HttpGet]
        //public Autor Get(int id, string codeL)
        //{

        //    var temp = new Autor
        //    {
        //        Id = 1,
        //        FirstName = "Kamil",
        //        LastName = "Pajak",
        //        Books = new[]
        //            {
        //                new Book
        //                {
        //                    Title="test",
        //                    ShortDescription="polski opisz",
        //                    LanguageCode="pl-PL"
        //                },
        //                 new Book
        //                {
        //                    Title="test1",
        //                    ShortDescription="Norweski ospi",
        //                    LanguageCode="nb-NO"
        //                },
        //                  new Book
        //                {
        //                    Title="test2",
        //                    ShortDescription="angielski opis",
        //                    LanguageCode="en-GB"
        //                }
        //            }

        //    };

        //    return temp;
        //}
        
        [HttpGet]
        public string Get123(string temp)
        {


            var userL = new UserLanguaheHandler();
            var pol = new PolishHandler();
            var eng = new EnglishHandler();
            

            userL.SetNext(pol).SetNext(eng);

            // The client should be able to send a request to any handler, not
            // just the first one in the chain.
            //Console.WriteLine("Chain: Monkey > Squirrel > Dog\n");
            //Client.ClientCode(monkey);
            //Console.WriteLine();

            //Console.WriteLine("Subchain: Squirrel > Dog\n");
            //Client.ClientCode(squirrel);

             

            

            return Client.ClientCode(userL, temp).ToString();
        }


    }



    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request, string lang);
    }

    // The default chaining behavior can be implemented inside a base handler
    // class.
    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            
            return handler;
        }

        public virtual object Handle(object request, string lang)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request, lang);
            }
            else
            {
                return null;
            }
        }
    }

    class UserLanguaheHandler : AbstractHandler
    {
        public override object Handle(object request, string lang)
        {
            if ((request as string) == lang)
            {
                return request;
            }
            else
            {
                return base.Handle(request,lang);
            }
        }
    }

    class PolishHandler : AbstractHandler
    {
        public override object Handle(object request, string lang)
        {
            if ((request as string) == "pl-PL")
            {
                return $"Monkey: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request, lang);
            }
        }
    }

    class EnglishHandler : AbstractHandler
    {
        public override object Handle(object request, string lang)
        {
            if (request.ToString() == "en-GB")
            {
                return $"Squirrel: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request, lang);
            }
        }
    }

    

    class Client
    {
        // The client code is usually suited to work with a single handler. In
        // most cases, it is not even aware that the handler is part of a chain.
        public static Book  ClientCode(AbstractHandler handler, string lang)
        {

            var tempList = new[]
            {
                new Autor
                {
                    Id=1,
                    FirstName="Kamil",
                    LastName="Pajak",
                    Books=new[]
                    {
                        new Book
                        {
                            Title="test",
                            ShortDescription="polski opisz",
                            LanguageCode="pl-PL"
                        },
                         new Book
                        {
                            Title="test1",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                          new Book
                        {
                            Title="test2",
                            ShortDescription="angielski opis",
                            LanguageCode="en-GB"
                        }
                    }

                },
                  new Autor
                {
                    Id=2,
                    FirstName="bartosz",
                    LastName="jarosz",
                    Books=new[]
                    {
                        new Book
                        {
                            Title="test",
                            ShortDescription="portugalski opisz",
                            LanguageCode="pt-BR"
                        },
                         new Book
                        {
                            Title="test1",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                          new Book
                        {
                            Title="test2",
                            ShortDescription="angielski opis",
                            LanguageCode="en-GB"
                        }
                    }

                },
                   new Autor
                {
                    Id=3,
                    FirstName="tt",
                    LastName="tt",
                    Books=new[]
                    {
                        new Book
                        {
                            Title="test",
                            ShortDescription="portugalski opisz",
                            LanguageCode="pt-BR"
                        },
                         new Book
                        {
                            Title="test1",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        }

                    }

                },

            };


            foreach (var item in tempList)
            {
                foreach (var bookTemp in item.Books)
                {
                    var result = handler.Handle(bookTemp.LanguageCode, lang);

                    if (result.ToString() == lang)
                    {
                        return bookTemp;
                    }                   
                }                
            }
            
            return tempList.ElementAt(0).Books.ElementAt(0);

        }
    }

  






}
