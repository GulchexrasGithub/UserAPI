//=================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//=================================

using System;
using Xeptions;

namespace UserAPI.Models.Users.Exceptions
{
    public class InvalidUserReferenceException : Xeption
    {
        public InvalidUserReferenceException(Exception innerException)
            : base(message: "Invalid user reference error occurred.", innerException)
        { }
    }
}