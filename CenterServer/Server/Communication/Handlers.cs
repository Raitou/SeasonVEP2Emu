using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;

namespace libcomservice
{
    public delegate void Handler(object val);
    public delegate void Handler<T>(T val);

    public class Handlers
    {
        public Dictionary<int, Type> TYPE_HANDLER = new Dictionary<int, Type>();        
        public Dictionary<int, MethodInfo> HANDLER = new Dictionary<int, MethodInfo>();        

        public void RegisterHandler(int PACKEID, MethodInfo handler)
        {
            HANDLER.Add(PACKEID, handler);
        }

        public void RegisterHandler(int PACKEID, Type construct_handler)
        {
            TYPE_HANDLER.Add(PACKEID, construct_handler);
        }
    }
}
