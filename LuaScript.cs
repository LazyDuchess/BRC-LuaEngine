using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine
{
    public class LuaScript
    {
        public string Name = "";
        public string Body = "";

        public static LuaScript FromFile(string filename)
        {
            if (!File.Exists(filename)) return null;
            var script = new LuaScript();
            script.Body = File.ReadAllText(filename);
            script.Name = Path.GetFileName(filename);
            return script;
        }

        public void Run(Script script)
        {
            script.DoString(Body, null, Name);
        }
    }
}
