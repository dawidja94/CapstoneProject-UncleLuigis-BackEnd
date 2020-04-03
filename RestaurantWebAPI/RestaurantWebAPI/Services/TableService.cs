using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Services
{
    public class TableService : ITableService
    {
        private readonly ApplicationDbContext _context;

        public TableService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        private void AllocateNewTables()
        {
            var response = new AllocateNewTablesResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            List<string> timeSlots = new List<string>()
            {
                "9:00AM", "10:00AM", "11:00AM", "12:00PM", "1:00PM", "2:00PM", "3:00PM", "4:00PM",
                "5:00PM", "6:00PM", "7:00PM", "8:00PM", "9:00PM"
            };

            List<TableReservation> tableReservations = new List<TableReservation>();

            try
            {
                var record = _context.TableReservations
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();

                // This logic should create unreserved table openings for the next 365 days.
                for (int i = 0; i < 1; i++)
                {
                    DateTime dateTime = DateTime.Today;
                    string dateString = "";

                    if (record != null)
                    {
                        dateString = record.ReservationDate;
                        dateTime = Convert.ToDateTime(dateString);
                        dateTime = dateTime.AddDays(i + 1);
                    }
                    else
                    {
                        if (i != 0)
                        {
                            dateTime = dateTime.AddDays(i + 1);
                        }
                    }

                    // Logic for tables for 6. There are 6 tables like this.
                    for (int j = 0; j < 6; j++)
                    {
                        int number = j + 1;
                        foreach (var timeSlot in timeSlots)
                        {
                            tableReservations.Add(new TableReservation
                            {
                                ReservationTable = $"Table #{number}-6",
                                TableSize = 6,
                                TimeSlot = timeSlot,
                                ReservationDate = dateTime.Date.ToString("MM/dd/yyyy")
                            });
                        }
                    }

                    // Logic for tables for 4. There are 8 tables like this.
                    for (int j = 0; j < 8; j++)
                    {
                        int number = j + 1;
                        foreach (var timeSlot in timeSlots)
                        {
                            tableReservations.Add(new TableReservation
                            {
                                ReservationTable = $"Table #{number}-4",
                                TableSize = 4,
                                TimeSlot = timeSlot,
                                ReservationDate = dateTime.Date.ToString("MM/dd/yyyy")
                            });
                        }
                    }

                    // Logic for tables for 2 people. There are 6 tables like this.
                    for (int j = 0; j < 6; j++)
                    {
                        int number = j + 1;
                        foreach (var timeSlot in timeSlots)
                        {
                            tableReservations.Add(new TableReservation
                            {
                                ReservationTable = $"Table #{number}-2",
                                TableSize = 2,
                                TimeSlot = timeSlot,
                                ReservationDate = dateTime.Date.ToString("MM/dd/yyyy")
                            });
                        }
                    }
                }

                _context.AddRange(tableReservations);
                _context.SaveChanges();
                response.IsSuccessful = true;
                response.Message = "Successsfully queried available table reservations.";
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
        }

        public CreateTableReservationResponse CreateTableReservation(CreateTableReservationRequest request)
        {
            var response = new CreateTableReservationResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var table = _context.TableReservations
                    .Include(x => x.Customer)
                    .FirstOrDefault(x => x.Id == request.TableId);

                if (table != null)
                {
                    // Start tracking entity.
                    _context.Update(table);

                    if (table.Customer == null)
                    {
                        // Look up the customer.
                        var customer = _context.Customers
                            .FirstOrDefault(x => x.Id == request.CustomerId);

                        table.Customer = customer;
                        table.PartySize = request.PartySize;

                        _context.SaveChanges();
                        response.IsSuccessful = true;
                        response.Message = "Successfully created table reservation.";
                        response.Reservation = table;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public GetAvailableTableReservationsResponse GetAvailableTableReservations(GetAvailableTableReservationsRequest request)
        {
            var response = new GetAvailableTableReservationsResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                AllocateNewTables();

                int partySize = 0;

                if (request.PartySize == 2)
                {
                    partySize = 2;
                }
                else if (request.PartySize >=2 && request.PartySize <=4)
                {
                    partySize = 4;
                }
                else if (request.PartySize >=4 && request.PartySize <= 6)
                {
                    partySize = 6;
                }

                var reservations = _context.TableReservations
                    .Include(x => x.Customer)
                    .Where(x => x.Customer == null)
                    .Where(x => x.TableSize == partySize)
                    .Where(x => x.ReservationDate == request.ReservationDate)
                    .Where(x => x.TimeSlot == request.TimeSlot)
                    .ToList();

                response.Reservations = reservations;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public GetTableReservationsByCustomerResponse GetTableReservationsByCustomer(GetTableReservationsByCustomerRequest request)
        {
            var response = new GetTableReservationsByCustomerResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var customer = _context.Customers
                    .Where(x => x.FirstName == request.Customer.FirstName)
                    .Where(x => x.LastName == request.Customer.LastName)
                    .Where(x => x.Email == request.Customer.Email)
                    .Where(x => x.PhoneNumber == request.Customer.PhoneNumber)
                    .FirstOrDefault();

                if (customer != null)
                {
                    var reservations = _context.TableReservations
                        .Include(x => x.Customer)
                        .Where(x => x.Customer.Id == customer.Id)
                        .ToList();

                    response.Reservations = reservations;
                    response.Message = $"Successfully retrieved reservations made by customer: {customer.FirstName} {customer.LastName}.";
                    response.IsSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public UpdateTableReservationResponse UpdateTableReservation(UpdateTableReservationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
