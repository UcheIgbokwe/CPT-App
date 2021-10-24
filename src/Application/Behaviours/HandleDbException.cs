using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Behaviours
{
    public class HandleDbException : Exception
    {
        public HandleDbException() : base("Database error detected. Please try again.")
        {
        }

        public HandleDbException(string message) : base(message)
        {
        }

        public HandleDbException(string message, Exception ex) : base(message, ex)
        {
            if(ex.InnerException != null)
            {
                if (ex.InnerException.ToString().Contains("duplicate"))
                {
                    throw new HttpStatusException(HttpStatusCode.Conflict, "Record already exist.");
                    
                }else{
                    throw new HttpStatusException(HttpStatusCode.BadGateway, ex.InnerException.ToString());
                }
            }else{
                throw new HttpStatusException(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}