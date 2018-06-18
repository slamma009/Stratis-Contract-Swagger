using System;
using System.Collections.Generic;
using System.Text;

namespace api.Controllers
{
    public class StratisMethodInfo
    {
        public StratisMethodInfo() { }
        public StratisMethodInfo(string name)
        {
            MethodName = name;
        }
        public string MethodName;
        public StratisParamterInfo[] Paramters;
    }

    public class StratisParamterInfo
    {
        public StratisParamterInfo() { }
        public StratisParamterInfo(string type, string name)
        {
            ParamterType = type;
            ParamterName = name;
        }
        public string ParamterType;
        public string ParamterName;
    }
}
