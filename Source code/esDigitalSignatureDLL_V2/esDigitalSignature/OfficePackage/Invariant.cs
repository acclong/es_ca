//---------------------------------------------------------------------------
//
// File: Invariant.cs
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
//
// Description: Provides methods that assert an application is in a valid state.
//
//---------------------------------------------------------------------------

using MS.Internal.WindowsBase;

namespace esDigitalSignature.OfficePackage
{
    using System;
    using System.Security; 
    using System.Security.Permissions;
    using Microsoft.Win32;
    using System.Diagnostics;
    using System.Windows;
    
    /// <summary>
    /// Provides methods that assert an application is in a valid state. 
    /// </summary>
    internal // DO NOT MAKE PUBLIC - See security notes on Assert
        static class Invariant
    {
        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Static ctor.  Initializes the Strict property.
        /// </summary>
        ///<SecurityNote>
        /// Critical - this function elevates to read from the registry. 
        /// TreatAsSafe - Not controllable from external input. 
        ///               The information stored indicates whether invariant behavior is "strict" or not. Considered safe. 
        ///</SecurityNote>
        [SecurityCritical, SecurityTreatAsSafe]
        static Invariant()
        {
            _strict = _strictDefaultValue;

#if PRERELEASE
            //
            // Let the user override the inital value of the Strict property from the registry.
            //

            new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\" + RegistryKeys.WPF).Assert(); 
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(RegistryKeys.WPF);

                if (key != null)
                {
                    object obj = key.GetValue("InvariantStrict");

                    if (obj is int)
                    {
                        _strict = (int)obj != 0;
                    }
                }
            }
            finally
            {
                CodeAccessPermission.RevertAll(); 
            }
#endif // PRERELEASE
        }

        #endregion Constructors

        //------------------------------------------------------
        //
        //  Internal Methods
        //
        //------------------------------------------------------

        #region Internal Methods

        /// <summary>
        /// Checks for a condition and shuts down the application if false.
        /// </summary>
        /// <param name="condition">
        /// If condition is true, does nothing.
        ///
        /// If condition is false, raises an assert dialog then shuts down the
        /// process unconditionally.
        /// </param>
        /// <SecurityNote>
        ///     Critical: This code will close the current process 
        ///     TreatAsSafe: This code is safe to call.
        ///                  Note that if this code were ever to become public,
        ///                  we have a potential denial-of-service vulnerability.
        ///                  Passing in false shuts down the process, even in
        ///                  partial trust.  However, not shutting down in
        ///                  partial trust is even worse: by definition a false condition
        ///                  means we've hit a bug in avalon code and we cannot safely
        ///                  continue.
        /// </SecurityNote>
        // [SecurityCritical, SecurityTreatAsSafe] - Removed for performance, OK so long as this class remains internal
        internal static void Assert(bool condition)
        {
            if (!condition)
            {
                FailFast(null, null);
            }
        }

        /// <summary>
        /// Checks for a condition and shuts down the application if false.
        /// </summary>
        /// <param name="condition">
        /// If condition is true, does nothing.
        ///
        /// If condition is false, raises an assert dialog then shuts down the
        /// process unconditionally.
        /// </param>
        /// <param name="invariantMessage">
        /// Message to display before shutting down the application.
        /// </param>
        /// <SecurityNote>
        ///     Critical: This code will close the current process 
        ///     TreatAsSafe: This code is safe to call.
        ///                  Note that if this code were ever to become public,
        ///                  we have a potential denial-of-service vulnerability.
        ///                  Passing in false shuts down the process, even in
        ///                  partial trust.  However, not shutting down in
        ///                  partial trust is even worse: by definition a false condition
        ///                  means we've hit a bug in avalon code and we cannot safely
        ///                  continue.
        /// </SecurityNote>
        // [SecurityCritical, SecurityTreatAsSafe] - Removed for performance, OK so long as this class remains internal
        internal static void Assert(bool condition, string invariantMessage)
        {
            if (!condition)
            {
                FailFast(invariantMessage, null);
            }
        }

        /// <summary>
        /// Checks for a condition and shuts down the application if false.
        /// </summary>
        /// <param name="condition">
        /// If condition is true, does nothing.
        ///
        /// If condition is false, raises an assert dialog then shuts down the
        /// process unconditionally.
        /// </param>
        /// <param name="invariantMessage">
        /// Message to display before shutting down the application.
        /// </param>
        /// <param name="detailMessage">
        /// Additional message to display before shutting down the application.
        /// </param>
        /// <SecurityNote>
        ///     Critical: This code will close the current process 
        ///     TreatAsSafe: This code is safe to call.
        ///                  Note that if this code were ever to become public,
        ///                  we have a potential denial-of-service vulnerability.
        ///                  Passing in false shuts down the process, even in
        ///                  partial trust.  However, not shutting down in
        ///                  partial trust is even worse: by definition a false condition
        ///                  means we've hit a bug in avalon code and we cannot safely
        ///                  continue.
        /// </SecurityNote>
        // [SecurityCritical, SecurityTreatAsSafe] - Removed for performance, OK so long as this class remains internal
        internal static void Assert(bool condition, string invariantMessage, string detailMessage)
        {
            if (!condition)
            {
                FailFast(invariantMessage, detailMessage);
            }
        }

        #endregion Internal Methods

        //------------------------------------------------------
        //
        //  Internal Properties
        //
        //------------------------------------------------------

        #region Internal Properties

        /// <summary>
        /// Property specifying whether or not the user wants to enable expensive
        /// verification diagnostics.  The Strict property is rarely used -- only
        /// when performance profiling shows a real problem.
        ///
        /// Default value is false on FRE builds, true on 


















        internal static bool Strict
        {
            get { return _strict; }

            set { _strict = value; }
        }

        #endregion Internal Properties

        //------------------------------------------------------
        //
        //  Private Methods
        //
        //------------------------------------------------------

        #region Private Methods

        /// <summary>
        ///     Shuts down the process immediately, with no chance for additional
        ///     code to run.
        /// 
        ///     In debug we raise a Debug.Assert dialog before shutting down.
        /// </summary>
        /// <param name="message">
        ///     Message to display before shutting down the application.
        /// </param>
        /// <param name="detailMessage">
        ///     Additional message to display before shutting down the application.
        /// </param>
        /// <SecurityNote>
        ///     Critical: This code will close the current process.
        ///     TreatAsSafe: This code is safe to call.
        ///         Note that if this code were made to be callable publicly,
        ///         we would have a potential denial-of-service vulnerability.
        /// </SecurityNote>
        [SecurityCritical, SecurityTreatAsSafe]
        private // DO NOT MAKE PUBLIC OR INTERNAL -- See security note
            static void FailFast(string message, string detailMessage)
        {
            if (Invariant.IsDialogOverrideEnabled)
            {
                // This is the override for stress and other automation.
                // Automated systems can't handle a popup-dialog, so let
                // them jump straight into the debugger.
                Debugger.Break();
            }

            Debug.Assert(false, "Invariant failure: " + message, detailMessage);

            System.Environment.FailFast("InvariantFailure");
        }

        #endregion Private Methods

        //------------------------------------------------------
        //
        //  Private Properties
        //
        //------------------------------------------------------

        #region Private Properties

        // Returns true if the default assert failure dialog has been disabled
        // on this machine.
        //
        // The dialog may be disabled by
        //   Installing a JIT debugger to the [HKEY_LOCAL_MACHINE\Software\Microsoft\.NETFramework]
        //     DbgJITDebugLaunchSetting and DbgManagedDebugger registry keys.
        ///<SecurityNote>
        /// Critical - this function elevates to read from the registry. 
        /// TreatAsSafe - Not controllable from external input. 
        ///               The information stored indicates whether dialog override is available or not. Safe to expose
        ///</SecurityNote>
        private static bool IsDialogOverrideEnabled
        {
            [SecurityCritical, SecurityTreatAsSafe]
            get
            {
                RegistryKey key;
                bool enabled;

                enabled = false;

                //extracting all the data under an elevation.
                object dbgJITDebugLaunchSettingValue;
                string dbgManagedDebuggerValue;
                PermissionSet ps = new PermissionSet(PermissionState.None);
                RegistryPermission regPerm = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\.NetFramework");
                ps.AddPermission(regPerm);
                ps.Assert();//BlessedAssert
                try
                {
                    key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\.NETFramework");
                    dbgJITDebugLaunchSettingValue = key.GetValue("DbgJITDebugLaunchSetting");
                    dbgManagedDebuggerValue = key.GetValue("DbgManagedDebugger") as string;
                }
                finally
                {
                    PermissionSet.RevertAssert();
                }
                //
                // Check for the enable.
                //
                if (key != null)
                {
                    //
                    // Only count the enable if there's a JIT debugger to launch.
                    //
                    enabled = (dbgJITDebugLaunchSettingValue is int && ((int)dbgJITDebugLaunchSettingValue & 2) != 0);
                    if (enabled)
                    {
                        enabled = dbgManagedDebuggerValue != null && dbgManagedDebuggerValue.Length > 0;
                    }
                }
                return enabled;
            }
        }

        #endregion Private Properties

        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------

        #region Private Fields

        // Property specifying whether or not the user wants to enable expensive
        // verification diagnostics.
        ///<SecurityNote>
        /// Critical - this data member required elevated permissions to be set. 
        /// TreatAsSafe - this data indicates whether "strict" invariant mode is to be used. Considered safe
        ///</SecurityNote> 
        [SecurityCritical, SecurityTreatAsSafe] 
        private static bool _strict;

        // Used to initialize the default value of _strict in the static ctor.
        private const bool _strictDefaultValue
#if DEBUG
            = true;     // Enable strict asserts by default on CHK builds.
#else
            = false;
#endif

        #endregion Private Fields
    }
}

