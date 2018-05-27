﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassifiedApp.Resources {
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
    public class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ClassifiedApp.Resources.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to Bad Request.
        /// </summary>
        public static string BadRequest {
            get {
                return ResourceManager.GetString("BadRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password and Confirm don&apos;t match..
        /// </summary>
        public static string ComparePassword {
            get {
                return ResourceManager.GetString("ComparePassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to رقم الهاتف غير متاح.
        /// </summary>
        public static string GsmNotAvailable {
            get {
                return ResourceManager.GetString("GsmNotAvailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Category ID is invalid.
        /// </summary>
        public static string InvalidCategory {
            get {
                return ResourceManager.GetString("InvalidCategory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid mobile number.
        /// </summary>
        public static string InvalidGSM {
            get {
                return ResourceManager.GetString("InvalidGSM", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field must be between {0} - {1} characters.
        /// </summary>
        public static string InvalidLength {
            get {
                return ResourceManager.GetString("InvalidLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password must be Minimum 8 characters atleast 1 Alphabet, 1 Number and 1 Special Character.
        /// </summary>
        public static string InvalidPassword {
            get {
                return ResourceManager.GetString("InvalidPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Token.
        /// </summary>
        public static string InvalidToken {
            get {
                return ResourceManager.GetString("InvalidToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username can only contains alphanumeric characters and underscore..
        /// </summary>
        public static string InvalidUsername {
            get {
                return ResourceManager.GetString("InvalidUsername", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field Required.
        /// </summary>
        public static string Required {
            get {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unauthorized Request.
        /// </summary>
        public static string UnauthorizedRequest {
            get {
                return ResourceManager.GetString("UnauthorizedRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User GSM is not activated.
        /// </summary>
        public static string UserGsmNotApproved {
            get {
                return ResourceManager.GetString("UserGsmNotApproved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username must be between 3-50 characters..
        /// </summary>
        public static string UsernameLength {
            get {
                return ResourceManager.GetString("UsernameLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username not available.
        /// </summary>
        public static string UsernameNotAvailable {
            get {
                return ResourceManager.GetString("UsernameNotAvailable", resourceCulture);
            }
        }
    }
}
