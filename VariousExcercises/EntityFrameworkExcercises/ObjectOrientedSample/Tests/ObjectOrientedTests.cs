﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkExcercises.ObjectOrientedSample.Entities;

namespace EntityFrameworkExcercises.ObjectOrientedSample
{
    [TestClass]
    public class ObjectOrientedTests
    {
        [TestMethod]
        public void GetStudentWithNavigationPro()
        {
            using (var context = new ObjectOrientedDbContext())
            {
                var st = context.Students
                                .Include(i=> i.StudentSubjects)
                                    .ThenInclude(i=> i.Subject)
                                .ToList();       
            }
        }

        [TestMethod]
        public void Update()
        {
            using (var context = new ObjectOrientedDbContext())
            {
                var st = context.Students.Single(i=> i.Id == 1);
                st.SetFirstName("Mofaggol");

                context.SaveChanges();

                var st2 = context.Students.Single(i => i.Id == 1);
            }
        }

        [TestMethod]
        public void Insert()
        {
            using (var context = new ObjectOrientedDbContext())
            {
                var st = new Student(firstName: "Matthias-1", lastName: "Stahl", department: "GroupWare", university: "Quipu");

                context.Students.Add(st);

                context.SaveChanges();

                var st2 = context.Students.Single(i => i.FirstName == "Matthias-1");
            }
        }
    }
}
