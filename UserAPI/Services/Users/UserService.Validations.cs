// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// ---------------------------------------------------------------

using System;
using UserAPI.Models.Users;
using UserAPI.Models.Users.Exceptions;

namespace UserAPI.Services.Users
{
    public partial class UserService
    {
        private void ValidateUserOnAdd(User user)
        {
            ValidateUserNotNull(user);

            Validate(
                (Rule: IsInvalid(user.Id), Parameter: nameof(User.Id)),
                (Rule: IsInvalid(user.Name), Parameter: nameof(User.Name)),
                (Rule: IsInvalid(user.PhoneNumber), Parameter: nameof(User.PhoneNumber)),
                (Rule: IsInvalid(user.Diagnosis), Parameter: nameof(User.Diagnosis)),
                (Rule: IsInvalid(user.Treatment), Parameter: nameof(User.Treatment)),
                (Rule: IsInvalid(user.Adress), Parameter: nameof(User.Adress)),
                (Rule: IsInvalid(user.Money), Parameter: nameof(User.Money)),
                (Rule: IsInvalid(user.BirthDate), Parameter: nameof(User.BirthDate)));
        }

        private void ValidateUserId(Guid userId) =>
            Validate((Rule: IsInvalid(userId), Parameter: nameof(User.Id)));


        private static void ValidateStorageUser(User maybeUser, Guid userId)
        {
            if (maybeUser is null)
            {
                throw new NotFoundUserException(userId);
            }
        }

        private void ValidateUserOnModify(User user)
        {
            ValidateUserNotNull(user);

            Validate(
                 (Rule: IsInvalid(user.Id), Parameter: nameof(User.Id)),
                (Rule: IsInvalid(user.Name), Parameter: nameof(User.Name)),
                (Rule: IsInvalid(user.PhoneNumber), Parameter: nameof(User.PhoneNumber)),
                (Rule: IsInvalid(user.Diagnosis), Parameter: nameof(User.Diagnosis)),
                (Rule: IsInvalid(user.Treatment), Parameter: nameof(User.Treatment)),
                (Rule: IsInvalid(user.Adress), Parameter: nameof(User.Adress)),
                (Rule: IsInvalid(user.Money), Parameter: nameof(User.Money)),
                (Rule: IsInvalid(user.BirthDate), Parameter: nameof(User.BirthDate)));
        }

        private static dynamic IsInvalid<T>(T value) => new
        {
            Condition = IsEnumInvalid(value),
            Message = "Value is not recognized"
        };

        private static bool IsEnumInvalid<T>(T value)
        {
            bool isDefined = Enum.IsDefined(typeof(T), value);

            return isDefined is false;
        }


        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Value is required"
        };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not same as {secondDateName}"
            };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != default && firstDate == secondDate,
                Message = $"Date is same as {secondDateName}"
            };

        private static void ValidateUserNotNull(User user)
        {
            if (user is null)
            {
                throw new NullUserException();
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidUserException = new InvalidUserException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidUserException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidUserException.ThrowIfContainsErrors();
        }
    }
}