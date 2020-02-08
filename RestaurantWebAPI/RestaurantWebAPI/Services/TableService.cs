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
        
        public AllocateNewTablesResponse AllocateNewTables(AllocateNewTablesRequest request)
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
                // This logic should create unreserved table openings for the next 5 days.
                for (int i = 0; i < 5; i++)
                {
                    DateTime date = new DateTime();

                    if (i != 0)
                    {
                        date.AddDays(i);
                    }


                    // Logic for tables for 6. There are 6 tables like this.
                    for (int j = 0; j < 6; j++)
                    {
                        foreach (var timeSlot in timeSlots)
                        {
                            tableReservations.Add(new TableReservation
                            {
                                ReservationTable = $"Table #{j++}-6",
                                TableSize = 6,
                                TimeSlot = timeSlot,
                                ReservationDate = date.Date.ToString("MM/dd/yyyy")
                            });
                        }
                    }

                    // Logic for tables for 4. There are 10 tables like this.
                    for (int j = 0; j < 10; j++)
                    {
                        foreach (var timeSlot in timeSlots)
                        {
                            tableReservations.Add(new TableReservation
                            {
                                ReservationTable = $"Table #{j++}-4",
                                TableSize = 4,
                                TimeSlot = timeSlot,
                                ReservationDate = date.Date.ToString("MM/dd/yyyy")
                            });
                        }
                    }

                    // Logic for tables for 2 people. There are 6 tables like this.
                    for (int j = 0; j < 6; j++)
                    {
                        foreach (var timeSlot in timeSlots)
                        {
                            tableReservations.Add(new TableReservation
                            {
                                ReservationTable = $"Table #{j++}-2",
                                TableSize = 2,
                                TimeSlot = timeSlot,
                                ReservationDate = date.Date.ToString("MM/dd/yyyy")
                            });
                        }
                    }

                    // Logic for tables for 4 people. There are 4 tables like this.
                    for (int j = 0; j < 4; j++)
                    {
                        foreach (var timeSlot in timeSlots)
                        {
                            tableReservations.Add(new TableReservation
                            {
                                ReservationTable = $"Table #{j++}-4",
                                TableSize = 2,
                                TimeSlot = timeSlot,
                                ReservationDate = date.Date.ToString("MM/dd/yyyy")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public CreateTableReservationResponse CreateTableReservation(CreateTableReservationRequest request)
        {
            throw new NotImplementedException();
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
                var reservations = _context.TableReservations
                    .Include(x => x.Customer)
                    .Where(x => x.Customer == null)
                    .ToList();

                response.Reservations = reservations;
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
