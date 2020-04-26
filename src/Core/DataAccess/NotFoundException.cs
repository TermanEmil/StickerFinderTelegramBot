using System;

namespace DataAccess
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string entityName, object key)
            : this($"{entityName} with the key {key} - Not Found")
        {
        }
    }
}
