﻿using System.Collections.Generic;
using System.Text;
using vCardLib.Enums;
using vCardLib.Models;

namespace vCardLib.Serializers
{
    /// <summary>
    /// Handles the serialization of version 2 cards
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public sealed class v2Serializer : Serializer
    {
        protected override void AddVersion(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("VERSION:2.1");
        }

        protected override void AddPhoneNumbers(StringBuilder stringBuilder, IEnumerable<TelephoneNumber> phoneNumbers)
        {
            foreach (var phoneNumber in phoneNumbers)
            {
                switch (phoneNumber.Type)
                {
                    case TelephoneNumberType.None:
                        stringBuilder.AppendLine("TEL:" + phoneNumber.Value);
                        break;
                    case TelephoneNumberType.MainNumber:
                        stringBuilder.AppendLine("TEL;MAIN-NUMBER:" + phoneNumber.Value);
                        break;
                    case TelephoneNumberType.Custom:
                        stringBuilder.AppendLine($"TEL;TYPE=\"{phoneNumber.CustomTypeName}\":" + phoneNumber.Value);
                        break;
                    default:
                        stringBuilder.AppendLine("TEL;" + phoneNumber.Type.ToString().ToUpper() + ":" + phoneNumber.Value);
                        break;
                }
            }
        }

        protected override void AddEmailAddresses(StringBuilder stringBuilder, IEnumerable<EmailAddress> emailAddresses)
        {
            foreach (var email in emailAddresses)
            {
                if (email.Type == EmailAddressType.None)
                {
                    stringBuilder.AppendLine("EMAIL:" + email.Value);
                }
                else
                {
                    stringBuilder.AppendLine("EMAIL;" + email.Type.ToString().ToUpper() + ":" + email.Value);
                }
            }
        }

        protected override void AddAddresses(StringBuilder stringBuilder, IEnumerable<Address> addresses)
        {
            foreach (var address in addresses)
            {
                if (address.Type == AddressType.None)
                {
                    stringBuilder.AppendLine("ADR:" + address.Location);
                }
                else
                {
                    stringBuilder.AppendLine("ADR;" + address.Type.ToString().ToUpper() + ":" + address.Location);
                }
            }
        }

        protected override void AddPhotos(StringBuilder stringBuilder, IEnumerable<Photo> photos)
        {
            foreach (var photo in photos)
            {
                stringBuilder.Append("PHOTO;" + photo.Encoding);
                if (photo.Type == PhotoType.URL)
                {
                    stringBuilder.AppendLine(":" + photo.PhotoURL);
                }
                else if (photo.Type == PhotoType.Image)
                {
                    stringBuilder.AppendLine(";ENCODING=BASE64:" + photo.ToBase64String());
                }
            }
        }

        protected override void AddExpertises(StringBuilder stringBuilder, IEnumerable<Expertise> expertises)
        {
            foreach (var expertise in expertises)
            {
                stringBuilder.AppendLine("EXPERTISE;LEVEL=" + expertise.Level.ToString().ToLower() + ":" +
                                         expertise.Area);
            }
        }

        protected override void AddHobbies(StringBuilder stringBuilder, IEnumerable<Hobby> hobbies)
        {
            foreach (var hobby in hobbies)
            {
                stringBuilder.AppendLine("HOBBY;LEVEL=" + hobby.Level.ToString().ToLower() + ":" + hobby.Activity);
            }
        }

        protected override void AddInterests(StringBuilder stringBuilder, IEnumerable<Interest> interests)
        {
            foreach (var interest in interests)
            {
                stringBuilder.AppendLine("INTEREST;LEVEL=" + interest.Level.ToString().ToLower() + ":" +
                                         interest.Activity);
            }
        }
    }
}