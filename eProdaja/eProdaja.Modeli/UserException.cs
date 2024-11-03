using System;
using System.Collections.Generic;
using System.Text;

namespace eProdaja.Modeli
{
    public class UserException:Exception
    {
        public UserException(string message):base(message) { }
    }
}
