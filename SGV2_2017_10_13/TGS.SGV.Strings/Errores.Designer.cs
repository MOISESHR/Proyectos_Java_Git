﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TGS.SGV.Strings {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Errores {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Errores() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TGS.SGV.Strings.Errores", typeof(Errores).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocurrio un error en la base de datos..
        /// </summary>
        public static string CONBD {
            get {
                return ResourceManager.GetString("CONBD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocurrio un error al conectarse al servicio web..
        /// </summary>
        public static string CONWS {
            get {
                return ResourceManager.GetString("CONWS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocurrio un error de protocolo de servicio web..
        /// </summary>
        public static string CONWSPT {
            get {
                return ResourceManager.GetString("CONWSPT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se pudo enviar el correo.
        /// </summary>
        public static string EnvioCorreo {
            get {
                return ResourceManager.GetString("EnvioCorreo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocurrio un error con en el servicio..
        /// </summary>
        public static string NEGOCIO {
            get {
                return ResourceManager.GetString("NEGOCIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocurrio un error inesperado en el servicio..
        /// </summary>
        public static string NOMANEJADOWS {
            get {
                return ResourceManager.GetString("NOMANEJADOWS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocurrio un error de tiempo de espera..
        /// </summary>
        public static string TIMEOUTWS {
            get {
                return ResourceManager.GetString("TIMEOUTWS", resourceCulture);
            }
        }
    }
}
