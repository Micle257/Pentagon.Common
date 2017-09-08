namespace Pentagon.Attributes {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;

    /// <summary> Represents a service for registration. </summary>
    public class RegisterService
    {
        /// <summary> Gets the registrations for the assembly. </summary>
        /// <param name="assembly"> The assembly. </param>
        /// <returns> An iteration of a <see cref="RegistrationItem" />. </returns>
        public IEnumerable<RegistrationItem> GetRegistrations(Assembly assembly)
        {
            var regs = new Collection<RegistrationItem>();
            var registerTypes = assembly.GetExportedTypes().Where(t => t.GetTypeInfo().GetCustomAttribute<RegisterAttribute>() != null);
            foreach (var type in registerTypes)
            {
                var att = type.GetTypeInfo().GetCustomAttribute<RegisterAttribute>();
                if (att.BindReference == null)
                {
                    if (type.GetTypeInfo().ImplementedInterfaces.Any())
                    {
                        var thatOne = type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(i => i.Name == $"I{type.Name}");
                        if (thatOne != null)
                        {
                            regs.Add(new RegistrationItem
                                     {
                                         ImplementationType = type,
                                         BindingType = thatOne,
                                         Type = att.Type
                                     });
                        }
                    }
                    regs.Add(new RegistrationItem
                             {
                                 ImplementationType = type,
                                 Type = att.Type
                             });
                }
                else if (att.BindReference != null)
                {
                    regs.Add(new RegistrationItem
                             {
                                 ImplementationType = type,
                                 BindingType = att.BindReference,
                                 Type = att.Type
                             });
                }
            }

            if (regs.Any())
                return regs;
            return null;
        }
    }
}