using GraphQL.Types;  

namespace BooksApi.GraphQL
{
    public class BookSchema : Schema
    {
        public BookSchema(BookQuery query)
        {
            Query = query;
        }
    }
}