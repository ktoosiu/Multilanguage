using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MultiChain.Handlers;
using MultiChain.Model;

namespace MultiChain.Services
{
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
                            Title="test2",
                            ShortDescription="dunski opis",
                            LanguageCode="da-DK"
                        },

                         new Book
                        {
                            Title="test4",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                        //   new Book
                        //{
                        //    Title="test1",
                        //    ShortDescription="angielski opis",
                        //    LanguageCode="en-GB"
                        //}
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
                        // new Book
                        //{
                        //    Title="test3",
                        //    ShortDescription="polski opisz",
                        //    LanguageCode="pl-PL"
                        //}
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
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Books");

            var booksColection = database.GetCollection<Book>("books");//odczytuje kolekcj
            var querableCollection = booksColection.AsQueryable<Book>();
            var resultList = new List<Book>();

            foreach (var item in querableCollection)
            {
                //foreach (var bookTemp in item.Books)
                //{
                    var result = handler.Handle(item.LanguageCode, lang);

                    if (result != null)
                    {
                        resultList.Add(item);
                    }
                //}
            }

            if (resultList.Count == 0)
            {
                var firstElement = new List<Book>();
                //firstElement.Add(tempList[0].Books.ElementAt(0));
                return firstElement;

            }
            else
                return resultList;
        }
    }
}
