﻿using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine
{
    public class LuaScript
    {
        private const string InternalVariablePrefix = "_LUAENGINE_INTERNAL_";
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

        public void RunForScriptBehavior(Script script, LuaScriptBehavior scriptBehavior)
        {
            var contextBody = AddLocalContext(script, Body, "script", scriptBehavior);
            script.DoString(contextBody, null, Name);
            RemoveLocalContext(script, "script");
        }

        private string AddLocalContext(Script script, string body, string variableName, object variableValue)
        {
            script.Globals[$"{InternalVariablePrefix}{variableName}"] = variableValue;
            var sb = new StringBuilder();
            sb.AppendLine($"local {variableName} = {InternalVariablePrefix}{variableName}");
            sb.AppendLine($"{InternalVariablePrefix}{variableName} = nil");
            sb.AppendLine(body);
            return sb.ToString();
        }

        private void RemoveLocalContext(Script script, string variableName)
        {
            script.Globals[$"{InternalVariablePrefix}{variableName}"] = DynValue.Nil;
        }
    }
}
