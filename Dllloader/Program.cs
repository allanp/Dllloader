using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.IO;

namespace Dllloader
{
    class TaskMessage
    {
        internal string methodName;
        internal object[] parameters;

        internal TaskMessage(string methodName, params object[] objects)
        {
            this.methodName = methodName;
            parameters = new object[objects.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = objects[i];
            }
        }

        internal static TaskMessage[] SampleMessages(string methodName, int count)
        {
            TaskMessage[] messages = new TaskMessage[count];

            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = new TaskMessage(methodName, i + 1, i + 2, i + 3);
            }

            Console.WriteLine();

            return messages;
        }
    }

    class Program
    {
        Program()
        {
            pluginDir = string.Empty;
            dllFiles = new List<string>();
            typesMap = new Dictionary<string, Type>();
        }

        static void Main(string[] args)
        {
            Console.WindowWidth += 50;

            new Program().Start();
        }

        string pluginDir;
        List<string> dllFiles;

        private void Start()
        {
            var msgs = TaskMessage.SampleMessages("Plus", 10);

            var targetMethodName = msgs[0].methodName;

            Console.WriteLine("Target Method Name: {0}", targetMethodName);

            pluginDir = @"D:\XXXXX\Codes\Dllloader\ComplexNumberCalc\bin\Debug";

            Console.WriteLine("Searching Dll files from: {0}", pluginDir);

            dllFiles.AddRange(Directory.GetFiles(pluginDir, "*.dll", SearchOption.AllDirectories));

            foreach (var dllFile in dllFiles)
            {
                Assembly ass = Assembly.LoadFile(dllFile);
                Console.WriteLine("Assembly fullname:\n {0}", ass.FullName);
                
                Console.WriteLine();

                var types = ass.GetTypes();
                foreach (var type in types)
                {
                    Console.WriteLine("Loaded type: {0}", type);

                    if (type.IsAssignableFrom(typeof(object)))
                    {

                    }

                    var methods = type.GetMethods();

                    foreach (var method in methods)
                    {
                        Console.WriteLine("\tMethod: {0}", method);

                        if (method.Name == targetMethodName)
                        {
                            ParameterInfo[] paramsInfo;
                            Console.WriteLine("\tFound the target method in the module: {0}", method.Module.Name);
                            Console.Write("\tCheck the method signature...");
                            if (checkMethodSignature(method, out paramsInfo))
                            {
                                Console.WriteLine("succeed.");
                                
                                object[] methodParam = new object[paramsInfo.Length];
                                object instance = Activator.CreateInstance(type);
                                foreach (var m in msgs)
                                {
                                    Console.WriteLine("\t\tConstructing parameters...");
                                    for (int i = 0; i < methodParam.Length; i++)
                                    {
                                        methodParam[i] = Activator.CreateInstance(paramsInfo[i].ParameterType, m.parameters[0], m.parameters[1]);
                                    }

                                    Console.WriteLine("\t\tInvoking the method: {0}", method.Name);
                                    object returnValue = method.Invoke(instance, methodParam);
                                    Console.WriteLine("\t\tReturn value: {0}", returnValue.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("failed.");
                            }
                        }
                    }
                }
            }

            Console.ReadLine();
        }


        Dictionary<string, Type> typesMap;

        private bool checkMethodSignature(MethodInfo method, out ParameterInfo[] paramsInfo)
        {
            paramsInfo = null;

            if (method == null)
                return false;

            paramsInfo = method.GetParameters();

            if (paramsInfo.Length != 2)
            {
                paramsInfo = null;
                return false;
            }

            var first = paramsInfo.First();

            foreach (var p in paramsInfo)
            {
                if (p.ParameterType != p.ParameterType)
                    return false;
            }

            return true;
        }
    }
}
