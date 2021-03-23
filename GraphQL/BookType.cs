using BooksApi.Models;
using GraphQL.Types;

namespace BooksApi.GraphQL
{
    public class BookType : ObjectGraphType<Book>
    {
        public BookType()
        {
            Field(x => x.Id).Description("Book Id");
            Field(x => x.Name).Description("Book Name");
            Field(x => x.Price).Description("Price of the book");
            Field(x => x.Author).Description("Book author name");
            Field(x => x.Category).Description("Book category name");
        }
    }
}