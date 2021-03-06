﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using LinqCommon;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quantifiers
{
    /// <summary>
    /// Quantifiers
    /// 1. Any
    /// 2. All
    /// 3. Contains
    /// More details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic15
    /// </summary>
    [TestClass]
    public class LinqExamples
    {
        [TestMethod]
        public void AnyAndContails01()
        {
            string[] words = { "believe", "relief", "receipt", "field" };

            bool iAfterE = words.Any(w => w.Contains("ei"));

            Debug.WriteLine("There is a word in the list that contains 'ei': {0}", iAfterE);
        }

        [TestMethod]
        public void Any02()
        {
            List<Product> products = LinqHellper.GetProducts();

            var productGroups =
                from prod in products
                group prod by prod.Category into prodGroup
                where prodGroup.Any(p => p.UnitsInStock == 0)
                select new { Category = prodGroup.Key, Products = prodGroup };
        }

        [TestMethod]
        public void All01()
        {
            int[] numbers = { 1, 11, 3, 19, 41, 65, 19 };

            bool onlyOdd = numbers.All(n => n % 2 == 1);

            Debug.WriteLine("The list contains only odd numbers: {0}", onlyOdd);
        }

        [TestMethod]
        public void All02()
        {
            List<Product> products = LinqHellper.GetProducts();

            var productGroups =
                from prod in products
                group prod by prod.Category into prodGroup
                where prodGroup.All(p => p.UnitsInStock > 0)
                select new { Category = prodGroup.Key, Products = prodGroup };
        }

        [TestMethod]
        public void Contains01()
        {

            string[] fruits = { "apple", "banana", "mango", "orange", "passionfruit", "grape" };

            string fruit = "mango";

            bool hasMango = fruits.Contains(fruit);

            Debug.WriteLine(
                "The array {0} contain '{1}'.",
                hasMango ? "does" : "does not",
                fruit);
        }

        /// <summary>
        /// public static bool Contains<TSource> (this System.Collections.Generic.IEnumerable<TSource> source, TSource value, System.Collections.Generic.IEqualityComparer<TSource> comparer);
        /// </summary>
        [TestMethod]
        public void Contains02()
        {

            Fruit[] fruits = { new Fruit { Name = "apple", Code = 9 },
                       new Fruit { Name = "orange", Code = 4 },
                       new Fruit { Name = "lemon", Code = 12 } };

            Fruit apple = new Fruit { Name = "apple", Code = 9 };
            Fruit kiwi = new Fruit { Name = "kiwi", Code = 8 };

            FruitComparer prodc = new FruitComparer();

            bool hasApple = fruits.Contains(apple, prodc);
            bool hasKiwi = fruits.Contains(kiwi, prodc);

            Debug.WriteLine("Apple? " + hasApple);
            Debug.WriteLine("Kiwi? " + hasKiwi);
        }
    }
}

public class Fruit
{
    public string Name { get; set; }
    public int Code { get; set; }
}

//Copied from https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.contains?view=netframework-4.7.2
class FruitComparer : IEqualityComparer<Fruit>
{
    // Products are equal if their names and product numbers are equal.
    public bool Equals(Fruit x, Fruit y)
    {

        //Check whether the compared objects reference the same data.
        if (Object.ReferenceEquals(x, y)) return true;

        //Check whether any of the compared objects is null.
        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
            return false;

        //Check whether the products' properties are equal.
        return x.Code == y.Code && x.Name == y.Name;
    }

    // If Equals() returns true for a pair of objects 
    // then GetHashCode() must return the same value for these objects.

    public int GetHashCode(Fruit product)
    {
        //Check whether the object is null
        if (Object.ReferenceEquals(product, null)) return 0;

        //Get hash code for the Name field if it is not null.
        int hashProductName = product.Name == null ? 0 : product.Name.GetHashCode();

        //Get hash code for the Code field.
        int hashProductCode = product.Code.GetHashCode();

        //Calculate the hash code for the product.
        return hashProductName ^ hashProductCode;
    }

}

