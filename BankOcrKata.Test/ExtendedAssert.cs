using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOcrKata.Test
{
    public static class ExtendedAssert
    {
        public static void Throws<TException>(Action executable) where TException : Exception
        {
            try
            {
                executable();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(TException), String.Format("Expected exception of type {0} but got {1}", typeof(TException), ex.GetType()));
                return;
            }
            Assert.Fail(String.Format("Expected exception of type {0}, but no exception was thrown.", typeof(TException)));
        }

        public static void Throws(Action executable)
        {
            Throws(executable, "Expected an exception but none was thrown.");
        }
        public static void Throws(Action executable, string message)
        {
            try
            {
                executable();
            }
            catch
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail(message);
        }
    }
}
