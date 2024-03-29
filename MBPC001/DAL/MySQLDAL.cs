﻿using MBPC001.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MBPC001.DAL
{
    public class MySQLDAL: iDAL
    {        
        readonly string connectionString = $"Server=ZW2207203;Database=mbpc;User=bob;Password={PasswordEncryptor.DecryptPassword("f3187476667ed7f4a31c92cf", "#$%RFG^&Hhh865dFF%^^&&%$Ffggdfg%")};";
        
        public Member ReadMember(int id)
        {
            return ReadMembers().Find(ls => ls.Id == id);
        }

        public List<Member> ReadMembers()
        {
            List<Member> Members;
            Members = new List<Member>();

            using (MySqlConnection connection = new MySqlConnection())
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select id, lastname from member";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var MemberDB = new Member(Int32.Parse(reader["id"].ToString()),
                                reader["lastname"].ToString());
                            Members.Add(MemberDB);
                        }
                    }
                }
            }
            return Members;
        }

        public List<Member> ReadMembersWithLot()
        {
            List<Member> Members;
            Members = new List<Member>();

            using (MySqlConnection connection = new MySqlConnection())
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select distinct member.id, lastname from member, lot where vendorId = member.id order by lastname";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var MemberDB = new Member(Int32.Parse(reader["id"].ToString()),
                                reader["lastname"].ToString());
                            Members.Add(MemberDB);
                        }
                    }
                }
            }
            return Members;
        }

        public List<Lot> ReadLotMember(string _id)
        {
            List<Lot> Lots;
            Lots = new List<Lot>();

            using (MySqlConnection connection = new MySqlConnection())
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select id, memberReferenece, description from Lot where vendorId = @mid";
                    command.Parameters.AddWithValue("mid", _id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var LotDB = new Lot(Int32.Parse(reader["id"].ToString()),
                                reader["memberReferenece"].ToString(),
                                reader["description"].ToString());
                            Lots.Add(LotDB);
                        }
                    }
                }
            }
            return Lots;
        }

        public List<Lot> ReadLots()
        {
            List<Lot> Lots;
            Lots = new List<Lot>();

            using (MySqlConnection connection = new MySqlConnection())
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select id, memberReferenece, description from Lot";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var LotDB = new Lot(Int32.Parse(reader["id"].ToString()),
                               reader["memberReferenece"].ToString(),
                               reader["description"].ToString());
                            Lots.Add(LotDB);
                        }
                    }
                }
            }
            return Lots;
        }
    }
}
