using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.Host.Helper
{
    internal sealed class Helper
    {
        public static Type GetDynamicEnumeration(ICollection<PluginContracts.IPlugin> plugins)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;

            // Create a dynamic assembly in the current application domain,
            // and allow it to be executed and saved to disk.
            AssemblyName aName = new AssemblyName("TempAssembly");
            AssemblyBuilder ab = currentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);

            // Define a dynamic module in "TempAssembly" assembly. For a single-
            // module assembly, the module has the same name as the assembly.
            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name + "_module");

            // Define a public enumeration with the name "Elevation" and an 
            // underlying type of Integer.
            EnumBuilder eb = mb.DefineEnum("AvailableRequestTypes", TypeAttributes.Public, typeof(int));

            int counter = 0;

            foreach (var item in plugins)
            {
                eb.DefineLiteral(item.Name, ++counter);
            }
            // Define two members, "High" and "Low".
            //eb.DefineLiteral("Low", 0);
            //eb.DefineLiteral("High", 1);

            // Create the type and save the assembly.
            return eb.CreateType();            
        }

        public static void GenerateDynamicEnum(ICollection<PluginContracts.IPlugin> plugins)
        {
            // Get the current application domain for the current thread
            AppDomain currentDomain = AppDomain.CurrentDomain;

            // Create a dynamic assembly in the current application domain,
            // and allow it to be executed and saved to disk.
            AssemblyName name = new AssemblyName("ExchangeRateEnums");
            AssemblyBuilder assemblyBuilder = currentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);

            // Define a dynamic module in "ExchangeRateEnums" assembly.
            // For a single-module assembly, the module has the same name as the assembly.
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(name.Name, name.Name + ".dll");

            // Define a public enumeration with the name "ExchangeRateEnums" and an underlying type of Integer.
            EnumBuilder myEnum = moduleBuilder.DefineEnum("EnumeratedTypes.ExchangeRateEnums", TypeAttributes.Public, typeof(int));

            // Get data from _plugins
            int counter = 1;
            foreach (var item in plugins)
            {
                myEnum.DefineLiteral(item.Name, counter++);
            }

            // Create the enum
            myEnum.CreateType();

            // Finally, save the assembly
            assemblyBuilder.Save(name.Name + ".dll");
        }       
    }
}
