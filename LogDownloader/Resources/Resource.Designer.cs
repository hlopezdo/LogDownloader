﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogDownloader.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LogDownloader.Resources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Download started.
        /// </summary>
        internal static string DownloadStarted {
            get {
                return ResourceManager.GetString("DownloadStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a There are not any environment selected.
        /// </summary>
        internal static string ErrorEnvironment {
            get {
                return ResourceManager.GetString("ErrorEnvironment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No folders have been selected.
        /// </summary>
        internal static string ErrorNotFolders {
            get {
                return ResourceManager.GetString("ErrorNotFolders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a An error has ocurred while trying to parse a name of the files.
        /// </summary>
        internal static string ErrorParsingName {
            get {
                return ResourceManager.GetString("ErrorParsingName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a There are not running any thread now.
        /// </summary>
        internal static string ErrorStop {
            get {
                return ResourceManager.GetString("ErrorStop", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Finished work.
        /// </summary>
        internal static string FinishedWork {
            get {
                return ResourceManager.GetString("FinishedWork", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Generating selected output folders.
        /// </summary>
        internal static string GenerateFolders {
            get {
                return ResourceManager.GetString("GenerateFolders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Initializing....
        /// </summary>
        internal static string Initializing {
            get {
                return ResourceManager.GetString("Initializing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Profile loaded.
        /// </summary>
        internal static string ProfileLoaded {
            get {
                return ResourceManager.GetString("ProfileLoaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Scanning folders.
        /// </summary>
        internal static string ScanningFolders {
            get {
                return ResourceManager.GetString("ScanningFolders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Stopped.
        /// </summary>
        internal static string Stopped {
            get {
                return ResourceManager.GetString("Stopped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Stopping....
        /// </summary>
        internal static string Stopping {
            get {
                return ResourceManager.GetString("Stopping", resourceCulture);
            }
        }
    }
}
