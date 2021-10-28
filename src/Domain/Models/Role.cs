using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public static class Role
    {
        public const string Admin = "admin";
        public const string User = "user";
    }

    public static class TestTypeModel
    {
        public const string PCR = "PCR";
        public const string RapidTest = "Rapid Test";
    }

    public static class StatusModel
    {
        public const string Pending = "pending";
        public const string Closed = "closed";
        public const string Cancelled = "cancelled";
    }

    public static class TestResultModel
    {
        public const string Positive = "positive";
        public const string Negative = "negative";
    }

    public static class GenderModel
    {
        public const string Male = "male";
        public const string Female = "female";
    }
}