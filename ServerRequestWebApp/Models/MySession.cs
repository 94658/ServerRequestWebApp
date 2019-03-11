using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerRequestWebApp.Models
{
    public class MySession
    {
        private MySession()
        {
            userName = "";
        }

        // Gets the current session.
        public static MySession Current
        {
            get
            {
                MySession session =
                  (MySession)HttpContext.Current.Session["__MySession__"];
                if (session == null)
                {
                    session = new MySession();
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }

        // **** add your session properties here
        public int ID { get; set; }
        public string userName { get; set; }
        public DateTime MyDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
