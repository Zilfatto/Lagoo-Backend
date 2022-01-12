﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lagoo.BusinessLogic.Resources.CommandsAndQueries {
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
    public class EventResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal EventResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Lagoo.BusinessLogic.Resources.CommandsAndQueries.EventResources", typeof(EventResources).Assembly);
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
        ///   Looks up a localized string similar to Address is not specified.
        /// </summary>
        public static string AddressIsEmpty {
            get {
                return ResourceManager.GetString("AddressIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Address cannot be longer than {0} characters.
        /// </summary>
        public static string AddressIsTooLong {
            get {
                return ResourceManager.GetString("AddressIsTooLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Beginning date is not specified.
        /// </summary>
        public static string BeginningDateIsEmpty {
            get {
                return ResourceManager.GetString("BeginningDateIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Comment cannot contain only whitespaces.
        /// </summary>
        public static string CommentIsEmpty {
            get {
                return ResourceManager.GetString("CommentIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Comment cannot be longer than {0} characters.
        /// </summary>
        public static string CommentIsTooLong {
            get {
                return ResourceManager.GetString("CommentIsTooLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Duration is not specified.
        /// </summary>
        public static string DurationIsEmpty {
            get {
                return ResourceManager.GetString("DurationIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Event with the specified ID was not found.
        /// </summary>
        public static string EventWasNotFound {
            get {
                return ResourceManager.GetString("EventWasNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified ID is invalid.
        /// </summary>
        public static string InvalidId {
            get {
                return ResourceManager.GetString("InvalidId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified sorting order is invalid.
        /// </summary>
        public static string InvalidSortingOrder {
            get {
                return ResourceManager.GetString("InvalidSortingOrder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified property is invalid for sorting.
        /// </summary>
        public static string InvalidSortingProperty {
            get {
                return ResourceManager.GetString("InvalidSortingProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type is invalid.
        /// </summary>
        public static string InvalidType {
            get {
                return ResourceManager.GetString("InvalidType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name is not specified.
        /// </summary>
        public static string NameIsEmpty {
            get {
                return ResourceManager.GetString("NameIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name cannot be longer than {0} characters.
        /// </summary>
        public static string NameIsTooLong {
            get {
                return ResourceManager.GetString("NameIsTooLong", resourceCulture);
            }
        }
    }
}
