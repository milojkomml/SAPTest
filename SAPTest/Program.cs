using SAPTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;

namespace SAPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            //int size = 0;
            //int startDay = 0;
            //int endDate = 0;
            //string newBookingAnswer = "";
            //List<Booking> listOfBookings = new List<Booking>();

            //Console.WriteLine("Enter size of hotel (number of rooms)");
            //size = int.Parse(Console.ReadLine());

            //if(size > 1000)
            //{
            //    Console.WriteLine("Number of rooms in hotel cant be higher then 1000!");
            //}
            //else
            //{
            //    var listOfRooms = solution.CreateEmptyRooms(size);

            //    do
            //    {
            //        Booking booking = new Booking();

            //        Console.WriteLine("Enter start date");
            //        booking.StartDay = int.Parse(Console.ReadLine());


            //        Console.WriteLine("Enter end date");
            //        booking.EndDay = int.Parse(Console.ReadLine());

            //        listOfBookings.Add(booking);

            //        Console.WriteLine("Add new booking request (y for yes or n for no)");
            //        newBookingAnswer = Console.ReadLine();
            //    }
            //    while (newBookingAnswer == "y");

            //    solution.ReserveHotelRoom(listOfRooms, listOfBookings);

            //    Console.WriteLine("Hotel size is: " + size);
            //}

            //solution.TestCase1();
            //solution.TestCase2();
            //solution.TestCase3();
            //solution.TestCase4();
            //solution.TestCase5();
            solution.TestCase6();
        }

        public class Solution
        {
            List<Room> rooms = new List<Room>();
            public List<Room> CreateEmptyRooms(int numberOfRooms)
            {

                for (int i = 0; i < numberOfRooms; i++)
                {
                    var room = new Room();
                    room.IdRoom = i + 1;

                    rooms.Add(room);
                }
                return rooms;
            }

            public List<Booking> ReserveHotelRoom(List<Room> rooms, List<Booking> listOfBookings)
            {
                int startDate = 0;
                int endDate = 0;
                var daysStaying = new List<int>();
                var listOfAvaliableRooms = new List<Room>();
                // check if we have room avaliable for all days
                bool avaliableForAllDays = true;
                bool dayForBookingIsFound = false;
                bool bookingRequestIsValid = true;
                bool bookingRequestIsConfirmed = false;
                int dayForBooking = 0;

                var sortedListOfBookings = listOfBookings.OrderBy(z => z.StartDay).ToList();

                if (startDate < 0 || endDate < 0)
                {
                    Console.WriteLine("Start or end date cant be negative!");
                }
                else if(startDate > 365 || endDate > 365)
                {
                    Console.WriteLine("Start or end date cant be larger then 365, we accept bookings for only next 365 days");
                }

                foreach (var b in listOfBookings)
                {
                    startDate = b.StartDay;
                    endDate = b.EndDay;
                    dayForBookingIsFound = false;
                    bookingRequestIsValid = true;
                    bookingRequestIsConfirmed = false;

                    if (startDate < 0 || endDate < 0)
                    {
                        Console.WriteLine("Start or end date cant be negative!");
                        bookingRequestIsValid = false;
                    }
                    if (startDate > 365 || endDate > 365)
                    {
                        Console.WriteLine("Start or end date cant be larger then 365, we accept bookings for only next 365 days");
                        bookingRequestIsValid = false;
                    }

                    for (int i = startDate; i <= endDate; i++)
                    {
                        daysStaying.Add(i);
                    }

                    //TODO
                    listOfAvaliableRooms.Clear();

                    foreach (var room in rooms)
                    {
                        avaliableForAllDays = true;
                        if (dayForBookingIsFound == false && bookingRequestIsValid == true)
                        {
                            foreach (var x in daysStaying)
                            {
                                if (room.FreeDays[x] == 0 && avaliableForAllDays == true)
                                {
                                    dayForBooking = room.IdRoom;
                                    if (x == endDate)
                                    {
                                        //dayForBookingIsFound = true;
                                        //Console.WriteLine("Booked room is Room " + room.IdRoom);
                                        b.Status = true;
                                    }
                                }
                                else
                                {
                                    avaliableForAllDays = false;
                                }
                            }
                        }
                        else
                        {

                            break;
                        }
                        if (avaliableForAllDays == true)
                        {
                            listOfAvaliableRooms.Add(room);
                            //foreach (var x in daysStaying)
                            //{
                            //    if (dayForBooking == room.IdRoom)
                            //    {
                            //        room.FreeDays.SetValue(1, x);
                            //    }
                            //}
                            if(daysStaying[0] > 0)
                            {
                                int index = daysStaying[0] - 1;
                                //Console.WriteLine("Index is: " + index);

                                if (room.FreeDays[index] == 1)
                                {
                                    listOfAvaliableRooms.Clear();
                                    listOfAvaliableRooms.Add(room);
                                }
                            }
                        }
                        
                    }
                    //Added part for room management
                    foreach (var ar in listOfAvaliableRooms)
                    {
                        if (bookingRequestIsConfirmed == false)
                        {
                            foreach (var d in daysStaying)
                            {
                                ar.FreeDays.SetValue(1, d);
                                bookingRequestIsConfirmed = true;
                            }
                            break;
                        }
                        else 
                        {
                            foreach (var d in daysStaying)
                            {
                                ar.FreeDays.SetValue(1, d);
                                bookingRequestIsConfirmed = true;
                                break;
                            }
                        }
                    }

                    //End of part for room management

                    daysStaying.Clear();
                    if (b.Status == true)
                    {
                        Console.WriteLine("Booking " + b.IdBooking + " with start date " + b.StartDay + " and end date " + b.EndDay + " is Accepted");
                    }
                    else
                    {
                        Console.WriteLine("Booking " + b.IdBooking + " with start date " + b.StartDay + " and end date " + b.EndDay + " is Declined");
                    }
                }
                return listOfBookings;
            }
            public void TestCase1()
            {
                List<Room> rooms = new List<Room>();
                rooms.Add(new Room { IdRoom = 1 });

                List<Booking> listOfBookings = new List<Booking>();
                listOfBookings.Add(new Booking { IdBooking = 1,  StartDay = -4, EndDay = 2 });

                ReserveHotelRoom(rooms, listOfBookings);
            }
            public void TestCase2()
            {
                List<Room> rooms = new List<Room>();
                rooms.Add(new Room { IdRoom = 1 });

                List<Booking> listOfBookings = new List<Booking>();
                listOfBookings.Add(new Booking { IdBooking = 1, StartDay = 200, EndDay = 400 });

                ReserveHotelRoom(rooms, listOfBookings);
            }
            public void TestCase3()
            {
                List<Room> rooms = new List<Room>();
                rooms.Add(new Room { IdRoom = 1 });
                rooms.Add(new Room { IdRoom = 2 });
                rooms.Add(new Room { IdRoom = 3 });

                List<Booking> listOfBookings = new List<Booking>();
                listOfBookings.Add(new Booking { IdBooking = 1, StartDay = 0, EndDay = 5 });
                listOfBookings.Add(new Booking { IdBooking = 2, StartDay = 7, EndDay = 13 });
                listOfBookings.Add(new Booking { IdBooking = 3, StartDay = 3, EndDay = 9 });
                listOfBookings.Add(new Booking { IdBooking = 4, StartDay = 5, EndDay = 7 });
                listOfBookings.Add(new Booking { IdBooking = 5, StartDay = 6, EndDay = 6 });
                listOfBookings.Add(new Booking { IdBooking = 6, StartDay = 0, EndDay = 4 });

                ReserveHotelRoom(rooms, listOfBookings);
            }
            public void TestCase4()
            {
                List<Room> rooms = new List<Room>();
                rooms.Add(new Room { IdRoom = 1 });
                rooms.Add(new Room { IdRoom = 2 });
                rooms.Add(new Room { IdRoom = 3 });

                List<Booking> listOfBookings = new List<Booking>();
                listOfBookings.Add(new Booking { IdBooking = 1, StartDay = 1, EndDay = 3 });
                listOfBookings.Add(new Booking { IdBooking = 2, StartDay = 2, EndDay = 5 });
                listOfBookings.Add(new Booking { IdBooking = 3, StartDay = 1, EndDay = 9 });
                listOfBookings.Add(new Booking { IdBooking = 4, StartDay = 0, EndDay = 15 });

                ReserveHotelRoom(rooms, listOfBookings);
            }
            public void TestCase5()
            {
                List<Room> rooms = new List<Room>();
                rooms.Add(new Room { IdRoom = 1 });
                rooms.Add(new Room { IdRoom = 2 });
                rooms.Add(new Room { IdRoom = 3 });

                List<Booking> listOfBookings = new List<Booking>();
                listOfBookings.Add(new Booking { IdBooking = 1, StartDay = 1, EndDay = 3 });
                listOfBookings.Add(new Booking { IdBooking = 2, StartDay = 0, EndDay = 15 });
                listOfBookings.Add(new Booking { IdBooking = 3, StartDay = 1, EndDay = 9 });
                listOfBookings.Add(new Booking { IdBooking = 4, StartDay = 2, EndDay = 5 });
                listOfBookings.Add(new Booking { IdBooking = 5, StartDay = 4, EndDay = 9 });

                ReserveHotelRoom(rooms, listOfBookings);
            }
            public void TestCase6()
            {
                List<Room> rooms = new List<Room>();
                rooms.Add(new Room { IdRoom = 1 });
                rooms.Add(new Room { IdRoom = 2 });

                List<Booking> listOfBookings = new List<Booking>();
                listOfBookings.Add(new Booking { IdBooking = 1, StartDay = 1, EndDay = 3 });
                listOfBookings.Add(new Booking { IdBooking = 2, StartDay = 0, EndDay = 4 });
                listOfBookings.Add(new Booking { IdBooking = 3, StartDay = 2, EndDay = 3 });
                listOfBookings.Add(new Booking { IdBooking = 4, StartDay = 5, EndDay = 5 });
                listOfBookings.Add(new Booking { IdBooking = 5, StartDay = 4, EndDay = 10 });
                listOfBookings.Add(new Booking { IdBooking = 6, StartDay = 10, EndDay = 10 });
                listOfBookings.Add(new Booking { IdBooking = 7, StartDay = 6, EndDay = 7 });
                listOfBookings.Add(new Booking { IdBooking = 8, StartDay = 8, EndDay = 10 });
                listOfBookings.Add(new Booking { IdBooking = 9, StartDay = 8, EndDay = 9 });

                ReserveHotelRoom(rooms, listOfBookings);
            }
        }
    }
}
