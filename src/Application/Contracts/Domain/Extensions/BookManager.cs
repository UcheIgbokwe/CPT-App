using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using Domain.Models;

namespace Application.Contracts.Domain.Extensions
{
    public static class BookManager
    {
        public static BookingResponse ToBooking(this Booking book)
        {
            BookingResponse newBooking = new();
            newBooking.Email = book.Email;
            newBooking.LocationId = book.LocationId;
            newBooking.TestResult = book.TestResult;
            newBooking.TestDate = book.TestDate;
            newBooking.Status = book.Status;
            newBooking.TestType = book.TestType;

            return newBooking;
        }
        public static LocationResponse ToSpaces(this LocationDetail space)
        {
            LocationResponse newSpace = new();
            newSpace.AvailableSpace = space.AvailableSpace;
            newSpace.CreatedAt = space.CreatedAt;
            newSpace.LocationName = space.LocationName;
            newSpace.Id = space.Id;

            return newSpace;
        }
    }
}