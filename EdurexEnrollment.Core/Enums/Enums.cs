using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace EdurexEnrollment.Core.Enums
{
    public enum UserRolesEnums
    {
        [Description("Freelance Marketers")]
        Freelance = 1,
        [Description("Sales Manager")]
        Sales_Manager = 2,
        [Description("Sales Executive")]
        Sales_Executive = 3,
        [Description("Independent_marketers")]
        Independent_marketers = 4,
        [Description("Admin")]
        Admin = 5,
        [Description("Enrolled")]
        Enrolled = 6,
        [Description("Marketer")]
        Marketer = 7,
    }
    public enum UserStatusEnums
    {
        [Description("None")]
        None = 0,
        [Description("Pending")]
        Pending = 1,
        [Description("Deposited")]
        Deposited = 2,
        [Description("Paid")]
        Paid = 3,
        [Description("Restricted")]
        Restricted = 4,

    }
    public enum UserProgramPaymentStatusEnums
    {
        [Description("Deposited")]
        Deposited = 1,
        [Description("Paid")]
        Paid = 2
    }
    public enum PaymentMethodEnums
    {
        [Description("Card")]
        Card = 1,
        [Description("Account Transfer")]
        AccountTransfer = 2,
        [Description("Bank COnnect")]
        BankConnect = 3,
        [Description("Discount")]
        Discount = 4,
        [Description("Offline")]
        Offline = 5
    }
    public enum PaymentStatusEnums
    {
        [Description("Initialized")]
        Initialized = 1,
        [Description("In Progress")]
        Pending = 2,
        [Description("Failed")]
        Failed = 3,
        [Description("Paid")]
        Paid = 4,
    }
    public enum UserProgramStatusEnums
    {
        [Description("Pending")]
        Pending = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Completed")]
        Completed = 3
    }

    public static class EnumHelper
    {
        public static string ToDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

    }
}
