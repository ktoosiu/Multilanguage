
using MultiChain.Interface;
using MultiChain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChain.Services
{
    public class MultiChain : IMultiChain
    {




        public async Task<Book> Get(string language)
        {
            var userL = new UserLanguaheHandler();
            var pol = new PolishHandler();
            var eng = new EnglishHandler();

            userL.SetNext(eng).SetNext(pol);

            var temp =  Client.ClientCode(userL, language);

            return temp;

        }

        public async Task<IEnumerable<Book>> GetAll(string language)
        {
            var userL = new UserLanguaheHandler();
            var pol = new PolishHandler();
            var eng = new EnglishHandler();

            userL.SetNext(eng).SetNext(pol);

            var temp = Client.ClientCodeAll(userL, language);

            

        
            return temp;
        }

        
    }


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
                return base.Handle(request, lang);
            }
        }
    }

    class PolishHandler : AbstractHandler
    {
        public override object Handle(object request, string lang)
        {
            if (lang == "pl-PL")
            {
                return request;

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
            if (lang == "en-GB")
            {
                return request;

            }
            else
            {
                return base.Handle(request, lang);
            }
        }
    }



    class Client
    {
        public static Book ClientCode(AbstractHandler handler, string lang)
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
                            Title="test1",
                            ShortDescription="angielski opis",
                            LanguageCode="en-GB"
                        },
                           new Book
                        {
                            Title="test2",
                            ShortDescription="dunski opis",
                            LanguageCode="da-DK"
                        },

                         new Book
                        {
                            Title="test4",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
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
                            Title="test5",
                            ShortDescription="portugalski opisz",
                            LanguageCode="pt-BR"
                        },
                         new Book
                        {
                            Title="test6",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                          new Book
                        {
                            Title="test7",
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
                            Title="test8",
                            ShortDescription="portugalski opisz",
                            LanguageCode="pt-BR"
                        },
                         new Book
                        {
                            Title="test9",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                         new Book
                        {
                            Title="test3",
                            ShortDescription="polski opisz",
                            LanguageCode="pl-PL"
                        }

                    }

                },

            };


            foreach (var item in tempList)
            {
                foreach (var bookTemp in item.Books)
                {
                    var result = handler.Handle(bookTemp.LanguageCode, lang);

                    if (result != null)
                    {
                        return bookTemp;
                    }
                }
            }



            return tempList[0].Books.ElementAt(0);

        }

        public static List<Book> ClientCodeAll(AbstractHandler handler, string lang)
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
                            Title="test1",
                            ShortDescription="angielski opis",
                            LanguageCode="en-GB"
                        },
                           new Book
                        {
                            Title="test2",
                            ShortDescription="dunski opis",
                            LanguageCode="da-DK"
                        },

                         new Book
                        {
                            Title="test4",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
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
                            Title="test5",
                            ShortDescription="portugalski opisz",
                            LanguageCode="pt-BR"
                        },
                         new Book
                        {
                            Title="test6",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                          new Book
                        {
                            Title="test7",
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
                            Title="test8",
                            ShortDescription="portugalski opisz",
                            LanguageCode="pt-BR"
                        },
                         new Book
                        {
                            Title="test9",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                         new Book
                        {
                            Title="test3",
                            ShortDescription="polski opisz",
                            LanguageCode="pl-PL"
                        }

                    }

                },

            };


            var resultList = new List<Book>();


            foreach (var item in tempList)
            {
                foreach (var bookTemp in item.Books)
                {
                    var result = handler.Handle(bookTemp.LanguageCode, lang);

                    if (result != null)
                    {
                        resultList.Add(bookTemp);
                    }
                }
            }

            if (resultList.Count == 0)
            {
                var firstElement = new List<Book>();
                firstElement.Add(tempList[0].Books.ElementAt(0));
                return firstElement;

            }
            else
                return resultList;

        }

    }


}
