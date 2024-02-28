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
        private const string PriorityPrefix = "--PRIORITY:";
        public string Name = "";
        public string Body = "";
        public int Priority = 0;

        public static LuaScript FromFile(string filename, bool usePriority = false)
        {
            if (!File.Exists(filename)) return null;
            var script = new LuaScript();
            script.Body = File.ReadAllText(filename);
            script.Name = Path.GetFileName(filename);
            if (usePriority)
            {
                using (var reader = new StringReader(script.Body))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith(PriorityPrefix))
                        {
                            var priorityString = line.Substring(PriorityPrefix.Length);
                            if (int.TryParse(priorityString, out var result))
                            {
                                script.Priority = result;
                                break;
                            }
                        }
                    }
                }
            }
            return script;
        }

        public void Run(Script script)
        {
            script.DoString(Body, null, Name);
        }

        public void RunInContext(Script script, LuaGameObject gameObject)
        {
            script.Globals["GameObject"] = gameObject;
            script.DoString(Body, null, Name);
        }
    }
}
