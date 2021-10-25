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
            newBooking.UserId = book.UserId;
            newBooking.LocationId = book.LocationId;
            newBooking.TestResult = book.TestResult;
            newBooking.TestDate = book.TestDate;
            newBooking.Status = book.Status;

            return newBooking;
        }
    }
}