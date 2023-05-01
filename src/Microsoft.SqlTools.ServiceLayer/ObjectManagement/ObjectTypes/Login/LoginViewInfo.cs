//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//
#nullable disable

namespace Microsoft.SqlTools.ServiceLayer.ObjectManagement
{
    public class LoginViewInfo : SqlObjectViewInfo
    {
        public LoginAuthenticationType[] AuthenticationTypes { get; set; }
        public bool CanEditLockedOutState { get; set; }
        public string[] Databases;
        public string[] Languages;
        public string[] ServerRoles;
        public bool SupportAdvancedPasswordOptions;
        public bool SupportAdvancedOptions;
    }
}