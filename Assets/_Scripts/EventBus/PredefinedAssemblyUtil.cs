using System;
using System.Collections.Generic;
using System.Reflection;

namespace _Scripts
{
    public static class PredefinedAssemblyUtil
    {
        enum AssemblyType
        {
            AssemblyCSharp,
            AssemblyCSharpEditor,
            AssemblyCSharpEditorFirstpass,
            AssemblyCSharpFirstpass,
        }

        static AssemblyType? GetAssemblyType(string assemblyName)
        {
            switch (assemblyName)
            {
                case "Assembly-CSharp":
                    return AssemblyType.AssemblyCSharp;
                case "Assembly-CSharp-Editor":
                    return AssemblyType.AssemblyCSharpEditor;
                case "Assembly-CSharp-Editor-firstpass":
                    return AssemblyType.AssemblyCSharpEditorFirstpass;
                case "Assembly-CSharp-firstpass":
                    return AssemblyType.AssemblyCSharpFirstpass;
                default:
                    return null;
            }
        }

        static void AddTypesFromAssembly(Type[] assembly, ICollection<Type> types, Type interfaceType)
        {
            if (assembly == null) return;

            for (int i = 0; i < assembly.Length; i++)
            {
                Type type = assembly[i];

                if (type != interfaceType && interfaceType.IsAssignableFrom(type))
                {
                    types.Add(type);
                }
            }
        }

        public static List<Type> GetTypes(Type interfaceType)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Dictionary<AssemblyType, Type[]> assemblyTypes = new Dictionary<AssemblyType, Type[]>();
            List<Type> types = new List<Type>();

            for (int i = 0; i < assemblies.Length; i++)
            {
                AssemblyType? assemblyType = GetAssemblyType(assemblies[i].GetName().Name);

                if (assemblyType != null)
                {
                    assemblyTypes.Add(assemblyType.Value, assemblies[i].GetTypes());
                }
            }

            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharp], types, interfaceType);
            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharpFirstpass], types, interfaceType);

            return types;
        }
    }
}